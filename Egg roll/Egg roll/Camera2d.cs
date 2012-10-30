using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Egg_roll
{
    public class Camera2d
    {
        static float zoom = 0.5f; // Camera Zoom
        static Matrix transform; // Matrix Transform
        static Vector2 pos = Vector2.Zero; // Camera Position
        static float rotation = 0.0f; // Camera Rotation

        public static Vector2 MousePosition
        {
            get
            {
                MouseState m = Mouse.GetState();
                Vector2 pos = new Vector2(m.X, m.Y);
                return GetPositionInWorld(pos);
            }
        }
        /// <summary>
        /// Remember the center of the screen is equals to NormalPosition
        /// </summary>
        public static Vector2 NormalPosition
        {
            get { return new Vector2(Stuff.Resolution.X / 2 / zoom, Stuff.Resolution.Y / 2 / Zoom); }
        }

        public static float Zoom
        {
            get { return zoom; }
            set { zoom = value; } //if (zoom < 0.1f) zoom = 0.1f; } //if (zoom > 5)  zoom = 5; } // Negative zoom will flip image
        }

        public static float Rotation
        {
            get { return rotation; }
            set { rotation = value; }
        }

        // Auxiliary function to move the camera
        public static void Move(Vector2 amount)
        {
            pos += amount;
        }

        public static void SmoothFollow(Vector2 targetPos, float panSpeed)
        {
            pos = Stuff.Lerp(Position, targetPos, panSpeed);
        }
        public static void SmoothFollow(Vector2 targetPos)
        {
            SmoothFollow(targetPos, 0.1f);
        }

        // Get set position
        public static Vector2 Position
        {
            get { return pos; }
            set
            {
                pos = value;
            }
        }

        /// <summary>
        /// Gets the position in the world, corresponding to a position on the screen(Doesn't calculate with rotation) 
        /// </summary>
        public static Vector2 GetPositionInWorld(Vector2 screenPosition)
        {
            Vector2 c = Camera2d.Position, mid = Camera2d.NormalPosition;
            float z = Camera2d.Zoom;
            Vector2 m = new Vector2(
                    (screenPosition.X / z) + (c.X - mid.X),
                    (screenPosition.Y / z) + (c.Y - mid.Y)
                );
            return m;
        }

        public static Vector2 GetPositionOnScreen(Vector2 worldPosition)
        {
            Vector2 c = Camera2d.Position, mid = Camera2d.NormalPosition;
            float z = Camera2d.Zoom;
            Vector2 m = new Vector2(
                       (worldPosition.X - (c.X - mid.X)) * z,
                       (worldPosition.Y - (c.Y - mid.Y)) * z
                   );
            return m;
        }

        public static Matrix GetTransformation(GraphicsDevice graphicsDevice)
        {
            transform =       // Thanks to o KB o for this solution
              Matrix.CreateTranslation(new Vector3(-pos.X, -pos.Y, 0)) *
                //Matrix.CreateRotationZ(Rotation) *
                                         Matrix.CreateScale(new Vector3(Zoom, Zoom, 1)) *
                                         Matrix.CreateTranslation(new Vector3(Stuff.Resolution.X * 0.5f, Stuff.Resolution.Y * 0.5f, 0));
            return transform;
        }
    }
}
