﻿using System;
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
        float interval = 75;

        public bool hasJumped;
        public bool isOnLeft;
        public bool isOnRight;
        //KeyboardState oldstate;

        public Player(Texture2D texture, Vector2 position, int newFrameWidth, int newFrameHeight)
        {
            origin = new Vector2(0, 0);
            speed = new Vector2(0, 0);
            this.texture = texture;
            this.position = position;
            currentFrame = 0;
            frameWidth = newFrameWidth;
            frameHeight = newFrameHeight;
            hasJumped = true;
            isOnLeft=true;
            isOnRight=true;
        }

        public void Update(GameTime gameTime,Map map)
        {
            rectangle = new Rectangle(currentFrame * frameWidth, 0, frameWidth, frameHeight);

            if (Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                AnimateRight(gameTime);
                speed.X = 3f;
                if (isOnLeft)
                {
                    isOnLeft = false;
                }
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                AnimateLeft(gameTime);
                speed.X = -3f;
                if (isOnRight)
                {
                    isOnRight = false;
                }
            }
            else
            {
                speed.X = 0f;
                isOnLeft = false;
                isOnRight = false;
            }

            KeyboardState newstate = Keyboard.GetState();
            //if (!oldstate.IsKeyDown(Keys.Space)&& hasJumped == false)
            //{
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
            Rectangle arectangle = new Rectangle((int)position.X, (int)position.Y, frameHeight, frameWidth);
            foreach (BigFatTile tile in map.CollisionMap1)
            {
                if (arectangle.isOnTopOf(tile.Rectangle))
                {
                    speed.Y = 0f;
                    hasJumped = false;
                    //break;
                }
                else if (arectangle.isOnBottomOf(tile.Rectangle))
                {
                    position.Y += 2;
                    speed.Y = 3;
                    //break;
                }
                if (arectangle.isOnLeftOf(tile.Rectangle)&&isOnLeft!=true)
                {
                    isOnLeft = true;
                    speed.X = 0f;
                }
                else if (arectangle.isOnRightOf(tile.Rectangle)&&isOnRight!=true)
                {
                    isOnRight = true;
                    speed.X = 0f;
                }
            }
            position += speed;
            speed.Y += 0.15f;
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
