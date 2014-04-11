﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Clumsy_Knight
{
    //
    class Map
    {
        private List<BigFatTile> collisionMap1 = new List<BigFatTile>();

        public List<BigFatTile> CollisionMap1
        {
            get { return collisionMap1; }
        }

        private int width, height;
        public int Width
        {
            get { return width; }
        }

        public int Height
        {
            get { return height; }
        }

        public Map() { }

        public void Generate(int[,] map, int size)
        {
            for (int x = 0; x < map.GetLength(1); x++)
                for (int y = 0; y < map.GetLength(0); y++)
                {
                    int number = map[y, x];

                    if (number > 0)
                        collisionMap1.Add(new BigFatTile(number, new Rectangle(1050+x * size, -420+y* size, size, size)));

                    width = (x + 1) * size;
                    height = (y + 1) * size;


                }
        }

        public void Update (GameTime gametime,Player player)
        {
            Rectangle arectangle = new Rectangle((int)player.position.X, (int)player.position.Y, player.frameHeight, player.frameWidth);
            foreach (BigFatTile tile in collisionMap1)
            {
                if (arectangle.isOnTopOf(tile.Rectangle))
                {
                    player.velocity.Y = 0f;
                    player.hasJumped = false;
                    break;
                }
            }

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (BigFatTile map in collisionMap1)
                map.Draw(spriteBatch);

        }
    }
}
