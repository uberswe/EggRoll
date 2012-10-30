using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Egg_roll
{
    class SourceObject : NameObject
    {
        protected Rectangle source;

        public SourceObject()
        {
        }

        public SourceObject(Texture2D texture, Vector2 position, string name, Rectangle source)
            : base(texture, position, name)
        {
            this.source = source;
        }

        public virtual Rectangle Source
        {
            get { return source; }
            set { source = value; }
        }

        public override Rectangle CollisionRect
        {
            get
            {
                return new Rectangle((int)PosX, (int)PosY, source.Width, source.Height);
            }
        }

    }
}
