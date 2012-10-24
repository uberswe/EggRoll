using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EggRollGameLib;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input.Touch;

namespace EggrollGameLib.ClassFiles.Menus
{
    class Button
    {
        public Vector2 position;
        Sprite sprite;
        float scale;
        public bool active, nostate;

        Rectangle hitbox
        {
            get
            {
                return new Rectangle((int)position.X, (int)position.Y, sprite.Source.Width, sprite.Source.Height);
            }
        }

        public Button(string assetName, Vector2 position)
        {
            sprite = new Sprite(assetName);
            scale = 1f;
        }
        public bool Update(float elaps, Input input)
        {
            sprite.Scale = Stuff.Lerp(sprite.Scale, scale, 0.1f);
            sprite.Position = position;
            if (nostate)
                sprite.iColor.SetColor(Color.White);
            else
            {
                if (active)
                    sprite.iColor.SetColor(Stuff.BlendColors(sprite.iColor, Color.Green, 90));
                else
                    sprite.iColor.SetColor(Stuff.BlendColors(sprite.iColor, Color.Red, 90));
            }

            bool buttonHit = false;
            foreach (TouchLocation tc in Input.tc)
                if (hitbox.Intersects(new Rectangle((int)tc.Position.X, (int)tc.Position.Y, 5, 5)))
                    buttonHit = true;

            if (buttonHit)
            {
                this.active = !this.active;
                sprite.Scale = this.scale / 1.2f;
                return true;
            }

            return false;
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            sprite.Draw(spriteBatch);
        }
    }
}