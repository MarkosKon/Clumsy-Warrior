namespace Clumsy_Knight
{
    using Microsoft.Xna.Framework;
    /// <summary>
    /// A concrete state.
    /// </summary>
    class DragonWalking : EnemyState
    {
        public DragonWalking(EnemyState state)
        {
            this.enemy = state.enemy;
            Initialize();
        }

        /// <summary>
        /// This concrete state is the default state and has two constructors
        /// for the initialization from the dragon constructor.
        /// </summary>
        /// <param name="dragon">Dragon object passed from the dragon contructor.</param>
        public DragonWalking(Dragon dragon)
        {
            this.enemy = dragon;
            Initialize();
        }

        private void Initialize()
        {
            interval = 150;
            frameWidth = 132;
            frameHeight = 120;
            currentFrameX = 0;
            currentFrameY = 0;
        }

        public override void Update(GameTime gameTime, Player player)
        {
            if (enemy.speed.X < 0)
                enemy.enemyRectangle = new Rectangle(currentFrameX * frameWidth, 150, frameWidth, frameHeight);
            else
                enemy.enemyRectangle = new Rectangle((5 - currentFrameX) * frameWidth, 553, frameWidth, frameHeight);
            Animate(gameTime, 5);
            enemy.walkingWaitTime += enemy.timer;
            enemy.enemyTexture.GetData(0, enemy.enemyRectangle, enemy.textureColors, 0, frameWidth * frameHeight);
            enemy.position += enemy.speed;
            StateChangeCheck(player);
        }

        private void StateChangeCheck(Player player)
        {
            if (WhereIsPlayer(player, 200, 40))
            {
                enemy.state = new DragonAttacking(this);
                enemy.walkingWaitTime = 0;
                enemy.standingWaitTime = 0;
            }
            else if (enemy.walkingWaitTime > 10000)
            {
                enemy.walkingWaitTime = 0;
                enemy.speed = -enemy.speed;
                enemy.state = new DragonStanding(this);
            }
        }
    }
}
