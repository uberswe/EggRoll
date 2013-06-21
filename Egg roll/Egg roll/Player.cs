using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Egg_roll.Menus;
using Microsoft.Xna.Framework.Graphics;

namespace Egg_roll
{
    class Player : Character
    {

        Button btnRight, btnLeft, btnJump, btnPause, btnDebug;
        float ay;
        ScreenManager screenManager;
        Rectangle collisionRect;
        float HayTimer = 0;

        float lastHaytime = 0f;
        int score;
        public int coin = 1;
        bool pressingJump = false;
        bool isDead = false;
        bool standingOnPig = false;
        public bool adjustPlayer = false;
        bool CoveredInPoop = false;
        bool RollingInTheDeep = false;

        Sprite[] collisionTilesDown = new Sprite[3];
        Sprite[] collisionTilesLeft = new Sprite[3];
        Sprite[] collisionTilesRight = new Sprite[3];
        Sprite currentTile;
        bool slopeUp, slopeDown;

        ParticleHandler ph = new ParticleHandler();

        public Player(List<Character> characters)
            : base()
        {
            this.characters = characters;
            sprite = new Sprite("egg");
            sprite.Scale = 0.5f;
            sprite.iColor.SetColor(Color.White);
            speed = 300;
            position = new Vector2(100, 1000);

            btnRight = new Button("UI buttons", new Vector2(250, 420), new Rectangle(0, 300, 150, 150), true, true);
            btnLeft = new Button("UI buttons", new Vector2(90, 420), new Rectangle(0, 150, 150, 150), true, true);
            btnJump = new Button("UI buttons", new Vector2(710, 420), new Rectangle(0, 0, 150, 150), true, true);
            btnPause = new Button("UI buttons", new Vector2(710, 75), new Rectangle(0, 450, 150, 150), true, false);
            btnDebug = new Button("UI buttons", new Vector2(510, 420), new Rectangle(0, 0, 150, 150), true, true);
            btnDebug.sprite.Rotation = 1f;
            weight = 9f;

            LoadContent();
        }

