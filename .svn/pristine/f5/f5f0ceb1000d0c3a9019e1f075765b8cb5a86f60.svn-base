using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using Microsoft.Xna.Framework.Media;
using EggRollGameLib;
using EggRollGameLib.ClassFiles;
using EggRollGameLib.ClassFiles.Menus;
using Microsoft.Devices.Sensors;

namespace Egg_roll
{
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        ScreenManager screenManager;

        Vector3 accelerometer;

        Accelerometer accel;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            // Frame rate is 30 fps by default for Windows Phone.
            TargetElapsedTime = TimeSpan.FromTicks(333333);

            // Extend battery life under lock.
            InactiveSleepTime = TimeSpan.FromSeconds(1);
            
        }

        protected override void Initialize()
        {
            accel = new Accelerometer(); 
            accel.Start();
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            screenManager = new ScreenManager(this, spriteBatch, graphics);
        }

        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (Accelerometer.IsSupported)
            {
                AccelerometerReading ar = accel.CurrentValue;
                accelerometer = ar.Acceleration;
                MainScene.AccelData(accelerometer);
            }

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
            {
                this.Exit();
            }

            screenManager.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            screenManager.Draw(gameTime, GraphicsDevice, spriteBatch);

            base.Draw(gameTime);
        }
    }
}
