namespace Clumsy_Knight
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Content;
    using Microsoft.Xna.Framework.Graphics;
    using System;

    /// <summary>
    /// The Skeleton class that inherits from the abstract Enemy
    /// class. Skeleton is a regular monster.
    /// </summary>
    public class Skeleton : Enemy
    {
        //Variables used to switch between states.
        //
        private float standingWaitTime;
        private float attackingWaitTime;
        private float walkingWaitTime;

        ///<summary>
        ///The constructor of the Skeleton class.
        /// </summary>
        /// <param name="difficulty">The game difficulty.</param>
        /// <param name="position">The position of the enemy on the screen.</param>
        public Skeleton(DifficultyLevel difficulty, Vector2 position) 
        {
            this.difficulty = difficulty;
            this.position = position;
            //Some initialization follows.
            //
            origin = new Vector2(0, 0);
            health = 0;
            right = false;
            interval = 150;
            rotation = 0f;
            frameWidth = 0;
            frameHeight = 0;
            enemyState = EnemyState.walking;
            currentFrame = 0;
            standingWaitTime = 0;
            walkingWaitTime = 0;
            attackingWaitTime = 0;
            //Initialize members according to the game difficulty.
            switch (this.difficulty)
            {
                case DifficultyLevel.normal:
                    health = 100;
                    speed = new Vector2(1.5f, 0);
                    damage = 10;
                    break;
                case DifficultyLevel.hard:
                    health = 200;
                    speed = new Vector2(2, 0);
                    damage = 15;
                    break;
                default:
                    //Something went wrong.
                    break;
            }

        }

        /// <summary>
        /// A method to load skeleton's spritesheet called from MainFunction.LoadContent.
        /// </summary>
        /// <param name="content">We need a content parameter from the main because we
        /// want to load the texture in this class.</param>
        public override void LoadContent(ContentManager content)
        {
            enemyTexture = content.Load<Texture2D>("sprites/enemy/skeleton");
        }

        /// <summary>
        ///A method to update skeleton's parameters called from MainFunction.Update.
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
                    //If skeleton was not standing recently initialize some values.
                    if (standingWaitTime==0)
                    {
                        interval = 150;
                        frameWidth = 44;
                        frameHeight = 94;
                        currentFrame = 0;
                    }
                    //If skeleton had been standing for a certain time, start walking.
                    else if (standingWaitTime > 10000)
                    {
                        standingWaitTime = 0;
                        enemyState = EnemyState.walking;
                        break;
                    }
                    enemyRectangle = new Rectangle(32 + currentFrame * frameWidth, 11, frameWidth, frameHeight);
                    AnimateStanding(gameTime);
                    break;
                case EnemyState.walking:
                    position = position + speed;
                    //If skeleton was not walking recently initialize some values.
                    if (walkingWaitTime == 0)
                    {
                        interval = 100;
                        frameWidth = 53;
                        frameHeight = 94;
                        currentFrame = 0;
                    }
                    else if (walkingWaitTime>15000)
                    {
                        walkingWaitTime = 0;
                        enemyState = EnemyState.standing;
                        speed = -speed;
                        break;
                    }
                    enemyRectangle = new Rectangle(58 + currentFrame * frameWidth, 108, frameWidth, frameHeight);
                    AnimateWalking(gameTime);
                    break;
                case EnemyState.attacking:
                    //If skeleton was not attacking recently initialize some values.
                    if (attackingWaitTime==0)
                    { 
                        interval = 200;
                        frameWidth = 86;
                        frameHeight = 94;
                        currentFrame = 0;
                    }
                    //If skeleton has been attacking recently, stops and stands for a while.
                    else if (attackingWaitTime > 5000)
                    {
                        attackingWaitTime = 0;
                        enemyState = EnemyState.standing;
                        break;
                    }
                    enemyRectangle = new Rectangle(6+currentFrame * frameWidth, 203, frameWidth, frameHeight);
                    AnimateAttacking(gameTime);
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
            //The following conditions are considered critical and override the previous if true.
            //
            //Find the centers.
            Vector2 skeletonCenter = new Vector2(this.position.X + (this.enemyRectangle.Width / 2), this.position.Y + (this.enemyRectangle.Height / 2));
            Vector2 playerCenter = new Vector2(player.position.X + (player.rectangle.Width / 2), player.position.Y + (player.rectangle.Height / 2));
            //Is the player near to the skeleton;
            if (Math.Abs(playerCenter.X - skeletonCenter.X) < 100)
            {
                //Is the player left to the skeleton;
                if ((playerCenter.X - skeletonCenter.X) < 0)
                {
                    //In other words, speed must be negative.
                    this.speed.X = (-1.0f) * Math.Abs(this.speed.X);
                }
                //Is the player right to the skeleton;
                else
                {
                    //Speed must be positive.
                    this.speed.X = Math.Abs(this.speed.X);
                }
                //If the player is really near and haven't attacked recently, start attacking.
                if (Math.Abs(playerCenter.X - skeletonCenter.X) < 10&&attackingWaitTime==0)
                {
                    enemyState = EnemyState.attacking;
                    walkingWaitTime = 0;
                    standingWaitTime = 0;
                }
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
            walkingWaitTime += timer;
            if (timer > interval)
            {
                currentFrame++;
                timer = 0;
                if (currentFrame > 7)
                {
                    currentFrame = 0;
                }
            }
        }

        /// <summary>
        /// This method "guides" the Draw method for the attacking animation.
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
        /// A method to draw the skeleton on screen called from MainFunction.Draw.
        /// </summary>
        /// <param name="spriteBatch">We give spriteBatch as parameter because the current class
        /// don't know anything about it.</param>
        public override void Draw(SpriteBatch spriteBatch) 
        {
            //Checks the enemy's direction and draws the texture accordingly.
            if (speed.X > 0)
            {
                spriteBatch.Draw(enemyTexture, position, enemyRectangle, Color.White, rotation, origin, 1f, SpriteEffects.None, 0f);
            }
            else
            {
                spriteBatch.Draw(enemyTexture, position, enemyRectangle, Color.White, rotation, origin, 1f, SpriteEffects.FlipHorizontally, 0f);
            }
        }
    }
}
