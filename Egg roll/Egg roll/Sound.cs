using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Media;

namespace Egg_roll
{
  static  class Sound
    {
     static Random rand = new Random();
      static  Song MenuSong;
      static Song BackgroundSong;
     static List<SoundEffect> PoopFX = new List<SoundEffect>();
     static List<SoundEffect> JumpFX = new List<SoundEffect>();
     static List<SoundEffect> PigFX = new List<SoundEffect>();
     static SoundEffect CoinFX;
     static SoundEffect MenuTapFX;

     static SoundEffect EggBreakFX;

     
    
     static SoundEffect HayFX;

      public static void LoadContent()
      {
          MenuTapFX = Stuff.Content.Load<SoundEffect>("Sound\\MenuTap");
          BackgroundSong = Stuff.Content.Load<Song>("Sound\\BackgroundSong");
              EggBreakFX = Stuff.Content.Load<SoundEffect>("Sound\\EggBreak");
            CoinFX = Stuff.Content.Load<SoundEffect>("Sound\\Coin");
           
            HayFX = Stuff.Content.Load<SoundEffect>("Sound\\hay");

            SoundEffect TmpFX;
            for (int i = 1; i <= 8; i++)
            {
                TmpFX = Stuff.Content.Load<SoundEffect>("Sound\\pig" + i);
                PigFX.Add(TmpFX);
            }

            for (int i = 1; i <= 5; i++)
            {
                TmpFX = Stuff.Content.Load<SoundEffect>("Sound\\Poop" + i);
              PoopFX.Add(TmpFX);
            }
            for (int i = 1; i <= 5; i++)
            {
                TmpFX = Stuff.Content.Load<SoundEffect>("Sound\\Jump" + i);
                JumpFX.Add(TmpFX);
            }
  
  }

        

      public static void PlayPoopSound(){
         
          PoopFX[rand.Next(1,6)].Play();
      
      }
      public static void PlayPigSound()
      {
      PigFX[rand.Next(0,8)].Play();
      }
      public static void PlayJumpSound()
      {
      JumpFX[rand.Next(0,4)].Play();
      }
      public static void PlayHaySound()
      {
      HayFX.Play();
      }
      public static void PlayEggBreakSound()
      {
      EggBreakFX.Play();
      }
      public static void PlayCoinSound()
      {
      CoinFX.Play();
      }
      public static void PlayMenuTapSound()
      {
          MenuTapFX.Play();
      }
        private static void PlaySong(Song song)
        {
            MediaPlayer.Volume=0.35f;
            MediaPlayer.Play(song);
            MediaPlayer.IsRepeating = true;
        }
        public static void PlayMenuSong()
        {
            PlaySong(MenuSong);
        }
        public static void PlayBackgroundSong()
        {

            
            PlaySong(BackgroundSong);
        }
        public static void StopMusic()
        {
            MediaPlayer.Stop();
        }
        public static void PauseMusic()
        {
            MediaPlayer.Pause();
        }
        public static void ResumeMusic()
        {
            MediaPlayer.Resume();
        }
    }
}