        public void Update(float elaps, float yaccel, ScreenManager screenManager)
        {
            ph.Update(elaps);
            if (!adjustPlayer)
            {
                position = WorldGen.StartPos;
                adjustPlayer = true;
            }
            collisionRect = new Rectangle(
                (int)position.X - (int)(sprite.Origin.X * sprite.Scale),
                (int)position.Y - (int)(sprite.Origin.Y * sprite.Scale),
                (int)(sprite.Texture.Width * sprite.Scale),
                (int)(sprite.Texture.Height * sprite.Scale));

            this.screenManager = screenManager;
            btnRight.Update(elaps);
            btnLeft.Update(elaps);
            btnJump.Update(elaps);
            btnPause.Update(elaps);
            btnDebug.Update(elaps);

            Point matrixPos = WorldGen.GetMatrixPos(new Vector2(position.X, position.Y));

            if (life == 0)
            {
                screenManager.playerDead();
            }
            else
            {
                Controls(matrixPos, elaps);
            }


            ay = yaccel;

            base.Update(elaps);

            //AdjustSpriteY();

            Camera2d.Position = position + (Stuff.AngleToVector(sprite.Rotation + (float)Math.PI) * 30);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {

            base.Draw(spriteBatch);

            ph.Draw(spriteBatch);
        }

        public void DrawUI(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            btnJump.Draw(spriteBatch);
            btnRight.Draw(spriteBatch);
            btnLeft.Draw(spriteBatch);
            btnPause.Draw(spriteBatch);
            btnDebug.Draw(spriteBatch);
            spriteBatch.End();
        }

        private void Controls(Point matrixPos, float elaps)
        {
            HayTimer += elaps;
            Vector2 dir = Vector2.Zero;

            if (btnLeft.active)
            {
                dir.X -= 2f;
            }
            if (btnRight.active || btnDebug.active)
            {
                dir.X += 2f;
            }

            if (btnPause.active)
            {
                Sound.PauseMusic();
                screenManager.CurrentMenu = 4;
            }

            if (ay > 0.15 || ay < -0.15)
            {
                if (ay >= 0.60)
                {
                    ay = (float)0.60;
                }
                if (ay <= -0.60)
                {
                    ay = (float)-0.60;
                }
                dir.X -= ay * 3;
            }

            #region Collision

            //CollisionTiles
            collisionTilesDown[0] = WorldGen.GetTileAtPosition(matrixPos, new Point(0, 1));
            collisionTilesDown[1] = WorldGen.GetTileAtPosition(matrixPos, new Point(1, 1));
            collisionTilesDown[2] = WorldGen.GetTileAtPosition(matrixPos, new Point(-1, 1));

            Rectangle bottomCollisionRect = CollisionRect;
            bottomCollisionRect.Y += (int)Math.Round(gravForce.Y * weight);
            Rectangle aboveCollisionRect = CollisionRect;
            aboveCollisionRect.Y -= 5;

            currentTile = WorldGen.GetTileAtPosition(matrixPos, new Point(0, 0));

            //Collision Bottom
            for (int j = 0; j < collisionTilesDown.Length; j++)
            {
                if (currentTile != null && currentTile.Name == "Hay")
                {
                    if (!RollingInTheDeep)
                    {
                        
                        lastHaytime= elaps;
                        RollingInTheDeep = true;
                        Sound.PlayHaySound();
                    }
                    if (HayTimer - lastHaytime > 0.9f)
                    {
                        RollingInTheDeep = false;
                        HayTimer = 0;
                    }

                }
                else
                {
                    RollingInTheDeep = false;
                }
                if (collisionTilesDown[j] != null && (collisionTilesDown[j].Name == "Block" || collisionTilesDown[j].Name == "Platform" || collisionTilesDown[j].Name == "BarnBlock" || collisionTilesDown[j].Name == "SoftBlock" || collisionTilesDown[j].Name == "Pig") && bottomCollisionRect.Intersects(collisionTilesDown[j].CollisionRect))
                {
                    for (int i = 0; (int)Math.Round(gravForce.Y * weight) > i; i++)
                    {
                        if (!(new Rectangle((int)position.X, (int)position.Y + i, CollisionRect.Width, CollisionRect.Height).Intersects(collisionTilesDown[j].CollisionRect)))
                        {
                            position.Y += 1;
                        }
                    }
                    while ((((aboveCollisionRect.Intersects(collisionTilesDown[j].CollisionRect) && collisionTilesDown[j].Name == "Block" && collisionTilesDown[j].Name != "BarnBlock") || (currentTile != null && (currentTile.Name == "BarnBlock" || currentTile.Name == "Block") && aboveCollisionRect.Intersects(currentTile.CollisionRect)) && onGround)))
                    {
                        position.Y--;
                        aboveCollisionRect.Y--;
                    }
                    onGround = true;
            
                    if (collisionTilesDown[j].Name == "Pig")
                    {
                        //PIGSOUNDS!
                        if (!standingOnPig)
                        {
                            standingOnPig = true;


                            Sound.PlayPigSound();
                            gravForce.Y -= 6f;
                        }
                    }
                }
            }
            if (!onGround)
            {
                standingOnPig = false;
            }
            Rectangle peekColliosionRect = new Rectangle(CollisionRect.X + (int)Math.Round(dir.X * elaps * speed), CollisionRect.Y, CollisionRect.Width, CollisionRect.Height);

            //CollisionRects
            collisionTilesLeft[0] = WorldGen.GetTileAtPosition(matrixPos, new Point(-1, 0));
            collisionTilesLeft[1] = WorldGen.GetTileAtPosition(matrixPos, new Point(-1, 1));
            collisionTilesLeft[2] = WorldGen.GetTileAtPosition(matrixPos, new Point(-1, -1));
            for (int i = 0; i < collisionTilesLeft.Length; i++)
            {
                if (collisionTilesLeft[i] == null || collisionTilesLeft[i].Name == "Coin" || collisionTilesLeft[i].Name == "Hay" || collisionTilesLeft[i].Name == "Poop" || collisionTilesLeft[i].Name == "Platform" || !peekColliosionRect.Intersects(collisionTilesLeft[i].CollisionRect))
                {
                    isCollidingLeft = false;
                }
                else if ((collisionTilesLeft[i] != null && (!onGround || i == 0)) && (collisionTilesLeft[i].Name != "SlopeDown" && collisionTilesLeft[i].Name != "SlopeUp") && (collisionTilesDown[0] == null || (collisionTilesDown[0] != null && collisionTilesDown[0].Name != "SlopeDown")))
                {
                    if (collisionTilesLeft[0] != null && collisionTilesLeft[0].Name == "Pig")
                    {
                        Kill();
                    }
                    isCollidingLeft = true;
                    if (currentTile != null && currentTile.Name == "SlopeDown")
                        isCollidingLeft = false;
                    break;
                }
            }

            collisionTilesRight[0] = WorldGen.GetTileAtPosition(matrixPos, new Point(1, 0));
            collisionTilesRight[1] = WorldGen.GetTileAtPosition(matrixPos, new Point(1, 1));
            collisionTilesRight[2] = WorldGen.GetTileAtPosition(matrixPos, new Point(1, -1));

            for (int i = 0; i < collisionTilesRight.Length; i++)
            {
                if (collisionTilesRight[i] == null || collisionTilesRight[i].Name == "Coin" || collisionTilesRight[i].Name == "Hay" || collisionTilesRight[i].Name == "Poop" || collisionTilesRight[i].Name == "Platform" || !peekColliosionRect.Intersects(collisionTilesRight[i].CollisionRect))
                {
                    isCollidingRight = false;

                }
                else if ((collisionTilesRight[i] != null && (!onGround || i == 0)) && (collisionTilesRight[i].Name != "SlopeDown" && collisionTilesRight[i].Name != "SlopeUp") && (collisionTilesDown[0] == null || (collisionTilesDown[0] != null && collisionTilesDown[0].Name != "SlopeDown")))
                {
                    if (collisionTilesRight[0] != null && collisionTilesRight[0].Name == "Pig")
                    {
                        Kill();
                    }
                    isCollidingRight = true;    
                    if ((currentTile != null && currentTile.Name == "SlopeUp") || (collisionTilesDown[0] != null && collisionTilesDown[0].Name == "SlopeUp"))
                        isCollidingRight = false;
                    break;
                }
            }
            slopeDown = false;
            slopeUp = false;
            //Slope
            if (gravForce.Y >= 0)
            {
                if (currentTile != null && (currentTile.Name == "SlopeUp" || currentTile.Name == "SlopeDown"))
                {
                    if (currentTile.Name == "SlopeUp")
                    {
                        position.Y = ((currentTile.Position.Y + 200) - ((sprite.Texture.Height / 2) * sprite.Scale)) - (position.X - currentTile.Position.X);
                        slopeUp = true;
                    }
                    else if (currentTile.Name == "SlopeDown")
                    {
                        position.Y = ((currentTile.Position.Y) - ((sprite.Texture.Height / 2) * sprite.Scale)) + (position.X - currentTile.Position.X);
                        slopeDown = true;
                    }
                    onGround = true;
                }
                for (int i = 0; i < collisionTilesDown.Length; i++)
                {
                    if (collisionTilesDown[i] != null && (collisionTilesDown[i].Name == "SlopeUp" || collisionTilesDown[i].Name == "SlopeDown") && CollisionRect.Intersects(collisionTilesDown[i].CollisionRect))
                    {
                        if (collisionTilesDown[i].Name == "SlopeUp" && (position.X - collisionTilesDown[i].Position.X) < 185)
                        {
                            position.Y = ((collisionTilesDown[i].Position.Y + 200) - ((sprite.Texture.Height / 2) * sprite.Scale)) - (position.X - collisionTilesDown[i].Position.X);
                            slopeUp = true;
                        }
                        else if (collisionTilesDown[i].Name == "SlopeDown" && ((position.X - 20) >= collisionTilesDown[i].Position.X))
                        {
                            position.Y = ((collisionTilesDown[i].Position.Y) - ((sprite.Texture.Height / 2) * sprite.Scale)) + (position.X - collisionTilesDown[i].Position.X);
                            slopeDown = true;
                        }
                        onGround = true;
                    }
                }
            }

            if (currentTile != null)
            {
                //Collision with coin
                if (currentTile.Name == "Coin")
                {
                    coin++;
                    Sound.PlayCoinSound();
                    WorldGen.DeleteOnPos(currentTile);
                    score += coin / 2;
                }
                //POOPCOLLISION
                if (currentTile.Name == "Poop")
                {

                    dir *= 0.2f;
                    if (!CoveredInPoop)
                    {
                        CoveredInPoop = true;
                        Sound.PlayPoopSound();
                    }
                }
                else
                {
                    CoveredInPoop = false;
                }
            }

            #endregion

            if (dir.X == 0 && onGround && sprite.Rotation != 0)
                dir.X = Stuff.AngleToVector(sprite.Rotation).X;

            float tempDir = Stuff.Lerp(direction.X, dir.X, 0.1f);

            if ((!isCollidingLeft && tempDir < 0) || (!isCollidingRight && tempDir > 0))
            {
                direction.X = tempDir;
            }
            else
                direction.X = -tempDir;

            //DYING
            if ((isCollidingLeft && dir.X < -2.7f) || (isCollidingRight && dir.X > 2.7f) || (onGround && gravForce.Y > 10.0f && onGround) || (position.Y > 4000 && !onGround))
            {
                //gravforce 5 is 3 tiles
                Kill();
            }

            sprite.Rotation += direction.X * (speed * 0.00050f);
            sprite.Rotation = sprite.Rotation % (float)(Math.PI * 2f);

            if ((btnJump.active || btnDebug.active))
            {
                if ((onGround || (slopeDown || slopeUp)) && !pressingJump)
                {
                    onGround = false;
                    gravForce = new Vector2(0, -3.3f);
                    Sound.PlayJumpSound();
                }
                else if (gravForce.Y >= -3.3f && pressingJump == true && gravForce.Y < 0f)
                {
                    gravForce.Y = gravForce.Y - 0.2f;
                }
                pressingJump = true;
            }
            else
            {
                pressingJump = false;
            }
        }

        public void Kill()
        {
            Sound.StopMusic();
            Sound.PlayEggBreakSound();
            life = 0;
            coin = 1;
            ph.Eggsplode(position, direction);
            isDead = true;
        }
        public bool IsDead
        {
            get { return isDead; }
        }

        public Rectangle CollisionRect
        {
            get { return collisionRect; }
        }

        public int Score
        {
            get { return (int)(Math.Round(position.X / 200f) - 6) + score; }
            set { score = value; }
        }

        public bool IsOnGround
        {
            get { return onGround; }
        }

    }
}
