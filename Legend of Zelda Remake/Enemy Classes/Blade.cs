using Microsoft.Xna.Framework;
using Sprint2;
using System;

namespace Sprint0
{
    public class Blade : Enemy
    {
        int moveTicks = 0;
        int tempTicks = 0;

        public static int BLADE_HEALTH = 15;
        public enum BladeState { Sensing, Chasing, Retreating }
        private BladeState state;
        public BladeState State 
        {
            get { return state; }
            set { state = value; }
        }

        public Blade(Game1 g)
        {
            game = g;
            CurrentEnemyType = Enemy.EnemyType.Blade;
            CurrentEnemyFrame = EnemySpriteFactory.EnemyFrame.Blade;
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

        public void Update(GameTime timer, int destX, int destY)
        {
            if (!Stunned)
            {
                switch (state)
                {
                    case BladeState.Sensing:
                        if (InBounds(destX, destY))
                        {
                            state = BladeState.Chasing;
                            CurrentDirection = GetDirection(destX, destY).Item1;
                        }
                        Move();
                        break;
                    case BladeState.Chasing:
                        AlternateSpeed = 0;
                        if (!InBounds(destX, destY))
                        {
                            state = BladeState.Retreating;
                            CurrentDirection = GlobalUtilities.GetDirectionOpposite(CurrentDirection);
                        }
                        Move();
                        break;
                    default:
                        AlternateSpeed = 1;
                        Move();
                        break;
                }
            }
            else
            {
                if (timer.TotalGameTime.TotalSeconds - StunTime > MaxStunTime)
                {
                    Stunned = false;
                    MaxStunTime = GlobalUtilities.DEFAULT_STUN_TIME;
                }
            }

            Update(timer);
        }

        private bool InBounds(int x, int y) 
        {
            if( (CurrentPosX < x && CurrentPosX + CurrentHitbox.Width > x) || (CurrentPosX < x + GlobalUtilities.LINK_WIDTH && CurrentPosX + CurrentHitbox.Width > x + GlobalUtilities.LINK_WIDTH)) 
            {
                return true;
            }
            if ((CurrentPosY < y && CurrentPosY + CurrentHitbox.Height > y) || (CurrentPosY < y + GlobalUtilities.LINK_HEIGHT && CurrentPosY + CurrentHitbox.Height > y + GlobalUtilities.LINK_HEIGHT)) 
            {
                return true;
            }
            return false;
        }

        public Tuple<Direction, bool> GetDirection(int destX, int destY) 
        {
            if (!((destX - CurrentPosX >= 0 && (CurrentPosX + CurrentHitbox.Width) - destX >= 0) || (destX + GlobalUtilities.LINK_WIDTH - CurrentPosX >= 0 && CurrentPosX + CurrentHitbox.Width - (destX + GlobalUtilities.LINK_WIDTH) > 0)) || !((destY - CurrentPosY >= 0 && (CurrentPosY + CurrentHitbox.Height) - destY >= 0) || (destY + GlobalUtilities.LINK_HEIGHT - CurrentPosY >= 0 && CurrentPosY + CurrentHitbox.Height - (destY + GlobalUtilities.LINK_HEIGHT) > 0)))
            {
                if ((destX - CurrentPosX >= 0 && (CurrentPosX + CurrentHitbox.Width) - destX >= 0) || (destX + GlobalUtilities.LINK_WIDTH - CurrentPosX >= 0 && CurrentPosX + CurrentHitbox.Width - (destX + GlobalUtilities.LINK_WIDTH) > 0))
                {
                    if (CurrentPosY > destY)
                    {
                        return Tuple.Create(Direction.Up, true);
                    }
                    else
                    {
                        return Tuple.Create(Direction.Down, true);
                    }
                }
                else if ((destY - CurrentPosY >= 0 && (CurrentPosY + CurrentHitbox.Height) - destY >= 0) || (destY + GlobalUtilities.LINK_HEIGHT - CurrentPosY >= 0 && CurrentPosY + CurrentHitbox.Height - (destY + GlobalUtilities.LINK_HEIGHT) > 0))
                {
                    if (CurrentPosX > destX)
                    {
                        return Tuple.Create(Direction.Left, true);
                    }
                    else
                    {
                        return Tuple.Create(Direction.Right, true);
                    }
                }
            }
            return Tuple.Create(CurrentDirection, false);
        }
    }
}
