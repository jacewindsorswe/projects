using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Sprint2;
using Sprint2.Block_Classes;
using Sprint2.Enemy_Classes;
using Sprint2.Item_Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;


namespace Sprint0
{
    public class Enemy : CollidableObject
    {
        public static int numEnemies = 0;
        public int enemyID = 0;

        public static readonly int MAX_ANIMATE_TICKS = 70;
        public static readonly int ALTERNATE_DAMAGE_FRAME_TICKS = 3;
        public static readonly List<Color> damageColors = new()
        {
            Color.OrangeRed,
            Color.GhostWhite,
            Color.Aquamarine,
            Color.LightGoldenrodYellow
        };
        private int damageColorIndex = 0;

        private int alternateSpeed = 0;
        public int AlternateSpeed 
        {
            get { return alternateSpeed; }
            set { alternateSpeed = value; }
        }

        public bool EasyDubMode = false;
        public bool EasyDubModeEnabled
        {
            get { return EasyDubMode; }
            set { EasyDubMode = value; }
        }

        public int ID 
        {
            get { return enemyID; }
        }

        public static SortedDictionary<int, Enemy> enemies = new SortedDictionary<int, Enemy>();
        private static List<Enemy> enemyList = new List<Enemy>();

        //private SoundEffect soundEffect;

        private bool stunned = false;
        private double stunTime = 0;
        private double maxStunTime = GlobalUtilities.DEFAULT_STUN_TIME;
        public double MaxStunTime 
        {
            get { return maxStunTime; }
            set { maxStunTime = value; }
        }
        public bool Stunned { get { return stunned; } set { stunned = value; } }
        public double StunTime { get { return stunTime; } set { stunTime = value; } }

        private bool active = true;
        public bool IsActive 
        {
            get 
            {
                return active;
            }
            set 
            {
                active = value;
            }
        }
        public static SortedDictionary<int, Enemy> EnemiesDictionary
        {
            get { return enemies; }
        }
        public static void RemoveEnemy(int key)
        {
            enemies.Remove(key);
            

        }

        public static List<Enemy> EnemyList
        {
            get
            {
                enemyList.Clear();
                foreach (KeyValuePair<int, Enemy> pair in enemies)
                {
                    enemyList.Add(pair.Value);
                }
                return enemyList;
            }
        }
        public enum EnemyType { Stalfost, Darknut, Wizzrobe, Goriya, BlueGoriya, Aquamentus, Keese, Blade, Gel, Wallmaster, Death, BlueGel, Rope, Dodongo, MoldormSection };

