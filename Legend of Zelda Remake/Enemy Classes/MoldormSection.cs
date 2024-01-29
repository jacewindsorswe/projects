using Microsoft.Xna.Framework;
using Sprint2;
using System;
using System.Drawing;

namespace Sprint0
{
    public class MoldormSection : Enemy
    {
        int moveTicks;
        public MoldormSection(Game1 g)
        {
            game = g;
            moveTicks = 0;

            CurrentEnemyType = Enemy.EnemyType.MoldormSection;
            CurrentEnemyFrame = EnemySpriteFactory.EnemyFrame.MoldormSection;
            graphics = game._graphics;
            EnemyStateMachine.GenerateEnemy(this);
            EnemyColor = Microsoft.Xna.Framework.Color.White;
           // CurrentPosX = graphics.PreferredBackBufferWidth / 2 + 65;
           // CurrentPosY = graphics.PreferredBackBufferHeight / 2 - 65;
            CurrentPosX = GlobalUtilities.DEFAULT_ENEMY_POS_X;
            CurrentPosY = GlobalUtilities.DEFAULT_ENEMY_POS_Y;
            numEnemies++;
            enemyID = numEnemies;
            Health = GlobalUtilities.enemyHealth[CurrentEnemyType];

            EnemiesDictionary.Add(ID, this);
        }
    }
}
