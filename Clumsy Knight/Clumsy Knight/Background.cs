namespace Clumsy_Knight
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Content;
    using Microsoft.Xna.Framework.Graphics;
    using Microsoft.Xna.Framework.Input;
    using System.Collections.Generic;

    /// <summary>
    /// A background class that is used to draw a dynamic 
    /// background that changes according to player's movement.
    /// </summary>
    public class Background
    {
        //Variables for the sky object.
        //
        public Texture2D skyTexture;
        public Rectangle skyRectangle;
        public Vector2 skyPosition;

        //Variables for the sun object.
        //
        public Texture2D sunTexture;
        public Rectangle sunRectangle;
        public Vector2 sunPosition;

        //Variables for the ground object.
        //
        public Texture2D groundTexture;
        public Rectangle groundRectangle;
        public Vector2 groundPosition;

        //Variables for the mountain object.
        //
        public Texture2D mountainTexture;
        public Rectangle mountainRectangle;
        public Vector2 mountainPosition;

        //Variables for the castle object.
        //
        public Texture2D castleTexture;
        public Rectangle castleRectangle;

        //Variables for the cloud objects.
        //
        public List<Texture2D> cloudTexture;
        public List<Rectangle> cloudRectangle;
        public List<Vector2> cloudPosition;
        private Vector2 cloudSpeed;
        
        /// <summary>
        /// The constructor
        /// </summary>
        public Background()
        {
            skyRectangle = new Rectangle(0, 0, 800, 480);
            skyPosition = new Vector2(-277, 0);
            sunRectangle = new Rectangle(0, 0, 2000, 2000);
            sunPosition = new Vector2(-280, 0);
            groundRectangle = new Rectangle(0, 0, 800, 480);
            groundPosition = new Vector2(-277, 0);
            mountainRectangle = new Rectangle(0, 0, 800, 480);
            mountainPosition = new Vector2(-100, 100);
            castleRectangle = new Rectangle(4000, 217, 300, 150);
            cloudRectangle=new List<Rectangle>();
            cloudTexture = new List<Texture2D>();
            cloudPosition = new List<Vector2>();
            cloudRectangle.Add(new Rectangle(0,0,120,60));
            cloudRectangle.Add(new Rectangle(0, 0, 150, 83));
            cloudRectangle.Add(new Rectangle(0, 0, 85, 48));
            cloudPosition.Add(new Vector2(0,100));
            cloudPosition.Add(new Vector2(400,80));
            cloudPosition.Add(new Vector2(700,76));
            cloudSpeed = new Vector2(3, 0);
        }

        /// <summary>
        /// This method is called from the Game's LoadContent method 
        /// (Eventually will be called from the "Gamescreen").
        /// </summary>
        /// <param name="content">We need a content parameter from the main because we
        /// want to load the texture in this class.</param>
        public void LoadContent(ContentManager contentManager)
        {
            skyTexture = contentManager.Load<Texture2D>("sprites/background/sky/sky");
            sunTexture = contentManager.Load<Texture2D>("sprites/background/sun/sun");
            groundTexture = contentManager.Load<Texture2D>("sprites/background/ground/ground");
            mountainTexture = contentManager.Load<Texture2D>("sprites/background/mountain/mountain");
            castleTexture = contentManager.Load<Texture2D>("sprites/background/castle/castle");
            cloudTexture.Add(contentManager.Load<Texture2D>("sprites/background/clouds/cloud1"));
            cloudTexture.Add(contentManager.Load<Texture2D>("sprites/background/clouds/cloud2"));
            cloudTexture.Add(contentManager.Load<Texture2D>("sprites/background/clouds/cloud3"));
        }

        /// <summary>
        ///This method is called from the Game's Update method
        ///(Eventually will be called from the "Gamescreen").
        /// </summary>
        /// <param name="gameTime">A GameTime parameter from the main.</param>
        public void Update(GameTime gameTime)
        {
            //If the player moves right move the background objects accordingly
            //in order to give a feel of perspective.
            if (Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                for (int i = 0; i < cloudPosition.Count; i++)
                {
                    cloudPosition[i] += cloudSpeed;
                }
                skyPosition.X += 3f;
                sunPosition.X += 3f;
                mountainPosition.X += 2.9f;
                groundPosition.X += 2.4f;
            }
            //The same if the player goes left.
            else if (Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                for (int i = 0; i < cloudPosition.Count; i++)
                {
                    cloudPosition[i] -= cloudSpeed;
                }
                skyPosition.X -= 3f;
                sunPosition.X -= 3f;
                mountainPosition.X -= 2.9f;
                groundPosition.X -= 2.4f;
            }
            /*if (skyRectangle.X + 800 <= 0)
            {
                skyRectangle.X = backgroundRectangle2.X + 800;
            }
            if (backgroundRectangle2.X + 800 <= 0)
            {
                backgroundRectangle2.X = skyRectangle.X + 800;
            }
            skyRectangle.X -= 3;
            backgroundRectangle2.X -= 3;*/
        }

        /// <summary>
        /// This method is called from the Game's Draw method
        /// (Eventually will be called from the "Gamescreen").
        /// </summary>
        /// <param name="spriteBatch">We give spriteBatch as parameter because the current class
        /// don't know anything about it.</param>
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(skyTexture,skyPosition,skyRectangle,Color.White);
            spriteBatch.Draw(sunTexture,sunPosition,sunRectangle,Color.White,0f,new Vector2(0,0),0.05f,SpriteEffects.None,0f);
            spriteBatch.Draw(mountainTexture,mountainPosition, mountainRectangle, Color.White);
            spriteBatch.Draw(groundTexture,groundPosition, groundRectangle, Color.White);
            spriteBatch.Draw(castleTexture, castleRectangle, Color.White);
            for (int i=0; i<3; i++)
            {
                spriteBatch.Draw(cloudTexture[i], cloudPosition[i], cloudRectangle[i], Color.White);
            }
        }

    }
}
