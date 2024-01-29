using Microsoft.Xna.Framework;
using Sprint2;
using System;
using System.Drawing;

namespace Sprint0
{
    public class Keese : Enemy
    {
        int moveTicks;
        public Keese(Game1 g)
        {
            game = g;
            moveTicks = 0;

            CurrentEnemyType = Enemy.EnemyType.Keese;
            CurrentEnemyFrame = EnemySpriteFactory.EnemyFrame.Keese1;
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

        public void Update(GameTime timer, int destX, int destY) 
        {
            moveTicks++;
            if (moveTicks % GlobalUtilities.GetEnemySpeed(CurrentEnemyType)[2] == 0)
            {
                moveTicks = 0;
                Tuple<Tuple<Direction, int>, Tuple<Direction, int>> movement = GetMovement(destX, destY);

                if (!Stunned)
                {
                    CurrentDirection = movement.Item1.Item1;
                    switch (CurrentDirection)
                    {
                        case Direction.Left:
                            Left(movement.Item1.Item2);
                            break;
                        case Direction.Right:
                            Right(movement.Item1.Item2);
                            break;
                        case Direction.Up:
                            Up(movement.Item1.Item2);
                            break;
                        default:
                            Down(movement.Item1.Item2);
                            break;
                    }

                    CurrentDirection = movement.Item2.Item1;
                    switch (CurrentDirection)
                    {
                        case Direction.Left:
                            Left(movement.Item2.Item2);
                            break;
                        case Direction.Right:
                            Right(movement.Item2.Item2);
                            break;
                        case Direction.Up:
                            Up(movement.Item2.Item2);
                            break;
                        default:
                            Down(movement.Item2.Item2);
                            break;
                    }
                }
                else 
                {
                    if(timer.TotalGameTime.TotalSeconds - StunTime > MaxStunTime) 
                    {
                        Stunned = false;
                        MaxStunTime = GlobalUtilities.DEFAULT_STUN_TIME;
                    }
                }
            }

            Update(timer);
        }

        private Tuple<Tuple<Direction, int>, Tuple<Direction, int>> GetMovement(int destX, int destY) 
        {
            Direction xD, yD;
            int xT = -1, yT = -1;
            
            if(CurrentPosX < destX) 
            {
                xD = Direction.Right;
            }
            else if(CurrentPosX > destX) 
            {
                xD = Direction.Left;
            }
            else 
            {
                xD = Direction.Up;
                xT = 0;
            }

            if (CurrentPosY < destY)
            {
                yD = Direction.Down;
            }
            else if (CurrentPosY > destY)
            {
                yD = Direction.Up;
            }
            else
            {
                yD = Direction.Up;
                yT = 0;
            }

            if(xT == -1) 
            {
                xT = GlobalUtilities.GetEnemySpeed(CurrentEnemyType)[0] / 2;
                if(yT == -1) 
                {
                    yT = GlobalUtilities.GetEnemySpeed(CurrentEnemyType)[0] - xT;
                }
            }
            if (yT == -1)
            {
                yT = GlobalUtilities.GetEnemySpeed(CurrentEnemyType)[0] - xT;
            }

            return Tuple.Create(Tuple.Create(xD, xT), Tuple.Create(yD, yT));
        }
    }
}
