using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Clumsy_Knight
{
    class Platform
    {
        Texture2D platformTexture;
        public Vector2 platformPosition;

        public Rectangle platformRectangle;
        

        public Platform(Texture2D platformTexture, Vector2 platformPosition)
        {
            this.platformTexture = platformTexture;
            this.platformPosition = platformPosition;

            platformRectangle = new Rectangle((int)platformPosition.X, (int)platformPosition.Y,
                platformTexture.Width, platformTexture.Height);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(platformTexture, platformRectangle, Color.White);
        }
    }
}