        private static Dictionary<EnemyType, List<EnemySpriteFactory.EnemyFrame>> enemyDictionary = new Dictionary<EnemyType, List<EnemySpriteFactory.EnemyFrame>>()
        {
            { EnemyType.Stalfost, new List<EnemySpriteFactory.EnemyFrame> { EnemySpriteFactory.EnemyFrame.Stalfos1, EnemySpriteFactory.EnemyFrame.Stalfos2 } },
            { EnemyType.Aquamentus, new List<EnemySpriteFactory.EnemyFrame> { EnemySpriteFactory.EnemyFrame.Aquamentus1, EnemySpriteFactory.EnemyFrame.Aquamentus2 } },
            { EnemyType.Keese, new List<EnemySpriteFactory.EnemyFrame> { EnemySpriteFactory.EnemyFrame.Keese1, EnemySpriteFactory.EnemyFrame.Keese2 } },
            { EnemyType.Gel, new List<EnemySpriteFactory.EnemyFrame> { EnemySpriteFactory.EnemyFrame.Gel1, EnemySpriteFactory.EnemyFrame.Gel2 } },
            { EnemyType.BlueGel, new List<EnemySpriteFactory.EnemyFrame> { EnemySpriteFactory.EnemyFrame.BlueGel1, EnemySpriteFactory.EnemyFrame.BlueGel2 } },
            { EnemyType.Wallmaster, new List<EnemySpriteFactory.EnemyFrame> { EnemySpriteFactory.EnemyFrame.Wallmaster1, EnemySpriteFactory.EnemyFrame.Wallmaster2 } },

            { EnemyType.Blade, new List<EnemySpriteFactory.EnemyFrame> { EnemySpriteFactory.EnemyFrame.Blade } },

            { EnemyType.Rope, new List<EnemySpriteFactory.EnemyFrame> { EnemySpriteFactory.EnemyFrame.Rope1, EnemySpriteFactory.EnemyFrame.Rope2, EnemySpriteFactory.EnemyFrame.Rope3, EnemySpriteFactory.EnemyFrame.Rope4 } },
            { EnemyType.MoldormSection, new List<EnemySpriteFactory.EnemyFrame> { EnemySpriteFactory.EnemyFrame.MoldormSection } },

            { EnemyType.Wizzrobe, new List<EnemySpriteFactory.EnemyFrame> { EnemySpriteFactory.EnemyFrame.WizzrobeUp1, EnemySpriteFactory.EnemyFrame.WizzrobeUp2, EnemySpriteFactory.EnemyFrame.WizzrobeLeft1, EnemySpriteFactory.EnemyFrame.WizzrobeLeft2, EnemySpriteFactory.EnemyFrame.WizzrobeRight1, EnemySpriteFactory.EnemyFrame.WizzrobeRight2 } },
            { EnemyType.Goriya, new List<EnemySpriteFactory.EnemyFrame> { EnemySpriteFactory.EnemyFrame.GoriyaForward, EnemySpriteFactory.EnemyFrame.GoriyaBackward, EnemySpriteFactory.EnemyFrame.GoriyaLeft1, EnemySpriteFactory.EnemyFrame.GoriyaLeft2, EnemySpriteFactory.EnemyFrame.GoriyaRight1, EnemySpriteFactory.EnemyFrame.GoriyaRight2 } },
            { EnemyType.BlueGoriya, new List<EnemySpriteFactory.EnemyFrame> { EnemySpriteFactory.EnemyFrame.BlueGoriyaForward, EnemySpriteFactory.EnemyFrame.BlueGoriyaBackward, EnemySpriteFactory.EnemyFrame.BlueGoriyaLeft1, EnemySpriteFactory.EnemyFrame.BlueGoriyaLeft2, EnemySpriteFactory.EnemyFrame.BlueGoriyaRight1, EnemySpriteFactory.EnemyFrame.BlueGoriyaRight2 } },
            { EnemyType.Darknut, new List<EnemySpriteFactory.EnemyFrame> { EnemySpriteFactory.EnemyFrame.DarknutForward, EnemySpriteFactory.EnemyFrame.DarknutBackward, EnemySpriteFactory.EnemyFrame.DarknutLeft1, EnemySpriteFactory.EnemyFrame.DarknutLeft2, EnemySpriteFactory.EnemyFrame.DarknutRight1, EnemySpriteFactory.EnemyFrame.DarknutRight2 } },

            { EnemyType.Dodongo, new List<EnemySpriteFactory.EnemyFrame> { EnemySpriteFactory.EnemyFrame.DodongoUp1, EnemySpriteFactory.EnemyFrame.DodongoUp2, EnemySpriteFactory.EnemyFrame.DodongoDown1, EnemySpriteFactory.EnemyFrame.DodongoDown2, EnemySpriteFactory.EnemyFrame.DodongoLeft1, EnemySpriteFactory.EnemyFrame.DodongoLeft2, EnemySpriteFactory.EnemyFrame.DodongoRight1, EnemySpriteFactory.EnemyFrame.DodongoRight2, EnemySpriteFactory.EnemyFrame.DodongoUpBomb, EnemySpriteFactory.EnemyFrame.DodongoDownBomb, EnemySpriteFactory.EnemyFrame.DodongoLeftBomb, EnemySpriteFactory.EnemyFrame.DodongoRightBomb } },

            { EnemyType.Death, new List<EnemySpriteFactory.EnemyFrame> { EnemySpriteFactory.EnemyFrame.Death1, EnemySpriteFactory.EnemyFrame.Death2, EnemySpriteFactory.EnemyFrame.Death3, EnemySpriteFactory.EnemyFrame.Death4 } }
        };

