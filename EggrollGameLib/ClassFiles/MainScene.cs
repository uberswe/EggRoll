using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using EggrollGameLib.ClassFiles;

namespace EggRollGameLib.ClassFiles
{
    public class MainScene
    {
        OnScreenMessages onScreenMessages;
        float elaps, totaltPlayTime;
        List<Character> characters;
        Player player; 

        public MainScene(ContentManager content, GraphicsDeviceManager graphicsManager)
        {
            Stuff.Initialize(content, graphicsManager);
            onScreenMessages = new OnScreenMessages();
            Camera2d.Position = Stuff.ScreenCenter;
            characters = new List<Character>();
            player = new Player(characters); 
        }

        public void Update(GameTime gameTime)
        {
            elaps = (float)gameTime.ElapsedGameTime.TotalSeconds;
            totaltPlayTime += elaps; 
            onScreenMessages.Update(elaps);

            int c = characters.Count;
            for (int i = 0; i < c; i++)
            {
                characters[i].Update(elaps); 
            }
        }

        public void Draw(SpriteBatch spriteBatch, GraphicsDevice graphics)
        {
            Matrix camera = Camera2d.GetTransformation(graphics);
            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.PointClamp, null, null, null, camera);
            onScreenMessages.Draw(spriteBatch);

            int c = characters.Count;
            for (int i = 0; i < c; i++)
            {
                characters[i].Draw(spriteBatch); 
            }

            spriteBatch.End();
        }
    }
}
