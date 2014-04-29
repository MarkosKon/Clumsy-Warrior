namespace Clumsy_Knight
{
    using Microsoft.Xna.Framework;
    class OrcAttacking : EnemyState
    {
        public OrcAttacking(EnemyState state)
        {
            this.enemy = state.enemy;
            Initialize();
        }

        private void Initialize()
        {
            interval = 150;
            frameWidth = 100;
            frameHeight = 100;
            currentFrameX = 0;
            currentFrameY = 0;
        }

        public override void Update(GameTime gameTime, Player player)
        {
            if (enemy.speed.X > 0)
                enemy.enemyRectangle = new Rectangle((6 - currentFrameX) * frameWidth, (3 + currentFrameY) * frameHeight, frameWidth, frameHeight);
            else
                enemy.enemyRectangle = new Rectangle(currentFrameX * frameWidth, currentFrameY * frameHeight, frameWidth, frameHeight);
            Animate(gameTime, 6);
            enemy.attackingWaitTime += enemy.timer;
            enemy.enemyTexture.GetData(0, enemy.enemyRectangle, enemy.textureColors, 0, frameWidth * frameHeight);
            StateChangeCheck(player);
        }

        private void StateChangeCheck(Player player)
        {
            if (enemy.attackingWaitTime > 12000)
            {
                enemy.attackingWaitTime = 0;
                enemy.state = new OrcStanding(this);
            }
        }
    }
}
