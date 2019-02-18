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
    class ItemSprite : Sprite
    {
        List<Vector2> blockPositions = new List<Vector2>();
        List<Rectangle> theTestRec = new List<Rectangle>();
        Rectangle blockRec = new Rectangle();

        ParticleEngine pe;

        float xSpawn, ySpawn;
        float blockSpawnRate = .1f;
        string recName = null;
        int BLOCK_FALL_SPEED;
        float timer, timer2;

        int current = 0;
        float interval = .5f;
        bool expIsNow = false;
        Vector2 expPos = new Vector2();
        int bombNum;
        Rectangle expRec = new Rectangle();

        bool itemHit = false;

        Random ran = new Random();

        
       
        public void LoadContent(ContentManager cm)
        {
            BLOCK_FALL_SPEED = ran.Next(2, 5);
            sexture = cm.Load<Texture2D>("Final\\SP_Spritesheet");
            pe = new ParticleEngine(sexture);
            Scale = 4;
        }

        public string Update(GameTime gameTime, BoatClass bc, SharkClass ss)
        {
            pe.EmitterLocation = ss.pus;

            if (gameTime.ElapsedGameTime.TotalSeconds % 15 == 0)
            {
                BLOCK_FALL_SPEED = ran.Next(2, 5);
            }
            recName = null;
            BlockSpawner(bc, gameTime);
            BlockMover(ss, bc, gameTime);

            if (itemHit == true)
            {
                ScoreShower(gameTime);
                pe.Update();
            }

            if (expIsNow == true)
            {
                AnimateBomb(gameTime);
            }

            return recName;
        }

        public bool Draw(SpriteBatch theSpriteBitch)
        {
            if(expIsNow == true)
            {
                theSpriteBitch.Draw(sexture, expPos, expRec, Color.White, 0, Vector2.Zero, 6, SpriteEffects.None, 0);
            }

            if (itemHit == true)
            {
                pe.Draw(theSpriteBitch);
            }

            for (int x = 0; x < blockPositions.Count; x++)
            {
                theSpriteBitch.Draw(sexture, blockPositions[x], theTestRec[x], Color.White, 0, Vector2.Zero, scale, SpriteEffects.None, 1);
            }
            return expIsNow;
        }

        public void BlockMover(SharkClass ss, BoatClass bc, GameTime gt)
        {
            for (int x = 0; x < blockPositions.Count; x++)
            {
                blockPositions[x] = new Vector2(blockPositions[x].X, blockPositions[x].Y + BLOCK_FALL_SPEED);
                blockRec = new Rectangle((int)blockPositions[x].X, (int)blockPositions[x].Y, theTestRec[x].Width, theTestRec[x].Height);

                if (blockPositions[x].Y > 800 + theTestRec[x].Height)
                {
                    blockPositions.RemoveAt(x);
                    theTestRec.RemoveAt(x);
                    x--;    //sad day for the Item
                    break;
                }

                if (blockRec.Intersects(new Rectangle((int)ss.pus.X, (int)ss.pus.Y, ss.sourceRec.Width * (int)ss.scale, ss.sourceRec.Height * (int)ss.scale)))
                {
                    switch (theTestRec[x].X)
                    {
                        case 429:
                            {
                                recName = "bomb";
                                expPos = new Vector2(ss.pus.X - ss.sourceRec.Width*ss.scale, ss.pus.Y - ss.sourceRec.Height);
                                expIsNow = true;
                            }
                            break;
                        case 126:
                            {
                                recName = "coke";
                                itemHit = true;
                            }
                            break;
                        case 140:
                            {
                                recName = "spag";
                                itemHit = true;
                            }
                            break;
                        case 154:
                            {
                                recName = "cookie";
                                itemHit = true;
                            }
                            break;
                        default:
                            recName = null;
                            break;
                    }
                    theTestRec.RemoveAt(x);
                    blockPositions.RemoveAt(x);
                    x--;
                }
            
            }
        }

        public void ScoreShower(GameTime gt)
        {
            timer2 += (float)gt.ElapsedGameTime.TotalSeconds;
            if (timer2 > interval)
            {
                current++;
                if (current > 2)
                {
                    current = 0;
                    itemHit = false;
                }
                timer2 = 0f;
            }
        }

        public void AnimateBomb(GameTime gt)
        {
            timer += (float)gt.ElapsedGameTime.TotalSeconds;
            if (timer > interval)
            {
                current++;
                if (current > 1)
                {
                    current = 0;
                    expIsNow = false;
                }
                timer = 0f;
            }

            expRec = new Rectangle((current * expRec.Width) + 382, 416, 39, 21);
        }

        public void IntersectPixels(Rectangle rectangleA, Color[] dataA, Rectangle rectangleB, Color[] dataB)
        {
            // Find the bounds of the rectangle intersection
            int top = Math.Max(rectangleA.Top, rectangleB.Top);
            int bottom = Math.Min(rectangleA.Bottom, rectangleB.Bottom);
            int left = Math.Max(rectangleA.Left, rectangleB.Left);
            int right = Math.Min(rectangleA.Right, rectangleB.Right);

            // Check every point within the intersection bounds
            for (int y = top; y < bottom; y++)
            {
                for (int x = left; x < right; x++)
                {
                    // Get the color of both pixels at this point
                    Color colorA = dataA[(x - rectangleA.Left) +
                                         (y - rectangleA.Top) * rectangleA.Width];
                    Color colorB = dataB[(x - rectangleB.Left) +
                                         (y - rectangleB.Top) * rectangleB.Width];

                    // If both pixels are not completely transparent,
                    if (colorA.A != 0 && colorB.A != 0)
                    {
                        
                    }
                }
            }
        }

        //public bool CollideTest(Rectangle location, Rectangle newRec)
        //{
        //    float right1, right2, left1, left2, top1, top2, bot1, bot2;


        //    left1 = newRec.X;
        //    top1 = newRec.Y;
        //    right1 = newRec.X + newRec.Width;
        //    bot1 = newRec.Y + newRec.Height;

        //    Rectangle pizzRec = new Rectangle(int.Parse(left1.ToString()), int.Parse(top1.ToString()), int.Parse(right1.ToString()), int.Parse(bot1.ToString()));

        //    left2 = location.X; ;
        //    top2 = location.Y;
        //    right2 = location.X + location.Width;
        //    bot2 = location.Y + location.Height;

        //    Rectangle chestRec = new Rectangle(int.Parse(left2.ToString()), int.Parse(top2.ToString()), int.Parse(right2.ToString()), int.Parse(bot2.ToString()));



        //    if (((right1 > right2 && left1 < right2) || (right1 > left2 && left1 < left2)) && ((top1 < top2 && top2 < bot1) || (top2 < top1 && top1 < bot2)))
        //    {
        //        return true;
        //    }

        //    return false;
        //}

        public void BlockSpawner(BoatClass bc, GameTime gt)
        {
            timer += (float)gt.ElapsedGameTime.TotalSeconds;

            if (ran.NextDouble() < blockSpawnRate && timer > 1.5)
            {
                timer = 0;
                if (ran.Next() % 7 == 0)
                {
                    xSpawn = bc.pus.X;
                    ySpawn = bc.pus.Y;

                    blockPositions.Add(new Vector2(xSpawn + (sourceRec.Width / 2 * bc.scale), ySpawn + (sourceRec.Height / 2 * bc.scale)));
                    theTestRec.Add(new Rectangle(429, 302, 18, 18));
                }
                else if (ran.Next() % 5 == 0)
                {
                    xSpawn = bc.pus.X;
                    ySpawn = bc.pus.Y;

                    blockPositions.Add(new Vector2(xSpawn + (sourceRec.Width / 2 * bc.scale), ySpawn + (sourceRec.Height / 2 * bc.scale)));
                    theTestRec.Add(new Rectangle(126, 294, 14, 16));
                }
                else if (ran.Next() % 2 == 1)
                {
                    xSpawn = bc.pus.X;
                    ySpawn = bc.pus.Y;

                    blockPositions.Add(new Vector2(xSpawn + (sourceRec.Width / 2 * bc.scale), ySpawn + (sourceRec.Height / 2 * bc.scale)));
                    theTestRec.Add(new Rectangle(140, 294, 14, 16));
                }
                else if (ran.Next() % 2 == 0)
                {
                    xSpawn = bc.pus.X;
                    ySpawn = bc.pus.Y;

                    blockPositions.Add(new Vector2(xSpawn + (sourceRec.Width / 2 * bc.scale), ySpawn + (sourceRec.Height / 2 * bc.scale)));
                    theTestRec.Add(new Rectangle(154, 294, 14, 16));
                }
            }
        }
    }
}
