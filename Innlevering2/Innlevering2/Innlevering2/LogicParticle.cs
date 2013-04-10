using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace Innlevering2
{
    class LogicParticle
    {
        private List<Particle> _particles;
        //private Vector2 _position;
        float test;

        Vector2 gravityCentre;
        Vector2 gravityDirection;
        float gravity;
        float distanceBetweenParticlesSquared;

        public LogicParticle(ContentManager content)
        {
            _particles = new List<Particle>();

            gravity = .5f;
            gravityCentre = new Vector2(GlobalVariables.WINDOW_WIDTH / 2, GlobalVariables.WINDOW_HEIGHT / 2);

            test = 0f;

            _particles.Add(new Particle(content, gravityCentre));
            for (int i = 1; i < GlobalVariables.NUMBER_OF_PARTICLES; i++)
            {
                _particles.Add(new Particle(content, new Vector2((float)Math.Cos(test) * 50f + (GlobalVariables.WINDOW_WIDTH / 2),
                                                                (float)Math.Sin(test) * 50f + (GlobalVariables.WINDOW_HEIGHT / 2))));
                test += (float)Math.PI / 16;
                //GlobalVariables.PARTICLE_TEXTURE_SCALE += 0.005f;
            }
        }

        public void Update()
        {
            for (int i = 0; i < _particles.Count; i++)
            {
                if (_particles[i].Position.X - _particles[i].Radius < 0)
                {
                    _particles[i].Position = new Vector2(_particles[i].Radius, _particles[i].Position.Y);
                    _particles[i].Velocity = new Vector2(-_particles[i].Velocity.X * .9f, _particles[i].Velocity.Y * .9f);
                }
                if (_particles[i].Position.X + _particles[i].Radius > GlobalVariables.WINDOW_WIDTH)
                {
                    _particles[i].Position = new Vector2(GlobalVariables.WINDOW_WIDTH - _particles[i].Radius, _particles[i].Position.Y);
                    _particles[i].Velocity = new Vector2(-_particles[i].Velocity.X * .9f, _particles[i].Velocity.Y * .9f);
                }
                if (_particles[i].Position.Y - _particles[i].Radius < 0)
                {
                    _particles[i].Position = new Vector2(_particles[i].Position.X, _particles[i].Radius);
                    _particles[i].Velocity = new Vector2(_particles[i].Velocity.X * .9f, -_particles[i].Velocity.Y * .9f);
                }
                if (_particles[i].Position.Y + _particles[i].Radius > GlobalVariables.WINDOW_HEIGHT)
                {
                    _particles[i].Position = new Vector2(_particles[i].Position.X, GlobalVariables.WINDOW_HEIGHT - _particles[i].Radius);
                    _particles[i].Velocity = new Vector2(_particles[i].Velocity.X * .9f, -_particles[i].Velocity.Y * .9f);
                }

                //UNCOMMENT THIS FOR "NORMAL" GRAVITY!
                //gravityDirection = new Vector2(0, 1); 
                //COMMENT THIS FOR "NORMAL" GRAVITY!
                gravityDirection = (gravityCentre - _particles[i].Position);

                if (!gravityDirection.Equals(Vector2.Zero))
                    Vector2.Normalize(ref gravityDirection, out gravityDirection);

                //-= FOR "REVERSED" GRAVITY
                _particles[i].Velocity += (gravityDirection * gravity);

                _particles[i].Position += _particles[i].Velocity;

                for (int j = i + 1; j < _particles.Count; j++)
                {
                    distanceBetweenParticlesSquared = ((_particles[j].Position.X - _particles[i].Position.X) * (_particles[j].Position.X - _particles[i].Position.X)) +
                                                    ((_particles[j].Position.Y - _particles[i].Position.Y) * (_particles[j].Position.Y - _particles[i].Position.Y));

                    if (distanceBetweenParticlesSquared <= ((_particles[i].Radius + _particles[j].Radius) * (_particles[i].Radius + _particles[j].Radius)))
                        _particles[i].ResolveCollision(_particles[j]);
                }
            }
        }

        public void Draw()
        {
            for (int i = 0; i < _particles.Count; i++)
            {
                GlobalVariables.SPRITEBATCH.Draw(_particles[i].Texture, _particles[i].Position, null, Color.Beige, 0f, Vector2.Zero, _particles[i].Scale, SpriteEffects.None, 0f);
            }
        }

        public void Start()
        {
            test = 0f;
            _particles[0].Velocity = new Vector2(0f, 0f);
            for (int i = 1; i < _particles.Count; i++)
            {
                //SET INITIAL SPEED OF PARTICLES (Vector2(x, y)
                _particles[i].Velocity = new Vector2((float)Math.Cos(test) * 25f, (float)Math.Sin(test) * 25f);
                test += (float)Math.PI / 16;
            }
        }

        public void Reset()
        {
            test = 0f;
            _particles[0].Reset(new Vector2(GlobalVariables.WINDOW_WIDTH / 2, GlobalVariables.WINDOW_HEIGHT / 2));
            for (int i = 1; i < _particles.Count; i++)
            {
                _particles[i].Reset(new Vector2((float)Math.Cos(test) * 50f + (GlobalVariables.WINDOW_WIDTH / 2), (float)Math.Sin(test) * 50f + (GlobalVariables.WINDOW_HEIGHT / 2)));
                test += (float)Math.PI / 16;
            }
        }
    }
}