        private static Dictionary<EnemyType, int> enemyDamageDictionary = new Dictionary<EnemyType, int>()
        {
            { EnemyType.Darknut, GlobalUtilities.DEFAULT_ENEMY_DAMAGE },
            { EnemyType.Stalfost, GlobalUtilities.DEFAULT_ENEMY_DAMAGE  },
            { EnemyType.Aquamentus, GlobalUtilities.DEFAULT_BOSS_DAMAGE  },
            { EnemyType.Wizzrobe, GlobalUtilities.DEFAULT_ENEMY_DAMAGE  },
            { EnemyType.Goriya, GlobalUtilities.DEFAULT_ENEMY_DAMAGE  },
            { EnemyType.BlueGoriya, GlobalUtilities.DEFAULT_ENEMY_DAMAGE },
            { EnemyType.Keese, GlobalUtilities.DEFAULT_ENEMY_DAMAGE  },
            { EnemyType.Blade, GlobalUtilities.DEFAULT_ENEMY_DAMAGE  },
            { EnemyType.Gel, GlobalUtilities.DEFAULT_ENEMY_DAMAGE },
            { EnemyType.BlueGel, GlobalUtilities.DEFAULT_ENEMY_DAMAGE },
            { EnemyType.Rope, GlobalUtilities.DEFAULT_ENEMY_DAMAGE },
            { EnemyType.Wallmaster, GlobalUtilities.DEFAULT_ENEMY_DAMAGE  },
            { EnemyType.MoldormSection, GlobalUtilities.DEFAULT_ENEMY_DAMAGE  },

            { EnemyType.Dodongo, GlobalUtilities.DEFAULT_BOSS_DAMAGE  },
        };

        // {width, height, scaling}
        private static double DODONGO_SCALAR = 3;
        public static readonly Dictionary<EnemySpriteFactory.EnemyFrame, double[]> enemySizes = new Dictionary<EnemySpriteFactory.EnemyFrame, double[]>()
        {
            { EnemySpriteFactory.EnemyFrame.Blade, new double[]{ 45, 45, 1 } },
            { EnemySpriteFactory.EnemyFrame.Gel1, new double[]{ 24, 24, 1 } },
            { EnemySpriteFactory.EnemyFrame.Gel2, new double[]{ 18, 27, 1 } },
            { EnemySpriteFactory.EnemyFrame.BlueGel1, new double[]{ 24, 24, 1 } },
            { EnemySpriteFactory.EnemyFrame.BlueGel2, new double[]{ 18, 27, 1 } },
            { EnemySpriteFactory.EnemyFrame.Rope1, new double[]{ 42, 45, 1 } },
            { EnemySpriteFactory.EnemyFrame.Rope2, new double[]{ 45, 42, 1 } },
            { EnemySpriteFactory.EnemyFrame.Rope3, new double[]{ 42, 45, 1 } },
            { EnemySpriteFactory.EnemyFrame.Rope4, new double[]{ 45, 42, 1 } },
            { EnemySpriteFactory.EnemyFrame.Wallmaster1, new double[]{ 48, 48, 1 } },
            { EnemySpriteFactory.EnemyFrame.Wallmaster2, new double[]{ 42, 45, 1 } },
            { EnemySpriteFactory.EnemyFrame.Keese1, new double[]{ 48 , 24, 1 } },
            { EnemySpriteFactory.EnemyFrame.Keese2, new double[]{ 30 , 24, 1 } },
            { EnemySpriteFactory.EnemyFrame.BlueGoriyaForward, new double[] { 39, 48, 1 } },
            { EnemySpriteFactory.EnemyFrame.BlueGoriyaBackward, new double[] { 39, 48, 1 } },
            { EnemySpriteFactory.EnemyFrame.BlueGoriyaRight1, new double[] { 39, 48, 1 } },
            { EnemySpriteFactory.EnemyFrame.BlueGoriyaRight2, new double[] { 42, 45, 1 } },
            { EnemySpriteFactory.EnemyFrame.BlueGoriyaLeft1, new double[] { 39, 48, 1 } },
            { EnemySpriteFactory.EnemyFrame.BlueGoriyaLeft2, new double[] { 42, 45, 1 } },

            { EnemySpriteFactory.EnemyFrame.DodongoDown1, new double[]{ 16, 16, DODONGO_SCALAR } },
            { EnemySpriteFactory.EnemyFrame.DodongoDown2, new double[]{ 16, 16, DODONGO_SCALAR } },
            { EnemySpriteFactory.EnemyFrame.DodongoDownBomb, new double[]{ 16, 16, DODONGO_SCALAR } },
            { EnemySpriteFactory.EnemyFrame.DodongoUp1, new double[]{ 16, 16, DODONGO_SCALAR } },
            { EnemySpriteFactory.EnemyFrame.DodongoUp2, new double[]{ 16, 16, DODONGO_SCALAR } },
            { EnemySpriteFactory.EnemyFrame.DodongoUpBomb, new double[]{ 16, 16, DODONGO_SCALAR } },
            { EnemySpriteFactory.EnemyFrame.DodongoRight1, new double[]{ 32, 16, DODONGO_SCALAR } },
            { EnemySpriteFactory.EnemyFrame.DodongoRight2, new double[]{ 32, 16, DODONGO_SCALAR } },
            { EnemySpriteFactory.EnemyFrame.DodongoRightBomb, new double[]{ 32, 16, DODONGO_SCALAR } },
            { EnemySpriteFactory.EnemyFrame.DodongoLeft1, new double[]{ 32, 16, DODONGO_SCALAR } },
            { EnemySpriteFactory.EnemyFrame.DodongoLeft2, new double[]{ 32, 16, DODONGO_SCALAR }},
            { EnemySpriteFactory.EnemyFrame.DodongoLeftBomb, new double[]{ 32, 16, DODONGO_SCALAR }},

            { EnemySpriteFactory.EnemyFrame.MoldormSection, new double[]{ 24, 30, 1 } },
        };

