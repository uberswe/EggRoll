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


namespace EggRollGameLib.ClassFiles.Menus
{
    /// <summary>
    /// This is a game component that implements IUpdateable.
    /// </summary>
    public class LoadingScreen : Microsoft.Xna.Framework.GameComponent
    {
        Color fontColor = Color.White;

        SpriteFont spriteFont;
        Vector2 position;

        public LoadingScreen(Game game)
            : base(game)
        {
            // TODO: Construct any child components here
        }

        /// <summary>
        /// Allows the game component to perform any initialization it needs to before starting
        /// to run.  This is where it can query for any required services and load content.
        /// </summary>
        public override void Initialize()
        {
            // TODO: Add your initialization code here

            base.Initialize();
        }

        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            // TODO: Add your update code here

            base.Update(gameTime);
        }

        public void Draw(GraphicsDevice graphics, SpriteBatch spriteBatch)
        {
            spriteFont = Stuff.Content.Load<SpriteFont>("loadingfont");
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
