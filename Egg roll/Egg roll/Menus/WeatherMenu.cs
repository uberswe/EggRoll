using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Egg_roll.Menus
{
    class WeatherMenu : Menu
    {
     

        ScreenManager screenManager;

        Button btnRainy, btnCloudy, btnSunny;

        Weather weather;

        public WeatherMenu(ScreenManager screenManager, Weather weather)
        {
            this.screenManager = screenManager;
            this.weather = weather;
            btnBack = new Button("Buttons", new Vector2(600, 400), new Rectangle(0, 120 * 6, 250, 120), false, false);
            btnSunny = new Button("Buttons", new Vector2(200, 100), new Rectangle(250, 120 * 2, 250, 120), false, false);
            btnCloudy = new Button("Buttons", new Vector2(500, 100), new Rectangle(250, 120 * 3, 250, 120), false, false);
            btnRainy = new Button("Buttons", new Vector2(200, 300), new Rectangle(250, 120 * 4, 250, 120), false, false);


        }

        public override void Initialize()
        {

            base.Initialize();
        }

        public virtual void Update(GameTime gameTime)
        {
            Input.Update();
            btnBack.Update((float)gameTime.ElapsedGameTime.TotalSeconds);
            btnSunny.Update((float)gameTime.ElapsedGameTime.TotalSeconds);
            btnCloudy.Update((float)gameTime.ElapsedGameTime.TotalSeconds);
            btnRainy.Update((float)gameTime.ElapsedGameTime.TotalSeconds);
            Controls();
        }

        public virtual void Draw(GraphicsDevice graphics, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            btnBack.Draw(spriteBatch);
            btnCloudy.Draw(spriteBatch);
            btnRainy.Draw(spriteBatch);
            btnSunny.Draw(spriteBatch);
            spriteBatch.End();
        }

        private void Controls()
        {
            if (btnBack.active)
            {
                Sound.PlayMenuTapSound();
                screenManager.CurrentMenu = 2;
            }
            if (btnSunny.active)
            {
                Sound.PlayMenuTapSound();
                screenManager.CurrentWeather = WeatherForecast.Sunny;
                screenManager.CurrentMenu = 2;
            }
            if (btnCloudy.active)
            {
                Sound.PlayMenuTapSound();
                screenManager.CurrentWeather = WeatherForecast.BlackLowCloud;
                screenManager.CurrentMenu = 2;
            }
            if (btnRainy.active)
            {
                Sound.PlayMenuTapSound();
                screenManager.CurrentWeather = WeatherForecast.HeavyRainShowers;
                screenManager.CurrentMenu = 2;
            }
        }
    }
}
