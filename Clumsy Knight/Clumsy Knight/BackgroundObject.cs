namespace Clumsy_Knight
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Content;
    using Microsoft.Xna.Framework.Graphics;

    /// <summary>
    /// Background Object Class
    /// </summary>
    class BackgroundObject
    {
        private Texture2D objectTexture;
        private Rectangle objectRectangle;
        public Vector2 objectPosition;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="texture"></param>
        /// <param name="rectangle"></param>
        /// <param name="position"></param>
        public BackgroundObject(Texture2D texture, Rectangle rectangle, Vector2 position)
        {
            objectTexture = texture;
            objectRectangle = rectangle;
            objectPosition = position;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(objectTexture, objectPosition, objectRectangle, Color.White);
        }
    }
}
