using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Clumsy_Knight
{
    public class Player
    {
        Texture2D texture;
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

        public float health=100;
        public float score=0;

        //KeyboardState oldstate;

        PlayerState newPlayerState;
        PlayerState oldPlayerState;

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
            //I added this line because we need it in background initialization.
            rectangle = new Rectangle(currentFrame * frameWidth, 0, frameWidth, frameHeight);
        }

        public void Update(GameTime gameTime,Map map)
        {
            origin = new Vector2(0,frameHeight);
            switch(newPlayerState)
            {
                case PlayerState.standing:
                    if (oldPlayerState!=PlayerState.standing)
                    {
                        interval = 150;
                        frameWidth = 96;
                        frameHeight = 67;
                        currentFrame = 0;
                    }
                    rectangle = new Rectangle(currentFrame * frameWidth, 9, frameWidth, frameHeight);
                    AnimateStanding(gameTime);
                    break;
                case PlayerState.walking:
                    if (oldPlayerState != PlayerState.walking)
                    {
                        interval = 100;
                        frameWidth = 105;
                        frameHeight = 67;
                        currentFrame = 0;
                    }
                    rectangle = new Rectangle(currentFrame * frameWidth, 200, frameWidth, frameHeight);
                    AnimateWalking(gameTime);
                    break;
                case PlayerState.attacking:
                    if (oldPlayerState != PlayerState.attacking)
                    {
                        interval = 100;
                        frameWidth = 145;
                        frameHeight = 96;
                        currentFrame = 0;
                    }
                    rectangle = new Rectangle(currentFrame * frameWidth, 287, frameWidth, frameHeight);
                    AnimateAttacking(gameTime);
                    break;
            }
            oldPlayerState = newPlayerState;
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

            KeyboardState newstate = Keyboard.GetState();

            if (newstate.IsKeyDown(Keys.Space) && hasJumped == false)
            {
                position.Y -= 10;
                speed.Y = -5;
                hasJumped = true;
            }
            if (position.Y >= 480)
            {
                hasJumped = false;
                speed.Y = 0;
            }
            Rectangle arectangle = new Rectangle((int)position.X, (int)position.Y-frameHeight, frameWidth, frameHeight);
            foreach (BigFatTile tile in map.CollisionMap1)
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
            position += speed;
            speed.Y += 0.15f;
        }

        public void AnimateStanding(GameTime gameTime)
        {
            timer += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            if (timer > interval)
            {
                currentFrame++;
                timer = 0;

                //3 is full
                if (currentFrame > 14)
                {
                    currentFrame = 0;
                }
            }
        }

        public void AnimateWalking(GameTime gameTime)
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

        public void AnimateAttacking(GameTime gameTime)
        {
            timer += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            if (timer > interval)
            {
                currentFrame++;
                timer = 0;

                //3 is full
                if (currentFrame > 5)
                {
                    currentFrame = 0;
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (speed.X > 0)
            {
                spriteBatch.Draw(texture, position, rectangle, Color.White, 0f, origin, 1f, SpriteEffects.FlipHorizontally, 0);
            }
            else
            {
                spriteBatch.Draw(texture, position, rectangle, Color.White, 0f, origin, 1f, SpriteEffects.None, 0);
            }
        }

        enum PlayerState
        {
            walking,
            standing,
            attacking,
            init
        }
    }
}
