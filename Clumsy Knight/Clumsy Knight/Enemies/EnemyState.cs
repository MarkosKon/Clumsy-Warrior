namespace Clumsy_Knight
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using System;
    public abstract class EnemyState
    {
        public Enemy enemy;

        // The current frame of the movement starting from 0.
        protected int currentFrameX;
        protected int currentFrameY;
        
        public int frameWidth;
        public int frameHeight;

        // The rate in which images from a movement change.
        protected float interval;

        //public Rectangle collisionRectangle;

        /// <summary>
        /// This method checks where is player and changes the state if the player is near.
        /// </summary>
        /// <param name="player">The player object passed from the Update method.</param>
        /// <param name="detectDistance">Change the enemy's direction if the player has been detected near.</param>
        /// <param name="attackDistance">Start attacking the player if he is really near.</param>
        public bool WhereIsPlayer(Player player, int detectDistance, int attackDistance)
        {
            //Find the centers.
            Vector2 dragonCenter = new Vector2(enemy.position.X + (enemy.enemyRectangle.Width / 2), enemy.position.Y + (enemy.enemyRectangle.Height / 2));
            Vector2 playerCenter = new Vector2(player.position.X + (player.rectangle.Width / 2), player.position.Y - (player.rectangle.Height / 2));
            float distanceX = dragonCenter.X - playerCenter.X;
            float distanceY = Math.Abs(dragonCenter.Y - playerCenter.Y);
            //Is the player near to the enemy;
            if (Math.Abs(distanceX) < detectDistance && distanceY < 50)
            {
                //Is the player left to the enemy;
                if ((distanceX) >= 0)
                {
                    //In other words speed must be negative.
                    enemy.speed.X = (-1.0f) * Math.Abs(enemy.speed.X);
                }
                //Is the player right to the enemy;
                else
                {
                    //Speed must be negative.
                    enemy.speed.X = Math.Abs(enemy.speed.X);
                }
                //If the player is really near and haven't attacked recently, start attacking.
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
        /// This method "guides" the Draw method for the animation.
        /// </summary>
        /// <param name="gameTime">We need a GameTime parameter from the main because we
        /// want to animate for a specific time.</param>
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

        /*public virtual void Draw(SpriteBatch spriteBatch,GameTime gameTime)
        {

        }*/
    }
}
