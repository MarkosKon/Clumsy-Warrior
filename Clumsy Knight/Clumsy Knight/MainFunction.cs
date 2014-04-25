namespace Clumsy_Knight
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using Microsoft.Xna.Framework.Input;
    using System.Collections.Generic;
    using Clumsy_Knight.Enemies;
    /// <summary>
    /// This is the main function of the game.
    /// </summary>
    public class MainFunction : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        SpriteBatch spriteBatch1;
        //Temporary, for displaying player score and health.
        SpriteFont font;

        bool paused = false;
        Texture2D pausedTexture;
        Rectangle pausedRectangle;
        Button btnPlay, btnQuit;


        GameState CurrentGameState = GameState.Mainmenu;

        KeyboardState keyboardState;
        KeyboardState oldKeyboardState;

        HighScoreScreen scoreScreen;
        //MenuComponent menuComponent;
        GameScreen activeScreen;
        StartScreen startScreen;
        ActionScreen actionScreen;

        //Instantiate some objects.
        //
        public List<Enemy> enemies = new List<Enemy>();
        //public Dragon boss;
        private Background background;
        private Camera camera;
        public Player player;
        public Map map;



        public MainFunction()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            IsMouseVisible = true;
            //boss=new Dragon(DifficultyLevel.normal, new Vector2(2500, 235));
            enemies.Add(new Dragon(DifficultyLevel.normal, new Vector2(2500, 235)));
            enemies.Add(new Skeleton(DifficultyLevel.normal, new Vector2(1370, 255)));
            enemies.Add(new Orc(DifficultyLevel.normal, new Vector2(550, 30)));
            camera = new Camera();
            map = new Map();

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            spriteBatch1 = new SpriteBatch(GraphicsDevice);

            startScreen = new StartScreen(this, spriteBatch1, Content.Load<SpriteFont>("menufont"), Content.Load<Texture2D>("MenuBackground"));
            Components.Add(startScreen);
            startScreen.Hide();

            actionScreen = new ActionScreen(this, spriteBatch1);//, Content.Load<Texture2D>("Destiny"));            
            Components.Add(actionScreen);
            actionScreen.Hide();

            scoreScreen = new HighScoreScreen(this, spriteBatch1);//, Content.Load<Texture2D>("Map3"));
            Components.Add(scoreScreen);
            startScreen.Hide();

            activeScreen = startScreen;
            activeScreen.Show();

            foreach (Enemy enemy in enemies)
            {
                enemy.LoadContent(Content);
            }
            //boss.LoadContent(Content);
            player = new Player(Content.Load<Texture2D>("sprites/player/knight"), new Vector2(2300, 0), 96, 67);
            background = new Background(player);
            background.LoadContent(Content);
            font = Content.Load<SpriteFont>("menufont");
            Tile.Content = Content;
            map.Generate(new int[,]{
                {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                {0,0,0,0,0,0,0,0,0,0,0,0,0,0,4,4,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,1,1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                {0,0,0,0,0,0,0,0,3,0,0,0,0,0,0,0,0,1,1,1,1,1,1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                {0,0,0,0,0,0,0,1,1,1,1,0,0,0,1,1,1,1,1,1,1,1,1,1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,1,1,1,1,1,0,0,0},
                {9,0,0,0,0,0,1,1,1,1,1,6,6,6,1,1,1,1,1,1,1,1,1,1,1,1,0,0,0,0,0,9,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                {1,0,0,0,0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0,0,8,0,5,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,5,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                {1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,0,0,0},
                {1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0,0,0},
                {1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,2,0,0},
            }, 45);
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            keyboardState = Keyboard.GetState();
            // Allows the game to exit
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                this.Exit();

            switch (CurrentGameState)
            {
                case GameState.Mainmenu:
                    if (activeScreen == startScreen)
                    {
                        if (CheckKey(Keys.Enter))
                        {
                            if (startScreen.SelectedIndex == 0) CurrentGameState = GameState.Playing;
                            {
                                activeScreen.Hide();
                                activeScreen = actionScreen;
                                activeScreen.Show();

                            }
                            if (startScreen.SelectedIndex == 1) CurrentGameState = GameState.HighScore;
                            {
                                activeScreen.Hide();
                                activeScreen = scoreScreen;
                                activeScreen.Show();
                            }
                            if (startScreen.SelectedIndex == 2)
                            {
                                this.Exit();
                            }

                        }
                    }
                    break;
                case GameState.Playing:
                    player.Update(gameTime, this.map);
                    //Check for collisions player-enemies.
                    player.texture.GetData(0, player.rectangle, player.textureColors, 0, player.frameHeight * player.frameWidth);
                    Rectangle pRectangle = new Rectangle((int)player.position.X, (int)player.position.Y - player.frameHeight, player.frameWidth, player.frameHeight);
                    for (int i = 0; i < enemies.Count; i++)
                    {
                        enemies[i].Update(gameTime, player);
                        enemies[i].enemyTexture.GetData(0, enemies[i].enemyRectangle, enemies[i].textureColors, 0, enemies[i].frameWidth * enemies[i].frameHeight);
                        Rectangle oRectangle = new Rectangle((int)enemies[i].position.X, (int)enemies[i].position.Y, enemies[i].frameWidth, enemies[i].frameHeight);
                        if (RectangleHelper.PixelCollision(pRectangle, player.textureColors, oRectangle, enemies[i].textureColors) && !player.isHit)
                        {
                            player.health -= enemies[i].damage / 5;
                            player.isHit = true;
                        }
                        else
                        {
                            player.isHit = false;
                        }
                        if (RectangleHelper.PixelCollision(oRectangle, enemies[i].textureColors, pRectangle, player.textureColors) && !enemies[i].isHit)
                        {
                            enemies[i].health -= player.damage / 5;
                            enemies[i].isHit = true;
                            if (enemies[i].health<=0)
                            {
                                enemies.RemoveAt(i);
                                player.score += 20;
                            }
                        }
                        else
                        {
                            enemies[i].isHit = false;
                        }
                    }
                    //boss.Update(gameTime, player);
                    background.Update(gameTime, player);
                    map.Update(gameTime, player);
                    camera.Update(gameTime, this);
                    break;
                //case GameState.HighScore:
                // Here we put everything for the high score 
                // break;
            }
            base.Update(gameTime);
            oldKeyboardState = keyboardState;
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.SkyBlue);

            spriteBatch.Begin(SpriteSortMode.Deferred,
                BlendState.AlphaBlend,
                null, null, null, null,
                camera.transform);
            spriteBatch1.Begin();
            switch (CurrentGameState)
            {
                case GameState.Mainmenu:
                    break;

                case GameState.Playing:
                    background.Draw(spriteBatch);
                    map.Draw(spriteBatch);
                    foreach(Enemy enemy in enemies)
                    {
                        enemy.Draw(spriteBatch);
                        //Temporary display health display for enemies.
                        spriteBatch.DrawString(font, "Health", new Vector2(enemy.position.X, enemy.position.Y-20), Color.Red);
                        spriteBatch.DrawString(font, enemy.health.ToString(), new Vector2(enemy.position.X, enemy.position.Y), Color.Black);
                    }
                    //boss.Draw(spriteBatch);
                    player.Draw(spriteBatch);
                    //Temporary health, score display for player.
                    spriteBatch.DrawString(font, "Health", new Vector2(player.position.X, player.position.Y - 120), Color.Red);
                    spriteBatch.DrawString(font, player.health.ToString(), new Vector2(player.position.X, player.position.Y-100), Color.Black);
                    spriteBatch.DrawString(font, "Score", new Vector2(player.position.X+100, player.position.Y - 120), Color.Red);
                    spriteBatch.DrawString(font, player.score.ToString(), new Vector2(player.position.X + 100, player.position.Y-100), Color.Black);
                    break;
                // case GameState.HighScore:
                //we draw the high score
                //   break;
            }
            base.Draw(gameTime);
            spriteBatch1.End();
            spriteBatch.End();
        }

        private bool CheckKey(Keys theKey)
        {
            return keyboardState.IsKeyUp(theKey) &&
            oldKeyboardState.IsKeyDown(theKey);
        }
    }

    enum GameState
    {
        Mainmenu,
        HighScore,
        Playing

    }

    /// <summary>
    /// This enum struct defines the difficulty level of the game.
    /// </summary>
    public enum DifficultyLevel
    {
        normal,
        hard
    }

    /// <summary>
    /// This enum struct represents the different states of an enemy.
    /// </summary>
    public enum EnemyState
    {
        standing,
        walking,
        attacking,
        takingDamage,
        dying
    }

    /// <summary>
    /// This enum struct represents the different states of the player.
    /// </summary>
    enum PlayerState
    {
        walking,
        standing,
        attacking,
        init
    }
}





