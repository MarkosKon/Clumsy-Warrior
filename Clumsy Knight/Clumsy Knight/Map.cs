using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Clumsy_Knight
{
    //
    public class Map
    {
        public List<Tile> collisionMap1 = new List<Tile>();

        public List<Tile> CollisionMap1
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
                        collisionMap1.Add(new Tile(number, new Rectangle(-277+x * size, -420+y* size, size, size)));

                    width = (x + 1) * size;
                    height = (y + 1) * size;
                }
        }

        public void Update (GameTime gametime,Player player)
        {
            
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (Tile tile in collisionMap1)
                tile.Draw(spriteBatch);
        }
    }
}
