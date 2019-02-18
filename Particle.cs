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
    class Particle
    {
        public Texture2D Texture { set; get; }
        public Vector2 Velocity { set; get; }
        public Vector2 Position { set; get; }
        public float Angle { set; get; }
        public float AngularVelocity { set; get; }
        public float Size { set; get; }
        public int TTL { set; get; }
        public Color TheColor { set; get; }
        Rectangle sourceRec;
        


        public Particle(Texture2D text, Vector2 vel, Vector2 pos, float ang, float angVel, float size, int time, Color col)
        {
            Texture = text;
            Velocity = vel;
            Position = pos;
            Angle = ang;
            AngularVelocity = angVel;
            Size = size;
            TTL = time;
            TheColor = col;
        }

        public void Update()
        {
            TTL--;
            Position += Velocity;
            Angle += AngularVelocity;
        }

        public void Draw(SpriteBatch theSpriteBatch)
        {
            sourceRec = new Rectangle(105, 511, 109, 55);
            theSpriteBatch.Draw(Texture, Position, sourceRec, TheColor, Angle, Vector2.Zero, Size, SpriteEffects.None, 0f);
        }
    }
}
