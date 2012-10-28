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
        TimeSpan timeSpan = new TimeSpan(0, 0, 2);

        int menu = -1;

        public ScreenManager(Game game, MainMenu mainMenu, MainScene mainScene, LoadingScreen loadingScreen)
            : base(game)
        {
            // TODO: Construct any child components here
            this.mainScene = mainScene;
            this.mainMenu = mainMenu;
            this.loadingScreen = loadingScreen;
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
                mainMenu.Update(gameTime);
                menu = mainMenu.MenuSelect;
            }
            else if (menu == 0)
            {
                mainScene.Update(gameTime);
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
            else
            {
                base.Game.Exit();
            }

        }
    }
}
