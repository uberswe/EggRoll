using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;

namespace EggRollGameLib
{
    public class Input
    {
        public KeyboardState ks, pks;
        public MouseState ms, pms;

        public void Initialize()
        {
            ks = new KeyboardState();
            pks = new KeyboardState();
            ms = new MouseState();
            pms = new MouseState();
        }
        public void Update()
        {
            pks = ks;
            ks = Keyboard.GetState();
            pms = ms;
            ms = Mouse.GetState();
        }

        /// <summary>
        /// true when a key is pressed and released again
        /// </summary>
        public bool KeyTap(Keys key)
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
        public bool KeyDown(Keys key)
        {
            if (ks.IsKeyDown(key))
                return true;
            return false;
        }

        public bool AnyKey()
        {
            if (ks != pks)
                return true;
            if (MouseLeftClick() || MouseRightClick())
                return true;
            return false;
        }

        public bool MouseRightPress()
        {
            if (ms.RightButton == ButtonState.Pressed)
                return true;
            return false;
        }
        public bool MouseRightClick()
        {
            if (pms.RightButton == ButtonState.Pressed && ms.RightButton == ButtonState.Released)
                return true;
            return false;
        }
        public bool MouseLeftPress()
        {
            if (ms.LeftButton == ButtonState.Pressed)
                return true;
            return false;
        }
        public bool MouseLeftClick()
        {
            if (pms.LeftButton == ButtonState.Pressed && ms.LeftButton == ButtonState.Released)
                return true;
            return false;
        }

        /// <summary>
        /// returns a normalized direction in which the mouse is moving
        /// </summary>
        public Vector2 MouseDirection()
        {
            Vector2 mv = new Vector2(ms.X, ms.Y);
            Vector2 pmv = new Vector2(pms.X, pms.Y);
            if (mv == pmv)
                return Vector2.Zero;
            else
                return Vector2.Normalize(mv - pmv);
        }
        public Vector2 MousePosition
        {
            get { return new Vector2(ms.X, ms.Y); }
        }
    }
}
