namespace Clumsy_Knight
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Content;
    using Microsoft.Xna.Framework.Graphics;
    using Microsoft.Xna.Framework.Input;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// A background class used for drawing a dynamic 
    /// background that changes according to player's movement.
    /// </summary>
    public class Background
    {
        private List<BackgroundObject> objects=new List<BackgroundObject>();

        // Used to place randomly the clouds if they disappear from the screen.
        Random rnd;
        
        /// <summary>
        /// The Constructor.
        /// </summary>
        /// <param name="player">A Player parameter from MainFunction.</param>
        /// <param name="contentManager">A ContentManager parameter.</param>
        public Background(float playerPositionX, ContentManager contentManager)
        {
            // Center vector calculates how much the camera will move everything to left or right.
            Vector2 center = new Vector2(playerPositionX - 400, 0);

            rnd=new Random();

            // Place everything according to player's position and to camera.
            objects.Add(new BackgroundObject(contentManager.Load<Texture2D>("sprites/background/sky/sky"), new Rectangle(0 , 0, 800, 480), new Vector2(center.X, 0)));
            objects.Add(new BackgroundObject(contentManager.Load<Texture2D>("sprites/background/sun/sun"), new Rectangle(0, 0, 100, 100), new Vector2(center.X + 3, 0)));
            objects.Add(new BackgroundObject(contentManager.Load<Texture2D>("sprites/background/mountain/mountain"), new Rectangle(0, 0, 498, 247), new Vector2(center.X + 100, center.Y + 100)));
            objects.Add(new BackgroundObject(contentManager.Load<Texture2D>("sprites/background/clouds/cloud1"), new Rectangle(0, 0, 120, 60), new Vector2(rnd.Next((int)center.X - 30, (int)center.X + 670), rnd.Next((int)center.Y, (int)center.Y + 200))));
            objects.Add(new BackgroundObject(contentManager.Load<Texture2D>("sprites/background/clouds/cloud2"), new Rectangle(0, 0, 150, 83), new Vector2(rnd.Next((int)center.X - 30, (int)center.X + 670), rnd.Next((int)center.Y, (int)center.Y + 200))));
            objects.Add(new BackgroundObject(contentManager.Load<Texture2D>("sprites/background/clouds/cloud3"), new Rectangle(0, 0, 85, 48), new Vector2(rnd.Next((int)center.X - 30, (int)center.X + 670), rnd.Next((int)center.Y, (int)center.Y + 200))));
            objects.Add(new BackgroundObject(contentManager.Load<Texture2D>("sprites/background/ground/ground"), new Rectangle(0, 0, 800, 136), new Vector2(center.X, center.Y+220)));
            objects.Add(new BackgroundObject(contentManager.Load<Texture2D>("sprites/background/ground/ground"), new Rectangle(0, 0, 800, 136), new Vector2(center.X + 798, center.Y + 220)));
            objects.Add(new BackgroundObject(contentManager.Load<Texture2D>("sprites/background/castle/castle"), new Rectangle(0, 0, 300, 150), new Vector2(3510, 40)));
        }

        /// <summary>
        /// A method to update background "objects" called from MainFunction.Update.
        /// </summary>
        /// <param name="gameTime">A GameTime parameter from the main.</param>
        public void Update(GameTime gameTime, Player player)
        {
            // If the player moves right move the background objects accordingly
            // in order to give a feel of perspective.
            if (Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                for (int i = 3; i < 6; i++)
                {
                    objects[i].objectPosition.X += player.speed.X + (player.speed.X / 30);
                }
                objects[0].objectPosition.X += player.speed.X;
                objects[1].objectPosition.X += player.speed.X;
                objects[2].objectPosition.X += player.speed.X - (player.speed.X / 30);
                objects[6].objectPosition.X += player.speed.X - (player.speed.X / 6);
                objects[7].objectPosition.X += player.speed.X - (player.speed.X / 6);
            }
            // The same if the player goes left.
            else if (Keyboard.GetState().IsKeyDown(Keys.Left)&&player.speed.X!=0)
            {
                for (int i = 3; i < 6; i++)
                {
                    objects[i].objectPosition.X += player.speed.X - (player.speed.X / 15);
                }
                objects[0].objectPosition.X += player.speed.X;
                objects[1].objectPosition.X += player.speed.X;
                objects[2].objectPosition.X += player.speed.X - (player.speed.X / 30);
                objects[6].objectPosition.X += player.speed.X - (player.speed.X / 6);
                objects[7].objectPosition.X += player.speed.X - (player.speed.X / 6); 
            }
            // The clouds are constantly moving regardless of the player's movement.
            else
            {
                for (int i = 3; i < 6; i++)
                {
                    //cloudSpeed.X = 0.1f;
                    objects[i].objectPosition.X += 0.1f;
                }
            }
            // If a cloud disappers from the screen randomly change its position to appear on the left of the screen. 
            for (int i = 3; i < 6; i++)
            {
                if (objects[i].objectPosition.X >= (objects[0].objectPosition.X + 800))
                {
                    objects[i].objectPosition.X = objects[0].objectPosition.X - rnd.Next(150, 500);
                    objects[i].objectPosition.Y = objects[0].objectPosition.Y + rnd.Next(0, 200);
                }
            }
            // Loop the ground if the player goes right.
            //
            if (objects[6].objectPosition.X <= (objects[0].objectPosition.X - 800))
            {
                objects[6].objectPosition.X = objects[0].objectPosition.X + 797;
            }
            if (objects[7].objectPosition.X <= (objects[0].objectPosition.X - 800))
            {
                objects[7].objectPosition.X = objects[0].objectPosition.X + 797;
            }
            // Loop the ground if the player goes left.
            //
            if (objects[6].objectPosition.X >= (objects[0].objectPosition.X + 800)) 
            {
                objects[6].objectPosition.X = objects[0].objectPosition.X - 797;
            }
            if (objects[7].objectPosition.X >= (objects[0].objectPosition.X + 800))
            {
                objects[7].objectPosition.X = objects[0].objectPosition.X - 797;
            }
        }
        /// <summary>
        /// A method to draw background "objects" on the screen called from MainFunction.Draw.
        /// </summary>
        /// <param name="spriteBatch">We give spriteBatch as parameter because the current class
        /// don't know anything about it.</param>
        public void Draw(SpriteBatch spriteBatch)
        {
            foreach(BackgroundObject bObject in objects)
            {
                bObject.Draw(spriteBatch);
            }
        }

    }
}
