namespace Clumsy_Knight
{
    using Microsoft.Xna.Framework;
    /// <summary>
    /// A concrete state.
    /// </summary>
    class SkeletonAttacking : EnemyState
    {
        public SkeletonAttacking(EnemyState state)
        {
            this.enemy = state.enemy;
            Initialize();
        }

        private void Initialize()
        {
            interval = 200;
            frameWidth = 86;
            frameHeight = 94;
            currentFrameX = 0;
            currentFrameY = 0;
        }

        public override void Update(GameTime gameTime, Player player)
        {
            // Decide if we want the right or the left animation of a movement by checking the direction.
            if (enemy.speed.X > 0)
                enemy.enemyRectangle = new Rectangle(6 + currentFrameX * frameWidth, 203, frameWidth, frameHeight);
            else
                enemy.enemyRectangle = new Rectangle((3 - currentFrameX) * frameWidth, 496, frameWidth, frameHeight);
            Animate(gameTime, 3);
            enemy.attackingWaitTime += enemy.timer;
            enemy.enemyTexture.GetData(0, enemy.enemyRectangle, enemy.textureColors, 0, frameWidth * frameHeight);
            StateChangeCheck(player);
        }

        private void StateChangeCheck(Player player)
        {
            if (enemy.attackingWaitTime > 4000)
            {
                enemy.attackingWaitTime = 0;
                enemy.state = new SkeletonWalking(this);
            }
        }
    }
}
