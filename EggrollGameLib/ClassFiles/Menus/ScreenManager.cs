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

        int menu = 1;

        public ScreenManager(Game game, MainMenu mainMenu, MainScene mainScene) : base(game)
        {
            // TODO: Construct any child components here
            this.mainScene = mainScene;
            this.mainMenu = mainMenu;
        }

        public override void Initialize()
        {
            // TODO: Add your initialization code here

            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            if (menu == 1)
            {
                menu = mainMenu.MenuSelect;
            }
            else
            {
                mainScene.Update(gameTime);
            }

            base.Update(gameTime);
        }

        public void Draw(GameTime gameTime, GraphicsDevice graphics, SpriteBatch spriteBatch)
        {
            if (menu == 1)
            {
                graphics.Clear(Color.Black);
                mainMenu.Draw(gameTime);
            }
            else
            {
                graphics.Clear(Color.CornflowerBlue);
                mainScene.Draw(spriteBatch, graphics);
            }
        }
    }
}
