using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using Sprint0;
using Sprint2;
using Sprint2.Item_Classes;
using Sprint2.Item_Classes.ItemStates;
using Sprint2.Link_Classes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Sprint0
{
    public class Link : CollidableObject
    {
        public static int numLinks = 0;
        private int timer = 0;
        private int linkID;
        private bool invisible = false;
        public LinkStateMachine stateMachine;

        public double TimeStopTime 
        {
            get { return stateMachine.TimeStopTime; }
        }
        public bool TimeStopped 
        {
            get { return stateMachine.TimeStopped; }
        }

        public Game1 game;
        public GraphicsDeviceManager graphics;
        public LinkSprite linkSprite;
        private int FrameTicks = 0;

        private int knockbackTickCounter = 0;
        private int invincibilityTickCounter = 0;
        private int timeStopCounter = 0;
        private int attackTicks = 0;
        
        public Color color = Color.White;
        public Color LinkColor 
        {
            get { return color; }
            set { color = value; }
        }
        public Item sword;

        private static int LINK_WIDTH = GlobalUtilities.LINK_WIDTH;
        private static int LINK_HEIGHT = GlobalUtilities.LINK_HEIGHT;

        public static bool GodMode = false;

        public bool GodModeEnabled
        {
            get { return GodMode; }
            set { GodMode = value; }
        }

        public static bool EasyDubMode = false;

        public bool EasyDubModeEnabled
        {
            get { return EasyDubMode; }
            set { EasyDubMode = value; }
        }

        public bool Invisible
        {
            get { return invisible; }
            set { invisible = value;}
        }
        private bool win = false;

        public bool invis
        {
            get { return invisible; }
            set { invisible = value; }
        }
        public bool Win 
        {
            get { return win; }
            set 
            { 
                win = value;
                stateMachine.win = value;
            }
        }
        public int ID 
        {
            get { return linkID; }
        }

        public bool IsInvincible 
        {
            get { return stateMachine.IsInvincible; }
        }

        public bool Invincible
        {
            get { return stateMachine.IsInvincible; }
            set { stateMachine.IsInvincible = value; }
        }

        public int NumKeys 
        {
            get { return stateMachine.NumKeys; }
        }
        public Link(Game1 g)
        {
            game = g;
            stateMachine = new LinkStateMachine();
            linkSprite = (LinkSprite)LinkSpriteFactory.Instance.CreateLink(LinkSpriteFactory.LinkType.DownIdle);
            CurrentHitbox = new Rectangle(stateMachine.posX, stateMachine.posY, LINK_WIDTH, LINK_HEIGHT);
            sword = ItemSpriteFactory.Instance.CreateItem(Item.ItemCategory.Sword, CurrentXPos, CurrentYPos);
            AddLinks();
        }
        private void AddLinks()
        {
            numLinks++;
            linkID = numLinks;
            links.Add(ID, this);
        }

        public void CollectKey() 
        {
            LinkInventory.PickupKey();
            game._soundEffectGenerator.Play("LOZ_Get_Heart");
        }
        public void UnlockDoor() 
        {
            LinkInventory.UnlockDoor();
            game._soundEffectGenerator.Play("LOZ_Door_Unlock");
        }

        //Controls how close the sword is to Link's body--the higher the number, the closer
        private const int HOLDING_MODIFIER = GlobalUtilities.LINK_HOLDING_MODIFIER;

        //Controls where the sword will spawn relative to link
        //Labeled as {x, y} where sword.X = link.X + offsets[0]
        //                        sword.Y = link.Y + offsets[1]

        public static SortedDictionary<int, Link> links = new SortedDictionary<int, Link>();
        public static SortedDictionary<int, Link> LinkDictionary 
        {
            get { return links; }
        }
        public static void RemoveLink(int id) 
        {
            links.Remove(id);
        }

        private bool active = true;
        public bool IsActive 
        {
            get { return active; }
            set { active = value; }
        }

        private Dictionary<LinkStateMachine.Direction, int[]> offsets = new Dictionary<LinkStateMachine.Direction, int[]>()
        {
            { LinkStateMachine.Direction.Left, new int[]{ (Sword.HORIZ_SWORD_WIDTH * -1) + HOLDING_MODIFIER, LINK_HEIGHT / 2 } },
            { LinkStateMachine.Direction.Right, new int[]{ LINK_WIDTH + (HOLDING_MODIFIER * -1), LINK_HEIGHT / 2 } },
            { LinkStateMachine.Direction.Up, new int[]{ 5, (Sword.VERT_SWORD_HEIGHT * -1) + HOLDING_MODIFIER } },
            { LinkStateMachine.Direction.Down, new int[]{ LINK_WIDTH / 2, LINK_HEIGHT + (HOLDING_MODIFIER * -1) } },
        };

        private double lastKnockbackMovementTime;
        public double KnockbackMovementTime 
        {
            get { return lastKnockbackMovementTime; }
            set { lastKnockbackMovementTime = value; }
        }
        private Rectangle currentHitbox;
        public Rectangle CurrentHitbox
        {
            get {return currentHitbox;}
            set {currentHitbox = value;}
        }

        public CollidableObject.Type CollidableType
        {
            get { return CollidableObject.Type.Link; }
        }
        public void MakeInvisible()
        {
            invisible = true;
            timer = 30;
        }
        public void MakeVisible()
        {
            invisible = false;
        }
        public void MakeInvincible(GameTime gameTime)
        {
            stateMachine.IsInvincible = true;
            stateMachine.invincibleTime = gameTime.TotalGameTime.TotalSeconds;
        }

        public void Left()
        {
            sword.IsActive = false;
            if (stateMachine.CurrentAction != LinkStateMachine.ActionType.Knockback && !invisible)
            {
                stateMachine.Left();
            }
        }
        public void Left(int numPixels)
        {
            stateMachine.MoveLeft(numPixels);
        }
        public void Right()
        {
            sword.IsActive = false;
            if (stateMachine.CurrentAction != LinkStateMachine.ActionType.Knockback && !invisible)
            {
                stateMachine.Right();
            }
        }
        public void Right(int numPixels)
        {
            stateMachine.MoveRight(numPixels);
        }
        public void Up()
        {
            sword.IsActive = false;
            if (stateMachine.CurrentAction != LinkStateMachine.ActionType.Knockback && !invisible)
            {
                stateMachine.Up();
            }
        }
        public void Up(int numPixels)
        {
            stateMachine.MoveUp(numPixels);
        }
        public void Down()
        {
            sword.IsActive = false;
            if (stateMachine.CurrentAction != LinkStateMachine.ActionType.Knockback && !invisible)
            {
                stateMachine.Down();
            }
        }
        public void Down(int numPixels)
        {
            stateMachine.MoveDown(numPixels);
        }
        public void AttackSword()
        {
            if (stateMachine.CurrentAction != LinkStateMachine.ActionType.Knockback)
            {
                stateMachine.AttackSword();
                sword.FrameDirection = ConvertLinkDirectionToItemDirection(stateMachine._direction);
                sword.IsActive = true;
                SetSwordPosition();
            }
        }
        public void ShootProjectile()
        {
            sword.IsActive = false;
            if (stateMachine.CurrentAction != LinkStateMachine.ActionType.Knockback)
            {
                stateMachine.ShootProjectile();
            }
        }
        public void Idle()
        {
            sword.IsActive = false;
            if (stateMachine.CurrentAction != LinkStateMachine.ActionType.Knockback)
            {
                stateMachine.Idle();
            }
        }
        public void TakeDamage(GameTime gameTime, int damage)
        {
            if (!stateMachine.IsInvincible && !TimeStopped)
            {
                stateMachine.TakeDamage(gameTime, damage);

                if (!stateMachine.dead)
                {
                    game._soundEffectGenerator.Play("LOZ_Link_Hurt");
                } 
                else
                {
                    game._soundEffectGenerator.Play("LOZ_Link_Die");
                    MediaPlayer.Stop();
                }
            }
        }

        public void StopTime(GameTime gameTime) 
        {
            stateMachine.TimeStopTime = gameTime.TotalGameTime.TotalSeconds;
            stateMachine.TimeStopped = true;
        }

        public void ResumeTime() 
        {
            stateMachine.TimeStopped = false;
        }

        

        public void Update(GameTime gameTime)
        {
            HandleInvisible();
            FrameTicks += 1;
            stateMachine.changeFrame(FrameTicks);

            HandleLinkAction(gameTime);

            if(stateMachine.IsInvincible && gameTime.TotalGameTime.TotalSeconds - stateMachine.KnockbackTime < GlobalUtilities.LINK_INVINCIBILITY_TIME && !GodModeEnabled) 
            {
                LinkHitboxColor();
            }
            else if(stateMachine.IsInvincible && !GodModeEnabled)
            {
                stateMachine.IsInvincible = false;
                color = Color.White;
                invincibilityTickCounter = 0;
            }

            if(stateMachine.TimeStopped && gameTime.TotalGameTime.TotalSeconds - stateMachine.TimeStopTime < GlobalUtilities.LINK_TIME_STOP_TIME) 
            {
                AnimateTimeStopFrame();
            }
            else if (stateMachine.TimeStopped) 
            {
                stateMachine.TimeStopped = false;
                color = Color.White;
                timeStopCounter = 0;
                GlobalUtilities.ResumeTime(this, game, gameTime);
            }
            AttackTicksHelper();
            HandleDeathWin();

            CurrentHitbox = new Rectangle(stateMachine.posX, stateMachine.posY, LINK_WIDTH, LINK_HEIGHT);
        }
        private void HandleLinkAction(GameTime gameTime)
        {
            if (stateMachine.CurrentAction == LinkStateMachine.ActionType.Knockback && gameTime.TotalGameTime.TotalSeconds - stateMachine.KnockbackTime >= GlobalUtilities.LINK_KNOCKBACK_TIME)
            {
                stateMachine.CurrentAction = LinkStateMachine.ActionType.Idle;
                knockbackTickCounter = 0;
            }
            else if (stateMachine.CurrentAction == LinkStateMachine.ActionType.Knockback)
            {

                if (knockbackTickCounter % GlobalUtilities.LINK_KNOCKBACK_INTERVAL == 0)
                {
                    stateMachine.Knockback(GlobalUtilities.LINK_KNOCKBACK_MOVEMENT);
                    lastKnockbackMovementTime = gameTime.TotalGameTime.TotalSeconds;
                    knockbackTickCounter = 0;
                }
                else
                {
                    knockbackTickCounter++;
                }
            }
        }
        private void HandleInvisible()
        {
            timer--;
            if (invisible && timer == 0)
            {
                MakeVisible();
            }
        }

        public bool Dead 
        {
            get { return stateMachine.dead; }
            set { stateMachine.dead = value; }
        }
        private void HandleDeathWin()
        {
            if (Win)
            {
                stateMachine.HandleWinAnimation();
            }

            if (stateMachine.dead)
            {
                stateMachine.HandleDeathAnimation();
            }
        }
        private void AttackTicksHelper()
        {
            if (stateMachine.CurrentAction == LinkStateMachine.ActionType.Attacking || stateMachine.CurrentAction == LinkStateMachine.ActionType.Shooting)
            {
                attackTicks++;
            }
            else
            {
                attackTicks = 0;
            }
            if (FrameTicks > 60)
            {
                FrameTicks = 0;
            }
        }


        private void AnimateTimeStopFrame() 
        {
            if(timeStopCounter % GlobalUtilities.LINK_TIME_STOP_BLINK == 0) 
            {
                if(color == Color.CornflowerBlue) 
                {
                    color = Color.IndianRed;
                }
                else if(color == Color.GhostWhite) 
                {
                    color = Color.CornflowerBlue;
                }
                else 
                {
                    color = Color.GhostWhite;
                }
            }
            timeStopCounter++;
        }
        private void LinkHitboxColor()
        {
            if (invincibilityTickCounter % GlobalUtilities.LINK_INVINCIBILITY_FRAME == 0)
            {
                if (color == Color.Red)
                {
                    color = Color.Blue;
                }
                else
                {
                    color = Color.Red;
                }
            }
            invincibilityTickCounter++;
        }

        public void Draw(SpriteBatch spriteBatch, Color col) 
        {
            if (!invisible)
            {
                linkSprite = stateMachine.generateSprite();
                linkSprite.Draw(spriteBatch, CurrentHitbox, color);
                IdleChecker();
            }
        }
        private void IdleChecker()
        {
            if (stateMachine.CurrentAction != LinkStateMachine.ActionType.Attacking && stateMachine.CurrentAction != LinkStateMachine.ActionType.Shooting)
            {
                Idle();
            }
            else
            {
                if (attackTicks % GlobalUtilities.FRAME_SPEED == 0)
                {
                    Idle();
                    attackTicks = 0;
                    sword.IsActive = false;
                }
            }
        }
        public int CurrentXPos
        {
            get { return stateMachine.posX; }
            set { stateMachine.posX = value; }
        }

        public int CurrentYPos
        {
            get { return stateMachine.posY; }
            set { stateMachine.posY = value; }
        }

        public LinkStateMachine.Direction direction
        {
            get { return stateMachine._direction; }
            set { stateMachine._direction = value; }
        }

        public void CollectRupee(int amount) 
        {
            stateMachine.CollectRupees(amount);
            game._soundEffectGenerator.Play("LOZ_Get_Rupee");
        }

        public LinkStateMachine.ActionType CurrentAction 
        {
            get { return stateMachine.CurrentAction; }
            set { stateMachine.CurrentAction = value; }
        }

        public static Item.Direction ConvertLinkDirectionToItemDirection(LinkStateMachine.Direction direct) 
        {
            switch (direct) 
            {
                case LinkStateMachine.Direction.Left:
                    return Item.Direction.Left;
                case LinkStateMachine.Direction.Right:
                    return Item.Direction.Right;
                case LinkStateMachine.Direction.Up:
                    return Item.Direction.Up;
                default:
                    return Item.Direction.Down;
            }
        }

        private void SetSwordPosition() 
        {
            sword.X = CurrentXPos + offsets[direction][0];
            sword.Y = CurrentYPos + offsets[direction][1];
        }

        public static LinkStateMachine.Direction GetOppositeDirection(LinkStateMachine.Direction direct) 
        {
            switch (direct) 
            {
                case LinkStateMachine.Direction.Up:
                    return LinkStateMachine.Direction.Down;
                case LinkStateMachine.Direction.Down:
                    return LinkStateMachine.Direction.Up;
                case LinkStateMachine.Direction.Right:
                    return LinkStateMachine.Direction.Left;
                default:
                    return LinkStateMachine.Direction.Right;
            }
        }

        public object Clone()
        {
            return new Link(game);
        }
    }

       

}

