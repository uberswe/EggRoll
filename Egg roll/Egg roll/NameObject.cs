using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Egg_roll
{
    class NameObject : GameObject
    {
        string name;

        public NameObject()
        {
        }

        public NameObject(Texture2D texture, Vector2 position, string name)
            : base(texture, position)
        {
            this.name = name;
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }
    }
}