        public int EnemyDamage
        {
            get { return enemyDamageDictionary[enemyType]; }
        }
        public static Dictionary<EnemyType, List<EnemySpriteFactory.EnemyFrame>> EnemyDictionary 
        {
            get { return enemyDictionary; }
        }

        public enum Direction { Up, Down, Left, Right };

        private Direction direction = Direction.Up;
        private int directChange = 0;
        public int DirectChange 
        {
            get 
            {
                return directChange;
            }
            set 
            {
                directChange = value;
            }
        }

        public Direction CurrentDirection
        {
            get { return direction; }
            set 
            { 
                direction = value;
                directChange = 0;
            } 
        }

        private EnemyType enemyType;
        public EnemyType CurrentEnemyType
        {
            get { return enemyType; }  
            set { enemyType = value; }
        }

        private EnemySpriteFactory.EnemyFrame currentEnemyFrame;
        public EnemySpriteFactory.EnemyFrame CurrentEnemyFrame
        {
            get { return currentEnemyFrame; }
            set { currentEnemyFrame = value; }
        }

        public Game1 game;
        public GraphicsDeviceManager graphics;

        private EnemySprite currentSprite;
        public EnemySprite CurrentSprite
        {
            get { return currentSprite; }
            set { currentSprite = value; }
        }
        private int FrameTicks = 0;
        public int CurrentFrameTicks
        {
            get { return FrameTicks; }
            set { FrameTicks = value; }
        }
        private int DamageTicks = 0;
        public int CurrentDamageTicks
        {
            get { return DamageTicks; }
            set { DamageTicks = value; }
        }

        private Color color = Color.White;
        public Color EnemyColor
        {
            get { return color; }
            set { color = value; }
        }

        private int posX;
        public int CurrentPosX
        {
            get 
            { 
                return posX; 
            }
            set 
            { 
                posX = value;
                currentHitbox.X = posX;
            }
        }

        private int posY;
        public int CurrentPosY
        {

            get 
            { 
                return posY; 
            }
            set 
            { 
                posY = value;
                currentHitbox.Y = posY;
            }
        }

        private int Frame = 1;
        public int CurrentFrame
        {
            get { return Frame; }
            set { Frame = value; }
        }

        private int health = 10;
        public int Health
        {
            get { return health; }
            set { health = value; }
        }

