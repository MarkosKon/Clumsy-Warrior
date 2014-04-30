namespace Clumsy_Knight
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Content;
    using Microsoft.Xna.Framework.Graphics;
    using System;

    /// <summary>
    /// The Dragon class that inherits from the abstract Enemy
    /// class. Dragon is used as a boss.
    /// </summary>
    public class Dragon :  Enemy 
    {
        /// <summary>
        /// The constructor of the Dragon class.
        /// </summary>
        /// <param name="difficulty">The game difficulty.</param>
        /// <param name="position">The position of the enemy on the screen.</param>
        public Dragon(DifficultyLevel difficulty, Vector2 position) 
        {
            this.difficulty = difficulty;
            this.position = position;
            // Some initialization follows.
            //
            origin = new Vector2(0, 0);
            health = 0;
            //interval = 150;
            rotation = 0f;
            //frameWidth = 0;
            //frameHeight = 0;
            //enemyState = EnemyState.walking;
            state = new DragonWalking(this);
            standingWaitTime = 0;
            walkingWaitTime = 0;
            attackingWaitTime = 0;
            // Initialize members according to the game difficulty.
            switch (this.difficulty)
            {
                case DifficultyLevel.normal:
                    health = 300;
                    speed = new Vector2(2,0);
                    damage = 2;
                    break;
                case DifficultyLevel.hard:
                    health = 500;
                    speed = new Vector2(2.8f, 0);
                    damage = 4;
                    break;
                default:
                    throw new NotImplementedException
                            ("Unrecognized game difficulty value.");
            }

        }

        /// <summary>
        /// A method to load dragon's spritesheet called from MainFunction.LoadContent.
        /// </summary>
        /// <param name="content">We need a content parameter from the main because we
        /// want to load the texture in this class.</param>
        public override void LoadContent(ContentManager content)
        {
            enemyTexture = content.Load<Texture2D>("sprites/enemy/dragon");
            textureColors = new Color[enemyTexture.Width * enemyTexture.Height];
        }

        /// <summary>
        /// A method to update dragon's parameters called from MainFunction.Update.
        /// </summary>
        /// <param name="gameTime">A GameTime parameter from the main.</param>
        /// <param name="player">The player object from the main as a parameter
        /// used in AI.</param>
        public override void Update(GameTime gameTime, Player player)
        {
            state.Update(gameTime, player);
        }
    }
}
