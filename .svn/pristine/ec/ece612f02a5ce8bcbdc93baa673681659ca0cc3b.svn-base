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
    /// <summary>
    /// This is a game component that implements IUpdateable.
    /// </summary>
    public class LoadingScreen
    {
        protected Color fontColor;
        protected SpriteFont spriteFont;
        protected Vector2 position;

        public LoadingScreen()
        {
            this.fontColor = Color.White;
        }

        public virtual void Initialize()
        {

        }

        public virtual void Update(GameTime gameTime)
        {
        }

        public virtual void Draw(GraphicsDevice graphics, SpriteBatch spriteBatch)
        {
            spriteFont = Stuff.Content.Load<SpriteFont>("Fonts\\loadingfont");
            position = new Vector2(200, 150);
            graphics.Clear(Color.Black);
            spriteBatch.Begin();
            spriteBatch.DrawString(
                spriteFont,
                "Loading...",
                position,
                fontColor);
            spriteBatch.End();
        }
    }
}
