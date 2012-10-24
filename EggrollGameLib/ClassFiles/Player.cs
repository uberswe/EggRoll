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
            sprite = new Sprite("egg");
            sprite.Scale = 0.3f;
            sprite.iColor.SetColor(Color.White);
            speed = 100;
            position = new Vector2(100, 300); 

            btnRight = new Button("pixel", new Vector2(250, 420));
            btnLeft = new Button("pixel", new Vector2(90, 420));
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
            if (btnLeft.active)
                dir.X -= 1f;
            if (btnRight.active)
                dir.X += 1f;


            direction = Stuff.Lerp(direction, dir, 0.1f);
            sprite.Rotation += direction.X / 10f;
        }
    }
}
