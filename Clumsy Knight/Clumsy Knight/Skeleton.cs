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
            interval = 150;
            rotation = 0f;
            frameWidth = 0;
            frameHeight = 0;
            enemyState = EnemyState.walking;
            currentFrameX = 0;
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
            textureColors = new Color[enemyTexture.Width * enemyTexture.Height];
        }

        /// <summary>
        ///A method to update skeleton's parameters called from MainFunction.Update.
        /// </summary>
        /// <param name="gameTime">A GameTime parameter from the main.</param>
        /// <param name="player">The player object from the main as a parameter
        /// used in AI.</param>
        public override void Update(GameTime gameTime, Player player)
        {
            currentFrameY = 0;
            //Check the state of the skeleton mainly for initializing/changing values used for draw,animate.
            //
            switch (enemyState)
            {
                case EnemyState.walking:
                    position = position + speed;
                    //If skeleton was not walking recently initialize some values.
                    if (walkingWaitTime == 0)
                    {
                        interval = 100;
                        frameWidth = 53;
                        frameHeight = 94;
                        currentFrameX = 0;
                    }
                    else if (walkingWaitTime > 15000)
                    {
                        walkingWaitTime = 0;
                        enemyState = EnemyState.standing;
                        break;
                    }
                    if (speed.X<0)
                        enemyRectangle = new Rectangle((8 - currentFrameX) * frameWidth, 400, frameWidth, frameHeight);
                    else
                        enemyRectangle = new Rectangle(58 + currentFrameX * frameWidth, 108, frameWidth, frameHeight);
                    Animate(gameTime, 7);
                    walkingWaitTime += timer;
                    break;
                case EnemyState.standing:
                    //If skeleton was not standing recently initialize some values.
                    if (standingWaitTime==0)
                    {
                        interval = 150;
                        frameWidth = 44;
                        frameHeight = 94;
                        currentFrameX = 0;
                    }
                    //If skeleton had been standing for a certain time, start walking.
                    else if (standingWaitTime > 10000)
                    {
                        standingWaitTime = 0;
                        speed = -speed;
                        enemyState = EnemyState.walking;
                        break;
                    }
                    if (speed.X<0)
                        enemyRectangle = new Rectangle(32 + currentFrameX * frameWidth, 11, frameWidth, frameHeight);
                    else
                        enemyRectangle = new Rectangle(-32+(5-currentFrameX) * frameWidth, 305, frameWidth, frameHeight);
                    Animate(gameTime,4);
                    standingWaitTime += timer;
                    break;
                case EnemyState.attacking:
                    //If skeleton was not attacking recently initialize some values.
                    if (attackingWaitTime==0)
                    { 
                        interval = 200;
                        frameWidth = 86;
                        frameHeight = 94;
                        currentFrameX = 0;
                    }
                    //If skeleton has been attacking recently, stops and stands for a while.
                    else if (attackingWaitTime > 4000)
                    {
                        attackingWaitTime = 0;
                        enemyState = EnemyState.standing;
                        break;
                    }
                    if (speed.X>0)
                        enemyRectangle = new Rectangle(6 + currentFrameX * frameWidth, 203, frameWidth, frameHeight);
                    else
                        enemyRectangle = new Rectangle((3-currentFrameX) * frameWidth, 496, frameWidth, frameHeight);
                    Animate(gameTime,3);
                    attackingWaitTime += timer;
                    break;
                default:
                    //Something went wrong.
                    break;
            }
            WhereIsPlayer(player, 100, 30);
        }
    }
}
