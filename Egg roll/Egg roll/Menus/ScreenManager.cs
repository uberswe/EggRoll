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
using System.IO.IsolatedStorage;


namespace Egg_roll.Menus
{

    public class ScreenManager
    {
        MainScene mainScene;
        MainMenu mainMenu;
        LoadingScreen loadingScreen;
        HighScores highScores;
        SettingsMenu settingsMenu;
        PauseMenu pauseMenu;
        DeadScreen deadScreen;
        WeatherMenu weatherMenu;

        ServerHandler serverHandler;
        Weather weather;

        string[] menuItems;

        bool gameIsAlive = true;

        int menu = -1;

        TimeSpan deadTime;
        GameTime gameTime;
        bool died = false;

        public WeatherForecast CurrentWeather;

        public ServerHandler Server
        {
            get { return serverHandler; }
        }

        public int CurrentMenu
        {
            get { return menu; }
            set { menu = value; }
        }

        public bool GameIsAlive
        {
            get { return gameIsAlive; }
        }

        public ScreenManager(ContentManager content, GraphicsDeviceManager graphics)
        {
            // TODO: Construct any child components here
            serverHandler = new ServerHandler();
            weather = new Weather();

            mainScene = new MainScene(content, graphics);
            loadingScreen = new LoadingScreen();

            mainMenu = new MainMenu(content.Load<SpriteFont>("Fonts\\menufont"));
            highScores = new HighScores(serverHandler);
            settingsMenu = new SettingsMenu(weather, this);
            pauseMenu = new PauseMenu();
            deadScreen = new DeadScreen(this);
            weatherMenu = new WeatherMenu(this, weather);

            CurrentWeather = weather.WeatherForecast;
        }

        public void Update(GameTime gameTime)
        {
            this.gameTime = gameTime;
            if (menu == -1)
            {
                mainMenu.Update(gameTime, this);
            }
            else if (menu == 0)
            {
                mainScene.Update(gameTime, this);
               
            }
            else if (menu == 1)
            {
                highScores.Update(gameTime, this);
            }
            else if (menu == 2)
            {
                settingsMenu.Update(gameTime, this);
            }
            else if (menu == 3)
            {
                gameIsAlive = false;
            }
            else if (menu == 4)
            {
                pauseMenu.Update(gameTime, this);
            }
            else if (menu == 5)
            {
                deadScreen.Update(gameTime);
            }
            else if (menu == 6)
            {
                weatherMenu.Update(gameTime);
            }
        }

        public void Draw(GameTime gameTime, GraphicsDevice graphics, SpriteBatch spriteBatch)
        {
            if (menu == -1)
            {
                mainMenu.Draw(graphics, spriteBatch);
            }
            else if (menu == 0)
            {
                mainScene.Draw(spriteBatch, graphics, gameIsAlive);
            }
            else if (menu == 1)
            {
                highScores.Draw(graphics, spriteBatch);
            }
            else if (menu == 2)
            {
                settingsMenu.Draw(graphics, spriteBatch);
            }
            else if (menu == 4)
            {
                mainScene.Draw(spriteBatch, graphics, false);
                pauseMenu.Draw(graphics, spriteBatch);
            }
            else if (menu == 5)
            {
                mainScene.Draw(spriteBatch, graphics, false);
                deadScreen.Draw(graphics, spriteBatch);
            }
            else if (menu == 6)
            {
                weatherMenu.Draw(graphics, spriteBatch);
            }
        }

        public void ResetGame()
        {
            mainScene.ResetGame();
            died = false;
            mainScene.SetResetPlayer = false;
            WorldGen.ResetWorld();
        }

        public void playerDead()
        {
            if (!died)
            {
                deadTime = gameTime.TotalGameTime;
                died = true;
            }
            else
            {
                TimeSpan wait = new TimeSpan(0, 0, 2);
                if (gameTime.TotalGameTime.CompareTo(deadTime + wait) == 1)
                {
                    CurrentMenu = 5;
                }
            }
        }

        public float GetPlayerScore {
            get { return mainScene.GetPlayerScore(); }
        }
    }
}
