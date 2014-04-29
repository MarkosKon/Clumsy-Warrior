namespace Clumsy_Knight
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    class DragonStanding : EnemyState
    {
        public DragonStanding(EnemyState state)
        {
            this.enemy = state.enemy;
            Initialize();
        }

        private void Initialize()
        {
            interval = 150;
            frameWidth = 116;
            frameHeight = 130;
            currentFrameX = 0;
            currentFrameY = 0;
        }

        public override void Update(GameTime gameTime, Player player)
        {
            if (enemy.speed.X < 0)
                enemy.enemyRectangle = new Rectangle(currentFrameX * frameWidth, 0, frameWidth, frameHeight);
            else
                enemy.enemyRectangle = new Rectangle(4 + (4 - currentFrameX) * frameWidth, 410, frameWidth, frameHeight);
            Animate(gameTime, 4);
            enemy.standingWaitTime += enemy.timer;
            enemy.enemyTexture.GetData(0, enemy.enemyRectangle, enemy.textureColors, 0, frameWidth * frameHeight);
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
            else if (enemy.standingWaitTime > 10000)
            {
                enemy.standingWaitTime = 0;
                enemy.state = new DragonWalking(this);
            }
        }
    }
}
