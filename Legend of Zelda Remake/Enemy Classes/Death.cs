using Microsoft.Xna.Framework;
using Sprint2.Enemy_Classes;
using System;
using System.Runtime.Serialization.Formatters;
using Sprint2;

namespace Sprint0
{
    public class Death : Enemy
    {
        private int changeFrame = 0;
        public Death(Game1 g)
        {
            game = g;
            CurrentEnemyType = Enemy.EnemyType.Death;
            CurrentEnemyFrame = EnemySpriteFactory.EnemyFrame.Death1;
            graphics = game._graphics;
            EnemyStateMachine.GenerateEnemy(this);
            EnemyColor = Microsoft.Xna.Framework.Color.White;
           // CurrentPosX = graphics.PreferredBackBufferWidth / 2 + 65;
           // CurrentPosY = graphics.PreferredBackBufferHeight / 2 - 65;
            CurrentPosX = GlobalUtilities.DEFAULT_ENEMY_POS_X;
            CurrentPosY = GlobalUtilities.DEFAULT_ENEMY_POS_Y;
            numEnemies++;
            enemyID = numEnemies;

            EnemiesDictionary.Add(ID, this);
        }

        public void Update(GameTime timer, int rateOfChange)
        {

            if (CurrentEnemyFrame == EnemySpriteFactory.EnemyFrame.Death4)
            {
                this.Alive = false;
                return;
            }

            changeFrame++;
            if (changeFrame % rateOfChange == 0)
            {
                if(CurrentEnemyFrame != EnemySpriteFactory.EnemyFrame.Death4)
                {
                    CurrentEnemyFrame++;
                }

            } 

            Update(timer);
        }

    }
}