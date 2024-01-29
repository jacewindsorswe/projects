using Sprint2;
using System.Collections.Generic;
using System.Diagnostics;

namespace Sprint0
{
    public static class EnemyStateMachine
    {
        private const int FULL_HEALTH = 100;

        public static void MoveUp(Enemy currentEnemy, int numPixels)
        {
            currentEnemy.CurrentPosY -= numPixels;
        }
        public static void MoveDown(Enemy currentEnemy, int numPixels)
        {
            currentEnemy.CurrentPosY += numPixels;
        }
        public static void MoveLeft(Enemy currentEnemy, int numPixels)
        {
            currentEnemy.CurrentPosX -= numPixels;
        }
        public static void MoveRight(Enemy currentEnemy, int numPixels)
        {
            currentEnemy.CurrentPosX += numPixels;
        }
        public static void Up(Enemy currentEnemy)
        {
            if (!(currentEnemy.CurrentDirection.Equals(Enemy.Direction.Up)))
            {
                currentEnemy.CurrentDirection = Enemy.Direction.Up;
            }
            else
            {
                Move(currentEnemy);
            }
        }

        public static void Up(Enemy currentEnemy, int numPixels)
        {
            if (!(currentEnemy.CurrentDirection.Equals(Enemy.Direction.Up)))
            {
                currentEnemy.CurrentDirection = Enemy.Direction.Up;
            }
            else
            {
                Move(currentEnemy, numPixels);
            }
        }

        public static void Down(Enemy currentEnemy)
        {
            if (!(currentEnemy.CurrentDirection.Equals(Enemy.Direction.Down)))
            {
                currentEnemy.CurrentDirection = Enemy.Direction.Down;
            }
            else
            {
                Move(currentEnemy);
            }
        }

        public static void Down(Enemy currentEnemy, int numPixels)
        {
            if (!(currentEnemy.CurrentDirection.Equals(Enemy.Direction.Down)))
            {
                currentEnemy.CurrentDirection = Enemy.Direction.Down;
            }
            else
            {
                Move(currentEnemy, numPixels);
            }
        }

        public static void Left(Enemy currentEnemy)
        {
            if (!(currentEnemy.CurrentDirection.Equals(Enemy.Direction.Left)))
            {
                currentEnemy.CurrentDirection = Enemy.Direction.Left;
            }
            else
            {
                Move(currentEnemy);
            }
        }

        public static void Left(Enemy currentEnemy, int numPixels)
        {
            if (!(currentEnemy.CurrentDirection.Equals(Enemy.Direction.Left)))
            {
                currentEnemy.CurrentDirection = Enemy.Direction.Left;
            }
            else
            {
                Move(currentEnemy, numPixels);
            }
        }

        public static void Right(Enemy currentEnemy)
        {
            if (!(currentEnemy.CurrentDirection.Equals(Enemy.Direction.Right)))
            {
                currentEnemy.CurrentDirection = Enemy.Direction.Right;
            }
            else
            {
                Move(currentEnemy);
            }
        }
        public static void Right(Enemy currentEnemy, int numPixels)
        {
            if (!(currentEnemy.CurrentDirection.Equals(Enemy.Direction.Right)))
            {
                currentEnemy.CurrentDirection = Enemy.Direction.Right;
            }
            else
            {
                Move(currentEnemy, numPixels);
            }
        }

        public static void Move(Enemy currentEnemy)
        {
            if (currentEnemy.Alive)
            {
                if (currentEnemy.CurrentDirection.Equals(Enemy.Direction.Up))
                {
                    currentEnemy.CurrentPosY -= GlobalUtilities.GetEnemySpeed(currentEnemy.CurrentEnemyType)[currentEnemy.AlternateSpeed];
                }
                else if (currentEnemy.CurrentDirection.Equals(Enemy.Direction.Down))
                {
                    currentEnemy.CurrentPosY += GlobalUtilities.GetEnemySpeed(currentEnemy.CurrentEnemyType)[currentEnemy.AlternateSpeed];
                }
                else if (currentEnemy.CurrentDirection.Equals(Enemy.Direction.Left))
                {
                    currentEnemy.CurrentPosX -= GlobalUtilities.GetEnemySpeed(currentEnemy.CurrentEnemyType)[currentEnemy.AlternateSpeed];
                }
                else
                {
                    currentEnemy.CurrentPosX += GlobalUtilities.GetEnemySpeed(currentEnemy.CurrentEnemyType)[currentEnemy.AlternateSpeed];
                }

            }

        }

        public static void Move(Enemy currentEnemy, int numPixels)
        {
            if (currentEnemy.Alive)
            {
                if (currentEnemy.CurrentDirection.Equals(Enemy.Direction.Up))
                {
                    currentEnemy.CurrentPosY -= numPixels;
                }
                else if (currentEnemy.CurrentDirection.Equals(Enemy.Direction.Down))
                {
                    currentEnemy.CurrentPosY += numPixels;
                }
                else if (currentEnemy.CurrentDirection.Equals(Enemy.Direction.Left))
                {
                    currentEnemy.CurrentPosX -= numPixels;
                }
                else
                {
                    currentEnemy.CurrentPosX += numPixels;
                }

            }

        }

