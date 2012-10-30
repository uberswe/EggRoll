using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace Egg_roll
{
    class GameObject
    {
        protected Texture2D texture;
        Vector2 position;

        public GameObject()
        {
        }

        public GameObject(Texture2D texture, Vector2 position)
        {
            this.texture = texture;
            this.position = position;
        }

        public float PosX
        {
            get { return position.X; }
            set { position.X = value; }
        }

        public float PosY
        {
            get { return position.Y; }
            set { position.Y = value; }
        }

        public Vector2 Position
        {
            get { return position; }
        }

        public virtual Rectangle CollisionRect
        {
            get { return new Rectangle((int)PosX, (int)PosY, texture.Width, texture.Height); }
        }

        public virtual void Draw(SpriteBatch spritebatch)
        {
            spritebatch.Draw(texture, position, Color.White);
        }
    }
}
