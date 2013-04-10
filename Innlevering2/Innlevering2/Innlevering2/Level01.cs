using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Innlevering2
{
    class Level01
    {
        Rectangle floor;
        Texture2D floorTexture;

        public Level01(GraphicsDevice graphicsDevice)
        {
            floor = new Rectangle(0, 400, 600, 10);
            floorTexture = new Texture2D(graphicsDevice, 1, 1, false, SurfaceFormat.Color);
            floorTexture.SetData(new[] { Color.White });
        }

        public void Draw(SpriteBatch batch)
        {
            batch.Draw(floorTexture, floor, Color.Red);
        }
    }
}
