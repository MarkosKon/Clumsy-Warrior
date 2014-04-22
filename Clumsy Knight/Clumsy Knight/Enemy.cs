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
        protected int test;
        //The spritesheet.
        public Texture2D enemyTexture;
        //A rectangle that contains the current frame of a movement from the spritesheet.
        public Rectangle enemyRectangle;
        //Always is set to (0,0).
        protected Vector2 origin;

        //Position of the enemy on screen.
        public Vector2 position;

        //If we want to rotate the image to z-axis.
        protected float rotation;

        //The current frame of the movement starting from 0.
        protected int currentFrameX;
        protected int currentFrameY;

        public int frameHeight;
        public int frameWidth;

        //Temporary time to help us change the movement frames.
        protected float timer;
        //The rate in which images from a movement change.
        protected float interval;

        //This variable exists because we want to animate the 3 frames from "standing" movement in the following order.
        //1-2-3-2-1-2-3-2.... not 1-2-3-1-2-3....
        protected bool right;

        //Health, speed and damage are relative to the DifficultyLevel.
        //
        public DifficultyLevel difficulty;
        public int health;
        //It is a vector because we add / subtract it to the position.
        public Vector2 speed;
        public int damage;

        //A struct declared at the end of MainFunction.cs
        public EnemyState enemyState;

        public bool isVisible=true;
        public bool isHit = false;

        public Color[] textureColors;

        //Variables used to switch between states.
        //
        protected float standingWaitTime;
        protected float attackingWaitTime;
        protected float walkingWaitTime;

        ///<summary>
        ///The constructor of the Abstract class.
        /// </summary>
        /// <param name="difficulty">The game difficulty.</param>
        /// <param name="position">The position of the enemy on the screen.</param>
        public virtual void Constructor(DifficultyLevel difficulty, Vector2 position)
        {
            
        }

        ///<summary>
        ///A virtual LoadContent method.
        ///</summary>
        /// <param name="content">We need a content parameter from the main because we
        /// want to load the texture in this class.</param>
        public virtual void LoadContent(ContentManager content)
        {

        }

        /// <summary>
        /// A virtual Update method.
        /// </summary>
        /// <param name="gameTime">A GameTime parameter from the main.</param>
        /// <param name="player">The player object from the main as a parameter
        /// used in AI.</param>
        public virtual void Update(GameTime gameTime, Player player)
        {

        }

        /// <summary>
        /// This method "guides" the Draw method for the animation.
        /// </summary>
        /// <param name="gameTime">We need a GameTime parameter from the main because we
        /// want to animate for a specific time.</param>
        public void Animate(GameTime gameTime,int targetFrames)
        {
            timer += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            if (timer > interval)
            {
                currentFrameX++;
                timer = 0;

                if (currentFrameX > targetFrames)
                {
                    currentFrameX = 0;
                    currentFrameY++;
                }
            }
        }

        ///<summary>
        /// A method to draw the sprite on screen.
        /// </summary>
        /// <param name="spriteBatch">We give spriteBatch as parameter because the current class
        /// don't know anything about it.</param>
        public void Draw(SpriteBatch spriteBatch) 
        {
            if (isVisible)
            {
                spriteBatch.Draw(enemyTexture, position, enemyRectangle, Color.White, rotation, origin, 1f, SpriteEffects.None, 0f);
            }
        }
    }
}
