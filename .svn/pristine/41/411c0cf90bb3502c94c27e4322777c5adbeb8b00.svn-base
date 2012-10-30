using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Egg_roll.Menus
{
    class HighScores : Menu
    {

        SoundEffect MenuTapFX;

        public HighScores()
        {
            btnBack = new Button("pixel", new Vector2(710, 420), new Rectangle(0, 0, 150, 100));
            LoadContent();
        }

        //Loads Content
        void LoadContent()
        {
            MenuTapFX = Stuff.Content.Load<SoundEffect>("Sound\\MenuTap");
        }

        public override void Initialize()
        {

            base.Initialize();
        }

        public void Update(GameTime gameTime, ScreenManager screenManager)
        {
            this.screenManager = screenManager;

            Input.Update();
            btnBack.Update((float)gameTime.ElapsedGameTime.TotalSeconds);

            Controls();

            base.Update(gameTime);
        }


        public override void Draw(GraphicsDevice graphics, SpriteBatch spriteBatch)
        {
            spriteFont = Stuff.Content.Load<SpriteFont>("Fonts\\loadingfont");
            position = new Vector2(100, 30);
            graphics.Clear(Color.Black);
            spriteBatch.Begin();
            spriteBatch.DrawString(
                spriteFont,
                "High Scores",
                position,
                fontColor);
            btnBack.Draw(spriteBatch);
            spriteBatch.End();

        }

        private void Controls()
        {
            if (btnBack.active)
            {
                Sound.PlaySoundEffect(MenuTapFX);
                screenManager.CurrentMenu = -1;
            }
        }
    }
}
