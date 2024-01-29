using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Sprint2;
using System.Drawing;
using Rectangle = Microsoft.Xna.Framework.Rectangle;
using Color = Microsoft.Xna.Framework.Color;
using static Sprint0.Enemy;
using Sprint2.Enemy_Classes;

namespace Sprint0 { 
    public class Dodongo : Enemy
    {
        private bool exploded = false;
        public bool Exploded 
        {
            get { return exploded; }
            set { exploded = value; }
        }
	    public Dodongo(Game1 g)
	    {
            game = g;
            CurrentEnemyType = Enemy.EnemyType.Dodongo;
            CurrentEnemyFrame = EnemySpriteFactory.EnemyFrame.DodongoDown1;
            graphics = game._graphics;
            EnemyStateMachine.GenerateEnemy(this);
            EnemyColor = Color.White;
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

        override public void TakeDamage(int damage) 
        {
            if (Stunned) 
            {
                if (!TakingDamage)
                {
                    EnemyStateMachine.TakeDamage(this, damage);

                    if (!Alive)
                    {
                        game._soundEffectGenerator.Play("LOZ_Enemy_Die");
                    }
                    else
                    {
                        game._soundEffectGenerator.Play("LOZ_Boss_Hit");
                    }
                }
            }
        }

        public void ExplodeInside(GameTime timer) 
        {
            StunTime = timer.TotalGameTime.TotalSeconds;

            exploded = true;
            SetExplodeAnimation(timer);
        }

        private void SetExplodeAnimation(GameTime gT) 
        {
            //System.Diagnostics.Debug.WriteLine(gT.TotalGameTime.TotalSeconds - StunTime);
            if (gT.TotalGameTime.TotalSeconds - StunTime > GlobalUtilities.DODONGO_PAUSE_TIME)
            {
                switch (CurrentDirection)
                {
                    case Direction.Left:
                        CurrentEnemyFrame = EnemySpriteFactory.EnemyFrame.DodongoLeftBomb;
                        break;
                    case Direction.Right:
                        CurrentEnemyFrame = EnemySpriteFactory.EnemyFrame.DodongoRightBomb;
                        break;
                    case Direction.Up:
                        CurrentEnemyFrame = EnemySpriteFactory.EnemyFrame.DodongoUpBomb;
                        break;
                    case Direction.Down:
                        CurrentEnemyFrame = EnemySpriteFactory.EnemyFrame.DodongoDownBomb;
                        break;
                }
            }
        }

        override public void Update(GameTime timer) 
        {
            if (!Stunned && !exploded) 
            {
                CheckMove += 1;
                DirectChange += 1;
                //int newDirection = rand.Next(0, 4);
                if (DirectChange % GlobalUtilities.DODONGO_DIRECTION_BUFFER == 0)
                {
                    CurrentDirection = GlobalUtilities.RandomDirection(CurrentDirection);
                }
                if (CheckMove % GlobalUtilities.DODONGO_MOVEMENT_BUFFER == 0)
                {
                    switch (CurrentDirection)
                    {
                        case Direction.Left:
                            Left();
                            break;
                        case Direction.Right:
                            Right();
                            break;
                        case Direction.Up:
                            Up();
                            break;
                        case Direction.Down:
                            Down();
                            break;
                    }
                }
            }
            else 
            {
                SetExplodeAnimation(timer);
                if (GlobalUtilities.TimeStopped)
                {
                    if (timer.TotalGameTime.TotalSeconds - StunTime >= GlobalUtilities.CLOCK_ACTIVE_TIME)
                    {
                        Stunned = false;
                    }
                }
                else
                {
                    if (timer.TotalGameTime.TotalSeconds - StunTime >= GlobalUtilities.DODONGO_STUN_TIME)
                    {
                        Stunned = false;

                        if (exploded)
                        {
                            Stunned = true;
                            TakeDamage(GlobalUtilities.enemyHealth[CurrentEnemyType] / 2);
                            exploded = false;
                        }
                    }
                }
            }

            EnemyColor = Color.White;
            CurrentFrameTicks += 1;
            EnemyStateMachine.ChangeFrame(this);

            EnemyColor = AnimateDamage();

            if (EnemySpriteFactory.dungeonEnemies.Contains(CurrentEnemyFrame))
            {
                CurrentHitbox = new Rectangle(CurrentPosX, CurrentPosY, (int)((int)(enemySizes[CurrentEnemyFrame][0] * enemySizes[CurrentEnemyFrame][2]) * GlobalUtilities.Res_Scalar), (int)((int)(enemySizes[CurrentEnemyFrame][1] * enemySizes[CurrentEnemyFrame][2]) * GlobalUtilities.Res_Scalar));
            }
            else
            {
                CurrentHitbox = new Rectangle(CurrentPosX, CurrentPosY, (int)(GlobalUtilities.DEFAULT_ENEMY_WIDTH * GlobalUtilities.Res_Scalar), (int)(GlobalUtilities.DEFAULT_ENEMY_HEIGHT * GlobalUtilities.Res_Scalar));
            }

            if (!Alive) 
            {
                game.deathAnimations.Add(new DeathAnimation(DeathAnimation.DeathType.Enemy, CurrentHitbox, timer.TotalGameTime.TotalSeconds));
            }
        }
    }
}
