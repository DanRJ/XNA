using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace Innlevering2
{
    class Particle
    {
        private string _textureName;
        private Rectangle _textureRectangle;
        private Vector2 _position;
        private Texture2D _texture;
        private float _scale;

        private Vector2 _velocity;
        private float _mass;
        private float _radius;

        public Particle(ContentManager content, Vector2 position)
        {
            _textureName = GlobalVariables.PARTICLE_TEXTURE_NAME;
            _textureRectangle = new Rectangle(0, 0, 150, 150);
            _position = position;
            _texture = content.Load<Texture2D>(_textureName);
            _scale = GlobalVariables.PARTICLE_TEXTURE_SCALE;
            _radius = (_texture.Height * _scale) / 2;
            _mass = 10f;
            _velocity = Vector2.Zero;

        }

        public void ResolveCollision(Particle particle)
        {
            Vector2 delta = this._position - particle.Position;
            float deltaLength = delta.Length();
            Vector2 normalizedDelta = delta / deltaLength;
            Vector2 minimumTranslationDistance = normalizedDelta * ((this._radius + particle.Radius) - deltaLength);

            Vector2 relativeVelocity = this._velocity - particle.Velocity;

            float relativeVelocityProjectionOnDelta = Vector2.Dot(relativeVelocity, normalizedDelta);

            Vector2 velocityComponentOnDelta = normalizedDelta * relativeVelocityProjectionOnDelta;

            this._position += minimumTranslationDistance / 2;
            particle.Position -= minimumTranslationDistance / 2;

            this._velocity -= velocityComponentOnDelta * .9f;
            particle.Velocity += velocityComponentOnDelta * .9f;
        }

        public Texture2D Texture
        {
            get { return _texture; }
        }

        public Rectangle TextureRectangle
        {
            get { return _textureRectangle; }
        }

        public Vector2 Position
        {
            get { return _position; }
            set { _position = value; }
        }

        public float Scale
        {
            get { return _scale; }
            set { _radius = value; }
        }

        public float Radius
        {
            get { return _radius; }
            set { _radius = value; }
        }

        public float Mass
        {
            get { return _mass; }
            set { _mass = value; }
        }

        public Vector2 Velocity
        {
            get { return _velocity; }
            set { _velocity = value; }
        }

        public void Reset(Vector2 position)
        {
            _position = position;
            _velocity = Vector2.Zero;
        }


    }
}
