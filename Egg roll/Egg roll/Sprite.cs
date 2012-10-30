using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using System.Xml.Serialization;

namespace Egg_roll
{
    partial class Sprite : SourceObject
    {
        bool visible, flippedHorizontal;
        float scale, rotation, layer;
        Vector2 size, origin;
        public ImprovedColor iColor;
        SpriteEffects effect;

        public Sprite()
        {
            Name = "pixel";
            iColor = new ImprovedColor();
            iColor.SetColor(Color.White);
            Scale = 1;
        }
        public Sprite(ContentManager content, string assetName)
        {
            Name = assetName;
            LoadContent(content);
            iColor = new ImprovedColor();
            iColor.SetColor(Color.White);
            Scale = 1;
        }
        public Sprite(string assetName)
        {
            Name = assetName;
            LoadContent(Stuff.Content);
            iColor = new ImprovedColor();
            iColor.SetColor(Color.White);
            Scale = 1;
        }

        #region Properties
        public bool FlippedHorizontal
        {
            get { return flippedHorizontal; }
            set
            {
                if (value)
                    Effect = SpriteEffects.FlipHorizontally;
                else
                    Effect = SpriteEffects.None;

                flippedHorizontal = value;
            }
        }
        public virtual float Layer
        {
            get
            {
                return layer;
            }
            set
            {
                if (value < 0)
                    value = 0;
                else if (value > 1)
                    value = 1;
                layer = value;
            }
        }

        public Texture2D Texture
        {
            get
            {
                return texture;
            }
            set
            {
                texture = value;

            }
        }

        public bool Visible
        {
            get { return visible; }
            set { visible = value; }
        }
        public SpriteEffects Effect
        {
            get { return effect; }
            set { effect = value; }
        }

        public virtual float Rotation
        {
            get { return rotation; }
            set { rotation = value; }
        }

        public override Rectangle Source
        {
            get
            {
                return source;
            }
            set
            {
                source = value;
                Size = new Vector2(Source.Width * Scale, Source.Height * Scale);
                Origin = new Vector2((float)Source.Width / 2f, (float)Source.Height / 2f);
            }
        }

        public Vector2 Size
        {
            get
            {
                return size;
            }
            set
            {
                size = value;
            }
        }

        public Vector2 Origin
        {
            get
            {
                return origin;
            }
            set
            {
                origin = value;
            }
        }

        public virtual float Scale
        {
            get
            {
                return scale;
            }
            set
            {
                scale = value;
                Size = new Vector2(Source.Width * Scale, Source.Height * Scale);
            }
        }
        #endregion

        #region Methods

        public virtual void LoadContent(ContentManager content)
        {
            Texture = content.Load<Texture2D>("Sprites\\" + Name);
            if (Source == Rectangle.Empty)
                Source = new Rectangle(0, 0, texture.Width, texture.Height);
            Visible = true;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            DrawAt(spriteBatch, Position);
        }
        public virtual void DrawAt(SpriteBatch spriteBatch, Vector2 position)//mainly for drawing on mesh, but can be useful for menu items and such.
        {
            if (Visible && Texture != null && iColor.A > 0)
            {
                spriteBatch.Draw(
                    this.Texture,
                    position,
                    this.Source,
                    this.iColor,
                    this.Rotation,
                    this.Origin,
                    this.Scale,
                    this.Effect,
                    this.Layer
                    );
            }
        }
        #endregion
    }
    public class ImprovedColor
    {
        public static implicit operator Color(ImprovedColor c)
        {
            return c.Color;
        }

        float r, g, b, a;
        public float R
        {
            get { return r; }
            set { r = MathHelper.Clamp(value, 0, 1); }
        }
        public float G
        {
            get { return g; }
            set { g = MathHelper.Clamp(value, 0, 1); }
        }
        public float B
        {
            get { return b; }
            set { b = MathHelper.Clamp(value, 0, 1); }
        }
        public float A
        {
            get { return a; }
            set { a = MathHelper.Clamp(value, 0, 1); }
        }
        public Color Color
        {
            get
            {
                return new Color(
                    (byte)(R * 255f),
                    (byte)(G * 255f),
                    (byte)(B * 255f),
                    (byte)(A * 255f)
                    );
            }
        }

        public ImprovedColor()
        {
            R = 0;
            G = 0;
            B = 0;
            A = 0;
        }
        /// <summary>
        /// Just sets the ImprovedColor to value
        /// </summary>
        /// <param name="color">Color to set.</param>
        public void SetColor(Color color)
        {
            R = (float)(color.R / 255f);
            G = (float)(color.G / 255f);
            B = (float)(color.B / 255f);
            A = (float)(color.A / 255f);
        }
        public void SetColor(int r, int g, int b, int a)
        {
            R = (float)(r / 255f);
            G = (float)(g / 255f);
            B = (float)(b / 255f);
            A = (float)(a / 255f);
        }
        public void SetColor(int r, int g, int b)
        {
            R = (float)(r / 255f);
            G = (float)(g / 255f);
            B = (float)(b / 255f);
            A = 1f;
        }
        /// <summary>
        /// Fades the ImprovedColor to target. 
        /// </summary>
        /// <param name="color">Target color to fade to.</param>
        /// <param name="amount">amount(0-1f) of fading where 0 is no fade, 1 is full change</param>
        public void FadeColorTo(Color color, float amount)
        {
            float nr = (float)(color.R / 255f);
            float ng = (float)(color.G / 255f);
            float nb = (float)(color.B / 255f);
            float na = (float)(color.A / 255f);
            r = Stuff.Lerp(r, nr, amount);
            g = Stuff.Lerp(g, ng, amount);
            b = Stuff.Lerp(b, nb, amount);
            a = Stuff.Lerp(a, na, amount);
        }

        /// <summary>
        /// adds values from color
        /// </summary>
        /// <param name="color">Color to add</param>
        public void BlendColor(Color color)
        {
            float nr = (float)(color.R / 255f);
            float ng = (float)(color.G / 255f);
            float nb = (float)(color.B / 255f);
            float na = (float)(color.A / 255f);
            BlendColor(nr, ng, nb, na);
        }
        public void BlendColor(float r, float g, float b)
        {
            BlendColor(r, g, b, 1f);
        }
        public void BlendColor(float r, float g, float b, float a)
        {
            R += r;
            G += g;
            B += b;
            A += a;
        }
    }
}