        private int attackDamage;
        private bool takingDamage = false;
        public bool TakingDamage
        {
            get { return takingDamage; }
            set { takingDamage = value; }
        }

        private bool alive = true;
        public bool Alive
        {
            get { return alive; }
            set 
            {
                alive = value;
                if (!value) 
                {
                    GlobalUtilities.DropItem(enemyType, posX + (currentHitbox.Width / 2), posY + (currentHitbox.Height / 2));
                    enemies.Remove(ID);
                }
            }
        }

        private Rectangle currentHitbox;
        public Rectangle CurrentHitbox
        {
            get {return currentHitbox;}
            set {currentHitbox = value;}
        }

        public CollidableObject.Type CollidableType 
        {
            get { return CollidableObject.Type.Enemy; }
        }

        private int checkMove = 0;
        public int CheckMove 
        {
            get { return checkMove; }
            set { checkMove = value; }
        }

        public void Left()
        {
            if (!EasyDubMode)
            {
                EnemyStateMachine.Left(this);
            }
        }
        public void Left(int numPixels)
        {
            if (!EasyDubMode)
            {
                EnemyStateMachine.MoveLeft(this, numPixels);
            }
        }
        public void Right()
        {
            if (!EasyDubMode)
            {
                EnemyStateMachine.Right(this);

            }
        }
        public void Right(int numPixels)
        {
            if (!EasyDubMode)
            {
                EnemyStateMachine.MoveRight(this, numPixels);

            }
        }
        public void Up()
        {
            if (!EasyDubMode)
            {
                EnemyStateMachine.Up(this);

            }
        }
        public void Up(int numPixels)
        {
            if (!EasyDubMode)
            {
                EnemyStateMachine.MoveUp(this, numPixels);

            }
        }
        public void Down()
        {
            if (!EasyDubMode)
            {
                EnemyStateMachine.Down(this);

            }
        }
        public void Down(int numPixels)
        {
            if (!EasyDubMode)
            {
                EnemyStateMachine.MoveDown(this, numPixels);

            }
        }
        public virtual void TakeDamage(int damage)
        {
            if (!takingDamage)
            {
                EnemyStateMachine.TakeDamage(this, damage);

                if (!alive)
                {
                    game._soundEffectGenerator.Play("LOZ_Enemy_Die");
                    //Death death = new Death(game);
                    //death.CurrentPosX = this.CurrentPosX;
                    //death.CurrentPosY = this.CurrentPosY;

                }
                else
                {
                    if (enemyType != Enemy.EnemyType.Aquamentus)
                    {
                        game._soundEffectGenerator.Play("LOZ_Enemy_Hit");

                    }
                    else
                    {
                        game._soundEffectGenerator.Play("LOZ_Boss_Hit");
                    }
                }
            }
        }

        public virtual void Draw(SpriteBatch spriteBatch, Color col)
        {
            if (GlobalUtilities.enemyFrameAnimationTicks.ContainsKey(enemyType))
            {
                if (checkMove % GlobalUtilities.enemyFrameAnimationTicks[enemyType] == 0)
                {
                    EnemyStateMachine.GenerateEnemy(this);
                }
            }
            else 
            {
                if (checkMove % GlobalUtilities.ENEMY_FRAME_ANIMATION_DEFAULT == 0)
                {
                    EnemyStateMachine.GenerateEnemy(this);
                }
            }
            currentSprite.Draw(spriteBatch, CurrentHitbox, color);

        }
        public void Stun(Item.ItemCategory item) 
        {
            switch (item) 
            {
                case Item.ItemCategory.Clock:
                    maxStunTime = GlobalUtilities.CLOCK_ACTIVE_TIME;
                    break;
                default:
                    maxStunTime = GlobalUtilities.DEFAULT_STUN_TIME;
                    break;
            }
            Stunned = true;
        }

