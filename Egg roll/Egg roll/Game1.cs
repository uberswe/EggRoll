using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using Microsoft.Xna.Framework.Media;
using EggRollGameLib;
using EggRollGameLib.ClassFiles;
using EggRollGameLib.ClassFiles.Menus;
namespace Egg_roll
{
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        MainScene mainScene;
        MainMenu mainMenu;

        int menu = 1; //1 = main menu, 0 = game?
        
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            // Frame rate is 30 fps by default for Windows Phone.
            TargetElapsedTime = TimeSpan.FromTicks(333333);

            // Extend battery life under lock.
            InactiveSleepTime = TimeSpan.FromSeconds(1);
            
        }

        protected override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            string[] menuItems = { "Start Game", "High Scores", "Level Manager", "Character select", "End Game" };
            spriteBatch = new SpriteBatch(GraphicsDevice);
            mainMenu = new MainMenu(this,
            spriteBatch,
            Content.Load<SpriteFont>("menufont"),
            menuItems);
            Components.Add(mainMenu);

            spriteBatch = new SpriteBatch(GraphicsDevice);
            mainScene = new MainScene(Content, graphics);
        }

        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (menu == 1)
            {
                menu = mainMenu.MenuSelect;
            }

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
            {
                this.Exit();
            }

            mainScene.Update(gameTime); 

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            if (menu == 1)
            {
                mainMenu.Draw(gameTime);
            }
            else
            {
                mainScene.Draw(spriteBatch, GraphicsDevice);
            }

            base.Draw(gameTime);
        }
    }
}
