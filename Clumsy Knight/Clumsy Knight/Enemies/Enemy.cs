namespace Clumsy_Knight
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Content;
    using Microsoft.Xna.Framework.Graphics;

    /// <summary>
    /// An abstract class with reusable content that specific enemies inherit from.
    /// </summary>
    public abstract class Enemy
    {
        public EnemyState state;
        // The spritesheet.
        public Texture2D enemyTexture;
        // A rectangle that contains the current frame of a movement from the spritesheet.
        public Rectangle enemyRectangle;
        // Always is set to (0,0) topleft side, although the "correct" would be to set it
        // to the bottom (left or right).
        protected Vector2 origin;

        // Position of the enemy on screen.
        public Vector2 position;

        // If we want to rotate the image to z-axis.
        protected float rotation;

        // Temporary time to help us change the movement frames.
        public float timer;

        // Health, speed and damage are relative to the DifficultyLevel.
        //
        public DifficultyLevel difficulty;
        public int health;
        // It is a vector because we add / subtract it to the position.
        public Vector2 speed;
        public int damage;

        public bool isVisible=true;
        public bool isHit = false;

        // An array to store the colors of a texture used for pixel collision.
        public Color[] textureColors;

        // Variables used to switch between states.
        //
        public float standingWaitTime;
        public float attackingWaitTime;
        public float walkingWaitTime;

        /// <summary>
        /// The constructor of the Abstract class.
        /// </summary>
        /// <param name="difficulty">The game difficulty.</param>
        /// <param name="position">The position of the enemy on the screen.</param>
        protected virtual void Constructor(DifficultyLevel difficulty, Vector2 position)
        {
            
        }

        /// <summary>
        /// A virtual LoadContent method.
        /// </summary>
        /// <param name="content">We need a content parameter from the main because we
        /// want to load the texture in this class.</param>
        public virtual void LoadContent(ContentManager content)
        {

        }

        /// <summary>
        /// A virtual Update method.
        /// </summary>
        /// <param name="gameTime">A GameTime parameter from the main.</param>
        /// <param name="player">The player object passed from the main as a parameter
        /// used in AI.</param>
        public virtual void Update(GameTime gameTime, Player player)
        {

        }
        
        /// <summary>
        /// A method to draw the sprite on screen.
        /// </summary>
        /// <param name="spriteBatch">We give spriteBatch as parameter because the current class
        /// don't know anything about it.</param>
        public void Draw(SpriteBatch spriteBatch) 
        {
            Color color = Color.White;
            if (isHit)
                color = Color.Black;
            if (isVisible)
            {
                spriteBatch.Draw(enemyTexture, position, enemyRectangle, color, rotation, origin, 1f, SpriteEffects.None, 0f);
            }
        }
    }
}
