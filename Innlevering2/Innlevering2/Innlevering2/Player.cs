using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


namespace Innlevering2
{
    /// <summary>
    /// Character controlled by input.
    /// </summary>
    public class Player : Character
    {
        private int movementSpeed = 4;
        private KeyboardState _currentKeyboardState;
        private int _spriteWidth = 100;
        private int _spriteHeight = 117;
        private float _animationStepTime;
        private float _animationTimer;
        private int _currentDirection; // 0: right, 1: left, 2: jump, 3: left
        private int _animationCounter;

        public Player(Point position)
            : base(position)
        {
            _activeSprite = new Rectangle(0, _spriteHeight, _spriteWidth, _spriteHeight);
            _characterBox.Width = _spriteWidth;
            _characterBox.Height = _spriteHeight;
            _animationStepTime = 1f / 10; //Animasjon hastighet
            _currentDirection = 0; 
            _animationCounter = 0;
        }
        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(float deltaTime)
        {
            _currentKeyboardState = Keyboard.GetState();

            if (_currentKeyboardState.IsKeyDown(Keys.D))
            {
                //the D-key moves character to the right
                _characterBox.X += movementSpeed;
                _currentDirection = 0;
                _animationTimer += deltaTime;
            }
            else if (_currentKeyboardState.IsKeyDown(Keys.A))
            {
                //the A-key moves character to the left
                _characterBox.X -= movementSpeed;
                _currentDirection = 1;
                _animationTimer += deltaTime;
            }
            else if (_currentKeyboardState.IsKeyDown(Keys.Space))
            {
                _characterBox.Y -= 6;
                _currentDirection = 2;
                _animationTimer += deltaTime;
            }
            if (_animationTimer >= _animationStepTime)
            {
                _animationCounter++;
                if (_animationCounter == 4)
                {
                    _animationCounter = 0;
                }
                _animationTimer -= _animationStepTime;
            }
            _activeSprite.X = _animationCounter * _spriteWidth;
            _activeSprite.Y = _currentDirection * _spriteHeight;
        }
    }
}
