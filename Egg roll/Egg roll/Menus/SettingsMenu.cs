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
    class SettingsMenu : Menu
    {
        Button btnChangeWeather, btnChangePlayer;

        StorageManager sm = new StorageManager();

        SpriteFont spriteFont2;



        Weather weather;

        ScreenManager screenManager;

        public SettingsMenu(Weather weather, ScreenManager screenManager)
        {
            this.weather = weather;
            this.screenManager = screenManager;
            btnBack = new Button("Buttons", new Vector2(600, 400), new Rectangle(0, 120 * 6, 250, 120), false, false);
            btnChangeWeather = new Button("Buttons", new Vector2(200, 200), new Rectangle(250, 120 * 1, 250, 120), false, false);
            btnChangePlayer = new Button("Buttons", new Vector2(200, 400), new Rectangle(250, 120 * 0, 250, 120), false, false);
            LoadContent();
        }

        //Loads Content
        void LoadContent()
        {
            
        }

        public void Update(GameTime gameTime, ScreenManager screenManager)
        {
            this.screenManager = screenManager;
            Input.Update();
            btnBack.Update((float)gameTime.ElapsedGameTime.TotalSeconds);
            btnChangeWeather.Update((float)gameTime.ElapsedGameTime.TotalSeconds);
            btnChangePlayer.Update((float)gameTime.ElapsedGameTime.TotalSeconds);

            Controls();
        }

        public override void Draw(GraphicsDevice graphics, SpriteBatch spriteBatch)
        {
            spriteFont = Stuff.Content.Load<SpriteFont>("Fonts\\loadingfont");
            spriteFont2 = Stuff.Content.Load<SpriteFont>("Fonts\\highscores");
            position = new Vector2(100, 30);
            graphics.Clear(Color.Black);
            spriteBatch.Begin();
            spriteBatch.DrawString(
                spriteFont,
                "Settings",
                position,
                fontColor);
            spriteBatch.DrawString(
                spriteFont2,
                "Current weather: " + weather.WeatherForecast.ToString(),
                new Vector2(100,100),
                fontColor);
            if (sm.LoadObj("PlayerName") != null)
            {
                spriteBatch.DrawString(
                    spriteFont2,
                    "PlayerName: " + sm.LoadObj("PlayerName"),
                    new Vector2(100, 300),
                    fontColor);
            }
            else
            {
                spriteBatch.DrawString(
                    spriteFont2,
                    "PlayerName: ",
                    new Vector2(100, 300),
                    fontColor);
            }
            btnBack.Draw(spriteBatch);
            btnChangeWeather.Draw(spriteBatch);
            btnChangePlayer.Draw(spriteBatch);
            spriteBatch.End();
        }
        private void Controls()
        {
            if (btnBack.active)
            {
                Sound.PlayMenuTapSound();
                screenManager.CurrentMenu = -1;
            }
            if (btnChangePlayer.active)
            {
                if (sm.LoadObj("PlayerName") != null)
                {
                    Guide.BeginShowKeyboardInput(Microsoft.Xna.Framework.PlayerIndex.One, "Player Name", "Please enter a name to use to identify your score.", sm.LoadObj("PlayerName").ToString(), new AsyncCallback(GetPlayerName), null);
                }
                else {
                    Guide.BeginShowKeyboardInput(Microsoft.Xna.Framework.PlayerIndex.One, "Player Name", "Please enter a name to use to identify your score.", "", new AsyncCallback(GetPlayerName), null);
                }
            }
            if (btnChangeWeather.active)
            {
                screenManager.CurrentMenu = 6;
            }
        }

        void GetPlayerName(IAsyncResult res)
        {
            string playerName = Guide.EndShowKeyboardInput(res);
            sm.SaveObj(playerName, "PlayerName");
        }
    }
}
