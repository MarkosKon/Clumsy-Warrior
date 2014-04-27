namespace Clumsy_Knight
{
    using Microsoft.Xna.Framework.Audio;
    using Microsoft.Xna.Framework.Content;
    using Microsoft.Xna.Framework.Media;

    // A class for the music and sound effects of the game.
    public class Sounds
    {
        public SoundEffect playerJumpingSound;
        public Song backgroundMusic;
        public SoundEffect enemyDeathSound;

        // A constructor for the sounds.
        public Sounds()
        {
            playerJumpingSound = null;
            backgroundMusic = null;
            enemyDeathSound = null;
        }

        // A LoadContent for the sounds.
        public void LoadContent(ContentManager Content)
        {
            playerJumpingSound = Content.Load<SoundEffect>("sounds/Jump");
            backgroundMusic = Content.Load<Song>("sounds/BackgroundMusic");
            enemyDeathSound = Content.Load<SoundEffect>("sounds/EnemyDeathSound");

            // This is will play the music for the background of the game.
            MediaPlayer.Play(backgroundMusic);

            // It will repeat the music when it finishes.
            MediaPlayer.IsRepeating = true;

        }
    }
}
