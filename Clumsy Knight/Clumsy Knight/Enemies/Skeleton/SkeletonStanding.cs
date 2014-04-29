namespace Clumsy_Knight
{
    using Microsoft.Xna.Framework;
    /// <summary>
    /// A concrete state.
    /// </summary>
    class SkeletonStanding : EnemyState
    {
        public SkeletonStanding(EnemyState state)
        {
            this.enemy = state.enemy;
            Initialize();
        }

        private void Initialize()
        {
            interval = 150;
            frameWidth = 44;
            frameHeight = 94;
            currentFrameX = 0;
            currentFrameY = 0;
        }

        public override void Update(GameTime gameTime, Player player)
        {
            if (enemy.speed.X < 0)
                enemy.enemyRectangle = new Rectangle(32 + currentFrameX * frameWidth, 11, frameWidth, frameHeight);
            else
                enemy.enemyRectangle = new Rectangle(-32 + (5 - currentFrameX) * frameWidth, 305, frameWidth, frameHeight);
            Animate(gameTime, 4);
            enemy.standingWaitTime += enemy.timer;
            enemy.enemyTexture.GetData(0, enemy.enemyRectangle, enemy.textureColors, 0, frameWidth * frameHeight);
            StateChangeCheck(player);
        }

        private void StateChangeCheck(Player player)
        {
            if (WhereIsPlayer(player, 150, 30))
            {
                enemy.state = new SkeletonAttacking(this);
                enemy.walkingWaitTime = 0;
                enemy.standingWaitTime = 0;
            }
            else if (enemy.standingWaitTime > 10000)
            {
                enemy.standingWaitTime = 0;
                enemy.state = new SkeletonWalking(this);
            }
        }
    }
}
