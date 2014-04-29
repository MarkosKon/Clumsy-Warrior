namespace Clumsy_Knight
{
    using Microsoft.Xna.Framework;
    /// <summary>
    /// A concrete state.
    /// </summary>
    class SkeletonWalking : EnemyState
    {
        public SkeletonWalking(EnemyState state)
        {
            this.enemy = state.enemy;
            Initialize();
        }
        /// <summary>
        /// This concrete state is the default state and has two constructors
        /// for the initialization from the skeleton constructor.
        /// </summary>
        /// <param name="skeleton">Skeleton object passed from the skeleton contructor.</param>
        public SkeletonWalking(Skeleton skeleton)
        {
            this.enemy = skeleton;
            Initialize();
        }

        private void Initialize()
        {
            interval = 100;
            frameWidth = 53;
            frameHeight = 94;
            currentFrameX = 0;
            currentFrameY = 0;
        }

        public override void Update(GameTime gameTime, Player player)
        {
            if (enemy.speed.X < 0)
                enemy.enemyRectangle = new Rectangle((8 - currentFrameX) * frameWidth, 400, frameWidth, frameHeight);
            else
                enemy.enemyRectangle = new Rectangle(58 + currentFrameX * frameWidth, 108, frameWidth, frameHeight);
            Animate(gameTime, 7);
            enemy.walkingWaitTime += enemy.timer;
            enemy.enemyTexture.GetData(0, enemy.enemyRectangle, enemy.textureColors, 0, frameWidth * frameHeight);
            enemy.position += enemy.speed;
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
            else if (enemy.walkingWaitTime > 10000)
            {
                enemy.walkingWaitTime = 0;
                enemy.speed = -enemy.speed;
                enemy.state = new SkeletonStanding(this);
            }
        }
    }
}
