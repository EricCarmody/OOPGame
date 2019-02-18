using System;
using System.Collections.Generic;
using System.Linq;
using System.Timers;
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
    class MenuClass
    {
        public enum Screen
        {
            Title,
            Game,
            Pause,
            Info,
            Score,
            GameOver
        }

        enum SelectorLocation
        {
            Top, Middle, Bottom
        }

        SelectorLocation sl = SelectorLocation.Top;

        Vector2 sharkSelector;
        Vector2 velocity = new Vector2();

        Vector2 top = new Vector2(250, 300);
        Vector2 mid = new Vector2(250, 350);
        Vector2 bot = new Vector2(250, 400);

        SpriteFont sf, sf2;

        Rectangle sourceRec = new Rectangle(441, 179, 14, 14);

        public Screen currentScreen = Screen.Title;
        Screen previousScreen;

        Texture2D[] infos = new Texture2D[3];
        
        int endPage = 0;
        Texture2D title, blueBox, texture, logo, loldong, highScore, gameOver;

        KeyboardState previousKeyboardState;

        int page = 0;
        bool gate = true;


        float timer, timer2, timer3, timer4;
        float interval = .33f;
        int current = 0;

        bool exiter = false;

        public void LoadContent(ContentManager tcm)
        {
            gameOver = tcm.Load<Texture2D>("Final\\lol-dong");
            highScore = tcm.Load<Texture2D>("Final\\FX_MenuHighScoresTextBackground");
            infos[0] = tcm.Load<Texture2D>("Final\\FX_MenuInstructions1");
            infos[1] = tcm.Load<Texture2D>("Final\\FX_MenuInstructions2");
            infos[2] = tcm.Load<Texture2D>("Final\\FX_MenuInstructions3");
            logo = tcm.Load<Texture2D>("Final\\FX_GameLogo");
            sf = tcm.Load<SpriteFont>("SpriteFont1");
            sf2 = tcm.Load<SpriteFont>("SpriteFont2");
            title = tcm.Load<Texture2D>("Final\\FX_Background1");
            blueBox = tcm.Load<Texture2D>("Final\\FX_MenuStartGameTextBackground");
            texture = tcm.Load<Texture2D>("Final\\SP_Spritesheet");
            sharkSelector = top;
        }

        void AnimateSelector(GameTime gt)
        {
            timer += (float)gt.ElapsedGameTime.TotalSeconds;
            if (timer > interval)
            {
                current++;
                if (current > 2)
                {
                    current = 1;
                }
                timer = 0f;
            }
            sourceRec = new Rectangle((current * sourceRec.Width) + 441, sourceRec.Y, 14, 14);
        }
        
        public void UpdateTitleScreen(KeyboardState kbs)
        {
            if (sl == SelectorLocation.Top)
            {
                if (kbs.IsKeyDown(Keys.Enter))
                {
                    currentScreen = Screen.Game;
                }
                if (kbs.IsKeyDown(Keys.Down) && sharkSelector.Y == top.Y)
                {
                    velocity.Y = 5;
                }
                if (sharkSelector.Y >= mid.Y)
                {
                    velocity.Y = 0;
                    sl = SelectorLocation.Middle;
                }
            }
            else if (sl == SelectorLocation.Middle)
            {
                if (kbs.IsKeyDown(Keys.Enter))
                {
                    currentScreen = Screen.Info;
                }

                if (kbs.IsKeyDown(Keys.Down))
                {
                    velocity.Y = 5;
                }

                if (kbs.IsKeyDown(Keys.Up))
                {
                    velocity.Y = -5;
                }

                if (sharkSelector.Y >= bot.Y)
                {
                    velocity.Y = 0;
                    sl = SelectorLocation.Bottom;
                }
                if(sharkSelector.Y <= top.Y)
                {
                    velocity.Y = 0;
                    sl = SelectorLocation.Top;
                }
            }
            else if (sl == SelectorLocation.Bottom)
            {

                if (kbs.IsKeyDown(Keys.Enter))
                {
                    exiter = true;
                }

                if (kbs.IsKeyDown(Keys.Up))
                {
                    velocity.Y = -5;
                }

                if (sharkSelector.Y <= mid.Y)
                {
                    velocity.Y = 0;
                    sl = SelectorLocation.Middle;
                }
            }
        }

        public void UpdateGameOverScreen(KeyboardState kbs, GameTime gt)
        {
            timer3 += (float)gt.ElapsedGameTime.TotalSeconds;
            if (timer3 > 1)
            {
                currentScreen = Screen.Score;
                
            }
        }

        public void UpdateHighScoreScreen(KeyboardState kbs, GameTime gt)
        {
            timer4 += (float)gt.ElapsedGameTime.TotalSeconds;
            if (timer4 > 1)
            {
                if (kbs.IsKeyDown(Keys.Enter))
                {
                    currentScreen = Screen.Title;
                }
            }
        }

        public void UpdatePauseScreen(KeyboardState kbs)
        {
            if (sl == SelectorLocation.Top)
            {
                if (kbs.IsKeyDown(Keys.Enter))
                {
                    currentScreen = Screen.Game;
                }
                if (kbs.IsKeyDown(Keys.Down) && sharkSelector.Y == top.Y)
                {
                    velocity.Y = 5;
                }
                if (sharkSelector.Y >= mid.Y)
                {
                    velocity.Y = 0;
                    sl = SelectorLocation.Middle;
                }
            }
            else if (sl == SelectorLocation.Middle)
            {
                if (kbs.IsKeyDown(Keys.Enter))
                {
                    currentScreen = Screen.Title;
                }

                if (kbs.IsKeyDown(Keys.Down))
                {
                    velocity.Y = 5;
                }

                if (kbs.IsKeyDown(Keys.Up))
                {
                    velocity.Y = -5;
                }

                if (sharkSelector.Y >= bot.Y)
                {
                    velocity.Y = 0;
                    sl = SelectorLocation.Bottom;
                }
                if (sharkSelector.Y <= top.Y)
                {
                    velocity.Y = 0;
                    sl = SelectorLocation.Top;
                }
            }
            else if (sl == SelectorLocation.Bottom)
            {
                if (kbs.IsKeyDown(Keys.Enter))
                {
                    exiter = true;
                }

                if (kbs.IsKeyDown(Keys.Up))
                {
                    velocity.Y = -5;
                }

                if (sharkSelector.Y <= mid.Y)
                {
                    velocity.Y = 0;
                    sl = SelectorLocation.Middle;
                }
            }
        }

        public void UpdateGameScreen(KeyboardState kbs)
        {
            if (kbs.IsKeyDown(Keys.Escape))
            {
                currentScreen = Screen.Pause;
            }
        }

        public void UpdateInfoScreen(KeyboardState kbs, GameTime gt)
        {
            timer2 += (float)gt.ElapsedGameTime.TotalSeconds;
            if (kbs.IsKeyDown(Keys.Escape))
            {
                currentScreen = Screen.Title;
            }
            if (timer2 > 1)
            {
                if (kbs.IsKeyDown(Keys.Enter))
                {
                    timer2 = 0f;
                    page++;
                    if (page > 2)
                    {
                        currentScreen = Screen.Title;
                        page = 0;
                    }
                }
            }
        }

        public bool UpdateScreen(GameTime gt, bool gameOver)
        {
            KeyboardState kbs = Keyboard.GetState();
            AnimateSelector(gt);
            sharkSelector += velocity;
            if (gameOver == true && gate == true)
            {
                gate = false;
                currentScreen = Screen.GameOver;
            }

            switch (currentScreen)
            {
                case Screen.Title:
                    {
                        UpdateTitleScreen(kbs);
                    }
                    break;

                case Screen.Game:
                    {
                        UpdateGameScreen(kbs);
                    }
                    break;

                case Screen.Pause:
                    {
                        UpdatePauseScreen(kbs);
                    }
                    break;
                case Screen.Info:
                    {
                        UpdateInfoScreen(kbs, gt);
                    }
                    break;
                case Screen.GameOver:
                    {
                        UpdateGameOverScreen(kbs, gt);
                    }
                    break;
                case Screen.Score:
                    {
                        UpdateHighScoreScreen(kbs, gt);
                    }
                    break;

            }
            return exiter;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            switch (currentScreen)
            {
                case Screen.Title:
                    {
                        spriteBatch.Draw(title, new Rectangle(0, 0, 1280, 800), Color.White);
                        spriteBatch.Draw(blueBox, new Vector2(150, 265), Color.White);
                        spriteBatch.Draw(texture, sharkSelector, sourceRec, Color.White, 0, Vector2.Zero, 4, SpriteEffects.None, 0);
                        spriteBatch.Draw(logo, new Vector2(450,100), Color.White);
                        spriteBatch.DrawString(sf, "Play", new Vector2(top.X + 100, top.Y), Color.Yellow);
                        spriteBatch.DrawString(sf, "Info", new Vector2(mid.X + 100, mid.Y), Color.Yellow);
                        spriteBatch.DrawString(sf, "Exit", new Vector2(bot.X + 100, bot.Y), Color.Yellow);
                    }
                    break;

                case Screen.Game:
                    {
                    }
                    break;

                case Screen.Pause:
                    {
                        spriteBatch.Draw(blueBox, new Vector2(500, 250), Color.White);
                        spriteBatch.Draw(texture, new Vector2(sharkSelector.X + 293, sharkSelector.Y), sourceRec, Color.White, 0, Vector2.Zero, 4, SpriteEffects.None, 0);
                        spriteBatch.DrawString(sf, "Resume", new Vector2(top.X + 350, top.Y), Color.Yellow);
                        spriteBatch.DrawString(sf, "Main Menu", new Vector2(mid.X + 350, mid.Y), Color.Yellow);
                        spriteBatch.DrawString(sf, "Exit Game", new Vector2(bot.X + 350, bot.Y), Color.Yellow);
                    }
                    break;

                case Screen.Info:
                    {
                        spriteBatch.Draw(infos[page], new Rectangle(0, 0, 1280, 800), Color.White);
                    }
                    break;
                case Screen.GameOver:
                    {
                        spriteBatch.Draw(gameOver, new Rectangle(0, 0, 1280, 800), Color.White);
                        spriteBatch.DrawString(sf2, "YOU FUCKING SUCK!", new Vector2(200, 300), Color.Purple, 0, Vector2.Zero, 2, SpriteEffects.None, 0);
                    }
                    break;
                case Screen.Score:
                    {
                        spriteBatch.Draw(highScore, new Rectangle(0, 0, 1280, 800), Color.White);
                    }
                    break;
            }
        }

    }
}
