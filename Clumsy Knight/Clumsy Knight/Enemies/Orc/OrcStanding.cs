namespace Clumsy_Knight
{
    using Microsoft.Xna.Framework;
    /// <summary>
    /// A concrete state.
    /// </summary>
    class OrcStanding : EnemyState
    {
        public OrcStanding(EnemyState state)
        {
            this.enemy = state.enemy;
            Initialize();
        }

        /// <summary>
        /// This concrete state is the default state and has two constructors
        /// for the initialization from the orc constructor.
        /// </summary>
        /// <param name="orc">Orc object passed from the orc contructor.</param>
        public OrcStanding(Orc orc)
        {
            this.enemy = orc;
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
                enemy.enemyRectangle = new Rectangle(600, 300, frameWidth, frameHeight);
            else
                enemy.enemyRectangle = new Rectangle(0, 0, frameWidth, frameHeight);
            enemy.enemyTexture.GetData(0, enemy.enemyRectangle, enemy.textureColors, 0, frameWidth * frameHeight);
            StateChangeCheck(player);
        }

        private void StateChangeCheck(Player player)
        {
            if (WhereIsPlayer(player, 120, 80))
            {
                enemy.state = new OrcAttacking(this);
                enemy.standingWaitTime = 0;
            }
        }
    }
}
