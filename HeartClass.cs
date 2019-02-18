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
    class HeartClass : Sprite
    {
        List<Vector2> heartPositions = new List<Vector2>();
        bool removeHeart = false;
        bool addHeart = false;

        SpriteFont sf;
        Vector2 scorePos = new Vector2(1000, 25);
        Vector2 scorePos2 = new Vector2(1112, 25);
        string score;
        int scoreCounter = 0;
        int scoreNum = 0;
        bool scoreShake = false;

        bool switcher = true;
        Random ran = new Random();
        float shakeX = 16f;


        bool gameOver;

        float rotation = 0f;

        string recName;

        int current = 0;
        int lastHeart;

        float interval = .18f;
        float timer = 0;
        float life = 5;
        float maxLife = 5;

        KeyboardState kbs;

        Rectangle heartRec = new Rectangle(127, 359, 13, 13);

        public void LoadContent(ContentManager tcm)
        {
            gameOver = false;
            sf = tcm.Load<SpriteFont>("SpriteFont1");
                for (int x = 0; x < maxLife; x++)
                {
                    heartPositions.Add(new Vector2((x * heartRec.Width * 2), 25));
                }
                scale = 4;
                base.LoadContent(tcm, "Final\\SP_Spritesheet");
        }

        public bool Update(GameTime gameTime, string recName)
        {
            if (life <= 0)
            {
                gameOver = true;
                scoreNum = 0;
            }
            
            this.recName = recName;
            ScoreUpdater(gameTime);
            ScoreRunner(gameTime);
            HeartUpdater();
            return gameOver;
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            timer += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (removeHeart == true)
            {
                lastHeart = (int)life - 1;
                   
                if (timer > interval)
                {
                    current++;
                    timer = 0f;
                }

                for (int x = 0; x < life - 1; x++)
                {
                    spriteBatch.Draw(sexture, heartPositions[x], heartRec, Color.White, 0, Vector2.Zero, scale, SpriteEffects.None, 0);
                }
                spriteBatch.Draw(sexture, heartPositions[lastHeart], new Rectangle((current * heartRec.Width + heartRec.X), heartRec.Y + heartRec.Height, 13, 13), Color.White, 0, Vector2.Zero, scale, SpriteEffects.None, 0);
                if (current == 3)
                {
                    current = 0;
                    heartPositions.RemoveAt((int)life - 1);
                    life--;
                    removeHeart = false;
                }
            }
            else if (addHeart == true)
            {
                lastHeart = (int)life - 1;

                if (timer > interval)
                {
                    current++;
                    timer = 0f;
                }
                for (int x = 0; x < life - 1; x++)
                {
                    spriteBatch.Draw(sexture, heartPositions[x], heartRec, Color.White, 0, Vector2.Zero, scale, SpriteEffects.None, 0);
                }
                spriteBatch.Draw(sexture, heartPositions[lastHeart], new Rectangle((current * heartRec.Width + heartRec.X), heartRec.Y, 13, 13), Color.White, 0, Vector2.Zero, scale, SpriteEffects.None, 0);
                if (current == 3)
                {
                    current = 0;
                    addHeart = false;
                }
            }
            else
            {
                foreach (Vector2 heartPosition in heartPositions)
                {
                    spriteBatch.Draw(sexture, heartPosition, heartRec, Color.White, 0, Vector2.Zero, scale, SpriteEffects.None, 0);
                }
            }
            //spriteBatch.DrawString(sf, "Score:", scorePos, Color.Yellow, rotation, Vector2.Zero, 1, SpriteEffects.None, 0);
            spriteBatch.DrawString(sf, score, scorePos2, Color.Yellow, rotation, Vector2.Zero, 1, SpriteEffects.None, 0);
        }

        public void ScoreRunner(GameTime gt)
        {
            if (scoreCounter > scoreNum)
            {
                scoreNum++;
                scoreShake = true;
            }

            if (scoreCounter == scoreNum && scoreShake == true)
            {
                ScoreShaker(gt);
                
            }
            score = scoreNum.ToString();
        }

        public void ScoreShaker(GameTime gt)
        {
            timer += (float)gt.ElapsedGameTime.TotalSeconds;
            if (switcher == true)
            {
                    scorePos2.X -= shakeX;
                    timer = 0f;
                    switcher = false;
            }
            else
            { 
                    scorePos2.X += shakeX;
                    timer = 0f;
                    switcher = true;
                    shakeX--;
            }
            if (shakeX <= 0)
            {
                scoreShake = false;
                shakeX = 16f;
            }
        }
        public void ScoreUpdater(GameTime gt)
        {
            if (recName == "coke")
            {
                scoreCounter += 100;
            }

            if (recName == "bomb")
            {
            }

            if (recName == "spag")
            {
                scoreCounter += 50;
            }

            if (recName == "cookie")
            {
                scoreCounter += 50;
            }
            
        }
        public void HeartUpdater()
        {
            kbs = Keyboard.GetState();
            
            if (recName == "coke" && current == 0)
            {
                if (life < maxLife)
                {
                    addHeart = true;
                    heartPositions.Add(new Vector2(life * (heartRec.Width * 2), 25));
                    life++;
                }

                if (life > maxLife)
                {
                    life = maxLife;
                    addHeart = false;
                }
            }

            if (recName == "bomb" && current == 0)
            {
                if (life > 0)
                {
                    removeHeart = true;
                }
                if (life <= 0)
                {
                    life = 0;
                    removeHeart = false;
                }
            }
        }
    }
}
