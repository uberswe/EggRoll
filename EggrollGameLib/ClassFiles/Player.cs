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

        public Player(List<Character> characters)
            : base()
        {
            this.characters = characters;
            sprite = new Sprite("pixel");
            sprite.Scale = 10;
            sprite.iColor.SetColor(Color.Black);
            speed = 100;

            btnRight = new Button("pixel", new Vector2(90, 420));
            btnLeft = new Button("pixel", new Vector2(250, 420));
            btnJump = new Button("pixel", new Vector2(720, 420));
        }

        public override void LoadContent()
        {
            base.LoadContent();
        }

        public override void Update(float elaps)
        {
            btnRight.Update(elaps);
            btnLeft.Update(elaps);
            btnJump.Update(elaps); 

            base.Update(elaps);
            Controls();
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
            if (Input.KeyDown(Keys.Left))
                dir.X -= 1f;
            if (Input.KeyDown(Keys.Right))
                dir.X += 1f;
            if (Input.KeyDown(Keys.Up))
                dir.Y -= 1f;
            if (Input.KeyDown(Keys.Down))
                dir.Y += 1f;

            foreach (TouchLocation t in Input.tc)
            {
                if (t.State == TouchLocationState.Pressed|| t.State== TouchLocationState.Moved)
                    dir = Stuff.GetDirectionFromAim(position, t.Position);
            }

            direction = Stuff.Lerp(direction, dir, 0.1f);
        }
    }
}
