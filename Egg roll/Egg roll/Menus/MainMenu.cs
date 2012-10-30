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
        List<Button> buttons = new List<Button>();
        string[] menuItems;
        float width = 0f;
        float height = 0f;

        SoundEffect MenuTapFX;

        ScreenManager screenManager;

        //Gets info from game and menu items.
        public MainMenu(SpriteFont spriteFont, string[] menuItems)
        {
            this.spriteFont = spriteFont;
            this.menuItems = menuItems;
            Input.Initialize();
            fontColor = Color.Black;
            MeasureMenu();
            LoadContent();
        }

        //Loads Content
        void LoadContent()
        {
            MenuTapFX = Stuff.Content.Load<SoundEffect>("Sound\\MenuTap");
        }

        //Centers the menu items
        private void MeasureMenu()
        {
            height = 0;
            width = 0;
            foreach (string item in menuItems)
            {
                Vector2 size = spriteFont.MeasureString(item);
                if (size.X > width)
                    width = size.X;
                height += spriteFont.LineSpacing + 5;
            }
            position = new Vector2(
            width / 2,
            50);
        }

        public override void Initialize()
        {

        }

        public void Update(GameTime gameTime, ScreenManager screenManager)
        {
            this.screenManager = screenManager;
            Input.Update();
            foreach (Button b in buttons)
            {
                b.Update((float)gameTime.ElapsedGameTime.TotalSeconds);
            }
            Controls();
        }

        //draws menu items
        public override void Draw(GraphicsDevice graphics, SpriteBatch spriteBatch)
        {
            graphics.Clear(Color.Black);
            List<Button> buttonstemp = new List<Button>();
            spriteBatch.Begin();
            Vector2 location = position;
            for (int i = 0; i < menuItems.Length; i++)
            {
                Vector2 stringSize = spriteFont.MeasureString(menuItems[i]);
                int width = Convert.ToInt32(stringSize.X + 200); 
                int height = Convert.ToInt32(stringSize.Y + 50);
                Button button = new Button("pixel", location, new Rectangle(0, 0, width, height));
                buttonstemp.Add(button);

                button.Draw(spriteBatch);

                spriteBatch.DrawString(
                spriteFont,
                menuItems[i],
                location,
                fontColor);
                location.Y += spriteFont.LineSpacing + 55;
            }
            spriteBatch.End();
            buttons = buttonstemp;

        }

        private void Controls()
        {
            if (Input.KeyDown(Keys.Space) || Input.KeyDown(Keys.Enter))
            {
                screenManager.CurrentMenu = 0;
            }
            int i = 0;
            foreach (Button b in buttons)
            {
                if (b.active)
                {
                    Sound.PlaySoundEffect(MenuTapFX);
                    screenManager.CurrentMenu = i;
                    break;
                }
                i++;
            }

        }
    }
}
