namespace Clumsy_Knight
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Content;
    using Microsoft.Xna.Framework.Graphics;
    using System;

    /// <summary>
    /// An abstract class with reusable content that specific enemies inherit from.
    /// </summary>
    public abstract class Enemy
    {
        //The spritesheet.
        public Texture2D enemyTexture;
        //A rectangle that contains the current frame of a movement from the spritesheet.
        public Rectangle enemyRectangle;
        //Always is set to (0,0) topleft side, although the "correct" would be to set it
        //to the bottom (left or right).
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

        //An array to store the colors of a texture used for pixel collision.
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
        protected virtual void Constructor(DifficultyLevel difficulty, Vector2 position)
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
        /// <param name="player">The player object passed from the main as a parameter
        /// used in AI.</param>
        public virtual void Update(GameTime gameTime, Player player)
        {

        }

        /// <summary>
        /// This method "guides" the Draw method for the animation.
        /// </summary>
        /// <param name="gameTime">We need a GameTime parameter from the main because we
        /// want to animate for a specific time.</param>
        /// <param name="targetFrames">How many frames horizontally is the current animation.</param>
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

        /// <summary>
        /// This method checks where is player and changes the state if the player is near.
        /// It is called from the derived enemy's Update.
        /// </summary>
        /// <param name="player">The player object passed from the Update method.</param>
        /// <param name="detectDistance">Change the enemy's direction if the player has been detected near.</param>
        /// <param name="attackDistance">Start attacking the player if he is really near.</param>
        public void WhereIsPlayer(Player player,int detectDistance, int attackDistance)
        {
            //Find the centers.
            Vector2 dragonCenter = new Vector2(this.position.X + (this.enemyRectangle.Width / 2), this.position.Y + (this.enemyRectangle.Height / 2));
            Vector2 playerCenter = new Vector2(player.position.X + (player.rectangle.Width / 2), player.position.Y - (player.rectangle.Height / 2));
            //Is the player near to the enemy;
            if (Math.Abs(playerCenter.X - dragonCenter.X) < detectDistance)
            {
                //Is the player left to the enemy;
                if ((playerCenter.X - dragonCenter.X) < 0)
                {
                    //In other words speed must be negative.
                    this.speed.X = (-1.0f) * Math.Abs(this.speed.X);
                }
                //Is the player right to the enemy;
                else
                {
                    //Speed must be negative.
                    this.speed.X = Math.Abs(this.speed.X);
                }
                //If the player is really near and haven't attacked recently, start attacking.
                if (Math.Abs(playerCenter.X - dragonCenter.X) < attackDistance && attackingWaitTime == 0)
                {
                    enemyState = EnemyState.attacking;
                    walkingWaitTime = 0;
                    standingWaitTime = 0;
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
