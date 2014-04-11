namespace Clumsy_Knight
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Content;
    using Microsoft.Xna.Framework.Graphics;
    //A Simple comment.

    /// <summary>
    /// The Skeleton class that inherits from the abstract Enemy
    /// class. Skeleton will be a regular monster.
    /// </summary>
    public class Skeleton : Enemy
    {
        //Variables used in AI patrol.
        //
        private float standingWaitTime;
        private float attackingWaitTime;

        ///<summary>
        ///The constructor of the Skeleton class.
        /// </summary>
        /// <param name="difficulty">The game difficulty.</param>
        /// <param name="position">The position of the enemy on the screen.</param>
        public Skeleton(DifficultyLevel difficulty, Vector2 position) 
        {
            //Some initialization follows.
            //
            origin = new Vector2(0, 0);
            this.difficulty = difficulty;
            this.position = position;
            health = 0;
            right = false;
            interval = 150;
            rotation = 0f;
            frameWidth = 0;
            frameHeight = 0;
            enemyState = EnemyState.walking;
            standingWaitTime = 0;
            //Initialize members according to the game difficulty.
            switch (this.difficulty)
            {
                case DifficultyLevel.normal:
                    health = 100;
                    speed = new Vector2(3, 0);
                    damage = 10;
                    break;
                case DifficultyLevel.hard:
                    health = 200;
                    speed = new Vector2(5, 0);
                    damage = 20;
                    break;
                default:
                    //Something went wrong.
                    break;
            }

        }

        /// <summary>
        /// This method is called from the Game's LoadContent method 
        /// (Eventually will be called from the "Gamescreen").
        /// </summary>
        /// <param name="content">We need a content parameter from the main because we
        /// want to load the texture in this class.</param>
        public override void LoadContent(ContentManager content)
        {
            enemyTexture = content.Load<Texture2D>("sprites/enemy/skeleton");
        }

        /// <summary>
        ///This method is called from the Game's Update method
        ///(Eventually will be called from the "Gamescreen").
        /// </summary>
        /// <param name="gameTime">A GameTime parameter from the main.</param>
        /// <param name="player">The player object from the main as a parameter
        /// used in AI.</param>
        public override void Update(GameTime gameTime, Player player)
        {
            //What is the state of the enemy;
            switch (enemyState)
            {
                case EnemyState.standing:
                    interval = 150;
                    frameWidth = 44;
                    frameHeight = 72;
                    enemyRectangle = new Rectangle(32 + currentFrame * frameWidth, 48, frameWidth, frameHeight);
                    AnimateStanding(gameTime);
                    //A patrol AI.
                    if (standingWaitTime > 4000)
                    {
                        standingWaitTime = 0;
                        enemyState = EnemyState.attacking;
                    }
                    break;
                case EnemyState.walking:
                    interval = 150;
                    frameWidth = 48;
                    frameHeight = 72;
                    enemyRectangle = new Rectangle(10 + currentFrame * frameWidth, 126, frameWidth, frameHeight);
                    AnimateWalking(gameTime);
                    position = position + speed;
                    //A patrol AI.
                    //
                    //Do your regular thing.
                    if (position.X <= 350 || position.X >= 720)
                    {
                        //Found a boundary change direction.
                        speed = -speed;
                        enemyState = EnemyState.standing;
                        currentFrame = 0;
                    }
                    break;
                case EnemyState.attacking:
                    interval = 180;
                    frameWidth = 86;
                    frameHeight = 98;
                    enemyRectangle = new Rectangle(7+currentFrame * frameWidth, 203, frameWidth, frameHeight);
                    AnimateAttacking(gameTime);
                    //A patrol AI.
                    if (attackingWaitTime > 13000)
                    {
                        attackingWaitTime = 0;
                        enemyState = EnemyState.walking;
                    }
                    break;
                case EnemyState.takingDamage:
                    frameWidth = 110;
                    frameHeight = 100;
                    enemyRectangle = new Rectangle(30, 802, frameWidth, frameHeight);
                    break;
                default:
                    //Something went wrong.
                    break;
            }
        }

        /// <summary>
        /// This method "guides" the Draw method for the standing animation.
        /// </summary>
        /// <param name="gameTime">We need a GameTime parameter from the main because we
        /// want to animate for a specific time.</param>
        public override void AnimateStanding(GameTime gameTime)
        {
            timer += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            standingWaitTime += timer;//a patrol AI.
            if (timer > interval)
            {
                currentFrame++;
                timer = 0;
                if (currentFrame > 4)
                {
                    currentFrame = 0;
                }
            }
        }

        /// <summary>
        /// This method "guides" the Draw method for the walking animation.
        /// </summary>
        /// <param name="gameTime">We need a GameTime parameter from the main because we
        /// want to animate for a specific time.</param>
        public override void AnimateWalking(GameTime gameTime)
        {
            timer += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            if (timer > interval)
            {
                currentFrame++;
                timer = 0;
                if (currentFrame > 8)
                {
                    currentFrame = 0;
                }
            }
        }

        /// <summary>
        /// This method "guides" the Draw method for the standing attacking.
        /// </summary>
        /// <param name="gameTime">We need a GameTime parameter from the main because we
        /// want to animate for a specific time.</param>
        public override void AnimateAttacking(GameTime gameTime)
        {
            timer += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            attackingWaitTime += timer;//a patrol AI.
            if (timer > interval)
            {
                currentFrame++;
                timer = 0;
                if (currentFrame > 4)
                {
                    currentFrame = 0;
                }
            }
        }

        /// <summary>
        /// This method is called from the Game's Draw method
        /// (Eventually will be called from the "Gamescreen").
        /// </summary>
        /// <param name="spriteBatch">We give spriteBatch as parameter because the current class
        /// don't know anything about it.</param>
        public override void Draw(SpriteBatch spriteBatch) 
        {
            //What is the state of the enemy;                          
            switch (enemyState)
            {
                case EnemyState.standing:
                    //Checks the enemy's direction and draws the texture accordingly.
                    if (speed.X > 0)
                    {
                        spriteBatch.Draw(enemyTexture, position, enemyRectangle, Color.White, rotation, origin, 1f, SpriteEffects.FlipHorizontally, 0f);
                    }
                    else
                    {
                        spriteBatch.Draw(enemyTexture, position, enemyRectangle, Color.White, rotation, origin, 1f, SpriteEffects.None, 0f);
                    }
                    break;
                case EnemyState.walking:
                    if (speed.X > 0)
                    {
                        spriteBatch.Draw(enemyTexture, position, enemyRectangle, Color.White, rotation, origin, 1f, SpriteEffects.None, 0f);
                    }
                    else
                    {
                        spriteBatch.Draw(enemyTexture, position, enemyRectangle, Color.White, rotation, origin, 1f, SpriteEffects.FlipHorizontally, 0f);
                    }
                    break;
                case EnemyState.attacking:
                    if (speed.X > 0)
                    {
                        spriteBatch.Draw(enemyTexture, position, enemyRectangle, Color.White, rotation, origin, 1f, SpriteEffects.FlipHorizontally, 0f);
                    }
                    else
                    {
                        spriteBatch.Draw(enemyTexture, position, enemyRectangle, Color.White, rotation, origin, 1f, SpriteEffects.None, 0f);
                    }
                    break;
                case EnemyState.takingDamage:
                    if (speed.X > 0)
                    {
                        spriteBatch.Draw(enemyTexture, position, enemyRectangle, Color.Red, rotation, origin, 1f, SpriteEffects.FlipHorizontally, 0f);
                    }
                    else
                    {
                        spriteBatch.Draw(enemyTexture, position, enemyRectangle, Color.Red, rotation, origin, 1f, SpriteEffects.None, 0f);
                    }
                    break;
                default:
                    //Something went wrong.
                    break;
            }
        }
    }
}
