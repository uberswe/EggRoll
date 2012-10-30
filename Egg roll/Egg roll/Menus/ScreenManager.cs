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

    public class ScreenManager
    {
        MainScene mainScene;
        MainMenu mainMenu;
        LoadingScreen loadingScreen;
        HighScores highScores;
        SettingsMenu settingsMenu;
        PauseMenu pauseMenu;
        string[] menuItems;

        bool gameIsAlive = true;

        TimeSpan timeSpan = new TimeSpan(0, 0, 2);

        int menu = -1;

        public int CurrentMenu
        {
            get { return menu; }
            set {  menu = value; }
        }

        public bool GameIsAlive
        {
            get { return gameIsAlive; }
        }

        public ScreenManager(ContentManager content, GraphicsDeviceManager graphics, string[] menuItems)
        {
            // TODO: Construct any child components here
            this.menuItems = menuItems;
            mainScene = new MainScene(content, graphics);
            loadingScreen = new LoadingScreen();
            
            mainMenu = new MainMenu(
            content.Load<SpriteFont>("Fonts\\menufont"),
            menuItems);
            highScores = new HighScores();
            settingsMenu = new SettingsMenu();
            pauseMenu = new PauseMenu();
        }

        public void Update(GameTime gameTime)
        {
            if (TimeSpan.Compare(gameTime.TotalGameTime, timeSpan) == -1)
            {
                loadingScreen.Update(gameTime);
            }
            else if (menu == -1)
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
        }

        public void Draw(GameTime gameTime, GraphicsDevice graphics, SpriteBatch spriteBatch)
        {
            if (TimeSpan.Compare(gameTime.TotalGameTime, timeSpan) == -1)
            {
                loadingScreen.Draw(graphics, spriteBatch);
            }
            else if (menu == -1)
            {
                mainMenu.Draw(graphics, spriteBatch);
            }
            else if (menu == 0)
            {
                mainScene.Draw(spriteBatch, graphics);
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
                pauseMenu.Draw(graphics, spriteBatch);
            }
        }

        public void ResetGame()
        {
            mainScene.ResetGame();
        }
    }
}
