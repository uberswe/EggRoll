using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Egg_roll.Menus
{
    class DeadScreen : Menu
    {
        protected Color fontColor;
        protected SpriteFont spriteFont;
        protected Vector2 position;
        StorageManager sm = new StorageManager();

        Button btnSendScore, btnRestart, btnChangePlayer, btnExit;

        ScreenManager screenManager;

        bool initiated, scoreSent, sendScore = false;

        Sprite transparentBackground;

      

        string playerName; //TODO make iso storage or something

        public DeadScreen(ScreenManager screenManager)
        {
            this.screenManager = screenManager;
            btnSendScore = new Button("Buttons", new Vector2(200, 400), new Rectangle(0, 120 * 12, 250, 120), false, false);
            btnRestart = new Button("Buttons", new Vector2(600, 400), new Rectangle(0, 120 * 10, 250, 120), false, false);
            btnChangePlayer = new Button("Buttons", new Vector2(200, 280), new Rectangle(250, 120 * 0, 250, 120), false, false);
            btnExit = new Button("Buttons", new Vector2(600, 280), new Rectangle(0, 120 * 7, 250, 120), false, false);
            this.fontColor = Color.White;
            transparentBackground = new Sprite("pixel");
            transparentBackground.Source = new Rectangle(0, 0, Stuff.Resolution.X, Stuff.Resolution.Y);
            transparentBackground.iColor.SetColor(0, 0, 0, 100);
            transparentBackground.Origin = Vector2.Zero;
            LoadContent();

        }

        //Loads Content
        void LoadContent()
        {
         
        }

        public virtual void Initialize()
        {

        }

        public virtual void Update(GameTime gameTime)
        {
            Input.Update();
            if (screenManager.Server.ScoresSent)
            {
                screenManager.Server.scoresSent = false;
                scoreSent = true;
                initiated = false;
            }

            
                btnSendScore.Update((float)gameTime.ElapsedGameTime.TotalSeconds);
                btnRestart.Update((float)gameTime.ElapsedGameTime.TotalSeconds);
                btnChangePlayer.Update((float)gameTime.ElapsedGameTime.TotalSeconds);
                btnExit.Update((float)gameTime.ElapsedGameTime.TotalSeconds);
            


            Controls();

        }

        public virtual void Draw(GraphicsDevice graphics, SpriteBatch spriteBatch)
        {
            spriteFont = Stuff.Content.Load<SpriteFont>("Fonts\\loadingfont");
            position = new Vector2(25, 50);
            Vector2 position2 = new Vector2(200, 150);
            Vector2 position3 = new Vector2(200, 250);
            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend);
            transparentBackground.Draw(spriteBatch);

            if (sendScore == true && playerName != null && !initiated)
            {
                screenManager.Server.BeginPushHighScores(screenManager.GetPlayerScore, playerName);
                initiated = true;
                sendScore = false;
            }
            
            if (!initiated && !scoreSent) {
                btnRestart.Draw(spriteBatch);
                btnSendScore.Draw(spriteBatch);
                btnChangePlayer.Draw(spriteBatch);
                btnExit.Draw(spriteBatch);
            spriteBatch.DrawString(
                spriteFont,
                "Your egg cracked, game over!",
                position,
                fontColor);
            try
            {
                playerName = sm.LoadObj("PlayerName").ToString();
            }
            catch { }
            if (playerName != null)
            {
                spriteBatch.DrawString(
                    spriteFont,
                    playerName + "'s score: " + screenManager.GetPlayerScore.ToString(),
                    new Vector2(200, 150),
                    fontColor);
            }
            else
            {
                spriteBatch.DrawString(
                    spriteFont,
                    "Your score: " + screenManager.GetPlayerScore.ToString(),
                    new Vector2(200, 150),
                    fontColor);
            }
        }
            else if (scoreSent) {
                spriteBatch.DrawString(
                    spriteFont,
                    "Score sent!",
                    position2,
                    fontColor);
                btnRestart.Draw(spriteBatch);
                btnExit.Draw(spriteBatch);
            }
            else
            {
                spriteBatch.DrawString(
                    spriteFont,
                    "Sending Score...",
                    position2,
                    fontColor);
                spriteBatch.DrawString(
                    spriteFont,
                    "Please wait...",
                    position3,
                    fontColor);
            }
            spriteBatch.End();
        }

        private void Controls()
        {
            if (!initiated)
            {
                if (btnExit.active)
                {
                    screenManager.CurrentMenu = -1;
                    screenManager.ResetGame();
                }
                if (btnRestart.active)
                {
                    Sound.PlayBackgroundSong();
                    Sound.PlayMenuTapSound();
                    screenManager.CurrentMenu = 0;
                    screenManager.ResetGame();
                    initiated = false;
                    scoreSent = false;
                }
                else if (btnSendScore.active)
                {
                    sendScore = true;
                    Sound.PlayMenuTapSound();

                    try
                    {
                        playerName = sm.LoadObj("PlayerName").ToString();
                    }
                    catch
                    { 
                    
                    }
                    if (playerName == null)
                    {
                        Guide.BeginShowKeyboardInput(Microsoft.Xna.Framework.PlayerIndex.One, "Player Name", "Please enter a name to use to identify your score.", "", new AsyncCallback(GetPlayerName), null);
                    }
                    else
                    {
                        screenManager.Server.BeginPushHighScores(screenManager.GetPlayerScore, playerName);
                        initiated = true;
                        sendScore = false;
                    }
                }
                if (btnChangePlayer.active)
                {
                    if (sm.LoadObj("PlayerName") != null)
                    {
                        Guide.BeginShowKeyboardInput(Microsoft.Xna.Framework.PlayerIndex.One, "Player Name", "Please enter a name to use to identify your score.", sm.LoadObj("PlayerName").ToString(), new AsyncCallback(GetPlayerName), null);
                    }
                    else
                    {
                        Guide.BeginShowKeyboardInput(Microsoft.Xna.Framework.PlayerIndex.One, "Player Name", "Please enter a name to use to identify your score.", "", new AsyncCallback(GetPlayerName), null);
                    }
                }
            }
        }

        void GetPlayerName(IAsyncResult res)
        {
            playerName = Guide.EndShowKeyboardInput(res);
            sm.SaveObj(playerName, "PlayerName");
        }
    }
}
