namespace Clumsy_Knight.Enemies
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Content;
    using Microsoft.Xna.Framework.Graphics;
    /// <summary>
    /// The orc is a regular monster that doesnt't move but hit hard (:O).
    /// </summary>
    public class Orc : Enemy
    {

        ///<summary>
        ///The constructor of the Orc class.
        /// </summary>
        /// <param name="difficulty">The game difficulty.</param>
        /// <param name="position">The position of the enemy on the screen.</param>
        public Orc(DifficultyLevel difficulty, Vector2 position)
        {
            this.difficulty = difficulty;
            this.position = position;
            //Some initialization follows.
            //
            origin = new Vector2(0, 0);
            health = 0;
            interval = 150;
            rotation = 0f;
            frameWidth = 100;
            frameHeight = 100;
            enemyState = EnemyState.standing;
            speed = new Vector2(1, 0);
            attackingWaitTime = 0;
            currentFrameX = 0;
            currentFrameY = 0;
            switch (this.difficulty)
            {
                case DifficultyLevel.normal:
                    health = 200;
                    damage = 30;
                    break;
                case DifficultyLevel.hard:
                    health = 300;
                    damage = 40;
                    break;
                default:
                    //Something went wrong.
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
        ///A method to update orc's parameters called from MainFunction.Update.
        /// </summary>
        /// <param name="gameTime">A GameTime parameter from the main.</param>
        /// <param name="player">The player object from the main as a parameter
        /// used in AI.</param>
        public override void Update(GameTime gameTime, Player player)
        {
            //The default state is standing. If the player is really near, the state will change for a limited
            //time to attacking and will go back to standing to check again how close is the player.
            switch (enemyState)
            {
                case EnemyState.standing:
                    if (speed.X>0)
                    {
                        enemyRectangle = new Rectangle(600, 300, frameWidth, frameHeight);
                    }
                    else
                    {
                        enemyRectangle = new Rectangle(0, 0, frameWidth, frameHeight);
                    }
                    WhereIsPlayer(player, 120, 80);
                    break;
                case EnemyState.attacking:
                    if (attackingWaitTime == 0)
                    {
                        currentFrameX = 0;
                        currentFrameY = 0;
                    }
                    if(speed.X>0)
                    {
                        enemyRectangle = new Rectangle((6-currentFrameX) * frameWidth, (3+currentFrameY) * frameHeight, frameWidth, frameHeight);
                    }
                    else
                    {
                        enemyRectangle = new Rectangle(currentFrameX * frameWidth, currentFrameY * frameHeight, frameWidth, frameHeight);
                    }
                    Animate(gameTime,6);
                    attackingWaitTime += timer;
                    if (attackingWaitTime > 12000)
                    {
                        attackingWaitTime = 0;
                        enemyState = EnemyState.standing;
                    }
                    break;
                default:
                    //Something wrong.
                    break;
            }
        }
    }
}
