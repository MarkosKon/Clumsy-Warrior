namespace Clumsy_Knight
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Content;
    using Microsoft.Xna.Framework.Graphics;
    using System;
    /// <summary>
    /// The orc is a regular monster that doesnt't move but hit hard (:O).
    /// </summary>
    public class Orc : Enemy
    {
        private float attackingWaitTime;
        private int currentFrameY;

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
            right = false;
            enemyState = EnemyState.standing;
            speed = new Vector2(1, 0);
            attackingWaitTime = 0;
            currentFrame = 0;
            currentFrameY = 0;
            switch (this.difficulty)
            {
                case DifficultyLevel.normal:
                    health = 200;
                    damage = 20;
                    break;
                case DifficultyLevel.hard:
                    health = 300;
                    damage = 30;
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
            switch (enemyState)
            {
                case EnemyState.standing:
                    interval = 150;
                    frameWidth = 100;
                    frameHeight = 100;
                    enemyRectangle = new Rectangle(0, 0, frameWidth, frameHeight);
                    //Find the centers.
                    Vector2 orcCenter=new Vector2(this.position.X + (this.enemyRectangle.Width / 2),this.position.Y + (this.enemyRectangle.Height / 2));
                    Vector2 playerCenter = new Vector2(player.position.X + (player.rectangle.Width / 2), player.position.Y + (player.rectangle.Height / 2));
                    //Is the player near to the orc;
                    if (Math.Abs(playerCenter.X - orcCenter.X) < 120)
                    {
                        //Is the player left to the orc;
                        if ((playerCenter.X - orcCenter.X) < 0)
                        {
                            //In other words, speed must be negative.
                            this.speed.X = (-1.0f) * Math.Abs(this.speed.X);
                        }
                        //Is the player right to the orc;
                        else
                        {
                            //Speed must be positive.
                            this.speed.X = Math.Abs(this.speed.X);
                        }
                        //If the player is really near, start attacking.
                        if (Math.Abs(playerCenter.X - orcCenter.X) < 70)
                        {
                            enemyState = EnemyState.attacking;
                            currentFrame = 0;
                            currentFrameY = 0;
                            timer = 0;
                            attackingWaitTime = 0;
                        }
                    }
                    break;
                case EnemyState.attacking:
                    interval = 150;
                    frameWidth = 100;
                    frameHeight = 100;
                    enemyRectangle = new Rectangle(currentFrame * frameWidth, currentFrameY*frameHeight, frameWidth, frameHeight);
                    AnimateAttacking(gameTime);
                    if (attackingWaitTime > 15000)
                    {
                        attackingWaitTime = 0;
                        enemyState = EnemyState.standing;
                    }
                    break;
                default:
                    //Something wrong.
                    break;
            }
            if (health<=0)
            {
                isVisible = false;
            }
            enemyTexture.GetData(0, enemyRectangle, textureColors, currentFrame*currentFrameY, frameHeight * frameWidth);
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
                if (currentFrame > 6)
                {
                    currentFrame = 0;
                    currentFrameY++;
                    if (currentFrameY>2)
                    {
                        currentFrameY = 0;
                    }
                }
            }
        }

        /// <summary>
        /// A method to draw the orc on screen called from MainFunction.Draw.
        /// </summary>
        /// <param name="spriteBatch">We give spriteBatch as parameter because the current class
        /// don't know anything about it.</param>
        public override void Draw(SpriteBatch spriteBatch)
        {
            if (isVisible)
            { 
                if (speed.X>0)
                {
                    spriteBatch.Draw(enemyTexture, position, enemyRectangle, Color.White, rotation, origin, 1f, SpriteEffects.FlipHorizontally, 0f);
                }
                else
                {
                    spriteBatch.Draw(enemyTexture, position, enemyRectangle, Color.White, rotation, origin, 1f, SpriteEffects.None, 0f);
                }
            }
        }
    }
}
