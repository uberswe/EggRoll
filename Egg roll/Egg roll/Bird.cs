using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using EggrollGameLib.ClassFiles;

namespace Egg_roll
{
    class Bird
    {
        SpriteBody body;
        Sprite sprite;
        bool firstCall;
        int speed;
        Vector2 position;

        public Bird()
        {
            body = new SpriteBody();

            Sprite tempSprite = new Sprite("henLeg");
            tempSprite.Origin = new Vector2(75, 20);
            BodyPart b = new BodyPart("LegLeft", tempSprite, new Vector2(-30, 130));
            b.SetPositionAnimation(new Vector2(-30, 140), new Vector2(-30, 120), 0.1f);
            body.AddBodyPart(b);
            tempSprite = new Sprite("henLeg");
            tempSprite.Origin = new Vector2(75, 20);
            b = new BodyPart("LegRight", tempSprite, new Vector2(-80, 130));
            b.SetPositionAnimation(new Vector2(-80, 140), new Vector2(-80, 120), 0.1f, false);

            body.AddBodyPart(b);

            body.AddBodyPart("Body", new Sprite("henBody"));

            tempSprite = new Sprite("henWing");
            tempSprite.Origin = new Vector2(190, 30);
            b = new BodyPart("Wing", tempSprite, new Vector2(-120, 0));
            b.SetRotationAnimation(-0.4f, 0.9f, 0.1f);
            body.AddBodyPart(b);

            body.scale = 0.7f;

            sprite = new Sprite("pixel");
            firstCall = true;
            speed = 8;
        }

        public void Update(Player player, float elaps)
        {
            if (firstCall)
            {
                sprite.Position = player.Position - new Vector2(800, 400);
                firstCall = false;
            }

            if (!player.IsDead && !player.CollisionRect.Intersects(new Rectangle(sprite.CollisionRect.Location.X, sprite.CollisionRect.Location.Y, 50, 50)))
            {
                body.Update(elaps);
                float distanceX = sprite.PosX - player.Position.X;
                float distanceY = sprite.PosY - player.Position.Y;

                sprite.Rotation = (float)Math.Atan2(distanceY, distanceX);
                body.rotation = sprite.Rotation + (float)Math.PI;
                sprite.PosX -= (float)(speed * Math.Cos(sprite.Rotation));
                sprite.PosY -= (float)(speed * Math.Sin(sprite.Rotation));
            }
            else if (!player.IsDead)
                player.Kill();

            if (player.Position.X - sprite.PosX > 800)
                sprite.PosX = player.Position.X - 800;

            if (speed < 18)
                UpdateSpeed();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            //sprite.Draw(spriteBatch); 
            body.Draw(spriteBatch, sprite.Position);
        }

        void UpdateSpeed()
        {
            int tiles = (int)(sprite.PosX / 200);
            if (tiles % 50 == 0)
                speed++;
        }
    }
}
