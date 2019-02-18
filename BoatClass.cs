using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework.Storage;

namespace heart
{
    class BoatClass : Sprite
    {
        Random ran = new Random();
        float timer = 0f;
        float set = 2.5f;
        float speed = 2.5f;
        bool directionSet = true;

        int current = 0;
        float interval = .25f;

        Vector2 startingPosition = new Vector2(0, 115);

        public void LoadContentBoat(ContentManager tcm)
        {
            pus = startingPosition;

            velocity = new Vector2(1, 0);

            base.LoadContent(tcm, "Final\\SP_Spritesheet");
            sourceRec = new Rectangle(193, 33, 64, 32);
            Scale = 4f;
        }

        public void Draw(SpriteBatch spriteBitch)
        {
            if (set > 0)
            {
                spriteBitch.Draw(sexture, pus, sourceRec, Color.White, 0, Vector2.Zero, scale, SpriteEffects.None, 0);
            }
            else
            {
                spriteBitch.Draw(sexture, pus, sourceRec, Color.White, 0, Vector2.Zero, scale, SpriteEffects.FlipHorizontally, 0);
            }
        }

        public void UpdateMovement(GameTime gt)
        {
            pus.X += set;
            AnimateBoat(gt);
            if(pus.X > (1280 - (sourceRec.Width)*Scale))
            {
                set = speed*-1;
            }

            if (pus.X < 0)
            {
                set = speed;
            } 
        }

        public void AnimateBoat(GameTime gt)
        {
            if (directionSet == true)
            {
                timer += (float)gt.ElapsedGameTime.TotalSeconds;
                if (timer > interval)
                {
                    current++;
                    if (current > 2)
                    {
                        directionSet = false;
                        current = 1;
                    }
                    timer = 0f;
                }
                sourceRec = new Rectangle((current * sourceRec.Width) + 193, sourceRec.Y, 64, 32);
            }
            else
            {
                timer += (float)gt.ElapsedGameTime.TotalSeconds;
                if (timer > interval)
                {
                    current--;
                    if (current < 1)
                    {
                        directionSet = true;
                        current = 0;
                    }
                    timer = 0f;
                }
                sourceRec = new Rectangle((current * sourceRec.Width) + 193, sourceRec.Y, 64, 32);
            }
        }

        public void Update(GameTime gt)
        {
            UpdateMovement(gt);
            base.Update(gt);
        }

        

    }
}
