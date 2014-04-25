namespace Clumsy_Knight.Enemies
{
    using Microsoft.Xna.Framework;
    class DragonWalking : DragonState
    {
        public DragonWalking(DragonState state)
        {
            this.dragon = state.dragon;
            Initialize();
        }

        public DragonWalking(Dragon dragon)
        {
            this.dragon = dragon;
            Initialize();
        }

        private void Initialize()
        {
            dragon.interval = 150;
            dragon.frameWidth = 132;
            dragon.frameHeight = 120;
            dragon.currentFrameX = 0;
        }

        public override void Update(GameTime gameTime, Player player)
        {
            StateChangeCheck(player);
            if (dragon.speed.X < 0)
                dragon.enemyRectangle = new Rectangle(dragon.currentFrameX * dragon.frameWidth, 150, dragon.frameWidth, dragon.frameHeight);
            else
                dragon.enemyRectangle = new Rectangle((5 - dragon.currentFrameX) * dragon.frameWidth, 553, dragon.frameWidth, dragon.frameHeight);
            dragon.Animate(gameTime, 5);
            dragon.walkingWaitTime += dragon.timer;
            dragon.position += dragon.speed;
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
            else if (dragon.walkingWaitTime>20000)
            {
                dragon.walkingWaitTime = 0;
                dragon.speed = -dragon.speed;
                dragon.state = new DragonStanding(this);
            }
        }
    }
}
