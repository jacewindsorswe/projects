using Microsoft.Xna.Framework;
using Sprint2;
using Sprint2.Link_Classes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace Sprint0
{
    public class LinkStateMachine
    {
        public const int LINK_HEALTH_DEFAULT = 60; //Link has 3 hearts to start--I have it so each heart = 20 points

        public enum Direction { Up, Down, Left, Right };
        public Direction _direction = Direction.Down;
        public int posX = GlobalUtilities.DEFAULT_LINK_POS_X;
        public int posY = GlobalUtilities.DEFAULT_LINK_POS_Y;
        public int Frame = 1;
        public bool takingDamage = false;

        public bool dead = false;
        public bool win = false;

        public bool invincible = false;
        public bool IsInvincible 
        {
            get { return invincible; }
            set { invincible = value; }
        }

        private bool timeStopped = false;
        public bool TimeStopped 
        {
            get { return timeStopped; }
            set { timeStopped = value; }
        }
        private double timeStopTime;
        public double TimeStopTime
        {
            get { return timeStopTime; }
            set { timeStopTime = value; }
        }

        private double knockTime;
        public double KnockbackTime 
        {
            get { return knockTime; }
            set { knockTime = value; }
        }
        public double invincibleTime;
        public double InvincibleTime 
        {
            get { return invincibleTime; }
            set { invincibleTime = value; }
        }
        

        public enum ActionType { Idle, Moving, Attacking, Shooting, Damaged, Knockback }
        private ActionType currentAction = ActionType.Idle;

        public ActionType CurrentAction 
        {
            get { return currentAction; }
            set { currentAction = value; }
        }

        public LinkSprite linkSprite;
        public bool attacking = false;
        LinkSpriteFactory.LinkType deathTurn = LinkSpriteFactory.LinkType.DownIdle;

        public int NumKeys 
        {
            get { return LinkInventory.Inventory[Sprint2.Item_Classes.Item.ItemCategory.Key]; }
        }

        public void Left()
        {
            if (!(_direction.Equals(Direction.Left)))
            {
                _direction = Direction.Left;
            }
            else
            {
                Move();
            }
        }
        public void Left(int numPixels)
        {
            if (!(_direction.Equals(Direction.Left)))
            {
                _direction = Direction.Left;
            }
            else
            {
                Move(numPixels);
            }
        }
        public void Right()
        {
            if (!(_direction.Equals(Direction.Right)))
            {
                _direction = Direction.Right;
            }
            else
            {
                Move();
            }
        }
        public void Right(int numPixels)
        {
            if (!(_direction.Equals(Direction.Right)))
            {
                _direction = Direction.Right;
                Move(numPixels);
            }
            else
            {
                Move(numPixels);
            }
        }
        public void Up()
        {
            if (!(_direction.Equals(Direction.Up)))
            {
                _direction = Direction.Up;
            }
            else
            {
                Move();
            }
        }
        public void Up(int numPixels)
        {
            if (!(_direction.Equals(Direction.Up)))
            {
                _direction = Direction.Up;
            }
            else
            {
                Move(numPixels);
            }
        }
        public void Down()
        {
            if (!(_direction.Equals(Direction.Down)))
            {
                _direction = Direction.Down;
            }
            else
            {
                Move();
            }
        }
        public void Down(int numPixels)
        {
            if (!(_direction.Equals(Direction.Down)))
            {
                _direction = Direction.Down;
            }
            else
            {
                Move(numPixels);
            }
        }

        public void MoveRight(int numPixels) 
        {
            posX += numPixels;
        }
        public void MoveLeft(int numPixels) 
        {
            posX -= numPixels;
        }
        public void MoveUp(int numPixels) 
        {
            posY -= numPixels;
        }
        public void MoveDown(int numPixels) 
        {
            posY += numPixels;
        }

        public void Move()
        {
            //Blocks normal movement if Link is being knocked back
            if (currentAction != ActionType.Knockback)
            {
                if (!currentAction.Equals(ActionType.Moving))
                {
                    currentAction = ActionType.Moving;
                }
                if (!dead)
                {
                    if (_direction.Equals(Direction.Left))
                    {
                        posX -= GlobalUtilities.LINK_SPEED;
                    }
                    else if (_direction.Equals(Direction.Right))
                    {
                        posX += GlobalUtilities.LINK_SPEED;
                    }
                    else if (_direction.Equals(Direction.Up))
                    {
                        posY -= GlobalUtilities.LINK_SPEED;
                    }
                    else
                    {
                        posY += GlobalUtilities.LINK_SPEED;
                    }
                }
            }
        }
        public void Move(int numPixels)
        {
            if (!currentAction.Equals(ActionType.Moving) && currentAction != ActionType.Knockback )
            {
                currentAction = ActionType.Moving;
            }
            if (!dead)
            {
                if (_direction.Equals(Direction.Left))
                {
                    posX -= numPixels;
                }
                else if (_direction.Equals(Direction.Right))
                {
                    posX += numPixels;
                }
                else if (_direction.Equals(Direction.Up))
                {
                    posY -= numPixels;
                }
                else
                {
                    posY += numPixels;
                }
            }
        }

        public void Knockback(int numPixels) 
        {
            Move(numPixels);
        }
        public void AttackSword()
        {
            if (currentAction != ActionType.Attacking && currentAction != ActionType.Knockback && !dead)
            {
                currentAction = ActionType.Attacking;
            }
        }
        public void ShootProjectile()
        {
            if (currentAction != ActionType.Shooting && currentAction != ActionType.Knockback && !dead)
            {
                currentAction = ActionType.Shooting;
            }
        }
        public void Idle()
        {
            if (currentAction != ActionType.Idle && currentAction != ActionType.Knockback)
            {
                currentAction = ActionType.Idle;
            }
        }
        public void TakeDamage(GameTime gameTime, int damage)
        {
            if (!IsInvincible)
            {
                LinkInventory.TakeDamage(damage);
                if (LinkInventory.Health <= 0)
                {
                    dead = true;
                    Game1.GameState = Game1.GameStateType.GameOver;
                    Game1.deathTime = gameTime.TotalGameTime.TotalSeconds;
                }
                takingDamage = true;

                currentAction = ActionType.Knockback;
                knockTime = gameTime.TotalGameTime.TotalSeconds;

                IsInvincible = true;
                invincibleTime = gameTime.TotalGameTime.TotalSeconds;
            }
        }

        public void changeFrame(int FrameTicks)
        {
            if(Frame == 1 && ((FrameTicks >= 15 && FrameTicks < 30) || (FrameTicks >= 45)))
            {
                Frame = 2;
            } else if (Frame == 2 && (FrameTicks >60 || FrameTicks <15 || (FrameTicks >= 30 && FrameTicks < 45)))
            {
                Frame = 1;
            }
        }

        public LinkSprite generateSprite()
        {
            if (!dead && !win)
            {
                switch (_direction)
                {
                    case (LinkStateMachine.Direction.Up):
                        HandleDirectionUp();
                        break;
                    case (LinkStateMachine.Direction.Down):
                        HandleDirectionDown();
                        break;
                    case (LinkStateMachine.Direction.Left):
                        HandleDirectionLeft();
                        break;
                    case (LinkStateMachine.Direction.Right):
                        HandleDirectionRight();
                        break;
                }
            }
            /*else
            {
                HandleDead();
            }*/
            return linkSprite;
        }

        public void HandleDead()
        {
            linkSprite = (LinkSprite)LinkSpriteFactory.Instance.CreateLink(LinkSpriteFactory.LinkType.Dead);
        }

        public void HandleDirectionUp()
        {
            switch (currentAction)
            {
                case (LinkStateMachine.ActionType.Idle):
                    linkSprite = (LinkSprite)LinkSpriteFactory.Instance.CreateLink(LinkSpriteFactory.LinkType.UpIdle);
                    break;

                case (LinkStateMachine.ActionType.Attacking):
                    linkSprite = (LinkSprite)LinkSpriteFactory.Instance.CreateLink(LinkSpriteFactory.LinkType.UpAttackSword);
                    break;

                case (LinkStateMachine.ActionType.Shooting):
                    linkSprite = (LinkSprite)LinkSpriteFactory.Instance.CreateLink(LinkSpriteFactory.LinkType.UpShootProjectile);
                    break;

                case (LinkStateMachine.ActionType.Moving):
                    if (Frame == 1)
                    {
                        linkSprite = (LinkSprite)LinkSpriteFactory.Instance.CreateLink(LinkSpriteFactory.LinkType.UpIdle);
                    }
                    else
                    {
                        linkSprite = (LinkSprite)LinkSpriteFactory.Instance.CreateLink(LinkSpriteFactory.LinkType.UpMove);
                    }
                    break;

                default:
                    break;
            }
        }
        public void HandleDirectionDown()
        {
            switch (currentAction)
            {
                case (LinkStateMachine.ActionType.Idle):
                    linkSprite = (LinkSprite)LinkSpriteFactory.Instance.CreateLink(LinkSpriteFactory.LinkType.DownIdle);
                    break;

                case (LinkStateMachine.ActionType.Attacking):
                    linkSprite = (LinkSprite)LinkSpriteFactory.Instance.CreateLink(LinkSpriteFactory.LinkType.DownAttackSword);
                    break;

                case (LinkStateMachine.ActionType.Shooting):
                    linkSprite = (LinkSprite)LinkSpriteFactory.Instance.CreateLink(LinkSpriteFactory.LinkType.DownShootProjectile);
                    break;

                case (LinkStateMachine.ActionType.Moving):
                    if (Frame == 1)
                    {
                        linkSprite = (LinkSprite)LinkSpriteFactory.Instance.CreateLink(LinkSpriteFactory.LinkType.DownIdle);
                    }
                    else
                    {
                        linkSprite = (LinkSprite)LinkSpriteFactory.Instance.CreateLink(LinkSpriteFactory.LinkType.DownMove);
                    }
                    break;

                default:
                    break;
            }
        }
        public void HandleDirectionLeft()
        {
            switch (currentAction)
            {
                case (LinkStateMachine.ActionType.Idle):
                    linkSprite = (LinkSprite)LinkSpriteFactory.Instance.CreateLink(LinkSpriteFactory.LinkType.LeftIdle);
                    break;

                case (LinkStateMachine.ActionType.Attacking):
                    linkSprite = (LinkSprite)LinkSpriteFactory.Instance.CreateLink(LinkSpriteFactory.LinkType.LeftAttackSword);
                    break;

                case (LinkStateMachine.ActionType.Shooting):
                    linkSprite = (LinkSprite)LinkSpriteFactory.Instance.CreateLink(LinkSpriteFactory.LinkType.LeftShootProjectile);
                    break;

                case (LinkStateMachine.ActionType.Moving):
                    if (Frame == 1)
                    {
                        linkSprite = (LinkSprite)LinkSpriteFactory.Instance.CreateLink(LinkSpriteFactory.LinkType.LeftIdle);
                    }
                    else
                    {
                        linkSprite = (LinkSprite)LinkSpriteFactory.Instance.CreateLink(LinkSpriteFactory.LinkType.LeftMove);
                    }
                    break;

                default:
                    break;
            }
        }
        public void HandleDirectionRight()
        {
            switch (currentAction)
            {
                case (LinkStateMachine.ActionType.Idle):
                    linkSprite = (LinkSprite)LinkSpriteFactory.Instance.CreateLink(LinkSpriteFactory.LinkType.RightIdle);
                    break;

                case (LinkStateMachine.ActionType.Attacking):
                    linkSprite = (LinkSprite)LinkSpriteFactory.Instance.CreateLink(LinkSpriteFactory.LinkType.RightAttackSword);
                    break;

                case (LinkStateMachine.ActionType.Shooting):
                    linkSprite = (LinkSprite)LinkSpriteFactory.Instance.CreateLink(LinkSpriteFactory.LinkType.RightShootProjectile);
                    break;

                case (LinkStateMachine.ActionType.Moving):
                    if (Frame == 1)
                    {
                        linkSprite = (LinkSprite)LinkSpriteFactory.Instance.CreateLink(LinkSpriteFactory.LinkType.RightIdle);
                    }
                    else
                    {
                        linkSprite = (LinkSprite)LinkSpriteFactory.Instance.CreateLink(LinkSpriteFactory.LinkType.RightMove);
                    }
                    break;

                default:
                    break;
            }
        }

        public void HandleDeathAnimation() 
        {
            Frame++;
            if(Frame % 5 == 0) 
            {
                SpinRight();
            }
            linkSprite = (LinkSprite)LinkSpriteFactory.Instance.CreateLink(deathTurn);
        }

        public void HandleWinAnimation()
        {
            linkSprite = (LinkSprite)LinkSpriteFactory.Instance.CreateLink(LinkSpriteFactory.LinkType.Winning);
        }

        private void SpinRight() 
        {
            switch (deathTurn) 
            {
                case LinkSpriteFactory.LinkType.DownIdle:
                    deathTurn = LinkSpriteFactory.LinkType.LeftIdle;
                    break;
                case LinkSpriteFactory.LinkType.LeftIdle:
                    deathTurn = LinkSpriteFactory.LinkType.UpIdle;
                    break;
                case LinkSpriteFactory.LinkType.UpIdle:
                    deathTurn = LinkSpriteFactory.LinkType.RightIdle;
                    break;
                default:
                    deathTurn = LinkSpriteFactory.LinkType.DownIdle;
                    break;
            }
        }

        public void CollectRupees(int amount) 
        {
            LinkInventory.PickupRupees(amount);
        }
        public void CollectKey() 
        {
            LinkInventory.PickupKey();
        }
        public void UnlockDoor() 
        {
            LinkInventory.UnlockDoor();
        }
    }
}
