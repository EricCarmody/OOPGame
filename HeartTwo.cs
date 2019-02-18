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
    class HeartTwo
    {
        Texture2D healthBar;
        int currentHealth = 6;

        KeyboardState oldState;

        public void LoadContent(ContentManager tcm)
        {
            //Load the HealthBar image from the disk into the Texture2D object
            healthBar = tcm.Load<Texture2D>("heartBar");
        }

        public void Update(GameTime gameTime)
        {
            // Allows the game to exit
            
            //Get the current keyboard state (which keys are currently pressed and released)
            KeyboardState keys = Keyboard.GetState();

            //If the Up Arrow is pressed, increase the Health bar
            if (keys.IsKeyDown(Keys.Up) == true)
            {


                if (oldState.IsKeyUp(Keys.Up))
                {
                    currentHealth += 1;
                    oldState = keys;
                }
            }

            //If the Down Arrowis pressed, decrease the Health bar
            if (keys.IsKeyDown(Keys.Down) == true)
            {
                currentHealth -= 1 ;
                oldState = keys;
            }

            //Force the health to remain between 0 and 100
            currentHealth = (int)MathHelper.Clamp(currentHealth, 0, 100); ;

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(healthBar, new Rectangle(515, 20, healthBar.Width, 55), new Rectangle(0, 45, healthBar.Width, 55), Color.Gray);

            spriteBatch.Draw(healthBar, new Rectangle(515, 20, (int)(healthBar.Width * ((double)6 * currentHealth/ 100)), 55),
                new Rectangle(0, 45, healthBar.Width, 50), new Color(255,0,0));

            spriteBatch.Draw(healthBar, new Rectangle(515, 20, healthBar.Width, 55), new Rectangle(0, 0, healthBar.Width, 55), Color.White);

        }
    }
}
