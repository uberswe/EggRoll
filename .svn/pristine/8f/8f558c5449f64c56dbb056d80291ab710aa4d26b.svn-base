using System;
using System.Net;
using System.Device.Location;
using System.IO;
using System.Threading;

namespace Egg_roll
{
    enum WeatherForecast
    {
        Sunny,
        SunnyIntervals,
        WhiteCloud,
        BlackLowCloud,
        HazySun,
        Mist,
        Fog,
        ClearSkyNight,
        LightRainShowers,
        HeavyRainShowers,
        LightSnowShowers,
        HeavySnowShowers,
        SleetShowers,
        LightHailShowers,
        HeavyHailShowers,
        ThunderyShowers,
        CloudyWithLightRain,
        CloudyWithHeavyRain,
        CloudyWithLightSnow,
        CloudyWithHeavySnow,
        CloudyWithSleet,
        CloudyWithLightHail,
        CloudyWithHeavyHail,
        ThunderStorms
    }

    class Weather
    {
        GeoCoordinateWatcher watcher;
        GeoPositionStatus positionStatus;
        string longitude;
        string latitude;
        WeatherForecast weatherForecast;
        bool isNight = false;
        const string APIKEY = "186ec88fa1230011122510";
        WebClient client;

        public Weather()
        {
            client = new WebClient();
            watcher = new GeoCoordinateWatcher(GeoPositionAccuracy.High); // using high accuracy
            watcher.MovementThreshold = 20; // use MovementThreshold to ignore noise in the signal

            watcher.StatusChanged += new EventHandler<GeoPositionStatusChangedEventArgs>(watcher_StatusChanged);
            watcher.PositionChanged += new EventHandler<GeoPositionChangedEventArgs<GeoCoordinate>>(watcher_PositionChanged);
            client.DownloadStringCompleted += new DownloadStringCompletedEventHandler(DownloadCompleted);
            Update();
        }

        public string Longitude
        {
            get { return longitude; }
        }

        public string Latitude
        {
            get { return latitude; }
        }

        public WeatherForecast WeatherForecast
        {
            get { return weatherForecast; }
        }

        public bool IsNight
        {
            get { return isNight; }
        }

        private void Update()
        {
            UpdatePosition();
        }

        private void UpdatePosition()
        {
            watcher.Start();
        }

        private void GetForecast()
        {
            if (latitude != null && longitude != null)
            {
                string URL = "http://free.worldweatheronline.com/feed/weather.ashx?key=" + APIKEY + "&q=" + latitude + "," + longitude;
                client.DownloadStringAsync(new Uri(URL));
            }
        }

        private void watcher_PositionChanged(object sender, GeoPositionChangedEventArgs<GeoCoordinate> e)
        {
            var epl = e.Position.Location;
            longitude = epl.Longitude.ToString();
            latitude = epl.Latitude.ToString();
        }

        public GeoPositionStatus PositionStatus
        {
            get { return positionStatus; }
        }

        private void watcher_StatusChanged(object sender, GeoPositionStatusChangedEventArgs e)
        {
            positionStatus = e.Status;
            if (positionStatus == GeoPositionStatus.Ready)
            {
                GetForecast();
                watcher.Stop();
            }
        }

