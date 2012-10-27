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
using EggrollGameLib.ClassFiles.Menus;
using EggrollGameLib.ClassFiles;

namespace EggRollGameLib.ClassFiles.Menus
{
    public class MainMenu : Microsoft.Xna.Framework.DrawableGameComponent
    {
        List<Button> buttons = new List<Button>();

        string[] menuItems;

        Color fontColor = Color.Black;

        SpriteBatch spriteBatch;
        SpriteFont spriteFont;
        Vector2 position;
        float width = 0f;
        float height = 0f;
        int menu = -1; //-1 = main, 0 = game

        public int MenuSelect
        {
            get { return menu; }
        }

        //Gets info from game and menu items.
        public MainMenu(Game game, SpriteBatch spriteBatch, SpriteFont spriteFont, string[] menuItems) : base(game)
        {
            this.spriteBatch = spriteBatch;
            this.spriteFont = spriteFont;
            this.menuItems = menuItems;
            Input.Initialize();
            MeasureMenu();
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
            (Game.Window.ClientBounds.Width - width) / 2,
            20);
        }

        public override void Initialize()
        {
            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            Input.Update();
            foreach (Button b in buttons)
            {
                b.Update((float)gameTime.ElapsedGameTime.TotalSeconds);
            }
            Controls();
            base.Update(gameTime);
        }

        //draws menu items
        public void Draw(GraphicsDevice graphics, SpriteBatch spriteBatch)
        {
            graphics.Clear(Color.Black);
            List<Button> buttonstemp = new List<Button>();
            spriteBatch.Begin();
            Vector2 location = position;
            int location1;
            int location2;
            for (int i = 0; i < menuItems.Length; i++)
            {
                location1 = Convert.ToInt32(location.X);
                location2 = Convert.ToInt32(location.Y);
                Vector2 stringSize = spriteFont.MeasureString(menuItems[i]);
                int width = Convert.ToInt32(stringSize.X);
                int height = Convert.ToInt32(stringSize.Y);
                Rectangle menuItem = new Rectangle(location1, location2, width, height);
                Button button = new Button("pixel", location, menuItem);
                button.sprite.Origin = Vector2.Zero;
                buttonstemp.Add(button);

                button.Draw(spriteBatch);

                spriteBatch.DrawString(
                spriteFont,
                menuItems[i],
                location,
                fontColor);
                location.Y += spriteFont.LineSpacing + 5;
            }
            spriteBatch.End();
            buttons = buttonstemp;

        }

        private void Controls()
        {
            if (Input.KeyDown(Keys.Space) || Input.KeyDown(Keys.Enter))
            {
                menu = 0;
            }
            int i = 0;
            foreach (Button b in buttons)
            {
                if (b.active)
                {
                    menu = i;
                    break;
                }
                i++;
            }

        }
    }
}
