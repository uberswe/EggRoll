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
using Egg_roll.Server;

namespace Egg_roll.Menus
{
    class HighScores : Menu
    {
        ServerHandler serverHandler;


        //Button btnUpdate;

        int dots = 0;

        ScoreTable scores;

        SpriteFont spriteFont2;

        public HighScores(ServerHandler serverHandler)
        {
            this.serverHandler = serverHandler;
            btnBack = new Button("Buttons", new Vector2(600, 400), new Rectangle(0, 120 * 6, 250, 120), false, false);
            //btnUpdate = new Button("Buttons", new Vector2(200, 400), new Rectangle(0, 120 * 9, 250, 120), false, false);
            LoadContent();
        }

        //Loads Content
        void LoadContent()
        {
          
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
            //btnUpdate.Update((float)gameTime.ElapsedGameTime.TotalSeconds);

            Controls();

            base.Update(gameTime);
        }


        public override void Draw(GraphicsDevice graphics, SpriteBatch spriteBatch)
        {
            if (scores == null)
            {
                serverHandler.BeginFetchHighScores();
            }
            spriteFont = Stuff.Content.Load<SpriteFont>("Fonts\\loadingfont");
            spriteFont2 = Stuff.Content.Load<SpriteFont>("Fonts\\highscores");
            position = new Vector2(100, 30);
            graphics.Clear(Color.Black);
            spriteBatch.Begin();
            spriteBatch.DrawString(
                spriteFont,
                "High Scores",
                position,
                fontColor);
            if (serverHandler.ScoresFetched)
            {
                scores = serverHandler.HighScores();
                int i = 0;
                foreach (string entry in scores.players)
                {
                    spriteBatch.DrawString(
                                spriteFont2,
                                entry + " " + scores.scores[i],
                                new Vector2(100, 100 + i * 25),
                                fontColor);
                    i++;
                }
            }
            else
            {
                int x = 0;
                string loading = "Loading";
                while (x < dots) {
                    loading += ".";
                    x++;
                }
                spriteBatch.DrawString(
                    spriteFont,
                    loading,
                    new Vector2(400, 100),
                    fontColor);
                dots++;
                if (dots > 5)
                {
                    dots = 0;
                }
            }



            btnBack.Draw(spriteBatch);
            //btnUpdate.Draw(spriteBatch);
            spriteBatch.End();

        }

        private void Controls()
        {
            if (btnBack.active)
            {
                Sound.PlayMenuTapSound();
                screenManager.CurrentMenu = -1;
                scores = null;
            }
            //if (btnUpdate.active)
            //{
            //    screenManager.Server.BeginFetchHighScores();
            //}
        }
    }
}
