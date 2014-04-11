using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace Clumsy_Knight
{
    //This class is for the bricks and tiles of the map.
    class Tile
    {
        protected Texture2D texture;

        private Rectangle rectangle;
        public Rectangle Rectangle
        {
            get { return rectangle; }
            protected set { rectangle = value; }
        }

        private static ContentManager content;
        public static ContentManager Content
        {
            protected get { return content; }
            set { content = value; }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture,rectangle, Color.White);
        }
    }
    class BigFatTile : Tile
    {
        public BigFatTile(int i, Rectangle newRectangle)
        {
            texture = Content.Load<Texture2D>("Map" + i);
            this.Rectangle = newRectangle;
        }
    }
}
