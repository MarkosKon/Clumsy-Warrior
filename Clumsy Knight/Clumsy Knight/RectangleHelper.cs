namespace Clumsy_Knight
{
    using Microsoft.Xna.Framework;
    using System;
    /// <summary>
    /// A class with extension methods that decide whether or not we have a collision between two rectangles
    /// from four different directions and a method for pixel collision.
    /// </summary>
    public static class RectangleHelper
    {
        public static bool isOnTopOf(this Rectangle r1, Rectangle r2)
        {
            return (r1.Bottom <= r2.Top + 5 &&
                r1.Bottom >= r2.Top - 5 &&
                r1.Right >= r2.Left &&
                r1.Left <= r2.Right);
        }

        public static bool isOnBottomOf(this Rectangle r1, Rectangle r2)
        {
            return (r1.Top >= r2.Bottom - 5 &&
                r1.Top <= r2.Bottom + 5 &&
                r1.Right >= r2.Left &&
                r1.Left <= r2.Right);
        }

        public static bool isOnLeftOf(this Rectangle r1, Rectangle r2)
        {
            return (r1.Right <= r2.Left + 5 &&
                    r1.Right >= r2.Left - 5 &&
                    r1.Top >= r2.Top - 40 &&
                    r1.Bottom <= r2.Bottom + 40);
        }

        public static bool isOnRightOf(this Rectangle r1, Rectangle r2)
        {
            return (r1.Left >= r2.Right - 5 &&
                    r1.Left <= r2.Right + 5 &&
                    r1.Top >= r2.Top - 40 &&
                    r1.Bottom <= r2.Bottom + 40);
        }

        /// <summary>
        /// A class that check's for pixel collision's. 
        /// Taken from: http://xboxforums.create.msdn.com/forums/p/23774/139805.aspx
        /// </summary>
        /// <param name="r1">First object's rectangle.</param>
        /// <param name="dataA">First object's color array.</param>
        /// <param name="r2">Second object's rectangle.</param>
        /// <param name="dataB">Second object's color array.</param>
        /// <returns></returns>
        public static bool PixelCollision(Rectangle r1, Color[] dataA,
                            Rectangle r2, Color[] dataB)
        {
            // Find the bounds of the rectangle intersection
            int top = Math.Max(r1.Top, r2.Top);
            int bottom = Math.Min(r1.Bottom, r2.Bottom);
            int left = Math.Max(r1.Left, r2.Left);
            int right = Math.Min(r1.Right, r2.Right);

            // Check every point within the intersection bounds
            for (int y = top; y < bottom; y++)
            {
                for (int x = left; x < right; x++)
                {
                    // Get the color of both pixels at this point
                    Color colorA = dataA[(x - r1.Left) +
                                         (y - r1.Top) * r1.Width];
                    Color colorB = dataB[(x - r2.Left) +
                                         (y - r2.Top) * r2.Width];

                    // If both pixels are not completely transparent,
                    if (colorA.A != 0 && colorB.A != 0&&colorB==new Color(255,0,0))
                    {
                        // then an intersection has been found
                        return true;
                    }
                }
            }
            // No intersection found
            return false;
        }
    }
}
