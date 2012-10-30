using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace EggRollGameLib
{
    public class Animation
    {
        string animationName;
        private List<Rectangle> frames;
        int width, height, selectedFrame, framesAmount;

        public int FramesAmount
        {
            get { return framesAmount; }
            set { framesAmount = value; }
        }

        public string AnimationName
        {
            get { return animationName; }
            set { animationName = value; }
        }

        /// <param name="width">width of each sprite on sheet.</param>
        /// <param name="height">height of each sprite on sheet.</param>
        public Animation(int width, int height)
            : this()
        {
            this.width = width;
            this.height = height;
        }
        /// <summary>
        /// sets up an animation with 150x150 sprites.
        /// </summary>
        public Animation()
        {
            this.width = 150;
            this.height = 150;
            frames = new List<Rectangle>();
        }

        public int SelectedFrame
        {
            get { return selectedFrame; }
            set { selectedFrame = value; }
        }
        public void AddFrame(int x, int y)
        {
            frames.Add(new Rectangle(x * width, y * height, width, height));
            framesAmount++;
        }

        /// <summary>
        /// return true when switching back to first frame. 
        /// </summary>
        public bool NextFrame()
        {
            selectedFrame = (selectedFrame + 1) % frames.Count;
            if (selectedFrame == FramesAmount - 1)
                return true;
            return false;
        }
        public Rectangle CurrentFrame()
        {
            return frames[selectedFrame];
        }

        public void ResetAnimation()
        {
            selectedFrame = 0;
        }
    }

    public class AnimatedSprite : Sprite
    {
        //list of animations.
        List<Animation> animations;
        float exposure, exposureCount;
        int currentAnimation, defaultAnimation;
        public static bool ChangeFrames = true;
        public bool paused, playOnce;
        static float elaps;

        public int DefaultAnimation
        {
            get { return defaultAnimation; }
            set { defaultAnimation = value; }
        }
        public float Exposure
        {
            get { return exposure; }
            set { exposure = value; }
        }
        public List<Animation> Animations
        {
            get { return animations; }
            set { animations = value; }
        }

        public AnimatedSprite(string assetName)
        {
            AssetName = assetName;
            LoadContent(Stuff.Content);
            animations = new List<Animation>();
            exposure = 0.07f;
            exposure = 0.10f;
            exposureCount = 0;
            defaultAnimation = 0;
        }

        /// <summary>
        /// Loads and plays an animation.
        /// also sets it to default to loop. 
        /// </summary>
        /// <param name="animationName">Name of the animation, set by the user</param>
        public void LoadAnimation(string animationName)
        {
            int c = animations.Count;
            for (int i = 0; i < c; i++)
            {
                if (animations[i].AnimationName == animationName)
                {
                    //reset current animation
                    animations[currentAnimation].SelectedFrame = 0;
                    currentAnimation = i;//change animation
                    defaultAnimation = currentAnimation; //change default 
                    break;
                }
            }
        }

        /// <summary>
        /// Plays an animation once, then goes back to default
        /// </summary>
        /// <param name="animationName">Name of the animation, set by the user.</param>
        public void PlayAnimation(string animationName)
        {
            paused = false;
            if (animations[currentAnimation].AnimationName != animationName)
            {
                int c = animations.Count;
                for (int i = 0; i < c; i++)
                {
                    if (animations[i].AnimationName == animationName)
                    {
                        ////reset current animation
                        exposureCount = 0;
                        currentAnimation = i;//change animation
                        break;
                    }
                }
            }
        }

        /// <summary>
        /// stops animation and returns to first sprite
        /// </summary>
        public void StopAnimating()
        {
            paused = true;
            //animations[currentAnimation].ResetAnimation();
        }

        public static void Update(float elapsedTime)
        {
            elaps = elapsedTime;
        }

        public override void DrawAt(SpriteBatch spriteBatch, Vector2 position)
        {
            Source = animations[currentAnimation].CurrentFrame();
            base.DrawAt(spriteBatch, position);

            if (paused == false)
            {
                exposureCount += elaps;
                if (exposureCount >= exposure)
                {
                    exposureCount = 0;

                    if (animations[currentAnimation].NextFrame())
                    {
                        if (playOnce)
                            StopAnimating();
                    }
                    if (paused == false && ChangeFrames && currentAnimation != defaultAnimation && animations[currentAnimation].SelectedFrame == 0)
                    {
                        currentAnimation = defaultAnimation;
                        //LoadAnimation(animations[defaultAnimation].AnimationName); // reason for doing load? dunno
                    }
                }
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            DrawAt(spriteBatch, Position);
        }
    }
}
