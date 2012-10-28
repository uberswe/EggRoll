using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using EggrollGameLib.ClassFiles;
using EggRollGameLib.ClassFiles.Menus;

namespace EggRollGameLib.ClassFiles
{
    public class MainScene
    {
        GraphicsDeviceManager graphics;
        OnScreenMessages onScreenMessages;
        float elaps, totaltPlayTime;
        Input input;
        List<Character> characters;
        Player player;
        List<Sprite> debugTiles;
        Rectangle DrawRect;

        public static float yaccel;

        public MainScene(ContentManager content, GraphicsDeviceManager graphicsManager)
        {
            Stuff.Initialize(content, graphicsManager);
            graphics = graphicsManager;
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
            for (int i = 0; i < 100; i++)
            {
                Sprite tempSprite = new Sprite("SpriteSheet");
                tempSprite.Source = new Rectangle(11 * 150, 9 * 150, 150, 150);
                tempSprite.Position = new Vector2(150*i, 250 + (-150*i));
                debugTiles.Add(tempSprite);
            }
        }

        public void Update(GameTime gameTime, ScreenManager screenManager)
        {
            DrawRect = new Rectangle((int)player.Position.X - (graphics.PreferredBackBufferWidth / 2) - 150, (int)player.Position.Y - (graphics.PreferredBackBufferHeight / 2) - 150, graphics.PreferredBackBufferWidth + 300, graphics.PreferredBackBufferHeight + 300);
            elaps = (float)gameTime.ElapsedGameTime.TotalSeconds;
            totaltPlayTime += elaps;
            Input.Update();
            onScreenMessages.Update(elaps);
            int c = characters.Count;
            for (int i = 0; i < c; i++)
            {
                characters[i].Update(elaps);
            }

            player.Update(elaps, yaccel, screenManager);
        }

        public static void AccelData(Vector3 AccelerationReading)
        {
            yaccel = AccelerationReading.Y;
        }

        public void Draw(SpriteBatch spriteBatch, GraphicsDevice graphics)
        {
            graphics.Clear(Color.CornflowerBlue);
            Matrix camera = Camera2d.GetTransformation(graphics);

            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.NonPremultiplied, SamplerState.PointClamp, null, null, null, camera);
            onScreenMessages.Draw(spriteBatch);

            int c = characters.Count;
            for (int i = 0; i < c; i++)
            {
                if (DrawRect.Contains((int)characters[i].Position.X, (int)characters[i].Position.Y))
                    characters[i].Draw(spriteBatch);
            }

            c = debugTiles.Count;
            for (int i = 0; i < c; i++)
            {
                if(DrawRect.Contains((int)debugTiles[i].Position.X, (int)debugTiles[i].Position.Y))
                    debugTiles[i].Draw(spriteBatch);
            }

            player.Draw(spriteBatch);

            spriteBatch.End();

            spriteBatch.Begin();

            player.DrawButtons(spriteBatch);

            
            spriteBatch.End();

        }

        public void ResetGame()
        {
            characters = new List<Character>();
            player = new Player(characters);
        }
    }
}
