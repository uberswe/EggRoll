using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace EggRollGameLib
{
    class FloatingMessage
    {
        Vector2 position, direction, initialDirection;
        public Vector2 size;
        Color color;
        float time, maxTime, speed;
        public bool remove, staticMessage;
        string message;
        SpriteFont font;
        public Rectangle Hitbox
        {
            get { return new Rectangle((int)position.X, (int)position.Y, (int)size.X, (int)size.Y); }
        }

        public FloatingMessage(SpriteFont font, Vector2 position, string message, float time, Color color, Vector2 direction, bool staticMessage)
        {
            this.staticMessage = staticMessage;
            this.time = time;
            this.maxTime = time;
            this.message = message;
            this.color = color;

            if (staticMessage)
                this.position = position;
            else
                this.position = Camera2d.GetPositionOnScreen(position);

            this.direction = Vector2.Normalize(direction);
            this.initialDirection = this.direction;
            this.font = font;
            size = font.MeasureString(message);
            speed = 50;
        }

        public void Update(float elaps, List<FloatingMessage> allMessages)
        {
            //int c = allMessages.Count;
            //for (int i = 0; i < c; i++)
            //{
            //    if (Hitbox.Intersects(allMessages[i].Hitbox)) 
            //    {
            //        direction = Stuff.Lerp(direction, Stuff.GetDirectionFromAim(allMessages[i].position, position), 0.1f); 
            //    } 
            //}
            time -= elaps;
            if (time <= 0)
                remove = true;
            position += elaps * direction * speed;
            //direction = Stuff.Lerp(direction, Vector2.Zero, 0.05f);
            //if (talker != null)
            //    position = Stuff.Lerp(position, Camera2d.GetPositionOnScreen(talker.position), 0.02f);

            BumpInsideScreen();

        }
        protected void BumpInsideScreen()
        {
            if (position.X > Stuff.Resolution.X)
            {
                position.X = Stuff.Resolution.X - 1;
                direction.X = -direction.X;
            }
            if (position.Y > Stuff.Resolution.Y)
            {
                position.Y = Stuff.Resolution.Y - 1;
                direction.Y = -direction.Y;
            }
            if (position.X < 0)
            {
                position.X = 0;
                direction.X = -direction.X;
            }
            if (position.Y < 0)
            {
                position.Y = 0;
                direction.Y = -direction.Y;
            }
        }
        public void Draw(SpriteBatch spriteBatch, Vector2 offset)
        {
            if (staticMessage == false)
                offset = Vector2.Zero;
            color.A = (byte)((255F * time) / maxTime);
            //spriteBatch.DrawString(font, message, position + new Vector2(2, 2), new Color(0, 0, 0, color.A), 0, size / 2f, 1, SpriteEffects.None, 0.5f);
            spriteBatch.DrawString(font, message, position, color, 0, size / 2f, 1, SpriteEffects.None, 0.4f);
            if (staticMessage) time = 0;
        }
    }
    public class OnScreenMessages
    {
        static List<FloatingMessage> messages;
        static SpriteFont font;

        public OnScreenMessages()
        {
            messages = new List<FloatingMessage>();
            font = Stuff.Content.Load<SpriteFont>("Font");
        }

        public void Update(float elaps)
        {
            int c = messages.Count;
            for (int i = 0; i < c; i++)
            {
                messages[i].Update(elaps, messages);
                if (messages[i].remove)
                {
                    messages.RemoveAt(i);
                    i--;
                    c--;
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            int c = messages.Count;
            Vector2 offset = Vector2.Zero;
            for (int i = 0; i < c; i++)
            {
                messages[i].Draw(spriteBatch, offset);
                if (messages[i].staticMessage)
                    offset.Y += messages[i].size.Y;
            }
        }

        public static void AddFloatingMessage(Vector2 position, Color color, string message)
        {
            messages.Add(new FloatingMessage(font, position, message, 1.5f, color, new Vector2(Stuff.RandomInt(-1000, 1001), Stuff.RandomInt(-1000, 1001)), false));
        }
        public static void AddStaticMessage(Vector2 position, Vector2 origin, Color color, string message)
        {
            FloatingMessage f = new FloatingMessage(font, position, message, 1, color, Vector2.Zero, true);
            f.size = origin * 2;
            messages.Add(f);

        }
        public static void AddStaticMessage(Vector2 position, Color color, string message)
        {
            messages.Add(new FloatingMessage(font, position, message, 1, color, Vector2.Zero, true));

        }

    }
}
