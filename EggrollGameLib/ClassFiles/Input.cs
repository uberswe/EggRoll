using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input.Touch;

namespace EggRollGameLib
{
    public class Input
    {
        public static KeyboardState ks, pks;
        public static MouseState ms, pms;
        public static TouchCollection tc;
        static int maxTouchCount;


        public static void Initialize()
        {
            ks = new KeyboardState();
            pks = new KeyboardState();
            ms = new MouseState();
            pms = new MouseState();
            //indicates how many simultaneous touch inputs the current screen can handle. 
            maxTouchCount = TouchPanel.GetCapabilities().MaximumTouchCount; 
                        
        }
        public static void Update()
        {
            pks = ks;
            ks = Keyboard.GetState();
            pms = ms;
            ms = Mouse.GetState();
            tc = TouchPanel.GetState();
        }

        /// <summary>
        /// returns a collection of touch locations, containing Position, TouchState etc. 
        /// </summary>
        public static List<TouchLocation> TouchLocations() 
        {
            List<TouchLocation> l = new List<TouchLocation>();
            for (int i = 0; i < maxTouchCount; i++)
                l.Add(tc[i]); 
            return l; 
        }

        

        /// <summary>
        /// true when a key is pressed and released again
        /// </summary>
        public static bool KeyTap(Keys key)
        {
            if (ks.IsKeyUp(key) && pks.IsKeyDown(key))
            {
                pks = ks; 
                return true;
            }
            return false;
        }
        /// <summary>
        /// true when a key is pressed
        /// </summary>
        public static bool KeyDown(Keys key)
        {
            if (ks.IsKeyDown(key))
                return true;
            return false;
        }

        public static bool AnyKey()
        {
            if (ks != pks)
                return true;
            if (MouseLeftClick() || MouseRightClick())
                return true;
            return false;
        }

        public static bool MouseRightPress()
        {
            if (ms.RightButton == ButtonState.Pressed)
                return true;
            return false;
        }
        public static bool MouseRightClick()
        {
            if (pms.RightButton == ButtonState.Pressed && ms.RightButton == ButtonState.Released)
                return true;
            return false;
        }
        public static bool MouseLeftPress()
        {
            if (ms.LeftButton == ButtonState.Pressed)
                return true;
            return false;
        }
        public static bool MouseLeftClick()
        {
            if (pms.LeftButton == ButtonState.Pressed && ms.LeftButton == ButtonState.Released)
                return true;
            return false;
        }

        /// <summary>
        /// returns a normalized direction in which the mouse is moving
        /// </summary>
        public static Vector2 MouseDirection()
        {
            Vector2 mv = new Vector2(ms.X, ms.Y);
            Vector2 pmv = new Vector2(pms.X, pms.Y);
            if (mv == pmv)
                return Vector2.Zero;
            else
                return Vector2.Normalize(mv - pmv);
        }
        public static Vector2 MousePosition
        {
            get { return new Vector2(ms.X, ms.Y); }
        }
    }
}
