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

        //Variables used in AI patrol.
        //
        private float standingWaitTime;
        private float attackingWaitTime;

        ///<summary>
        ///The constructor of the Dragon class.
        /// </summary>
        /// <param name="difficulty">The game difficulty.</param>
        /// <param name="position">The position of the enemy on the screen.</param>
        public Dragon(DifficultyLevel difficulty, Vector2 position) 
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
                    speed = new Vector2(2,0);
                    damage = 10;
                    break;
                case DifficultyLevel.hard:
                    health = 200;
                    speed = new Vector2(2.8f, 0);
                    damage = 20;
                    break;
                default:
                    //Something went wrong.
                    break;
            }

        }

        /// <summary>
        ///A method to load dragon's spritesheet called from MainFunction.LoadContent.
        /// </summary>
        /// <param name="content">We need a content parameter from the main because we
        /// want to load the texture in this class.</param>
        public override void LoadContent(ContentManager content)
        {
            enemyTexture = content.Load<Texture2D>("sprites/enemy/dragon");
        }

        /// <summary>
        ///A method to update dragon's parameters called from MainFunction.Update.
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
                    frameWidth = 116;
                    frameHeight = 130;//150 (ignore this comment).
                    enemyRectangle = new Rectangle(9 + currentFrame * frameWidth, 0, frameWidth, frameHeight);
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
                    frameWidth = 132;
                    frameHeight = 120;
                    enemyRectangle = new Rectangle(currentFrame * frameWidth, 150, frameWidth, frameHeight);
                    AnimateWalking(gameTime);
                    position = position + speed;
                    //A patrol AI.
                    //
                    //Is the player near to the dragon;
                    if (Math.Abs(player.position.X - this.position.X) < 200)
                    {
                        //Is the player left to the dragon;
                        if ((player.position.X+(player.rectangle.Width/2) - (this.position.X+(this.enemyRectangle.Width/2))) < 0)
                        {
                            this.speed.X = (-1.0f) * Math.Abs(this.speed.X);//speed must be negative.
                        }
                        //Is the player right to the dragon;
                        else
                        {
                            //In other words speed must be positive.
                            this.speed.X = Math.Abs(this.speed.X);
                        }
                    }
                    //Is the player away from the dragon;
                    else
                    {
                        //Do your regular thing.
                        if (position.X <= 2450 || position.X >= 3000)
                        {
                            //Found a boundary change direction.
                            speed = -speed;
                            enemyState = EnemyState.standing;
                            currentFrame = 0;
                        }
                    }
                    break;
                case EnemyState.attacking:
                    interval = 180;
                    frameWidth = 140;
                    frameHeight = 130;
                    enemyRectangle = new Rectangle(20+currentFrame * frameWidth, 671, frameWidth, frameHeight);
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
            standingWaitTime += timer;
            if (timer > interval)
            {
                if (!right)
                {
                    currentFrame++;
                }
                else
                {
                    currentFrame--;
                }
                timer = 0;
                if (currentFrame > 2)
                {
                    right = true;
                    currentFrame = 1;
                }
                else if (currentFrame <= 0)
                {
                    right = false;
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
                if (currentFrame > 5)
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
            attackingWaitTime += timer;
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
        /// A method to draw the dragon on screen called from MainFunction.Draw.
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
                        spriteBatch.Draw(enemyTexture, position, enemyRectangle, Color.White, rotation, origin, 1f, SpriteEffects.None, 0f);
                    }
                    else
                    {
                        spriteBatch.Draw(enemyTexture, position, enemyRectangle, Color.White, rotation, origin, 1f, SpriteEffects.FlipHorizontally, 0f);
                    }
                    break;
                case EnemyState.walking:
                    if (speed.X > 0)
                    {
                        spriteBatch.Draw(enemyTexture, position, enemyRectangle, Color.White, rotation, origin, 1f, SpriteEffects.FlipHorizontally, 0f);
                    }
                    else
                    {
                        spriteBatch.Draw(enemyTexture, position, enemyRectangle, Color.White, rotation, origin, 1f, SpriteEffects.None, 0f);
                    }
                    break;
                case EnemyState.attacking:
                    if (speed.X > 0)
                    {
                        spriteBatch.Draw(enemyTexture, position, enemyRectangle, Color.White, rotation, origin, 1f, SpriteEffects.None, 0f);
                    }
                    else
                    {
                        spriteBatch.Draw(enemyTexture, position, enemyRectangle, Color.White, rotation, origin, 1f, SpriteEffects.FlipHorizontally, 0f);
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
