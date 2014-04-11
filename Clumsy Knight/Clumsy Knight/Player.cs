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
        public Vector2 velocity;

        int currentFrame;
        public int frameWidth;
        public int frameHeight;

        float timer;
        float interval = 75;

        public bool hasJumped;
        //KeyboardState oldstate;

        public Player(Texture2D texture, Vector2 position, int newFrameWidth, int newFrameHeight)
        {
            origin = new Vector2(0, 0);
            velocity = new Vector2(0, 0);
            this.texture = texture;
            this.position = position;
            currentFrame = 0;
            frameWidth = newFrameWidth;
            frameHeight = newFrameHeight;
            hasJumped = true;
        }

        public void Update(GameTime gameTime)
        {
            position += velocity;
            //rectangle = new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);

            rectangle = new Rectangle(currentFrame * frameWidth, 0, frameWidth, frameHeight);


            //position = position + velocity;

            if (Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                AnimateRight(gameTime);
                velocity.X = 3f;
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                AnimateLeft(gameTime);
                velocity.X = -3f;
            }
            else
            {
                velocity.X = 0f;
            }

            KeyboardState newstate = Keyboard.GetState();
            //if (!oldstate.IsKeyDown(Keys.Space)&& hasJumped == false)
            //{
            if (newstate.IsKeyDown(Keys.Space) && hasJumped == false)
            {
                position.Y -= 10;
                velocity.Y = -5;
                hasJumped = true;
            }
            if (position.Y >= 480)
            {
                hasJumped = false;
                velocity.Y = 0;
            }
            velocity.Y += 0.15f;
           // }
            //oldstate = newstate;
            
            //Gravity it will fall faster and faster.
                /*if (hasJumped == true)
                {
                    float i = 1;
                    velocity.Y += 0.15f * i;
                }

                if (hasJumped == false)
                {
                    //float i = 1;
                    //velocity.Y += 0.15f * i;
                    velocity.Y = 0;
                }
                 */
        }

        public void AnimateRight(GameTime gameTime)
        {
            timer += (float)gameTime.ElapsedGameTime.TotalMilliseconds / 2;
            if (timer > interval)
            {
                currentFrame++;
                timer = 0;

                //3 is full
                if (currentFrame > 3)
                {
                    currentFrame = 0;
                }
            }
        }

        public void AnimateLeft(GameTime gameTime)
        {
            timer += (float)gameTime.ElapsedGameTime.TotalMilliseconds / 2;
            if (timer > interval)
            {
                currentFrame++;
                timer = 0;

                //7 is full
                if (currentFrame > 7 || currentFrame < 4)
                {
                    currentFrame = 4;
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, rectangle, Color.White, 0f, origin, 1.0f, SpriteEffects.None, 0);
        }
    }
}
