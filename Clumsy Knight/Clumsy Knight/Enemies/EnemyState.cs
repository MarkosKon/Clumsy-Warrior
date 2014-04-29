namespace Clumsy_Knight
{
    using Microsoft.Xna.Framework;
    using System;
    /// <summary>
    /// The abstract state.
    /// </summary>
    public abstract class EnemyState
    {
        //The (abstract) context.
        public Enemy enemy;

        // The current frame of the movement starting from 0.
        protected int currentFrameX;
        protected int currentFrameY;
        
        public int frameWidth;
        public int frameHeight;

        // The rate in which images from a movement change.
        protected float interval;

        /// <summary>
        /// This method changes the direction of the enemy is the player is near. 
        /// Also returns true to the state check if the player is really near.
        /// </summary>
        /// <param name="player">The player object passed from the Update method if tge concrete state.</param>
        /// <param name="detectDistance">The distance in which enemy spots the player.</param>
        /// <param name="attackDistance">The distance in which enemy will attack the player.</param>
        public bool WhereIsPlayer(Player player, int detectDistance, int attackDistance)
        {
            Vector2 enemyCenter = new Vector2(enemy.position.X + (enemy.enemyRectangle.Width / 2), enemy.position.Y + (enemy.enemyRectangle.Height / 2));
            Vector2 playerCenter = new Vector2(player.position.X + (player.rectangle.Width / 2), player.position.Y - (player.rectangle.Height / 2));
            float distanceX = enemyCenter.X - playerCenter.X;
            float distanceY = Math.Abs(enemyCenter.Y - playerCenter.Y);
            // Is the player near to the enemy;
            if (Math.Abs(distanceX) < detectDistance && distanceY < 50)
            {
                // Is the player left to the enemy;
                if ((distanceX) >= 0)
                {
                    // In other words speed must be negative.
                    enemy.speed.X = (-1.0f) * Math.Abs(enemy.speed.X);
                }
                // Is the player right to the enemy;
                else
                {
                    // Speed must be negative.
                    enemy.speed.X = Math.Abs(enemy.speed.X);
                }
                // If the player is really near return true to the state check.
                if (Math.Abs(distanceX) < attackDistance)
                {
                    return true;
                }
            }
            return false;
        }

        public virtual void Update(GameTime gameTime, Player player)
        {

        }

        /// <summary>
        /// This method "guides" the Draw method of the concrete enemy for the animation.
        /// </summary>
        /// <param name="gameTime">We need a GameTime parameter from the main because we
        /// want to change frames at regural intervals.</param>
        /// <param name="targetFrames">How many frames horizontally is the current animation.</param>
        public void Animate(GameTime gameTime, int targetFrames)
        {
            enemy.timer += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            if (enemy.timer > interval)
            {
                currentFrameX++;
                enemy.timer = 0;

                if (currentFrameX > targetFrames)
                {
                    currentFrameX = 0;
                    currentFrameY++;
                }
            }
        }
    }
}