        private void DownloadCompleted(Object sender, DownloadStringCompletedEventArgs e)
        {
            string htmlParse = e.Result;
            string searchString = "<![CDATA[http://www.worldweatheronline.com/images/wsymbols01_png_64/";
            int from = htmlParse.IndexOf(searchString) + searchString.Length;
            string currentWeather = htmlParse.Substring(from, htmlParse.IndexOf("]]></weatherIconUrl>") - from);

            //Different kinds of weather
            switch (currentWeather)
            {
                case "wsymbol_0001_sunny.png":
                    weatherForecast = WeatherForecast.Sunny;
                    isNight = false;
                    break;
                case "wsymbol_0002_sunny_intervals.png":
                    weatherForecast = WeatherForecast.SunnyIntervals;
                    isNight = false;
                    break;
                case "wsymbol_0003_white_cloud.png":
                    weatherForecast = WeatherForecast.WhiteCloud;
                    isNight = false;
                    break;
                case "wsymbol_0004_black_low_cloud.png":
                    weatherForecast = WeatherForecast.BlackLowCloud;
                    isNight = false;
                    break;
                case "wsymbol_0005_hazy_sun.png":
                    weatherForecast = WeatherForecast.HazySun;
                    isNight = false;
                    break;
                case "wsymbol_0006_mist.png":
                    weatherForecast = WeatherForecast.Mist;
                    isNight = false;
                    break;
                case "wsymbol_0007_fog.png":
                    weatherForecast = WeatherForecast.Fog;
                    isNight = false;
                    break;
                case "wsymbol_0008_clear_sky_night.png":
                    weatherForecast = WeatherForecast.ClearSkyNight;
                    isNight = true;
                    break;
                case "wsymbol_0009_light_rain_showers.png":
                    weatherForecast = WeatherForecast.LightRainShowers;
                    isNight = false;
                    break;
                case "wsymbol_0010_heavy_rain_showers.png":
                    weatherForecast = WeatherForecast.HeavyRainShowers;
                    isNight = false;
                    break;
                case "wsymbol_0011_light_snow_showers.png":
                    weatherForecast = WeatherForecast.LightSnowShowers;
                    isNight = false;
                    break;
                case "wsymbol_0012_heavy_snow_showers.png":
                    weatherForecast = WeatherForecast.HeavySnowShowers;
                    isNight = false;
                    break;
                case "wsymbol_0013_sleet_showers.png":
                    weatherForecast = WeatherForecast.SleetShowers;
                    isNight = false;
                    break;
                case "wsymbol_0014_light_hail_showers.png":
                    weatherForecast = WeatherForecast.LightHailShowers;
                    isNight = false;
                    break;
                case "wsymbol_0015_heavy_hail_showers.png":
                    weatherForecast = WeatherForecast.HeavyHailShowers;
                    isNight = false;
                    break;
                case "wsymbol_0016_thundery_showers.png":
                    weatherForecast = WeatherForecast.ThunderyShowers;
                    isNight = false;
                    break;
                case "wsymbol_0017_cloudy_with_light_rain.png":
                    weatherForecast = WeatherForecast.CloudyWithLightRain;
                    isNight = false;
                    break;
                case "wsymbol_0018_cloudy_with_heavy_rain.png":
                    weatherForecast = WeatherForecast.CloudyWithHeavyRain;
                    isNight = false;
                    break;
                case "wsymbol_0019_cloudy_with_light_snow.png":
                    weatherForecast = WeatherForecast.CloudyWithLightSnow;
                    isNight = false;
                    break;
                case "wsymbol_0020_cloudy_with_heavy_snow.png":
                    weatherForecast = WeatherForecast.CloudyWithHeavySnow;
                    isNight = false;
                    break;
                case "wsymbol_0021_cloudy_with_sleet.png":
                    weatherForecast = WeatherForecast.CloudyWithSleet;
                    isNight = false;
                    break;
                case "wsymbol_0022_cloudy_with_light_hail.png":
                    weatherForecast = WeatherForecast.CloudyWithLightHail;
                    isNight = false;
                    break;
                case "wsymbol_0023_cloudy_with_heavy_hail.png":
                    weatherForecast = WeatherForecast.CloudyWithHeavyHail;
                    isNight = false;
                    break;
                case "wsymbol_0024_thunderstorms.png":
                    weatherForecast = WeatherForecast.ThunderStorms;
                    isNight = false;
                    break;
                case "wsymbol_0025_light_rain_showers_night.png":
                    weatherForecast = WeatherForecast.LightRainShowers;
                    isNight = true;
                    break;
                case "wsymbol_0026_heavy_rain_showers_night.png":
                    weatherForecast = WeatherForecast.HeavyRainShowers;
                    isNight = true;
                    break;
                case "wsymbol_0027_light_snow_showers_night.png":
                    weatherForecast = WeatherForecast.LightSnowShowers;
                    isNight = true;
                    break;
                case "wsymbol_0028_heavy_snow_showers_night.png":
                    weatherForecast = WeatherForecast.HeavySnowShowers;
                    isNight = true;
                    break;
                case "wsymbol_0029_sleet_showers_night.png":
                    weatherForecast = WeatherForecast.SleetShowers;
                    isNight = true;
                    break;
                case "wsymbol_0030_light_hail_showers_night.png":
                    weatherForecast = WeatherForecast.LightHailShowers;
                    isNight = true;
                    break;
                case "wsymbol_0031_heavy_hail_showers_night.png":
                    weatherForecast = WeatherForecast.HeavyHailShowers;
                    isNight = true;
                    break;
                case "wsymbol_0032_thundery_showers_night.png":
                    weatherForecast = WeatherForecast.ThunderyShowers;
                    isNight = true;
                    break;
                case "wsymbol_0033_cloudy_with_light_rain_night.png":
                    weatherForecast = WeatherForecast.CloudyWithLightRain;
                    isNight = true;
                    break;
                case "wsymbol_0034_cloudy_with_heavy_rain_night.png":
                    weatherForecast = WeatherForecast.CloudyWithHeavyRain;
                    isNight = true;
                    break;
                case "wsymbol_0035_cloudy_with_light_snow_night.png":
                    weatherForecast = WeatherForecast.CloudyWithLightSnow;
                    isNight = true;
                    break;
                case "wsymbol_0036_cloudy_with_heavy_snow_night.png":
                    weatherForecast = WeatherForecast.CloudyWithHeavySnow;
                    isNight = true;
                    break;
                case "wsymbol_0037_cloudy_with_sleet_night.png":
                    weatherForecast = WeatherForecast.CloudyWithSleet;
                    isNight = true;
                    break;
                case "wsymbol_0038_cloudy_with_light_hail_night.png":
                    weatherForecast = WeatherForecast.CloudyWithLightHail;
                    isNight = true;
                    break;
                case "wsymbol_0039_cloudy_with_heavy_hail_night.png":
                    weatherForecast = WeatherForecast.CloudyWithHeavyHail;
                    isNight = true;
                    break;
                case "wsymbol_0040_thunderstorms_night.png":
                    weatherForecast = WeatherForecast.ThunderStorms;
                    isNight = true;
                    break;
            }
        }
    }
}