        public int projCount = 0;
        public virtual void Update(GameTime timer)
        {
            FrameTicks++;
            Random rand = new Random();

            if (!Stunned)
            {
                int attackTime = 150;
                projCount++;
                if (projCount >= attackTime)
                {
                    if (enemyType == EnemyType.Goriya || enemyType == EnemyType.BlueGoriya)
                    {
                        EnemyProjectile ep = new EnemyProjectile(this, game);
                        ep.throwBoomerang(timer);
                    }
                    if (enemyType == EnemyType.Aquamentus)
                    {
                        EnemyProjectile ep = new EnemyProjectile(this, game);
                        ep.throwFireBall(timer);
                    }
                    projCount = 0;
                }

                if (CurrentEnemyType != EnemyType.Keese && CurrentEnemyType != EnemyType.Blade && CurrentEnemyType != EnemyType.Death && CurrentEnemyType != EnemyType.Dodongo && CurrentEnemyType != EnemyType.Rope)
                {
                    checkMove++;
                    directChange++;
                    //int newDirection = rand.Next(0, 4);
                    if (directChange % GlobalUtilities.ENEMY_DIRECTION_BUFFER == 0)
                    {
                        CurrentDirection = GlobalUtilities.RandomDirection(CurrentDirection);
                    }
                    if (checkMove % GlobalUtilities.ENEMY_MOVEMENT_BUFFER == 0)
                    {
                        switch (direction)
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
            else 
            {
                if(timer.TotalGameTime.TotalSeconds - stunTime >= maxStunTime) 
                {
                    Stunned = false;
                }
            }
            
            color = Color.White;
            EnemyStateMachine.ChangeFrame(this);

            color = AnimateDamage();

            if (EnemySpriteFactory.dungeonEnemies.Contains(currentEnemyFrame))
            {
                CurrentHitbox = new Rectangle(CurrentPosX, CurrentPosY, (int)((int)(enemySizes[currentEnemyFrame][0] * enemySizes[currentEnemyFrame][2]) * GlobalUtilities.Res_Scalar), (int)((int)(enemySizes[currentEnemyFrame][1] * enemySizes[currentEnemyFrame][2]) * GlobalUtilities.Res_Scalar));
            }
            else
            {
                CurrentHitbox = new Rectangle(CurrentPosX, CurrentPosY, (int)(GlobalUtilities.DEFAULT_ENEMY_WIDTH * GlobalUtilities.Res_Scalar), (int)(GlobalUtilities.DEFAULT_ENEMY_HEIGHT * GlobalUtilities.Res_Scalar));
            }

            if (currentEnemyFrame == EnemySpriteFactory.EnemyFrame.Aquamentus1 || currentEnemyFrame == EnemySpriteFactory.EnemyFrame.Aquamentus2)
            {
                CurrentHitbox = new Microsoft.Xna.Framework.Rectangle(CurrentPosX, CurrentPosY, (int)((int)(Aquamentus.DRAGON_WIDTH * Aquamentus.SCALING_FACTOR) * GlobalUtilities.Res_Scalar), (int)((int)(Aquamentus.DRAGON_HEIGHT * Aquamentus.SCALING_FACTOR) * GlobalUtilities.Res_Scalar));
            }
        }

        public Color AnimateDamage() 
        {
            if (takingDamage)
            {
                DamageTicks++;
                if (DamageTicks % MAX_ANIMATE_TICKS != 0)
                {
                    if(DamageTicks % ALTERNATE_DAMAGE_FRAME_TICKS == 0) 
                    {
                        if(damageColorIndex == damageColors.Count - 1) 
                        {
                            damageColorIndex = -1;
                        }
                        damageColorIndex++;
                    }
                    return damageColors[damageColorIndex];
                }
                else 
                {
                    takingDamage = false;
                    DamageTicks = 0;
                    return Color.White;
                }
            }
            return Color.White;
        }

        public EnemySpriteFactory.EnemyFrame GetEnemyDirectionFrame() 
        {
            EnemySpriteFactory.EnemyFrame direct;
            switch (enemyType) 
            {
                case EnemyType.Darknut:
                    switch (direction) 
                    {
                        case Direction.Left:
                            currentEnemyFrame = EnemySpriteFactory.EnemyFrame.DarknutLeft1;
                            direct = GetProperFrame();
                            break;
                        case Direction.Right:
                            currentEnemyFrame = EnemySpriteFactory.EnemyFrame.DarknutRight1;
                            direct = GetProperFrame();
                            break;
                        case Direction.Up:
                            direct = EnemySpriteFactory.EnemyFrame.DarknutBackward;
                            break;
                        case Direction.Down:
                            direct = EnemySpriteFactory.EnemyFrame.DarknutForward;
                            break;
                        default:
                            direct = GetProperFrame();
                            break;
                    }
                    break;
                case EnemyType.Wizzrobe:
                    switch (direction)
                    {
                        case Direction.Left:
                            currentEnemyFrame = EnemySpriteFactory.EnemyFrame.WizzrobeLeft1;
                            direct = GetProperFrame();
                            break;
                        case Direction.Right:
                            currentEnemyFrame = EnemySpriteFactory.EnemyFrame.WizzrobeRight1;
                            direct = GetProperFrame();
                            break;
                        case Direction.Up:
                            currentEnemyFrame = EnemySpriteFactory.EnemyFrame.WizzrobeUp1;
                            direct = currentEnemyFrame;
                            break;
                        case Direction.Down:
                            currentEnemyFrame = EnemySpriteFactory.EnemyFrame.WizzrobeUp2;
                            direct = currentEnemyFrame;
                            //direct = GetProperFrame();
                            break;
                        default:
                            direct = GetProperFrame();
                            break;
                    }
                    break;
                case EnemyType.Goriya:
                    switch (direction)
                    {
                        case Direction.Left:
                            currentEnemyFrame = EnemySpriteFactory.EnemyFrame.GoriyaLeft1;
                            direct = GetProperFrame();
                            break;
                        case Direction.Right:
                            currentEnemyFrame = EnemySpriteFactory.EnemyFrame.GoriyaRight1;
                            direct = GetProperFrame();
                            break;
                        case Direction.Up:
                            currentEnemyFrame = EnemySpriteFactory.EnemyFrame.GoriyaForward;
                            direct = EnemySpriteFactory.EnemyFrame.GoriyaBackward;
                            break;
                        case Direction.Down:
                            currentEnemyFrame = EnemySpriteFactory.EnemyFrame.GoriyaBackward;
                            direct = EnemySpriteFactory.EnemyFrame.GoriyaForward;
                            break;
                        default:
                            direct = GetProperFrame();
                            break;
                    }
                    break;
                case EnemyType.BlueGoriya:
                    switch (direction)
                    {
                        case Direction.Left:
                            currentEnemyFrame = EnemySpriteFactory.EnemyFrame.BlueGoriyaLeft1;
                            direct = GetProperFrame();
                            break;
                        case Direction.Right:
                            currentEnemyFrame = EnemySpriteFactory.EnemyFrame.BlueGoriyaRight1;
                            direct = GetProperFrame();
                            break;
                        case Direction.Up:
                            currentEnemyFrame = EnemySpriteFactory.EnemyFrame.BlueGoriyaForward;
                            direct = EnemySpriteFactory.EnemyFrame.BlueGoriyaBackward;
                            break;
                        case Direction.Down:
                            currentEnemyFrame = EnemySpriteFactory.EnemyFrame.BlueGoriyaBackward;
                            direct = EnemySpriteFactory.EnemyFrame.BlueGoriyaForward;
                            break;
                        default:
                            direct = GetProperFrame();
                            break;
                    }
                    break;
                case EnemyType.Rope:
                    switch (direction)
                    {
                        case Direction.Left:
                            currentEnemyFrame = EnemySpriteFactory.EnemyFrame.Rope3;
                            direct = GetProperFrame();
                            break;
                        case Direction.Right:
                            currentEnemyFrame = EnemySpriteFactory.EnemyFrame.Rope1;
                            direct = GetProperFrame();
                            break;
                        case Direction.Up:
                            currentEnemyFrame = EnemySpriteFactory.EnemyFrame.Rope3;
                            direct = GetProperFrame();
                            break;
                        case Direction.Down:
                            currentEnemyFrame = EnemySpriteFactory.EnemyFrame.Rope1;
                            direct = GetProperFrame();
                            break;
                        default:
                            direct = GetProperFrame();
                            break;
                    }
                    break;
                case EnemyType.Dodongo:
                    switch (direction)
                    {
                        case Direction.Left:
                            currentEnemyFrame = EnemySpriteFactory.EnemyFrame.DodongoLeft1;
                            direct = GetProperFrame();
                            break;
                        case Direction.Right:
                            currentEnemyFrame = EnemySpriteFactory.EnemyFrame.DodongoRight1;
                            direct = GetProperFrame();
                            break;
                        case Direction.Up:
                            currentEnemyFrame = EnemySpriteFactory.EnemyFrame.DodongoUp1;
                            direct = GetProperFrame();
                            break;
                        case Direction.Down:
                            currentEnemyFrame = EnemySpriteFactory.EnemyFrame.DodongoDown1;
                            direct = GetProperFrame();
                            break;
                        default:
                            direct = GetProperFrame();
                            break;
                    }
                    break;
                case EnemyType.Death:
                    switch (direction)
                    {
                        case Direction.Left:
                            currentEnemyFrame = EnemySpriteFactory.EnemyFrame.Death1;
                            direct = EnemySpriteFactory.EnemyFrame.Death1;
                            break;
                        case Direction.Right:
                            currentEnemyFrame = EnemySpriteFactory.EnemyFrame.Death2;
                            direct = EnemySpriteFactory.EnemyFrame.Death2;
                            break;
                        case Direction.Up:
                            currentEnemyFrame = EnemySpriteFactory.EnemyFrame.Death3;
                            direct = EnemySpriteFactory.EnemyFrame.Death3;
                            break;
                        case Direction.Down:
                            currentEnemyFrame = EnemySpriteFactory.EnemyFrame.Death4;
                            direct = EnemySpriteFactory.EnemyFrame.Death4;
                            break;
                        default:
                            direct = GetProperFrame();
                            break;
                    }
                    break;
                case EnemyType.Keese:
                    direct = EnemySpriteFactory.EnemyFrame.Keese1;
                    break;
                case EnemyType.Blade:
                    direct = EnemySpriteFactory.EnemyFrame.Blade;
                    break;
                case EnemyType.Wallmaster:
                    direct = EnemySpriteFactory.EnemyFrame.Wallmaster1;
                    break;
                case EnemyType.Gel:
                    direct = EnemySpriteFactory.EnemyFrame.Gel1;
                    break;
                case EnemyType.MoldormSection:
                    direct = EnemySpriteFactory.EnemyFrame.MoldormSection;
                    break;
                default:
                    direct = EnemySpriteFactory.EnemyFrame.Stalfos1;
                    break;
            }


            return direct;
        }

        public void Move() 
        {
            if (!EasyDubMode)
            {
                switch (CurrentDirection)
                {
                    case Direction.Up:
                        Up();
                        break;
                    case Direction.Right:
                        Right();
                        break;
                    case Direction.Down:
                        Down();
                        break;
                    default:
                        Left();
                        break;
                }
            }
            
        }

        public EnemySpriteFactory.EnemyFrame GetProperFrame() 
        {
            List<EnemySpriteFactory.EnemyFrame> possibleFrames = enemyDictionary.GetValueOrDefault(enemyType);
            int ind = possibleFrames.IndexOf(currentEnemyFrame);

            //The way I have it set up, each alternate frame is "next" to each other in the 'enemyDictionary' dictionary
            //So, if the frame is located at an even index, adding one will switch it to its alternate
            //If it is at an odd index, subtracting one will switch it to its alternate
            if (Frame == 1)
            {
                if (ind % 2 == 0)
                {
                    currentEnemyFrame += 1;
                }
                else
                {
                    currentEnemyFrame -= 1;
                }
            }
            return currentEnemyFrame;
        }

        public object Clone()
        {
            switch (CurrentEnemyType) 
            {
                case EnemyType.Stalfost:
                    return new Stalfos(game);
                case EnemyType.Aquamentus:
                    return new Aquamentus(game);
                case EnemyType.Keese:
                    return new Keese(game);
                case EnemyType.Goriya:
                    return new Goriya(game);
                case EnemyType.Wizzrobe:
                    return new Wizzrobe(game);
                case EnemyType.Death:
                    return new Death(game);
                default:
                    return new Darknut(game);
            }
        }
    }

}