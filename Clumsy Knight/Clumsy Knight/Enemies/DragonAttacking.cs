namespace Clumsy_Knight.Enemies
{
    using Microsoft.Xna.Framework;
    public class DragonAttacking : DragonState
    {
        public DragonAttacking(DragonState state)
        {
            this.dragon = state.dragon;
            Initialize();
        }

        private void Initialize()
        {
            dragon.interval = 180;
            dragon.frameWidth = 140;
            dragon.frameHeight = 125;
            dragon.currentFrameX = 0;
        }

        public override void Update(GameTime gameTime, Player player)
        {
            StateChangeCheck(player);
            if (dragon.speed.X < 0)
                dragon.enemyRectangle = new Rectangle(dragon.currentFrameX * dragon.frameWidth, 280, dragon.frameWidth, dragon.frameHeight);
            else
                dragon.enemyRectangle = new Rectangle((4 - dragon.currentFrameX) * dragon.frameWidth, 680, dragon.frameWidth, dragon.frameHeight);
            dragon.Animate(gameTime, 4);
            dragon.attackingWaitTime += dragon.timer;
            dragon.enemyTexture.GetData(0, dragon.enemyRectangle, dragon.textureColors, 0, dragon.frameWidth * dragon.frameHeight);
        }

        private void StateChangeCheck(Player player)
        {   
             if (dragon.attackingWaitTime>13000)
             {
                dragon.attackingWaitTime = 0;
                dragon.state = new DragonWalking(this);
             }
        }

    }
}
