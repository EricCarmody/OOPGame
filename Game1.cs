//Shark Park by Eric Carmody
//Bitches, Eat Meat Too.
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
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        HeartClass hc;

        SharkClass sc = new SharkClass();
        Texture2D lolText;
        Texture2D backGround;

        MenuClass menu;

        BoatClass bc;

        ItemSprite itemSprite1;

        bool expIsNow = false;
        bool gameOver = false;
        bool exiter = false;
        string recName;
        bool joke;
        string textString;
        KeyboardState oldKeyboardState, currentKeyboardState;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.IsFullScreen = true;
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            hc = new HeartClass();
            
            graphics.PreferredBackBufferHeight = 800;
            graphics.PreferredBackBufferWidth = 1280;
            graphics.ApplyChanges();
            sc = new SharkClass();
            bc = new BoatClass();
            menu = new MenuClass();
            
            itemSprite1 = new ItemSprite();
    

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            itemSprite1.LoadContent(this.Content);
            bc.LoadContentBoat(this.Content);
            hc.LoadContent(this.Content);
            sc.LoadContent(this.Content);
            
            lolText = Content.Load<Texture2D>("Old\\newlos");
            backGround = Content.Load<Texture2D>("Final\\FX_GameplayBackground");
            menu.LoadContent(this.Content);
            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            exiter = menu.UpdateScreen(gameTime, gameOver);
            if (exiter == true)
            {
                this.Exit();
            }
            if (menu.currentScreen == heart.MenuClass.Screen.Game)
            {
                if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                    this.Exit();
                bc.Update(gameTime);
                
                sc.Update(gameTime, expIsNow);
                recName = itemSprite1.Update(gameTime, bc, sc);
                gameOver = hc.Update(gameTime, recName);
            }

            if (menu.currentScreen == heart.MenuClass.Screen.Pause)
            {
                UpdateInput();
            }

            if (menu.currentScreen == heart.MenuClass.Screen.Info)
            {

            }

            if (menu.currentScreen == heart.MenuClass.Screen.GameOver)
            {
                LoadContent();
            }

            if (menu.currentScreen == heart.MenuClass.Screen.Score)
            {

            }

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.White);

            spriteBatch.Begin(SpriteBlendMode.AlphaBlend, SpriteSortMode.Immediate, SaveStateMode.None, Matrix.Identity); 
            graphics.GraphicsDevice.SamplerStates[0].MagFilter = TextureFilter.Point; 
            graphics.GraphicsDevice.SamplerStates[0].MinFilter = TextureFilter.Point; 
            graphics.GraphicsDevice.SamplerStates[0].MipFilter = TextureFilter.Point;

            if (menu.currentScreen == heart.MenuClass.Screen.Game)
            {
                spriteBatch.Draw(backGround, Vector2.Zero, new Rectangle(0, 0, 1280, 800), Color.White);
                if (joke == true)
                {
                    spriteBatch.Draw(lolText, new Rectangle(0, 0, graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight), Color.White);
                }
                hc.Draw(spriteBatch, gameTime); 
                sc.Draw(spriteBatch);
                expIsNow = itemSprite1.Draw(spriteBatch);
                bc.Draw(spriteBatch);
            }

            if (menu.currentScreen == heart.MenuClass.Screen.Pause)
            {
                UpdateInput();
                spriteBatch.Draw(backGround, Vector2.Zero, new Rectangle(0, 0, 1280, 800), Color.White);
                if (joke == true)
                {
                    spriteBatch.Draw(lolText, new Rectangle(0, 0, graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight), Color.White);
                }
                hc.Draw(spriteBatch, gameTime);
                sc.Draw(spriteBatch);
                expIsNow = itemSprite1.Draw(spriteBatch);
                bc.Draw(spriteBatch);
                menu.Draw(spriteBatch);
            }

            if (menu.currentScreen == heart.MenuClass.Screen.Title)
            {
                menu.Draw(spriteBatch);
            }

            if (menu.currentScreen == heart.MenuClass.Screen.Info)
            {
                menu.Draw(spriteBatch);
            }

            if (menu.currentScreen == heart.MenuClass.Screen.GameOver)
            {
                menu.Draw(spriteBatch);
            }

            if (menu.currentScreen == heart.MenuClass.Screen.Score)
            {
                menu.Draw(spriteBatch);
            }

            spriteBatch.End();

            base.Draw(gameTime);
        }

        private void UpdateInput()
        {
            oldKeyboardState = currentKeyboardState;
            currentKeyboardState = Keyboard.GetState();

            Keys[] pressedKeys;
            pressedKeys = currentKeyboardState.GetPressedKeys();

            foreach (Keys key in pressedKeys)
            {
                if (oldKeyboardState.IsKeyUp(key))
                {

                    if (key == Keys.Back)
                    {// overflows
                        textString = textString.Remove(textString.Length - 1, 1);
                    }

                    else if (key == Keys.Space)//Add Space
                    {
                        if (textString.Length != 0)
                        {
                            textString = textString.Insert(textString.Length, " ");
                        }
                    }
                    else if (key == Keys.OemComma)//Add Comma
                    {
                        if (textString.Length != 0)
                        {
                            textString = textString.Insert(textString.Length, ",");
                        }
                    }
                    else if (key == Keys.OemTilde) // This clears log
                    {
                        if (oldKeyboardState != currentKeyboardState)
                        {
                            if (textString.Length != 0)
                            {
                                textString = null;
                            }
                        }
                    }
                    else
                    {
                        textString += key.ToString();
                    }
                    if (textString == "NUTSACK")
                    {
                        joke = true;
                    }
                    if (textString == "NONUTS")
                    {
                        joke = false;
                    }
                }
            }
        }

    }
}
