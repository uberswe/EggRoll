using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input.Touch;

namespace Egg_roll.Menus
{
    class MenuEgg
    {
        float ay;
        List<Sprite> groundTiles;
        Sprite sprite;
        Vector2 position, direction;
        float speed;
        public MenuEgg()
        {
            sprite = new Sprite("egg");
            sprite.Scale = 1f;
            sprite.Effect = SpriteEffects.FlipHorizontally;
            position = new Vector2(Stuff.Resolution.X * 0.8f, Stuff.Resolution.Y * 0.6f);
            speed = 300;

            groundTiles = new List<Sprite>();
            for (int i = -4; i < 10; i++)
            {
                Sprite s = new Sprite("tiles");
                s.Source = new Rectangle(0, 0, 250, 250);
                s.Scale = 1f;
                s.Position = new Vector2(200 * i, Stuff.Resolution.Y);
                groundTiles.Add(s);
            }
        }

        public void Update(float elaps)
        {
            Controls();
            foreach (Sprite s in groundTiles)
            {
                s.Position += elaps * speed * -direction;

                if (s.Position.X < Camera2d.GetPositionInWorld(new Vector2(-200, 0)).X )
                    s.PosX += groundTiles.Count * 200f;
                else if (s.Position.X > Camera2d.GetPositionInWorld(new Vector2(Stuff.Resolution.X + 200, 0)).X)
                    s.PosX -= groundTiles.Count * 200f;
            }
            Camera2d.Position = position + (Stuff.AngleToVector(sprite.Rotation + (float)Math.PI) * 30) +
                new Vector2(-Stuff.Resolution.X * 0.5f, -Stuff.Resolution.Y * 0.45f);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (Sprite s in groundTiles)
            {
                s.Draw(spriteBatch);
            }
            sprite.DrawAt(spriteBatch, position);
        }

        private void Controls()
        {
            Vector2 dir = Vector2.Zero;
            ay = MainScene.yaccel;

            if (ay > 0.15f || ay < -0.15f)
            {
                if (ay >= 0.60f)
                {
                    ay = 0.60f;
                }
                if (ay <= -0.60f)
                {
                    ay = -0.60f;
                }
                dir.X -= ay * 3f;
            }

            if (dir.X == 0 && sprite.Rotation != 0)
                dir.X = Stuff.AngleToVector(sprite.Rotation).X;
            float spd = 0.1f;
            if (position.X < Stuff.Resolution.X * 0.4f)
            {
                spd = 0.05f;
                dir = new Vector2(1f, 0);
            }
            if (position.X > Stuff.Resolution.X * 0.9f)
            {
                spd = 0.05f;
                dir = new Vector2(-1f, 0);
            }

            direction = Stuff.Lerp(direction, dir, spd);

            sprite.Rotation += direction.X * (speed * 0.00025f);
            sprite.Rotation = sprite.Rotation % (float)(Math.PI * 2f);

        }
    }
}
