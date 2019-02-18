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
    class ParticleEngine
    {
        private Random ran;
        public Vector2 EmitterLocation { get; set; }
        private List<Particle> particles;
        private Texture2D textures;

        public ParticleEngine(Texture2D sexture)
        {
            textures = sexture;
            this.particles = new List<Particle>();
            ran = new Random();
        }

        private Particle ParticleGenerator()
        {
            Texture2D texture = textures;
            Vector2 position = new Vector2(EmitterLocation.X, EmitterLocation.Y - 20);
            Vector2 velocity = Vector2.Zero;
            float angle = 0;
            float angularVelocity = 0f;
            Color color = Color.White;
            int ttl = 5;

            return new Particle(texture, velocity, position, angle, angularVelocity, 1, ttl, color);
        }

        public void Draw(SpriteBatch theSpriteBatch)
        {
            for (int index = 0; index < particles.Count; index++)
            {
                particles[index].Draw(theSpriteBatch);
            }
        }

        public void Update()
        {
            int total = 1;
            for (int x = 0; x < total; x++)
            {
                particles.Add(ParticleGenerator());
            }

            for (int part = 0; part < particles.Count; part++)
            {
                particles[part].Update();
                if (particles[part].TTL <= 0)
                {
                    particles.RemoveAt(part);
                    part--;
                }
            }
        }
    }
}

