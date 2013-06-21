using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Egg_roll;

namespace EggrollGameLib.ClassFiles
{
    class SpriteBody
    {
        List<BodyPart> bodyParts;
        public float rotation, scale;

        public SpriteBody()
        {
            scale = 1f;
        }

        public void AddBodyPart(BodyPart bodyPart)
        {
            if (bodyParts == null)
                bodyParts = new List<BodyPart>();
            bodyParts.Add(bodyPart);
        }
        public void AddBodyPart(string name, Sprite sprite, Vector2 position)
        {
            BodyPart b = new BodyPart(name, sprite, position);
            AddBodyPart(b);
        }
        public void AddBodyPart(string name, Sprite sprite)
        {
            AddBodyPart(name, sprite, Vector2.Zero);
        }

        public void Update(float elaps)
        {
            int c = bodyParts.Count;
            for (int i = 0; i < c; i++)
            {
                bodyParts[i].Update(elaps);
            }
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 position)
        {
            int c = bodyParts.Count;
            for (int i = 0; i < c; i++)
            {
                //calibrate bodypart position with rotation and scale. 
                Vector2 meshPos = new Vector2(
                    (bodyParts[i].position.X * (float)Math.Cos(rotation)) - (bodyParts[i].position.Y * (float)Math.Sin(rotation)),
                    (bodyParts[i].position.X * (float)Math.Sin(rotation)) + (bodyParts[i].position.Y * (float)Math.Cos(rotation))
                    );
                
                bodyParts[i].Draw(spriteBatch, position + (meshPos * scale), rotation, scale);
            }
        }
    }

    class BodyPart
    {
        public Vector2 position;
        Sprite sprite;
        public float rotation, scale;
        string name;

        bool rotDir;
        float rotMin, rotMax, rotSpd;

        bool scaleDir;
        float scaleMin, scaleMax, scaleSpd;

        bool posDir;
        Vector2 posMin, posMax;
        float posSpd, posDist;

        public BodyPart(string name, Sprite sprite, Vector2 position)
        {
            this.sprite = sprite;
            sprite.Effect = SpriteEffects.FlipHorizontally; 
            this.name = name;
            this.position = position;
        }

        public void Update(float elaps)
        {
            //idle rotation
            if (rotMin != rotMax)
            {
                if (rotDir)
                {
                    rotation = Stuff.Lerp(rotation, rotMax, rotSpd);
                    if (rotation > rotMax - ((rotMax - rotMin) * 0.1f))
                        rotDir = false;
                }
                else
                {
                    rotation = Stuff.Lerp(rotation, rotMin, rotSpd);
                    if (rotation <= rotMin + ((rotMax - rotMin) * 0.1f))
                        rotDir = true;
                }
            }
            if (scaleMin != scaleMax)
            {
                if (scaleDir)
                {
                    scale = Stuff.Lerp(scale, scaleMax, scaleSpd);
                    if (scale > scaleMax - ((scaleMax - scaleMin) * 0.1f))
                        scaleDir = false;
                }
                else
                {
                    scale = Stuff.Lerp(scale, scaleMin, scaleSpd);
                    if (scale <= scaleMin + ((scaleMax - scaleMin) * 0.1f))
                        scaleDir = true;
                }
            }
            if (posMin != posMax)
            {
                if (posDir)
                {
                    position = Stuff.Lerp(position, posMax, posSpd);
                    float dist = Vector2.Distance(position, posMax);
                    if (dist < posDist * 0.1f)
                        posDir = false;
                }
                else
                {
                    position = Stuff.Lerp(position, posMin, posSpd);
                    float dist = Vector2.Distance(position, posMin);
                    if (dist < posDist * 0.1f)
                        posDir = true;
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 position, float rotation, float scale)
        {
            sprite.Scale = this.scale + scale;
            sprite.Rotation = this.rotation + rotation;
            sprite.DrawAt(spriteBatch, position);
        }

        public void SetRotationAnimation(float minValue, float maxValue, float rotationSpeed, bool startDir)
        {
            this.rotDir = startDir;
            if (this.rotDir) this.rotation = rotMin;
            else this.rotation = rotMax;
            this.rotMin = minValue;
            this.rotMax = maxValue;
            this.rotSpd = rotationSpeed;
        }
        public void SetRotationAnimation(float minValue, float maxValue, float rotationSpeed)
        { SetRotationAnimation(minValue, maxValue, rotationSpeed, true); }

        public void SetScaleAnimation(float minValue, float maxValue, float scaleSpeed, bool startDir)
        {
            this.scaleDir = startDir;
            if (this.scaleDir) this.scale = scaleMin;
            else this.scale = scaleMax;
            this.scaleMin = minValue;
            this.scaleMax = maxValue;
            this.scaleSpd = scaleSpeed;
        }
        public void SetScaleAnimation(float minValue, float maxValue, float scaleSpeed)
        { SetScaleAnimation(minValue, maxValue, scaleSpeed, true); }
        public void SetPositionAnimation(Vector2 minValue, Vector2 maxValue, float positionSpeed, bool startDir)
        {
            this.posDir = startDir;
            if (posDir) this.position = posMin;
            else this.position = posMax;
            this.posMin = minValue;
            this.posMax = maxValue;
            this.posDist = Vector2.Distance(posMin, posMax);
            this.posSpd = positionSpeed;
        }
        public void SetPositionAnimation(Vector2 minValue, Vector2 maxValue, float positionSpeed)
        { SetPositionAnimation(minValue, maxValue, positionSpeed, true); }
    }
}
