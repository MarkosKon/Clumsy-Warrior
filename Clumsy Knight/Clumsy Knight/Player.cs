namespace Clumsy_Knight
{
    using Microsoft.Xna.Framework.Graphics;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Input;
    public class Player
    {
        public Texture2D texture;
        public Rectangle rectangle;
        Vector2 origin;

        public Vector2 position;
        public Vector2 speed;

        int currentFrame;
        public int frameWidth;
        public int frameHeight;

        float timer;
        float interval;

        public bool hasJumped;
        public bool isOnLeft;
        public bool isOnRight;
        public bool isHit=false;

        public float health=200;
        public int damage = 4;
        public float score=0;

        Sounds sounds = new Sounds();

        // These state variable are used as a check to avoid initiliazing many times other member variables
        // used for drawing a specific movement from the spritesheet.
        PlayerState newPlayerState;
        PlayerState oldPlayerState;

        public Color[] textureColors;

        public Player(Texture2D texture, Vector2 position, int newFrameWidth, int newFrameHeight)
        {
            origin = new Vector2(0,0);
            speed = new Vector2(0, 0);
            this.texture = texture;
            this.position = position;
            interval = 75;
            currentFrame = 0;
            frameWidth = newFrameWidth;
            frameHeight = newFrameHeight;
            hasJumped = true;
            isOnLeft=true;
            isOnRight=true;
            newPlayerState = PlayerState.standing;
            oldPlayerState = PlayerState.init;
            rectangle = new Rectangle(currentFrame * frameWidth, 0, frameWidth, frameHeight);
            textureColors = new Color[texture.Width * texture.Height];
        }

        public void Update(GameTime gameTime, Map map, Sounds sounds)
        {
            // The animation frames dimensions differ from movement to movement so we specify as origin the bottom left of the frame.
            // By doing this the player will not fall inside the tiles if the frameHeight increases.
            origin = new Vector2(0,frameHeight);
            // According to player state initialize/change some values used by draw.
            //
            switch(newPlayerState)
            {
                case PlayerState.standing:
                    // If player was not standing in the previous update call.
                    if (oldPlayerState!=PlayerState.standing)
                    {
                        interval = 150;
                        frameWidth = 96;
                        frameHeight = 67;
                        currentFrame = 0;
                    }
                    if (speed.X>0)
                    {
                        rectangle = new Rectangle((13-currentFrame) * frameWidth, 420, frameWidth, frameHeight);
                    }
                    else
                    {
                        rectangle = new Rectangle(currentFrame * frameWidth, 9, frameWidth, frameHeight);
                    }
                    Animate(gameTime,13);
                    break;
                case PlayerState.walking:
                    if (oldPlayerState != PlayerState.walking)
                    {
                        interval = 100;
                        frameWidth = 105;
                        frameHeight = 67;
                        currentFrame = 0;
                    }
                    if (speed.X > 0)
                    {
                        rectangle = new Rectangle((5-currentFrame) * frameWidth, 610, frameWidth, frameHeight);
                    }
                    else
                    {
                        rectangle = new Rectangle(currentFrame * frameWidth, 200, frameWidth, frameHeight); 
                    }
                    Animate(gameTime,5);
                    break;
                case PlayerState.attacking:
                    if (oldPlayerState != PlayerState.attacking)
                    {
                        interval = 60;
                        frameWidth = 145;
                        frameHeight = 96;
                        currentFrame = 0;
                    }
                    if (speed.X>0)
                    {
                        rectangle = new Rectangle((5-currentFrame) * frameWidth, 698, frameWidth, frameHeight);
                    }
                    else
                    {
                        rectangle = new Rectangle(currentFrame * frameWidth, 287, frameWidth, frameHeight);
                    }
                    Animate(gameTime,5);
                    break;
            }
            oldPlayerState = newPlayerState;
            // The following code changes the state and member variables of the player. It is focused on the behaviour of
            // the player unlike with the previous tha focuses on the drawing of the player.
            //
            // Check for keyboard input.
            //
            if (Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                newPlayerState = PlayerState.walking;
                speed.X = 3f;
                if (isOnLeft)
                {
                    isOnLeft = false;
                }
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                newPlayerState = PlayerState.walking;
                speed.X = -3f;
                if (isOnRight)
                {
                    isOnRight = false;
                }
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.E))
            {
                newPlayerState = PlayerState.attacking;
                if (speed.X > 0)
                {
                    // We assign small numbers in speed.X instead of 0 because we don't want to change the direction
                    // of the player (Draw method draws according to player's direction).
                    speed.X = 0.00001f;
                }
                else
                {
                    speed.X = -0.00001f;
                }
            }
            else
            {
                newPlayerState = PlayerState.standing;
                if (speed.X > 0)
                {
                    speed.X = 0.00001f;
                }
                else
                {
                    speed.X = -0.00001f;
                }
                isOnLeft = false;
                isOnRight = false;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Space) && hasJumped == false)
            {
                position.Y -= 10;
                speed.Y = -6;
                hasJumped = true;
                //effect.Play();
                sounds.playerJumpingSound.Play();
            }
            if (position.Y >= 480)
            {
                hasJumped = false;
                speed.Y = 0;
            }
            // Check for collisions with the map.
            //
            Rectangle arectangle = new Rectangle((int)position.X, (int)position.Y-frameHeight, frameWidth, frameHeight);
            foreach (Tile tile in map.CollisionMap1)
            {
                if (arectangle.isOnTopOf(tile.Rectangle))
                {
                    speed.Y = 0f;
                    hasJumped = false;
                }
                else if (arectangle.isOnBottomOf(tile.Rectangle))
                {
                    position.Y += 2;
                    speed.Y = 1;
                }
                if (arectangle.isOnLeftOf(tile.Rectangle)&&isOnLeft!=true)
                {
                    isOnLeft = true;
                    speed.X = 0.00001f;
                }
                else if (arectangle.isOnRightOf(tile.Rectangle)&&isOnRight!=true)
                {
                    isOnRight = true;
                    speed.X = -0.00001f;
                }
            }
            // Change the position and the Y speed anyway.
            position += speed;
            speed.Y += 0.15f;
        }

        public void Animate(GameTime gameTime,int targetFrames)
        {
            timer += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            if (timer > interval)
            {
                currentFrame++;
                timer = 0;

                if (currentFrame > targetFrames)
                {
                    currentFrame = 0;
                }
            }
        }


        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, rectangle, Color.White, 0f, origin, 1f, SpriteEffects.None, 0);
        }
    }
}
