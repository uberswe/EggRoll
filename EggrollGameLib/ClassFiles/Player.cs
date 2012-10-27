using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EggRollGameLib;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;

using Microsoft.Xna.Framework.Audio;
using EggrollGameLib.ClassFiles.Menus;
using Microsoft.Xna.Framework.Graphics;

namespace EggrollGameLib.ClassFiles
{
    class Player : Character
    {
        SoundEffect Jump;
        SoundEffect DamageTaken;
        Button btnRight, btnLeft, btnJump;
        float ay;


        public Player(List<Character> characters)
            : base()
        {
            this.characters = characters;
            sprite = new Sprite("egg");
            sprite.Scale = 0.3f;
            sprite.iColor.SetColor(Color.White);
            speed = 300;
            position = new Vector2(100, 300);

            btnRight = new Button("pixel", new Vector2(250, 420), new Rectangle(0, 0, 150, 100));
            btnLeft = new Button("pixel", new Vector2(90, 420), new Rectangle(0, 0, 150, 100));
            btnJump = new Button("pixel", new Vector2(710, 420), new Rectangle(0, 0, 150, 100));
            weight = 9f;
        }

        public override void LoadContent()
        {
            base.LoadContent();
        }

        public void Update(float elaps, float yaccel)
        {btnRight.Update(elaps);
            btnLeft.Update(elaps);
            btnJump.Update(elaps);

            if (position.Y >= 300)
            {
                onGround = true;
                position.Y = 300;
            }
            else
                onGround = false;

            Controls();

            ay = yaccel;

            base.Update(elaps);
            Camera2d.Position = position + (Stuff.AngleToVector(sprite.Rotation + (float)Math.PI) * 20);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }

        public void DrawButtons(SpriteBatch spriteBatch)
        {
            btnJump.Draw(spriteBatch);
            btnRight.Draw(spriteBatch);
            btnLeft.Draw(spriteBatch);
        }

        private void Controls()
        {
            Vector2 dir = Vector2.Zero;
            if (btnLeft.active)
                dir.X -= 2f;
            if (btnRight.active)
                dir.X += 2f;

            if (ay > 0.15 || ay < -0.15)
            {
                dir.X -= ay * 3;
            }

            if (dir.X == 0 && onGround && sprite.Rotation != 0)
                dir.X = Stuff.AngleToVector(sprite.Rotation).X;

            direction.X = Stuff.Lerp(direction.X, dir.X, 0.1f);
            sprite.Rotation += direction.X * (speed * 0.00075f);
            sprite.Rotation = sprite.Rotation % (float)(Math.PI * 2f);

            if (btnJump.active)
            {
                if (onGround)
                {
                    onGround = false;
                    gravForce = new Vector2(0, -3f);
                }
                //gravForce = Vector2.Zero; 
                //position = new Vector2(100, 100); 
            }
        }
    }
}
