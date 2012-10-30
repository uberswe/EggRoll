using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;

namespace Egg_roll
{
    class Sound
    {
        public static void LoadContent()
        {
            ContentManager Content = Stuff.Content;

        }

        public static void PlaySoundEffect(SoundEffect eff)
        {
            eff.Play();
        }
    }
}
