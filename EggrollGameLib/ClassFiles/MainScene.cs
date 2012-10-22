using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace EggRollGameLib.ClassFiles
{
    public class MainScene
    {
        OnScreenMessages onScreenMessages;
        float elaps, totaltPlayTime;
        Sprite testSprite;

        public MainScene(ContentManager content, GraphicsDeviceManager graphicsManager)
        {
            Stuff.Initialize(content, graphicsManager);
            onScreenMessages = new OnScreenMessages();
            testSprite = new Sprite("pixel");
            testSprite.Scale = 20;
            testSprite.iColor.SetColor(Color.Black);
            testSprite.Position = new Vector2(300, 300); 
            Camera2d.Position = Stuff.ScreenCenter; 
        }

        public void Update(GameTime gameTime)
        {
            elaps = (float)gameTime.ElapsedGameTime.TotalSeconds;
            totaltPlayTime += elaps; 
            onScreenMessages.Update(elaps);
        }

        public void Draw(SpriteBatch spriteBatch, GraphicsDevice graphics)
        {
            Matrix camera = Camera2d.GetTransformation(graphics);
            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.PointClamp, null, null, null, camera);
            onScreenMessages.Draw(spriteBatch);

            testSprite.Draw(spriteBatch); 
            spriteBatch.End();
        }
    }
}
