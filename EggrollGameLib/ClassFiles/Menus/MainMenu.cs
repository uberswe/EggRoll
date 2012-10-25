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


namespace EggRollGameLib.ClassFiles.Menus
{
    public class MainMenu : Microsoft.Xna.Framework.DrawableGameComponent
    {
        string[] menuItems;

        Color fontColor = Color.White;

        SpriteBatch spriteBatch;
        SpriteFont spriteFont;
        Vector2 position;
        float width = 0f;
        float height = 0f;
        int menu = 1; //1 = main, 0 = game

        public int MenuSelect
        {
            get { return menu; }
        }

        //Gets info from game and menu items.
        public MainMenu(Game game, SpriteBatch spriteBatch, SpriteFont spriteFont, string[] menuItems)
            : base(game)
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
            base.Update(gameTime);
            Controls();
        }

        //draws menu items
        public override void Draw(GameTime gameTime)
        {
            if (menu == 1)
            {
                base.Draw(gameTime);
                Vector2 location = position;
                spriteBatch.Begin();
                for (int i = 0; i < menuItems.Length; i++)
                {
                    spriteBatch.DrawString(
                    spriteFont,
                    menuItems[i],
                    location,
                    fontColor);
                    location.Y += spriteFont.LineSpacing + 5;
                }

                spriteBatch.End();
            }
        }

        private void Controls()
        {
            if (Input.KeyDown(Keys.Space) || Input.KeyDown(Keys.Enter))
            {
                menu = 0;
            }

            foreach (TouchLocation t in Input.tc)
            {
                if (t.State == TouchLocationState.Pressed || t.State == TouchLocationState.Moved)
                {
                    menu = 0;
                }

            }
        }
    }
}
