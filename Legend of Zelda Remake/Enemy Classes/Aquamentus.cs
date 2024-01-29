using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Sprint2;

namespace Sprint0 { 
    public class Aquamentus : Enemy
    {
        public static int DRAGON_WIDTH = 24;
        public static int DRAGON_HEIGHT = 32;
        public static int DRAGON_HEALTH = 20;

        public static double SCALING_FACTOR = 3;
	    public Aquamentus(Game1 g)
	    {
            game = g;
            CurrentEnemyType = Enemy.EnemyType.Aquamentus;
            CurrentEnemyFrame = EnemySpriteFactory.EnemyFrame.Aquamentus1;
            graphics = game._graphics;
            EnemyStateMachine.GenerateEnemy(this);
            EnemyColor = Microsoft.Xna.Framework.Color.White;
            //  CurrentPosX = graphics.PreferredBackBufferWidth / 2 + 65;
            //  CurrentPosY = graphics.PreferredBackBufferHeight / 2 - 65;
            CurrentPosX = GlobalUtilities.DEFAULT_ENEMY_POS_X;
            CurrentPosY = GlobalUtilities.DEFAULT_ENEMY_POS_Y;
            numEnemies++;
            enemyID = numEnemies;
            Health = GlobalUtilities.enemyHealth[CurrentEnemyType];
            EnemiesDictionary.Add(ID, this);
	    }
        override public void Draw(SpriteBatch sB, Color col)
        {
            EnemyStateMachine.GenerateEnemy(this);
            CurrentSprite.Draw(sB, CurrentHitbox, this.EnemyColor);
        }
    }
}
