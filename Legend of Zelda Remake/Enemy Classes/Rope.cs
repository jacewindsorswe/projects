using System;
using Microsoft.Xna.Framework;
using Sprint2;

namespace Sprint0
{
    public class Rope : Enemy
    {
        private bool seen = false;
        public bool Seen
        {
            get { return seen; }
            set { seen = value; }
        }

        private double seenTime = 0;
        public double SeenTime 
        {
            get { return seenTime; }
            set { seenTime = value; }
        }

        public Rope(Game1 g)
        {
            game = g;
            CurrentEnemyType = Enemy.EnemyType.Rope;
            CurrentEnemyFrame = EnemySpriteFactory.EnemyFrame.Rope1;
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

        public void Update(GameTime timer, Rectangle linkHitbox) 
        {
            CheckMove++;
            if (SeeLink(linkHitbox)) 
            {
                if (!seen) 
                {
                    seenTime = timer.TotalGameTime.TotalSeconds;
                    seen = true;
                }
            }
            if (seen) 
            {
                AlternateSpeed = 1;
                if(timer.TotalGameTime.TotalSeconds - seenTime >= GlobalUtilities.ROPE_CHASE_TIME) 
                {
                    seen = false;
                    AlternateSpeed = 0;
                }
            }
            else 
            {
                if (!Stunned)
                {
                    AlternateSpeed = 0;
                    DirectChange++;
                    if (DirectChange % GlobalUtilities.ENEMY_DIRECTION_BUFFER == 0)
                    {
                        CurrentDirection = GlobalUtilities.RandomDirection(CurrentDirection);
                    }
                    if (CheckMove % GlobalUtilities.ENEMY_MOVEMENT_BUFFER == 0)
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
            }
            if (!Stunned)
            {
                Move();
            }
            else 
            {
                if(timer.TotalGameTime.TotalSeconds - StunTime >= MaxStunTime) 
                {
                    Stunned = false;
                    MaxStunTime = GlobalUtilities.DEFAULT_STUN_TIME;
                }
            }

            Update(timer);
        }

        private bool SeeLink(Rectangle linkHitbox) 
        {
            if ((linkHitbox.Y > CurrentHitbox.Y && linkHitbox.Y < CurrentHitbox.Y + CurrentHitbox.Height) || (linkHitbox.Y + linkHitbox.Height > CurrentHitbox.Y && linkHitbox.Y + linkHitbox.Height < CurrentHitbox.Y + CurrentHitbox.Height))
            {
                if ((CurrentEnemyFrame == EnemySpriteFactory.EnemyFrame.Rope1 || CurrentEnemyFrame == EnemySpriteFactory.EnemyFrame.Rope2) && linkHitbox.X > CurrentHitbox.X) //Rope is "looking" right
                {
                    CurrentDirection = Direction.Right;
                    return true;
                }
                else if((CurrentEnemyFrame == EnemySpriteFactory.EnemyFrame.Rope3 || CurrentEnemyFrame == EnemySpriteFactory.EnemyFrame.Rope4) && linkHitbox.X < CurrentHitbox.X)
                {
                    CurrentDirection = Direction.Left;
                    return true;
                }
            }
            return false;
        }
    }
}