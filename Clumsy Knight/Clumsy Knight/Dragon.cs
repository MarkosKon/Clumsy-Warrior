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
        ///<summary>
        ///The constructor of the Dragon class.
        /// </summary>
        /// <param name="difficulty">The game difficulty.</param>
        /// <param name="position">The position of the enemy on the screen.</param>
        public Dragon(DifficultyLevel difficulty, Vector2 position) 
        {
            this.difficulty = difficulty;
            this.position = position;
            //Some initialization follows.
            //
            origin = new Vector2(0, 0);
            health = 0;
            interval = 150;
            rotation = 0f;
            frameWidth = 0;
            frameHeight = 0;
            enemyState = EnemyState.walking;
            standingWaitTime = 0;
            walkingWaitTime = 0;
            attackingWaitTime = 0;
            //Initialize members according to the game difficulty.
            switch (this.difficulty)
            {
                case DifficultyLevel.normal:
                    health = 300;
                    speed = new Vector2(2,0);
                    damage = 10;
                    break;
                case DifficultyLevel.hard:
                    health = 500;
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
            textureColors = new Color[enemyTexture.Width * enemyTexture.Height];
        }

        /// <summary>
        ///A method to update dragon's parameters called from MainFunction.Update.
        /// </summary>
        /// <param name="gameTime">A GameTime parameter from the main.</param>
        /// <param name="player">The player object from the main as a parameter
        /// used in AI.</param>
        public override void Update(GameTime gameTime, Player player)
        {
            currentFrameY = 0;
            //Check the state of the dragon mainly for initializing/changing values used for draw,animate.
            //
            switch (enemyState)
            {
                case EnemyState.standing:
                    if (standingWaitTime==0)
                    { 
                        interval = 150;
                        frameWidth = 116;
                        frameHeight = 130;
                        currentFrameX = 0;
                    }
                    else if (standingWaitTime > 10000)
                    {
                        standingWaitTime = 0;
                        enemyState = EnemyState.walking;
                        break;
                    }
                    if (speed.X<0)
                        enemyRectangle = new Rectangle(currentFrameX * frameWidth, 0, frameWidth, frameHeight);
                    else
                        enemyRectangle = new Rectangle(4+(4-currentFrameX) * frameWidth, 410, frameWidth, frameHeight);
                    Animate(gameTime,4);
                    standingWaitTime += timer;
                    break;
                case EnemyState.walking:
                    if (walkingWaitTime==0)
                    { 
                        interval = 150;
                        frameWidth = 132;
                        frameHeight = 120;
                        currentFrameX = 0;
                    }
                    else if (walkingWaitTime>20000)
                    {
                        walkingWaitTime = 0;
                        enemyState = EnemyState.standing;
                        speed = -speed;
                        break;
                    }
                    if (speed.X < 0)
                        enemyRectangle = new Rectangle(currentFrameX * frameWidth, 150, frameWidth, frameHeight);
                    else
                        enemyRectangle = new Rectangle((5-currentFrameX) * frameWidth, 553, frameWidth, frameHeight);
                    Animate(gameTime,5);
                    walkingWaitTime += timer;
                    position = position + speed;
                    break;
                case EnemyState.attacking:
                    if (attackingWaitTime==0)
                    { 
                        interval = 180;
                        frameWidth = 140;
                        frameHeight = 125;
                        currentFrameX = 0;
                    }
                    else if (attackingWaitTime > 13000)
                    {
                        attackingWaitTime = 0;
                        enemyState = EnemyState.walking;
                        break;
                    }
                    if (speed.X<0)
                        enemyRectangle = new Rectangle(currentFrameX * frameWidth, 280, frameWidth, frameHeight);
                    else
                        enemyRectangle = new Rectangle((4-currentFrameX) * frameWidth, 680, frameWidth, frameHeight);
                    Animate(gameTime,4);
                    attackingWaitTime += timer;
                    break;
                default:
                    //Something went wrong.
                    break;
            }
            //The following conditions are considered critical and override the previous if true.
            //
            //Find the centers.
            Vector2 dragonCenter = new Vector2(this.position.X + (this.enemyRectangle.Width / 2), this.position.Y + (this.enemyRectangle.Height / 2));
            Vector2 playerCenter = new Vector2(player.position.X + (player.rectangle.Width / 2), player.position.Y + (player.rectangle.Height / 2));
            //Is the player near to the dragon;
            if (Math.Abs(playerCenter.X - dragonCenter.X) < 200)
            {
                //Is the player left to the dragon;
                if ((playerCenter.X - dragonCenter.X) < 0)
                {
                    //In other words speed must be negative.
                    this.speed.X = (-1.0f) * Math.Abs(this.speed.X);
                }
                //Is the player right to the dragon;
                else
                {
                    //Speed must be negative.
                    this.speed.X = Math.Abs(this.speed.X);
                }
                //If the player is really near and haven't attacked recently, start attacking.
                if (Math.Abs(playerCenter.X - dragonCenter.X) < 40 && attackingWaitTime == 0)
                {
                    enemyState = EnemyState.attacking;
                    walkingWaitTime = 0;
                    standingWaitTime = 0;
                }
            }
        }
    }
}
