using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EggRollGameLib;
using Microsoft.Xna.Framework;

namespace EggrollGameLib.ClassFiles
{
    class Player : Character
    {
        public Player(List<Character> characters)
            : base()
        {
            this.characters = characters;
            sprite = new Sprite("pixel");
            sprite.Scale = 10;
            sprite.iColor.SetColor(Color.Black); 
            

        }
        
        public override void LoadContent()
        {
            base.LoadContent();
        }
        
        public override void Update(float elaps)
        {
            base.Update(elaps);
        }
        
        public override void Draw(Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }

        private void Controls() 
        {
            
        }
    }
}
