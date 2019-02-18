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

public abstract class Sprite
{
	    // Our Image
        public Texture2D sexture;

        // Our part of the image being displayed
        public Rectangle sourceRec;

        // The size of image if redone with a rectangle
        protected Rectangle sizeRec;

        // The Rectangle that will detect collsion.
        public Rectangle colRec;

        // Position, where the image and rectangle are.
        public Vector2 pus;

        // Velocity, our speed basically.
        public Vector2 velocity;

        // This is used to allow the game1.cs to enter a name in to choose a texture.
        string assetName;
        
        //These here are not set yet.
        public int setWidth = 1080;
        public int setHeight = 800;

        //Our scale, if greater it increases image size, if smaller, decreases image size.
        public float scale = 1f;

        //Our scale, if greater it increases image size, if smaller, decreases image size.
        float rot = 0f;

        
        public void LoadContent(ContentManager theContentManager, string theASSetName)  // This is how we load stuff up.
        {
            // This loads the image from the Content Pipeline to our Texture2D object.
            sexture = theContentManager.Load<Texture2D>(theASSetName);

            // We obtain the asset name for later use.
            assetName = theASSetName;

            //Gets the full spectrum of the source rectangle.
            sourceRec = new Rectangle(0, 0, sexture.Width, sexture.Height);

            //Gets the full spectrum of the source rectangle, with our scale variable being applied to adjust size.
            sizeRec = new Rectangle(0, 0, sexture.Width * (int)scale, sexture.Height * (int)scale);
        }

        public void Update(GameTime gameTime)  // This is where change will occur.
        {
            pus += velocity * (float)gameTime.ElapsedGameTime.TotalSeconds;
            // Changes the rectangle region everytime the position changes.
            colRec = new Rectangle((int)pus.X, (int)pus.Y, sexture.Width, sexture.Height);
        }

        public void Draw(SpriteBatch spriteBitch)  //This is our drawing to the screen.
        {
            // Just a use of a SpriteBatch.Draw method.  There are many different  parameters for each method call.
            spriteBitch.Draw(sexture, pus, sourceRec, Color.White, rot, Vector2.Zero, scale, SpriteEffects.None, 0);
            
        }
        
        // These are our properties
        // We are using properties when the change of one object affects another.
        public Rectangle SourceRec
        {
            get
            {
                return sourceRec;
            }
            set
            {   
                // This is changed
                sourceRec = value;
                // This is affected.
                sizeRec = new Rectangle(0, 0, sourceRec.Width, sourceRec.Height);
            }
        }

        public float Scale
        {
            get
            {
                return scale;
            }
            set
            {
                // This is changed
                scale = value;
                // This is affected.
                sizeRec = new Rectangle(0, 0, sexture.Width * (int)scale, sexture.Height * (int)scale);
            }
        }
	
}
