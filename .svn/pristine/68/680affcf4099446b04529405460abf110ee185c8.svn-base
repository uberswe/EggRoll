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
using EggRollGameLib.ClassFiles.Menus;
using EggRollGameLib.ClassFiles;


namespace EggRollGameLib.ClassFiles.Menus
{

    public class ScreenManager : Microsoft.Xna.Framework.DrawableGameComponent
    {
        MainScene mainScene;
        MainMenu mainMenu;
        LoadingScreen loadingScreen;
        HighScores highScores;
        SettingsMenu settingsMenu;
        PauseMenu pauseMenu;

        SpriteBatch spriteBatch;

        TimeSpan timeSpan = new TimeSpan(0, 0, 2);

        int menu = -1;

        public int CurrentMenu
        {
            get { return menu; }
            set {  menu = value; }
        }

        public ScreenManager(Game game, SpriteBatch spriteBatch, GraphicsDeviceManager graphics) : base(game)
        {
            // TODO: Construct any child components here

            mainScene = new MainScene(game.Content, graphics);
            loadingScreen = new LoadingScreen(game);
            string[] menuItems = { "Start Game", "High Scores", "Settings", "End Game" };
            this.spriteBatch = spriteBatch;
            mainMenu = new MainMenu(game,
            spriteBatch,
            game.Content.Load<SpriteFont>("menufont"),
            menuItems);
            game.Components.Add(mainMenu);
            highScores = new HighScores(game);
            settingsMenu = new SettingsMenu(game);
            pauseMenu = new PauseMenu(game);
        }

        public override void Initialize()
        {
            // TODO: Add your initialization code here

            base.Initialize();
        }

        public override void Update(GameTime gameTime)
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
                base.Game.Exit();
            }
            else if (menu == 4)
            {
                pauseMenu.Update(gameTime, this);
            }
            else
            {
                base.Game.Exit();
            }

            base.Update(gameTime);
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
            else if (menu == 3)
            {
                base.Game.Exit();
            }
            else if (menu == 4)
            {
                pauseMenu.Draw(graphics, spriteBatch);
            }
            else
            {
                base.Game.Exit();
            }

        }

        public void ResetGame()
        {
            mainScene.ResetGame();
        }
    }
}
