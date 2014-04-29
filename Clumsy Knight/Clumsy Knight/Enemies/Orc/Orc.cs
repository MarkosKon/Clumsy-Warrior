namespace Clumsy_Knight
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Content;
    using Microsoft.Xna.Framework.Graphics;
    /// <summary>
    /// The orc is a regular monster that doesnt't move but hit hard (:O).
    /// </summary>
    public class Orc : Enemy
    {

        /// <summary>
        /// The constructor of the Orc class.
        /// </summary>
        /// <param name="difficulty">The game difficulty.</param>
        /// <param name="position">The position of the enemy on the screen.</param>
        public Orc(DifficultyLevel difficulty, Vector2 position)
        {
            this.difficulty = difficulty;
            this.position = position;
            // Some initialization follows.
            //
            origin = new Vector2(0, 0);
            health = 0;
            rotation = 0f;
            state = new OrcStanding(this);
            speed = new Vector2(1, 0);
            attackingWaitTime = 0;
            switch (this.difficulty)
            {
                case DifficultyLevel.normal:
                    health = 200;
                    damage = 6;
                    break;
                case DifficultyLevel.hard:
                    health = 300;
                    damage = 8;
                    break;
                default:
                    // Something went wrong.
                    break;
            }
        }

        /// <summary>
        /// A method to load orc's spritesheet called from MainFunction.LoadContent.
        /// </summary>
        /// <param name="content">We need a content parameter from the main because we
        /// want to load the texture in this class.</param>
        public override void LoadContent(ContentManager content)
        {
            enemyTexture = content.Load<Texture2D>("sprites/enemy/orc");
            textureColors = new Color[enemyTexture.Width * enemyTexture.Height];
        }

        /// <summary>
        /// A method to update orc's parameters called from MainFunction.Update.
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
