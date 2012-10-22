using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EggRollGameLib;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;

namespace EggrollGameLib.ClassFiles
{
    class Player : Character
    {
        Input input;

        public Player(Input input, List<Character> characters)
            : base()
        {
            this.input = input;
            this.characters = characters;
            sprite = new Sprite("pixel");
            sprite.Scale = 10;
            sprite.iColor.SetColor(Color.Black);
            speed = 100;
        }

        public override void LoadContent()
        {
            base.LoadContent();
        }

        public override void Update(float elaps)
        {
            base.Update(elaps);
            Controls();
        }

        public override void Draw(Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }

        private void Controls()
        {
            Vector2 dir = Vector2.Zero;
            if (input.KeyDown(Keys.Left))
                dir.X -= 1f;
            if (input.KeyDown(Keys.Right))
                dir.X += 1f;
            if (input.KeyDown(Keys.Up))
                dir.Y -= 1f;
            if (input.KeyDown(Keys.Down))
                dir.Y += 1f;

            foreach (TouchLocation t in input.tc)
            {
                if (t.State == TouchLocationState.Pressed|| t.State== TouchLocationState.Moved)
                    dir = Stuff.GetDirectionFromAim(position, t.Position);
            }

            direction = Stuff.Lerp(direction, dir, 0.1f);
        }
    }
}
