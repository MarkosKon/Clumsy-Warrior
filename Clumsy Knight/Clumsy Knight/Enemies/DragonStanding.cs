namespace Clumsy_Knight.Enemies
{
    using Microsoft.Xna.Framework;
    class DragonStanding : DragonState
    {
        public DragonStanding(DragonState state)
        {
            this.dragon = state.dragon;
            Initialize();
        }

        private void Initialize()
        {
            dragon.interval = 150;
            dragon.frameWidth = 116;
            dragon.frameHeight = 130;
            dragon.currentFrameX = 0;
        }

        public override void Update(GameTime gameTime, Player player)
        {
            StateChangeCheck(player);
            if (dragon.speed.X < 0)
                dragon.enemyRectangle = new Rectangle(dragon.currentFrameX * dragon.frameWidth, 0, dragon.frameWidth, dragon.frameHeight);
            else
                dragon.enemyRectangle = new Rectangle(4 + (4 - dragon.currentFrameX) * dragon.frameWidth, 410, dragon.frameWidth, dragon.frameHeight);
            dragon.Animate(gameTime, 4);
            dragon.standingWaitTime += dragon.timer;
            dragon.enemyTexture.GetData(0, dragon.enemyRectangle, dragon.textureColors, 0, dragon.frameWidth * dragon.frameHeight);
        }

        private void StateChangeCheck(Player player)
        {
            if (WhereIsPlayer(player, 200, 40))
            {
                dragon.state = new DragonAttacking(this);
                dragon.walkingWaitTime = 0;
                dragon.standingWaitTime = 0;
            }
            else if (dragon.standingWaitTime>10000)
            {
                dragon.standingWaitTime = 0;
                dragon.state = new DragonWalking(this);
            }
        }
    }
}
