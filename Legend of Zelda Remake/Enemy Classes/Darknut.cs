using System;
using Sprint2;

namespace Sprint0 { 
public class Darknut : Enemy
{
	public Darknut(Game1 g)
	{
            game = g;
            CurrentEnemyType = Enemy.EnemyType.Darknut;
            CurrentEnemyFrame = EnemySpriteFactory.EnemyFrame.DarknutLeft1;
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
