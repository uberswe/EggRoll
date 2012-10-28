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

using EggrollGameLib.ClassFiles;
using EggrollGameLib.ClassFiles.Menus;

namespace EggRollGameLib.ClassFiles.Menus
{
    public class PauseMenu : Microsoft.Xna.Framework.GameComponent
    {
        Button btnBack, btnResume;

        Color fontColor = Color.White;

        SpriteFont spriteFont;
        Vector2 position;

        ScreenManager screenManager;

        public PauseMenu(Game game)
            : base(game)
        {
            btnBack = new Button("pixel", new Vector2(710, 420), new Rectangle(0, 0, 150, 100));
            btnResume = new Button("pixel", new Vector2(710, 300), new Rectangle(0, 0, 150, 100));
        }

        /// <summary>
        /// Allows the game component to perform any initialization it needs to before starting
        /// to run.  This is where it can query for any required services and load content.
        /// </summary>
        public override void Initialize()
        {
            // TODO: Add your initialization code here

            base.Initialize();
        }

        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public void Update(GameTime gameTime, ScreenManager screenManager)
        {
            this.screenManager = screenManager;
            Input.Update();
            btnBack.Update((float)gameTime.ElapsedGameTime.TotalSeconds);
            btnResume.Update((float)gameTime.ElapsedGameTime.TotalSeconds);

            Controls();

            base.Update(gameTime);
        }


        public void Draw(GraphicsDevice graphics, SpriteBatch spriteBatch)
        {
            spriteFont = Stuff.Content.Load<SpriteFont>("loadingfont");
            position = new Vector2(100, 30);
            graphics.Clear(Color.Black);
            spriteBatch.Begin();
            spriteBatch.DrawString(
                spriteFont,
                "Game Paused",
                position,
                fontColor);
            btnBack.Draw(spriteBatch);
            btnResume.Draw(spriteBatch);
            spriteBatch.End();

        }
        private void Controls()
        {

            if (btnBack.active)
            {
                screenManager.CurrentMenu = -1;
                screenManager.ResetGame();
            }
            else if (btnResume.active)
            {
                screenManager.CurrentMenu = 0;
            }

        }
    }
}
