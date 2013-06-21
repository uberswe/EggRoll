using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Egg_roll
{
    class Particle
    {
        public Sprite sprite;
        public Vector2 position;
        public Vector2 direction;
        float speed, rotDir;

        public Particle()
        {
        }


        public void Update(float elaps)
        {
            position += elaps * direction * speed;
            sprite.Rotation += rotDir; 
            //turn direction down.(imitate gravity) 
            //direction = Stuff.Lerp(direction, new Vector2(0, 1), 0.05f);
            if (speed < 100)
                speed = Stuff.Lerp(speed, 200, 0.02f);
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            sprite.DrawAt(spriteBatch, position);
        }
        public void Activate(Vector2 position)
        {
            rotDir = Stuff.RandomInt(-100, 101);
            rotDir /= 500f; 
            this.position = position;
            direction = Stuff.RandomDirection();
            //if (direction.Y > 0)
            //    direction.Y = -direction.Y;
            speed = Stuff.RandomInt(0, 800);

        }
    }
    class ParticleHandler
    {
        List<Particle> particles;
        float time;
        bool active;

        public ParticleHandler()
        {
            particles = new List<Particle>();
            //add egg yolk particles
            for (int x = 0; x < 5; x++)
            {
                for (int i = 0; i < 5; i++)
                {
                    for (int j = 2; j < 4; j++)
                    {
                        Particle p = new Particle();
                        p.sprite = new Sprite("deadEgg");
                        p.sprite.Source = new Rectangle(135 * i, 170 * j, 135, 170);
                        if (j % 2 == 0)
                            p.sprite.iColor.SetColor(255, 255, 0, 150);
                        else
                            p.sprite.iColor.SetColor(255, 255, 255, 100);
                        p.sprite.Scale = 2f; 
                        particles.Add(p);
                    }
                }
            }
            //add shell pieces
            for (int i = 0; i < 5; i++)
            {
                Particle p = new Particle();
                p.sprite = new Sprite("deadEgg");
                p.sprite.Source = new Rectangle(270 * i, 0, 270, 340);
                p.sprite.Scale = 0.5f;
                particles.Add(p);
            }
        }
        public void Update(float elaps)
        {
            if (active)
            {
                time += elaps;
                if (time > 3)
                    active = false;
            }

            int c = particles.Count;
            for (int i = 0; i < c; i++)
            {
                particles[i].Update(elaps);
            }
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            if (active)
            {
                int c = particles.Count;
                for (int i = 0; i < c; i++)
                {
                    particles[i].Draw(spriteBatch);
                }
            }
        }

        public void Eggsplode(Vector2 position, Vector2 direction)
        {
            if (active == false)
            {
                active = true;
                time = 0;
                foreach (Particle p in particles)
                {
                    p.Activate(position);
                    p.direction = Vector2.Normalize((-direction / 4f) + p.direction);
                }
            }
        }
    }
}
