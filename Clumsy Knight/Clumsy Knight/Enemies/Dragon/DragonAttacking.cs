namespace Clumsy_Knight
{
    using Microsoft.Xna.Framework;
    public class DragonAttacking : EnemyState
    {
        public DragonAttacking(EnemyState state)
        {
            this.enemy = state.enemy;
            Initialize();
        }

        private void Initialize()
        {
            interval = 180;
            frameWidth = 140;
            frameHeight = 125;
            currentFrameX = 0;
            currentFrameY = 0;
        }

        public override void Update(GameTime gameTime, Player player)
        {
            if (enemy.speed.X < 0)
                enemy.enemyRectangle = new Rectangle(currentFrameX * frameWidth, 280, frameWidth, frameHeight);
            else
                enemy.enemyRectangle = new Rectangle((4 - currentFrameX) * frameWidth, 680, frameWidth, frameHeight);
            Animate(gameTime, 4);
            enemy.attackingWaitTime += enemy.timer;
            enemy.enemyTexture.GetData(0, enemy.enemyRectangle, enemy.textureColors, 0, frameWidth * frameHeight);
            StateChangeCheck(player);
        }

        private void StateChangeCheck(Player player)
        {
            if (enemy.attackingWaitTime > 13000)
            {
                enemy.attackingWaitTime = 0;
                enemy.state = new DragonWalking(this);
            }
        }
    }
}
