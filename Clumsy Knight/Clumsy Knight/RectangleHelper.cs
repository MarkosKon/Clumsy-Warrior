namespace Clumsy_Knight
{
    using Microsoft.Xna.Framework;
    /// <summary>
    /// An extension method that decides whether or not we have a collision between two rectangles
    /// from four different directions.
    /// </summary>
    static class RectangleHelper
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
                    r1.Top >= r2.Top - 30 &&
                    r1.Bottom <= r2.Bottom + 30);
        }
        public static bool isOnRightOf(this Rectangle r1, Rectangle r2)
        {
            return (r1.Left >= r2.Right - 5 &&
                    r1.Left <= r2.Right + 5 &&
                    r1.Top >= r2.Top - 30 &&
                    r1.Bottom <= r2.Bottom + 30);
        }
    }
}
