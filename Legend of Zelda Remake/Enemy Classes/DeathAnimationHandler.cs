using Microsoft.Xna.Framework;
using Sprint0;
using Sprint2.Enemy_Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sprint2
{
    public class DeathAnimationHandler
    {
        public Game1 game;
        public List<DeathAnimation> deathAnimations;
        public DeathAnimationHandler(Game1 g)
        {
            game = g;

        }

        public void Update(GameTime gameTime)
        {
            deathAnimations = game.deathAnimations;
            List<DeathAnimation> removals = new();
            foreach (DeathAnimation deathAnimation in deathAnimations.ToList())
            {
                deathAnimation.Update(gameTime);
                if (gameTime.TotalGameTime.TotalSeconds - deathAnimation.CreatedTime > GlobalUtilities.ENEMY_DEATH_ANIMATION_SPEED)
                {
                    removals.Add(deathAnimation);
                }


                foreach (DeathAnimation removal in removals)
                {
                    deathAnimations.Remove(removal);
                }
            }
        }

    }
}
