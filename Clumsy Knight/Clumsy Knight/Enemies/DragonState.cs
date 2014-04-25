namespace Clumsy_Knight.Enemies
{
    using Microsoft.Xna.Framework;
    using System;
    public abstract class DragonState
    {
        public Dragon dragon;

        public virtual void Update(GameTime gameTime, Player player)
        {

        }
        /// <summary>
        /// This method checks where is player and changes the state if the player is near.
        /// </summary>
        /// <param name="player">The player object passed from the Update method.</param>
        /// <param name="detectDistance">Change the enemy's direction if the player has been detected near.</param>
        /// <param name="attackDistance">Start attacking the player if he is really near.</param>
        public bool WhereIsPlayer(Player player, int detectDistance, int attackDistance)
        {
            //Find the centers.
            Vector2 dragonCenter = new Vector2(dragon.position.X + (dragon.enemyRectangle.Width / 2), dragon.position.Y + (dragon.enemyRectangle.Height / 2));
            Vector2 playerCenter = new Vector2(player.position.X + (player.rectangle.Width / 2), player.position.Y - (player.rectangle.Height / 2));
            //Is the player near to the enemy;
            if (Math.Abs(playerCenter.X - dragonCenter.X) < detectDistance)
            {
                //Is the player left to the enemy;
                if ((playerCenter.X - dragonCenter.X) < 0)
                {
                    //In other words speed must be negative.
                    dragon.speed.X = (-1.0f) * Math.Abs(dragon.speed.X);
                }
                //Is the player right to the enemy;
                else
                {
                    //Speed must be negative.
                    dragon.speed.X = Math.Abs(dragon.speed.X);
                }
                //If the player is really near and haven't attacked recently, start attacking.
                if (Math.Abs(playerCenter.X - dragonCenter.X) < attackDistance)
                {
                    return true;
                }
            }
            return false;
        }

    }
}
