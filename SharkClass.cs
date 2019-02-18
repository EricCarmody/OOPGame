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
    class SharkClass : Sprite
    {
        const int MOVE_DOWN = 1;
        const int MOVE_UP = -1;
        const int MOVE_RIGHT = 1;
        const int MOVE_LEFT = -1;

        KeyboardState kbs;
        const string name = "Final\\SP_Spritesheet";

        Vector2 startPosition = new Vector2(300, 400);

        Vector2 accX = new Vector2(200, 0);
        Vector2 accY = new Vector2(0, 200);

        #region(Ani Stuff)

        int current = 0;
        float interval = .33f;

        int lastHeart;

        float timer = 0;

        #endregion
    
        public void LoadContent(ContentManager theContentManager)
        {
            scale = 5;
            pus = startPosition;
            base.LoadContent(theContentManager, name);
            sourceRec = new Rectangle(440, 224, 14, 16);
            colRec = sourceRec;
        }

        public void Update(GameTime gameTime, bool expIsNow)
        {
            UpdateMovement(gameTime, expIsNow);
            //SharkCurser();
            BorderStopper();
            base.Update(gameTime);
        }

        public void BorderStopper()
        {
            if (pus.X < 0)
            {
                pus.X = 0;
                velocity = Vector2.Zero;
            }

            if (pus.X > 1280 - (scale*sourceRec.Width))
            {
                pus.X = 1280 - (scale * sourceRec.Width);
                velocity = Vector2.Zero;
            }

            if (pus.Y < 225)
            {
                pus.Y = 225;
                velocity = Vector2.Zero;
            }

            if (pus.Y > 800 - (sourceRec.Height*scale))
            {
                pus.Y = 800 - (sourceRec.Height*scale);
                velocity = Vector2.Zero;
            }
        }

        //public void SharkCurser()
        //{
        //    kbs = Keyboard.GetState();
        //    if(kbs.IsKeyDown(Keys.RightShift))
        //    {
        //        sourceRec = new Rectangle(210, 75, 135, 60);
        //    }
        //}

        public void UpdateMovement(GameTime gameTime, bool expIsNow)
        {
            kbs = Keyboard.GetState();
            if (expIsNow == true)
            {
                velocity = Vector2.Zero;
            }
            else
            {
                if (kbs.IsKeyDown(Keys.Right))
                {
                    velocity += MOVE_RIGHT * accX * (float)gameTime.ElapsedGameTime.TotalSeconds;
                    AnimateDown(gameTime);
                }

                if (kbs.IsKeyDown(Keys.Left))
                {
                    velocity += MOVE_LEFT * accX * (float)gameTime.ElapsedGameTime.TotalSeconds;
                    AnimateDown(gameTime);
                }

                if (kbs.IsKeyDown(Keys.Up))
                {
                    velocity += MOVE_UP * accY * (float)gameTime.ElapsedGameTime.TotalSeconds;
                    AnimateDown(gameTime);
                }

                if (kbs.IsKeyDown(Keys.Down))
                {
                    velocity += MOVE_DOWN * accY * (float)gameTime.ElapsedGameTime.TotalSeconds;
                    AnimateDown(gameTime);
                }
            }
        }

        public void AnimateDown(GameTime gt)
        {
            timer += (float)gt.ElapsedGameTime.TotalSeconds;
            if (timer > interval)
            {
                current++;
                if (current > 2)
                {
                    current = 0;
                }
                timer = 0f;
            }
            sourceRec = new Rectangle((current * sourceRec.Width) + 440 , sourceRec.Y, 16, 16);
        }
    }
}

