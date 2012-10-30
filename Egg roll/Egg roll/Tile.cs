using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Egg_roll
{
    class Tile : SourceObject
    {
        Color color;

        public Tile(Texture2D texture, Rectangle source, Vector2 position, Color color, string name)
            :base(texture, position, name, source)
        {
            this.color = color;
        }

        public Color TileColor
        {
            get { return color; }
            set { color = value; }
        }

        public override void Draw(SpriteBatch spritebatch)
        {
            spritebatch.Draw(texture, Position, Source, color, 0, Vector2.Zero, 1f, SpriteEffects.None, 0);
        }
    }
}
