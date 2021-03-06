﻿namespace Clumsy_Knight
{
    using Microsoft.Xna.Framework;

    /// <summary>
    /// A simple class that creates a translation Matrix.
    /// </summary>
    public class Camera
    {
        public Matrix transform;
        Vector2 center;

        /// <summary>
        /// The Constructor
        /// </summary>
        public Camera()
        {
        }

        /// <summary>
        /// A method to update the camera called from MainFunction.Update.
        /// </summary>
        /// <param name="gameTime">A GameTime parameter from the main.</param>
        /// <param name="game">The Game object itself from the main.</param>
        public void Update(GameTime gameTime, MainFunction game)
        {
            // Find how far away is the player's left side from the screen center in x  axis (x axis center in this case
            // is 400).
            center = new Vector2(game.player.position.X- 400, 0);
            // Make a Matrix tha will translate if multiplied with another matrix all vectors by distance equal to 
            // the center (which is a distance) variable. This Matrix will be used as a parameter in the 
            // SpriteBatch.Draw() function.
            transform = Matrix.CreateTranslation(new Vector3(-center.X, 0, 0));
        }
    }
}
