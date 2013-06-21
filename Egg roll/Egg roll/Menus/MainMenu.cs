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
using Microsoft.Xna.Framework.Input.Touch;

namespace Egg_roll.Menus
{
    public class MainMenu : LoadingScreen
    {
        Sprite logo;
        MenuEgg menuEgg;
        List<Button> buttons;
        //string[] menuItems;
        float width = 0f;
        float height = 0f;

       

        ScreenManager screenManager;

        //Gets info from game and menu items.
        //public MainMenu(SpriteFont spriteFont, string[] menuItems)
        public MainMenu(SpriteFont spriteFont)
        {
            this.spriteFont = spriteFont;
            //this.menuItems = menuItems;
            Input.Initialize();
            fontColor = Color.White;
            //MeasureMenu();
            LoadContent();

            logo = new Sprite("logo");
            logo.Scale = 0.4f;
            logo.Position = new Vector2(Stuff.Resolution.X * 0.675f, Stuff.Resolution.Y * 0.3f);

            buttons = new List<Button>();

            //start game
            Button b = new Button("Buttons", new Vector2(150, 50), new Rectangle(0, 120 * 2, 250, 120), false, false);
            buttons.Add(b);
            //high scores
            b = new Button("Buttons", new Vector2(150, 170), new Rectangle(0, 120 * 4, 250, 120), false, false);
            buttons.Add(b);
            //settings
            b = new Button("Buttons", new Vector2(150, 290), new Rectangle(0, 120 * 5, 250, 120), false, false);
            buttons.Add(b);
            //exit game
            b = new Button("Buttons", new Vector2(150, 410), new Rectangle(0, 120 * 7, 250, 120), false, false);
            buttons.Add(b);
            menuEgg = new MenuEgg();
        }

        //Loads Content
        void LoadContent()
        {
         
        }

        public override void Initialize()
        {

        }

        public void Update(GameTime gameTime, ScreenManager screenManager)
        {
            float elaps = (float)gameTime.ElapsedGameTime.TotalSeconds;
            this.screenManager = screenManager;
            Input.Update();
            int c = buttons.Count;
            for (int i = 0; i < c; i++)
            {
                buttons[i].Update(elaps);
            }
            Controls();
            menuEgg.Update(elaps);
        }

        //draws menu items
        public override void Draw(GraphicsDevice graphics, SpriteBatch spriteBatch)
        {
            graphics.Clear(Color.CornflowerBlue); 
            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.NonPremultiplied, SamplerState.PointClamp, null, null, null, Camera2d.GetTransformation(graphics));
            menuEgg.Draw(spriteBatch);
            spriteBatch.End();

            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.NonPremultiplied);
            int c = buttons.Count;
            for (int i = 0; i < c; i++)
            {
                buttons[i].Draw(spriteBatch);
            }
            logo.Draw(spriteBatch); 
            spriteBatch.End();
        }

        private void Controls()
        {
            //det här fungerar inte ens? 
            if (Input.KeyDown(Keys.Space) || Input.KeyDown(Keys.Enter))
            {
                screenManager.CurrentMenu = 0;
            }
            int i = 0;
            foreach (Button b in buttons)
            {
                if (b.active)
                {
                    Sound.PlayMenuTapSound();
                    if (i == 0)
                        Sound.PlayBackgroundSong();
                    screenManager.CurrentMenu = i;
                    break;
                }
                i++;
            }

        }
    }
}