        public static void TakeDamage(Enemy currentEnemy, int damage)
        {
            currentEnemy.Health -= damage;
            if (currentEnemy.Health <= 0)
            {
                currentEnemy.Alive = false;

                //currentEnemy.Health = FULL_HEALTH;
            }
            currentEnemy.TakingDamage = true;

        }
        public static int debug = 0;
        public static void ChangeFrame(Enemy currentEnemy)
        {
            if (GlobalUtilities.enemyFrameAnimationTicks.ContainsKey(currentEnemy.CurrentEnemyType)) 
            {
                if(currentEnemy.CurrentFrameTicks % GlobalUtilities.enemyFrameAnimationTicks[currentEnemy.CurrentEnemyType] == 0) 
                {
                    if (currentEnemy.CurrentFrame == 1) 
                    {
                        currentEnemy.CurrentFrame = 2;
                    }
                    else 
                    {
                        currentEnemy.CurrentFrame = 1;
                    }
                }
            }
            else 
            {
                if (currentEnemy.CurrentFrameTicks % GlobalUtilities.ENEMY_FRAME_ANIMATION_DEFAULT == 0)
                {
                    if (currentEnemy.CurrentFrame == 1)
                    {
                        currentEnemy.CurrentFrame = 2;
                    }
                    else
                    {
                        currentEnemy.CurrentFrame = 1;
                    }
                }
            }
        }

        public static void GenerateEnemy(Enemy currentEnemy)
        {
            EnemySprite replacementSprite;
            if (currentEnemy.Alive)
            {
                if (!GlobalUtilities.TimeStopped)
                {
                    replacementSprite = HandleDirection(currentEnemy);
                    currentEnemy.CurrentSprite = replacementSprite;
                }
            }
            
        }

        public static EnemySprite HandleDirection(Enemy currentEnemy) 
        {
            EnemySprite replacementSprite;
            if(currentEnemy.CurrentEnemyType == Enemy.EnemyType.Stalfost) 
            {
                if (currentEnemy.CurrentFrame == 1)
                {
                    currentEnemy.CurrentEnemyFrame = EnemySpriteFactory.EnemyFrame.Stalfos1;
                }
                else
                {
                    currentEnemy.CurrentEnemyFrame = EnemySpriteFactory.EnemyFrame.Stalfos2;

                }
            }
            else if(currentEnemy.CurrentEnemyType == Enemy.EnemyType.Aquamentus) 
            {
                if (currentEnemy.CurrentFrame == 1)
                {
                    currentEnemy.CurrentEnemyFrame = EnemySpriteFactory.EnemyFrame.Aquamentus1;
                }
                else
                {
                    currentEnemy.CurrentEnemyFrame = EnemySpriteFactory.EnemyFrame.Aquamentus2;

                }
            }
            else if (currentEnemy.CurrentEnemyType == Enemy.EnemyType.Keese)
            {
                if (currentEnemy.CurrentFrame == 1)
                {
                    currentEnemy.CurrentEnemyFrame = EnemySpriteFactory.EnemyFrame.Keese1;
                }
                else
                {
                    currentEnemy.CurrentEnemyFrame = EnemySpriteFactory.EnemyFrame.Keese2;

                }
            }
            else if (currentEnemy.CurrentEnemyType == Enemy.EnemyType.Gel)
            {
                if (currentEnemy.CurrentFrame == 1)
                {
                    currentEnemy.CurrentEnemyFrame = EnemySpriteFactory.EnemyFrame.Gel1;
                }
                else
                {
                    currentEnemy.CurrentEnemyFrame = EnemySpriteFactory.EnemyFrame.Gel2;

                }
            }
            else if (currentEnemy.CurrentEnemyType == Enemy.EnemyType.BlueGel)
            {
                if (currentEnemy.CurrentFrame == 1)
                {
                    currentEnemy.CurrentEnemyFrame = EnemySpriteFactory.EnemyFrame.BlueGel1;
                }
                else
                {
                    currentEnemy.CurrentEnemyFrame = EnemySpriteFactory.EnemyFrame.BlueGel2;

                }
            }
            else if (currentEnemy.CurrentEnemyType == Enemy.EnemyType.Wallmaster)
            {
                if (currentEnemy.CurrentFrame == 1)
                {
                    currentEnemy.CurrentEnemyFrame = EnemySpriteFactory.EnemyFrame.Wallmaster1;
                }
                else
                {
                    currentEnemy.CurrentEnemyFrame = EnemySpriteFactory.EnemyFrame.Wallmaster2;

                }
            }
            else 
            {
                if (!(currentEnemy.CurrentEnemyType == Enemy.EnemyType.Dodongo && ((Dodongo)currentEnemy).Exploded))
                {
                    currentEnemy.CurrentEnemyFrame = currentEnemy.GetEnemyDirectionFrame();
                }
            }
            replacementSprite = (EnemySprite)EnemySpriteFactory.Instance.CreateSmallEnemy(currentEnemy.CurrentEnemyFrame);
            if(currentEnemy.CurrentEnemyType == Enemy.EnemyType.Aquamentus || currentEnemy.CurrentEnemyType == Enemy.EnemyType.Dodongo) 
            {
                replacementSprite = (EnemySprite)EnemySpriteFactory.Instance.CreateLargeEnemy(currentEnemy.CurrentEnemyFrame);
            }
            return replacementSprite;
        }


    }


}
