using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


namespace Innlevering2
{
    public class Character
    {
        /// <summary>
        /// _characterBox is the area the sprite is drawn to, and serves as a collision area aswell
        /// </summary>
        protected Rectangle _characterBox;
        /// <summary>
        /// _activeSprite is the current sprite we are currently using
        /// </summary>
        protected Rectangle _activeSprite;
        protected Texture2D _textureSheet;

        public Character(Point position)
        {
            //Set the width and height in the child class FIKS NEDENFOR!!!!!!!!!!!! 
            //100 nedenfor representerer Y-aksen, vi vil vel ha en start position som er i midten.
            _characterBox = new Rectangle(position.X, , 0, 0);
        }
        /// <summary>
        /// A virtual function can be overridden using the override keyword.
        /// </summary>
        /// <param name="texture">The texture used for this object instance</param>
        public virtual void SetTextureSheet(Texture2D texture)
        {
            _textureSheet = texture;
        }
        public virtual void Update(float deltaTime)
        {

        }
        /// <summary>
        /// The Draw Method for all characters.
        /// </summary>
        /// <param name="drawer">The Spritebatch we are going to use.</param>
        public virtual void Draw(SpriteBatch drawer)
        { 
            //Simple check, allows us to find out why the game crashed
            if (_textureSheet == null)
            {
                Console.WriteLine("Fail! Tried to draw a character from thin air! There is no spritesheet set!");
                System.Threading.Thread.Sleep(100);
            }
            drawer.Draw(_textureSheet, _characterBox, _activeSprite, Color.White);
        }
    }
}
