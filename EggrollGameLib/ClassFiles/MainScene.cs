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
        Input input;
        List<Character> characters;
        Player player;
        List<Sprite> debugTiles;

        SpriteFont Font1;
        Vector2 FontPos;

        Vector3 gyro;

        public MainScene(ContentManager content, GraphicsDeviceManager graphicsManager)
        {
            Stuff.Initialize(content, graphicsManager);
            onScreenMessages = new OnScreenMessages();
            Camera2d.Position = Stuff.ScreenCenter;
            input = new Input();
            characters = new List<Character>();
            player = new Player(characters);

            debugTiles = new List<Sprite>();
            for (int i = -10; i < 30; i++)
            {
                Sprite s = new Sprite("SpriteSheet");
                s.Source = new Rectangle(1 * 150, 9 * 150, 150, 150);
                s.Position = new Vector2(i * 150, 400);
                debugTiles.Add(s);
            }
        }

        public void Update(GameTime gameTime, Vector3 gyroReading)
        {
            elaps = (float)gameTime.ElapsedGameTime.TotalSeconds;
            totaltPlayTime += elaps;
            Input.Update(gyroReading);
            onScreenMessages.Update(elaps);

            int c = characters.Count;
            for (int i = 0; i < c; i++)
            {
                characters[i].Update(elaps);
            }

            gyro = gyroReading;

            player.Update(elaps, gyroReading);
        }

        public void Draw(SpriteBatch spriteBatch, GraphicsDevice graphics)
        {
            Matrix camera = Camera2d.GetTransformation(graphics);

            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.NonPremultiplied, SamplerState.PointClamp, null, null, null, camera);
            onScreenMessages.Draw(spriteBatch);

            int c = characters.Count;
            for (int i = 0; i < c; i++)
            {
                characters[i].Draw(spriteBatch);
            }

            c = debugTiles.Count;
            for (int i = 0; i < c; i++)
            {
                debugTiles[i].Draw(spriteBatch);
            }

            player.Draw(spriteBatch);

            FontPos = new Vector2(20, 20);

            Font1 = Stuff.Content.Load<SpriteFont>("menufont");

            string output = gyro.X.ToString("0.00") + " Y = " + gyro.Y.ToString("0.00") + " Z = " + gyro.Z.ToString("0.00");

            Vector2 FontOrigin = Font1.MeasureString(output) / 2;
            spriteBatch.DrawString(Font1, output, FontPos, Color.LightGreen, 0, FontOrigin, 1.0f, SpriteEffects.None, 0.5f);

            spriteBatch.End();

            spriteBatch.Begin();

            player.DrawButtons(spriteBatch);

            
            spriteBatch.End();

        }
    }
}
