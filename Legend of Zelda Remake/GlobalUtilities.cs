using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using Sprint0;
using Sprint2.Block_Classes;
using Sprint2.Enemy_Classes;
using Sprint2.Item_Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using static Sprint0.CollisionDetector;

namespace Sprint2
{
    public static class GlobalUtilities 
    {
        // RoomParse Global Utility Variables
        public static readonly int CSV_NUM_COLUMNS = 12;
        public static readonly int CSV_NUM_ROWS = 7;

        // HandleDoors() method
        public static readonly int DEFAULT_LEFT_WALL_X = 30;
        public static readonly int DEFAULT_LEFT_WALL_Y = 216;
        public static readonly int LEFT_WALL_LOCKED_X = 38;

        public static readonly int DEFAULT_TOP_WALL_X = 312;
        public static readonly int DEFAULT_TOP_WALL_Y = 30;
        public static readonly int TOP_WALL_LOCKED_X = 336;
        public static readonly int TOP_WALL_LOCKED_Y = 38;

        public static readonly int DEFAULT_RIGHT_WALL_Y = 216;
        public static readonly int RIGHT_WALL_LOCKED_Y = 215;

        public static readonly int DEFAULT_BOTTOM_WALL_X = 350;
        public static readonly int DEFAULT_BOTTOM_WALL_Y = 433;
        public static readonly int BOTTOM_WALL_LOCKED_X = 338;


        public static readonly int DEFAULT_HEADS_UP_DISPLAY_SIZE = 168;
        public static readonly double MAX_RESOLUTION = 2; //Corresponds to the maximum number resolution_scalar can be set to

        public static readonly double GAME_OVER_DELAY = 1;
        public static readonly int GAME_OVER_TEXT_WIDTH = 50;
        public static readonly int GAME_OVER_TEXT_HEIGHT = 50;

        public static readonly double GAME_WIN_TRANSITION_TIME = 7.0;

        public static readonly int BASE_RESOLUTION_WIDTH = 768;
        public static readonly int BASE_RESOLUTION_HEIGHT = 528;

        public static readonly int PROBABILITY_SPREAD_LENGTH = 100;

        private static double resolution_scalar = 1.0; 

        public static readonly int HEADS_UP_DISPLAY_SIZE = (int)(DEFAULT_HEADS_UP_DISPLAY_SIZE * resolution_scalar);
        private static int[] resolution = { (int)(BASE_RESOLUTION_WIDTH * resolution_scalar), (int)((BASE_RESOLUTION_HEIGHT * resolution_scalar) + HEADS_UP_DISPLAY_SIZE) }; //Standard resolution by default

        public static int[] Resolution
        {
            get { return resolution; }
        }
        public static double Res_Scalar
        {
            get { return resolution_scalar; }
        }

        private static bool DISABLE_VERTICAL = false;
        private static bool DISABLE_HORIZONTAL = false;

        public static bool DisableHorizontal 
        {
            get 
            {
                return DISABLE_HORIZONTAL;
            }
            set 
            {
                DISABLE_HORIZONTAL = value;
            }
        }

        public static bool DisableVertical 
        {
            get 
            {
                return DISABLE_VERTICAL;
            }
            set 
            {
                DISABLE_VERTICAL = value;
            }
        }

        public static readonly double DEATH_TRANSITION_TIME = 2.0;
        public static readonly double LINK_DEATH_ANIMATION_TIME = 0.3;
        public static readonly double GAME_OVER_DISPLAY = 2;

        public static readonly double WIN_TRANSITION_TIME = 1.0;

        public static readonly int LINK_COLLISION_OFFSET = 0; //If 0, Link will phase slightly into objects *when moving*; if <0, link will go into objects; if >0, link will bounce that distance out when attempting to move
        public static readonly int ENEMY_COLLISION_OFFSET = 0; //As above, but for Enemies

        public static readonly int STANDARD_BLOCK_WIDTH = 48;
        public static readonly int STANDARD_BLOCK_HEIGHT = 48;

        public static readonly int STANDARD_DOOR_WIDTH = 20;
        public static readonly int STANDARD_DOOR_HEIGHT = 32;

        public static readonly int DEFAULT_ENEMY_WIDTH = 40;
        public static readonly int DEFAULT_ENEMY_HEIGHT = 40;
        public static readonly int DEFAULT_ENEMY_DAMAGE = 10;
        public static readonly int DEFAULT_BOSS_DAMAGE = 30;

        public static readonly int DEFAULT_ENEMY_POS_X = GraphicsDeviceManager.DefaultBackBufferWidth / 2 + 65;
        public static readonly int DEFAULT_ENEMY_POS_Y = GraphicsDeviceManager.DefaultBackBufferHeight / 2 - 65;

        public static readonly int DEFAULT_LINK_POS_Y = (BASE_RESOLUTION_HEIGHT / 2) + HEADS_UP_DISPLAY_SIZE;
        public static readonly int DEFAULT_LINK_POS_X = BASE_RESOLUTION_WIDTH / 2;
        public const int LINK_HOLDING_MODIFIER = 4;

        public static readonly int DEFAULT_BOSS_SIZE_X = 24;
        public static readonly int DEFAULT_BOSS_SIZE_Y = 32;
        public static readonly int DEFAULT_DEATH_SIZE = 15;

        public static readonly int LINK_KNOCKBACK_MOVEMENT = (int)(15 * resolution_scalar); //Corresponds to the number of pixels Link moves when being knocked back
        public static readonly int LINK_KNOCKBACK_INTERVAL = 1; //Corresponds to the number of ticks between knockback moves (i.e., link will move at LINK_KNOCKBACK_MOVEMENT pixels per LINK_KNOCKBACK_INTERVAL ticks)
        public static readonly double LINK_KNOCKBACK_TIME = 0.2; //Corresponds to the number of seconds Link should be being knocked back after getting hit by an enemy

        public static readonly double LINK_INVINCIBILITY_TIME = 1.5; //Corresponds to the amount of time Link should have invincibility for after getting hit
        public static readonly int LINK_INVINCIBILITY_FRAME = 30; //Corresponds to the number of ticks between color changes when Link is invincible

        public static readonly int ITEM_ANIMATE_TIME = 5; //Corresponds to the number of ticks between item animations (like a blinking Rupee)
        public static readonly int PARTICLE_ANIMATE_TIME = 2;
        public static readonly int HEART_ANIMATE_TIME = 3; 

        public static readonly int BOOMERANG_ANIMATE_TIME = 5;
        public static readonly int FIRE_ANIMATE_TIME = 10;
        public static readonly double ARROW_UPDATE_TIME = 0.4;
        public static readonly double SWORD_ANIMATE_TIME = 0.1;

        public static readonly double BOMB_FRAME_UPDATE = 1.2;
        public static readonly double SWORD_UPDATE_TIME = 1;

        public static readonly int FRAME_SPEED = 15; //Corresponds to the number of ticks link is holding sword/shooting for

        public static readonly int BOUNDARY_SIZE = 10; //When Link crosses this threshold (either when X = BOUNDARY_SIZE || X = Screen_Width - BOUNDARY_SIZE || Y = BOUNDARY_SIZE || X = Screen_Height - BOUNDARY_SIZE), a room transition will be triggered
        public static readonly int BOUNDARY_LEFT_X = (int)(BOUNDARY_SIZE * Res_Scalar);
        public static readonly int BOUNDARY_RIGHT_X = (int)(Resolution[0] - BOUNDARY_LEFT_X);
        public static readonly int BOUNDARY_UP_Y = (int)(BOUNDARY_SIZE * Res_Scalar) + HEADS_UP_DISPLAY_SIZE;
        public static readonly int BOUNDARY_BOTTOM_Y = (int)(Resolution[1] - (BOUNDARY_SIZE * Res_Scalar));

        public static readonly int BLADE_FORWARD_TICK_COUNT = 1;

        public static readonly int LINK_WIDTH = (int)(42 * Res_Scalar);
        public static readonly int LINK_HEIGHT = (int)(42 * Res_Scalar);

        public static readonly int ENEMY_MOVEMENT_BUFFER = 20; //Every ENEMY_MOVEMENT_BUFFER ticks, enemy moves GetEnemySpeed(EnemyType)[0] pixels
        public static readonly int ENEMY_DIRECTION_BUFFER = 180; //Every ENEMY_DIRECTION_BUFFER / 60 second(s), enemy will change directions

        public static readonly int DODONGO_MOVEMENT_BUFFER = 20; //Every ENEMY_MOVEMENT_BUFFER ticks, enemy moves GetEnemySpeed(EnemyType)[0] pixels
        public static readonly int DODONGO_DIRECTION_BUFFER = 240; //Every ENEMY_DIRECTION_BUFFER / 60 second(s), enemy will change directions

        public static readonly int LINK_SPEED = (int)(7 * Res_Scalar);

        public static readonly double ENEMY_DEATH_ANIMATION_SPEED = 0.3;
        public static readonly double PROJECTILE_DEATH_ANIMATION_SPEED = 0.2;

        public static readonly int PROJECTILE_DEATH_ANIMATION_SIZE = 48;

        public static readonly int STALFOS_HEALTH = 11;

        public static readonly double DEFAULT_STUN_TIME = 1.5;
        public static readonly double DODONGO_STUN_TIME = 2.5;
        public static readonly double DODONGO_PAUSE_TIME = 1;

        public static readonly double CLOCK_ACTIVE_TIME = 10;
        public static readonly double STANDARD_STUN_TIME = 3;

        public static readonly int LINK_TIME_STOP_BLINK = 3;
        public static readonly double LINK_TIME_STOP_TIME = CLOCK_ACTIVE_TIME;

        public static bool ForceMuted = false;

        private static readonly List<Item.ItemCategory> possibleProjectiles = new List<Item.ItemCategory>()
        {
            Item.ItemCategory.Boomerang,
            Item.ItemCategory.BlueBoomerang,
            Item.ItemCategory.DownArrow,
            Item.ItemCategory.LeftArrow,
            Item.ItemCategory.RightArrow,
            Item.ItemCategory.UpArrow,
            Item.ItemCategory.RightSwordProj,
            Item.ItemCategory.LeftSwordProj,
            Item.ItemCategory.DownSwordProj,
            Item.ItemCategory.UpSwordProj,
        };
        public static readonly int NUMBER_EXPLOSIONS = 6;
        public static readonly double TIME_DELAY_BETWEEN_EXPLOSIONS = 0.1;

        public static DeathAnimation.DeathType convertItemtoDeathType(Item.ItemCategory item) 
        {
            if (possibleProjectiles.Contains(item)) 
            {
                return DeathAnimation.DeathType.Projectile;
            }
            else 
            {
                return DeathAnimation.DeathType.None;
            }
        }

        public static readonly double ROPE_CHASE_TIME = 1;

        //Enemy speed information : { x, y, z } = { PIXEL_SPEED1, PIXEL_SPEED2, TICK_SPEED }
        public static readonly Dictionary<Enemy.EnemyType, List<int>> enemySpeeds = new()
        {
            { Enemy.EnemyType.Keese, new List<int> { 5, -1, 3 } },
            { Enemy.EnemyType.Blade, new List<int> { 5, 2, 1 } },
            { Enemy.EnemyType.Wallmaster, new List<int> { 10, -1, 1 } },
            { Enemy.EnemyType.Aquamentus, new List<int> { 8, -1, 1 } },
            { Enemy.EnemyType.Goriya, new List<int> { 8, -1, 1 } },
            { Enemy.EnemyType.BlueGoriya, new List<int> { 8, -1, 1 } },
            { Enemy.EnemyType.Wizzrobe, new List<int> { 8, -1, 1 } },
            { Enemy.EnemyType.Gel, new List<int> { 10, -1, 1 } },
            { Enemy.EnemyType.BlueGel, new List<int> { 10, -1, 1 } },
            { Enemy.EnemyType.Darknut, new List<int> { 8, -1, 1 } },
            { Enemy.EnemyType.Rope, new List<int> { 2, 4, 1 } },
            { Enemy.EnemyType.Stalfost, new List<int> { 15, -1, 1 } },
            { Enemy.EnemyType.Dodongo, new List<int> { 15, -1, 1 } },
            { Enemy.EnemyType.Death, new List<int> { 0, 0, 0} },
            { Enemy.EnemyType.MoldormSection, new List<int> { 20, -1, 1 } },
        };

        //Enemy health information
        public static readonly Dictionary<Enemy.EnemyType, int> enemyHealth = new()
        {
            { Enemy.EnemyType.Keese, 10 },
            { Enemy.EnemyType.Blade, 10 },
            { Enemy.EnemyType.Wallmaster, 10 },
            { Enemy.EnemyType.Aquamentus, 30 },
            { Enemy.EnemyType.Goriya, 30 },
            { Enemy.EnemyType.BlueGoriya, 40 },
            { Enemy.EnemyType.Wizzrobe, 10 },
            { Enemy.EnemyType.Gel, 10 },
            { Enemy.EnemyType.BlueGel, 10 },
            { Enemy.EnemyType.Darknut, 10 },
            { Enemy.EnemyType.Rope, 10 },
            { Enemy.EnemyType.Stalfost, 10 },
            { Enemy.EnemyType.Dodongo, 60 },
            { Enemy.EnemyType.Death, 10 },
            { Enemy.EnemyType.MoldormSection, 10 },
        };

        //Projectile damage information
        public static readonly Dictionary<Item.ItemCategory, int> projectileDamage = new()
        {
            { Item.ItemCategory.DownArrow, 10 },
            { Item.ItemCategory.LeftArrow, 10 },
            { Item.ItemCategory.RightArrow, 10 },
            { Item.ItemCategory.UpArrow, 10 },
            { Item.ItemCategory.UpSwordProj, 5 },
            { Item.ItemCategory.DownSwordProj, 5 },
            { Item.ItemCategory.LeftSwordProj, 5 },
            { Item.ItemCategory.RightSwordProj, 5 },
            { Item.ItemCategory.Sword, 15 },
            { Item.ItemCategory.ExplosiveCloud, 15 },
            { Item.ItemCategory.Boomerang, 0 },
            { Item.ItemCategory.BlueBoomerang, 0 }
        };

        //Delay times for attacks so they can't be spammed
        public static readonly double DELAY_TIME_A = 0.5;
        public static readonly double DELAY_TIME_B = 0.5;

        public static double lastATime = 0;
        public static double lastBTime = 0;

        //Enemy frame animation information (the higher the number, the slower the switching between frames occurs)
        public static readonly int ENEMY_FRAME_ANIMATION_DEFAULT = 10;

        public static readonly Dictionary<Enemy.EnemyType, int> enemyFrameAnimationTicks = new()
        {
            { Enemy.EnemyType.Dodongo, 10 },
            { Enemy.EnemyType.Gel, 3 },
            { Enemy.EnemyType.BlueGel, 3 },
            { Enemy.EnemyType.Rope, 8 },
        };

        public static int[] GetEnemySpeed(Enemy.EnemyType type) 
        {
            List<int> speeds = new();
            speeds.Add((int)(enemySpeeds[type][0] * Res_Scalar));
            speeds.Add((int)(enemySpeeds[type][1] * Res_Scalar));
            speeds.Add(enemySpeeds[type][2]);

            return new int[] { speeds[0], speeds[1], speeds[2] };
        }

        //For { x, y }, after y seconds, x will disappear
        public static readonly Dictionary<Item.ItemCategory, double> disappearTime = new Dictionary<Item.ItemCategory, double>()
        {
            { Item.ItemCategory.Bomb, GlobalUtilities.BOMB_FRAME_UPDATE * 3 },
            { Item.ItemCategory.DownArrow, 0.6 },
            { Item.ItemCategory.LeftArrow, 0.6 },
            { Item.ItemCategory.RightArrow, 0.6 },
            { Item.ItemCategory.UpArrow, 0.6 },
            { Item.ItemCategory.DownSwordProj, 0.7 },
            { Item.ItemCategory.LeftSwordProj, 0.7 },
            { Item.ItemCategory.RightSwordProj, 0.7 },
            { Item.ItemCategory.UpSwordProj, 0.7 },
            { Item.ItemCategory.Fire, 0.7 },
            { Item.ItemCategory.Boomerang, 1.0 },
            { Item.ItemCategory.BlueBoomerang, 1.7 },
            { Item.ItemCategory.OrangeParticle, 0.2 },
            { Item.ItemCategory.ExplosiveCloud, ENEMY_DEATH_ANIMATION_SPEED },
            { Item.ItemCategory.DragonFire, 0.7 }
        };

        //For ladder room
        private static readonly List<int[]> disabledVerticalZones = new()
        {
            new int[]{ (int)(96 * Res_Scalar), Resolution[1] - (105 + LINK_HEIGHT), (int)(576 * Res_Scalar), LINK_HEIGHT},
            //new int[]{ (int)(192 * Res_Scalar), Resolution[1] - (105 + LINK_HEIGHT), (int)(336 * Res_Scalar), LINK_HEIGHT},
            //new int[]{ (int)(576 * Res_Scalar), Resolution[1] - (105 + LINK_HEIGHT), (int)(96 * Res_Scalar), LINK_HEIGHT},
            new int[]{ (int)(336 * Res_Scalar), Resolution[1] - (264 + LINK_HEIGHT), (int)(192 * Res_Scalar), LINK_HEIGHT},
            new int[]{ (int)(576 * Res_Scalar), Resolution[1] - (264 + LINK_HEIGHT), (int)(96 * Res_Scalar), LINK_HEIGHT},
        };

        private static readonly List<int[]> disabledHorizontalZones = new()
        {
            new int[]{ (int)(96 * Res_Scalar), Resolution[1] - (int)(264 * Res_Scalar), (int)(576 * Res_Scalar), LINK_HEIGHT},
            new int[]{ (int)(144 * Res_Scalar), 0, (int)(48 * Res_Scalar), Resolution[1] - ((int)(105 * Res_Scalar) + LINK_HEIGHT)},
            new int[]{ (int)(528 * Res_Scalar), Resolution[1] - (int)(264 * Res_Scalar), (int)(48 * Res_Scalar), (int)(159 * Res_Scalar) - LINK_HEIGHT},
        };

        private static List<CollidableObject> allEntities = new();
        public static List<CollidableObject> AllEntities
        {
            get 
            {
                allEntities.Clear();
                foreach (KeyValuePair<int, Projectile> pair in Projectile.ProjectileDictionary)
                {
                    allEntities.Add(pair.Value);
                }

                foreach (KeyValuePair<int, Item> pair in Item.ItemDictionary)
                {
                    allEntities.Add(pair.Value);
                }

                foreach (KeyValuePair<int, BlockStateMachine> pair in BlockStateMachine.BlockDictionary)
                {
                    allEntities.Add(pair.Value);
                }

                foreach (KeyValuePair<int, Enemy> pair in Enemy.EnemiesDictionary)
                {
                    allEntities.Add(pair.Value);
                }

                return allEntities;
            }
        }

        public static CollisionType GetCollisionOpposite(CollisionType type)
        {
            switch (type)
            {
                case CollisionType.Left:
                    return CollisionType.Right;
                case CollisionType.Right:
                    return CollisionType.Left;
                case CollisionType.Top:
                    return CollisionType.Bottom;
                default:
                    return CollisionType.Top;
            }
        }

        public static Enemy.Direction GetDirectionOpposite(Enemy.Direction type)
        {
            switch (type)
            {
                case Enemy.Direction.Left:
                    return Enemy.Direction.Right;
                case Enemy.Direction.Right:
                    return Enemy.Direction.Left;
                case Enemy.Direction.Up:
                    return Enemy.Direction.Down;
                default:
                    return Enemy.Direction.Up;
            }
        }

        public static LinkStateMachine.Direction GetDirectionOpposite(LinkStateMachine.Direction type)
        {
            switch (type)
            {
                case LinkStateMachine.Direction.Left:
                    return LinkStateMachine.Direction.Right;
                case LinkStateMachine.Direction.Right:
                    return LinkStateMachine.Direction.Left;
                case LinkStateMachine.Direction.Up:
                    return LinkStateMachine.Direction.Down;
                default:
                    return LinkStateMachine.Direction.Up;
            }
        }

        public static CollidableObject GetCollidable(CollidableObject.Type type, int id)
        {
            switch (type)
            {
                case CollidableObject.Type.Item:
                    return Item.ItemDictionary[id];
                case CollidableObject.Type.Projectile:
                    return Projectile.ProjectileDictionary[id];
                case CollidableObject.Type.StandardBlock:
                    return BlockStateMachine.BlockDictionary[id];
                case CollidableObject.Type.PushableBlock:
                    return BlockStateMachine.BlockDictionary[id];
                case CollidableObject.Type.Enemy:
                    return Enemy.EnemiesDictionary[id];
                case CollidableObject.Type.Link:
                    return Link.links[id];
                default:
                    return BlockStateMachine.BlockDictionary[id];
            }
        }

        public static Enemy.Direction RandomDirection(Enemy.Direction exclude) 
        {
            Random rand = new();
            int direction = rand.Next(0, 4);
            Enemy.Direction direct = (Enemy.Direction)direction;

            while(direct == exclude) 
            {
                direction = rand.Next(0, 4);
                direct = (Enemy.Direction)direction;
            }

            return direct;
        }

        public static void IncreaseResolution() 
        {
            resolution[0] = (int)(resolution[0] / resolution_scalar);
            resolution[1] = (int)(resolution[1] / resolution_scalar);
            if (resolution_scalar != MAX_RESOLUTION)
            {
                resolution_scalar += 0.01;
            }
            resolution[0] = (int)(resolution[0] * resolution_scalar);
            resolution[1] = (int)(resolution[1] * resolution_scalar);
        }

        public static void DecreaseResolution()
        {
            resolution[0] = (int)(resolution[0] / resolution_scalar);
            resolution[1] = (int)(resolution[1] / resolution_scalar);
            if (resolution_scalar != 1)
            {
                resolution_scalar -= 0.01;
            }
            resolution[0] = (int)(resolution[0] * resolution_scalar);
            resolution[1] = (int)(resolution[1] * resolution_scalar);
        }

        public static bool IsVerticalLock(int linkX, int linkY) 
        {
            foreach (int[] entry in disabledVerticalZones) 
            {
                Rectangle Intersect = Rectangle.Intersect(new Rectangle(entry[0], entry[1], entry[2], entry[3]), new Rectangle(linkX, linkY, LINK_WIDTH, LINK_HEIGHT));
                if (!Intersect.IsEmpty) 
                {
                    return true;
                }
            }
            return false;
        }

        public static bool IsHorizontalLock(int linkX, int linkY)
        {
            foreach (int[] entry in disabledHorizontalZones)
            {
                Rectangle Intersect = Rectangle.Intersect(new Rectangle(entry[0], entry[1], entry[2], entry[3]), new Rectangle(linkX, linkY, LINK_WIDTH, LINK_HEIGHT));
                if (!Intersect.IsEmpty)
                {
                    return true;
                }
            }
            return false;
        }

        public static bool EnemiesLeft() 
        {
            foreach(KeyValuePair<int, Enemy> pair in Enemy.EnemiesDictionary) 
            {
                if (pair.Value.IsActive) 
                {
                    return true;
                }
            }
            return false;
        }

        public static void RidClosedDoors(List<int> idList) 
        {
            List<BlockSpriteFactory.BlockKind> closedDoors = new()
            {
                BlockSpriteFactory.BlockKind.ClosedDoorDown,
                BlockSpriteFactory.BlockKind.ClosedDoorUp,
                BlockSpriteFactory.BlockKind.ClosedDoorLeft,
                BlockSpriteFactory.BlockKind.ClosedDoorRight,
            };

            List<int> blocksToRemove = new();
            foreach(KeyValuePair<int, BlockStateMachine> pair in BlockStateMachine.BlockDictionary) 
            {
                if (pair.Value.IsActive) 
                {
                    if (closedDoors.Contains(pair.Value.Kind) && idList.Contains(pair.Key)) 
                    {
                        blocksToRemove.Add(pair.Key);
                    }
                }
            }
            foreach(int blockId in blocksToRemove) 
            {
                BlockStateMachine.RemoveBlock(blockId);
            }
        }

        public static void DeactivateClosedDoors(List<int> deactiveBlocks)
        {
            //System.Diagnostics.Debug.WriteLine("Deactivating Closed Doors");
            List<BlockSpriteFactory.BlockKind> closedDoors = new()
            {
                BlockSpriteFactory.BlockKind.ClosedDoorDown,
                BlockSpriteFactory.BlockKind.ClosedDoorUp,
                BlockSpriteFactory.BlockKind.ClosedDoorLeft,
                BlockSpriteFactory.BlockKind.ClosedDoorRight,
                BlockSpriteFactory.BlockKind.DarkClosedDoorDown,
                BlockSpriteFactory.BlockKind.DarkClosedDoorUp,
                BlockSpriteFactory.BlockKind.DarkClosedDoorLeft,
                BlockSpriteFactory.BlockKind.DarkClosedDoorRight,
            };

            foreach (KeyValuePair<int, BlockStateMachine> pair in BlockStateMachine.BlockDictionary)
            {
                if (pair.Value.IsActive)
                {
                    if (closedDoors.Contains(pair.Value.Kind) && !deactiveBlocks.Contains(pair.Key))
                    {
                        //System.Diagnostics.Debug.WriteLine("Deactivating!");
                        deactiveBlocks.Add(pair.Key);
                    }
                }
            }
        }

        public static void ActivateClosedDoors(List<int> deactivatedBlocks)
        {
            List<BlockSpriteFactory.BlockKind> closedDoors = new()
            {
                BlockSpriteFactory.BlockKind.ClosedDoorDown,
                BlockSpriteFactory.BlockKind.ClosedDoorUp,
                BlockSpriteFactory.BlockKind.ClosedDoorLeft,
                BlockSpriteFactory.BlockKind.ClosedDoorRight,
            };

            foreach (KeyValuePair<int, BlockStateMachine> pair in BlockStateMachine.BlockDictionary)
            {
                if (closedDoors.Contains(pair.Value.Kind))
                {
                    if (!pair.Value.IsActive && deactivatedBlocks.Contains(pair.Key))
                    {
                        //System.Diagnostics.Debug.WriteLine("Activating!");
                        pair.Value.IsActive = true;
                    }
                }
            }
        }

        public static bool LinkInDoorway(Link link) 
        {
            return (link.CurrentXPos <= BOUNDARY_LEFT_X + 75 || link.CurrentXPos >= BOUNDARY_RIGHT_X - (75 + link.CurrentHitbox.Width) || link.CurrentYPos <= BOUNDARY_UP_Y + 75 || link.CurrentYPos >= BOUNDARY_BOTTOM_Y - (75 + link.CurrentHitbox.Height));
        }

        public static Item.ItemCategory ConvertHUDToItem(OverheadDisplay.HUDItem hItem) 
        {
            switch (hItem) 
            {
                case OverheadDisplay.HUDItem.WoodenSword:
                    return Item.ItemCategory.Sword;
                case OverheadDisplay.HUDItem.Bow:
                    return Item.ItemCategory.Bow;
                case OverheadDisplay.HUDItem.Bomb:
                    return Item.ItemCategory.Bomb;
                case OverheadDisplay.HUDItem.WoodenBoomerang:
                    return Item.ItemCategory.Boomerang;
                case OverheadDisplay.HUDItem.Map:
                    return Item.ItemCategory.GoldenTicket;
                case OverheadDisplay.HUDItem.Compass:
                    return Item.ItemCategory.Compass;
                default:
                    return Item.ItemCategory.Rupee;
            }
        }

        private static readonly List<BlockStateMachine.BlockType> doors = new()
        {
            BlockStateMachine.BlockType.HorizontalClosedDoor,
            BlockStateMachine.BlockType.VerticalClosedDoor,
            BlockStateMachine.BlockType.HorizontalLockedDoor,
            BlockStateMachine.BlockType.VerticalLockedDoor,
            BlockStateMachine.BlockType.ExplodableDoor
        };

        public static void ToggleOppositeDoor(LinkStateMachine.Direction oppositeDirection, int roomX, int roomY, Dungeon dungeon) 
        {
            LinkStateMachine.Direction direction = GetDirectionOpposite(oppositeDirection);

            switch (oppositeDirection) 
            {
                case LinkStateMachine.Direction.Up:
                    roomY--;
                    break;
                case LinkStateMachine.Direction.Down:
                    roomY++;
                    break;
                case LinkStateMachine.Direction.Left:
                    roomX--;
                    break;
                case LinkStateMachine.Direction.Right:
                    roomX++;
                    break;
            }
            foreach (Tuple<CollidableObject.Type, int> entity in dungeon.RoomArray[roomX, roomY].roomContent) 
            {
                if(entity.Item1 == CollidableObject.Type.StandardBlock) 
                {
                    if (BlockStateMachine.BlockDictionary.ContainsKey(entity.Item2))
                    {
                        if (doors.IndexOf(((BlockStateMachine)GetCollidable(entity.Item1, entity.Item2)).Type) != -1 && Dungeon.CoordinatesToDirection(GetCollidable(entity.Item1, entity.Item2).CurrentHitbox.X, GetCollidable(entity.Item1, entity.Item2).CurrentHitbox.Y, GetCollidable(entity.Item1, entity.Item2).CurrentHitbox.Height, GetCollidable(entity.Item1, entity.Item2).CurrentHitbox.Width) == direction)
                        {
                            ((BlockStateMachine)GetCollidable(entity.Item1, entity.Item2)).Exists = false;
                            break;
                        }
                    }
                }
            }
        }

        public static void DoGameOverTransition(GameTime gT, Link link, Dungeon dungeon) 
        {
            //Despawn all enemies
            foreach(CollidableObject entity in AllEntities) 
            {
                if(entity.IsActive && entity.CollidableType == CollidableObject.Type.Enemy) 
                {
                    entity.IsActive = false;
                }
            }

            //Update link
            link.Update(gT);
        }

        public static void DoGameWinTransition(GameTime gT, Link link, Dungeon dungeon)
        {
            //Despawn all enemies
            foreach (CollidableObject entity in AllEntities)
            {
                if (entity.IsActive && entity.CollidableType == CollidableObject.Type.Enemy)
                {
                    entity.IsActive = false;
                }
            }

            //Update link
            link.Update(gT);
        }

        private static Dictionary<Item.ItemCategory, double> defaultProbabilityIndex = new Dictionary<Item.ItemCategory, double>()
        {
            { Item.ItemCategory.Rupee, .05 },
            { Item.ItemCategory.Bomb, .01 },
            { Item.ItemCategory.SmallHeart, .1 },
            { Item.ItemCategory.Clock, .05 },
        };

        private static Dictionary<Enemy.EnemyType, Dictionary<Item.ItemCategory, double>> probabilityIndex = new()
        {
            { Enemy.EnemyType.Stalfost, new(){ { Item.ItemCategory.Rupee, .04 }, { Item.ItemCategory.Key, .1 }, { Item.ItemCategory.Bomb, .05 }, { Item.ItemCategory.SmallHeart, .01 } } },
            { Enemy.EnemyType.Goriya, new(){ { Item.ItemCategory.Rupee, .3 }, { Item.ItemCategory.Boomerang, .01 } } },
            { Enemy.EnemyType.BlueGoriya, new(){ { Item.ItemCategory.Rupee, .4 }, { Item.ItemCategory.Boomerang, .05 } } },
            { Enemy.EnemyType.Gel, new(){ { Item.ItemCategory.Rupee, .02 }, { Item.ItemCategory.SmallHeart, .1 } } },
            { Enemy.EnemyType.BlueGel, new(){ { Item.ItemCategory.Rupee, .02 }, { Item.ItemCategory.SmallHeart, .1 } } },
            { Enemy.EnemyType.Aquamentus, new(){ { Item.ItemCategory.BigHeart, 1 } } },
            { Enemy.EnemyType.Dodongo, new(){ { Item.ItemCategory.Bomb, 1 } } },
        };

        private static List<Tuple<Item.ItemCategory, int, int>> ConvertToSpread(Dictionary<Item.ItemCategory, double> probIndex) 
        {
            int lastNum = 0;
            List<Tuple<Item.ItemCategory, int, int>> totals = new();
            foreach (KeyValuePair<Item.ItemCategory, double> probability in probIndex) 
            {
                totals.Add(Tuple.Create<Item.ItemCategory, int, int>(probability.Key, lastNum, (int)(PROBABILITY_SPREAD_LENGTH * probability.Value) + lastNum));
                lastNum = (int)(PROBABILITY_SPREAD_LENGTH * probability.Value) + lastNum;
            }
            return totals;
        }

        public static void DropItem(Enemy.EnemyType enemy, int x, int y) 
        {
            int rand = RandomNumberGenerator.GetInt32(0, 101);
            if (probabilityIndex.ContainsKey(enemy))
            {
                foreach (Tuple<Item.ItemCategory, int, int> entry in ConvertToSpread(probabilityIndex[enemy]))
                {
                    if (rand >= entry.Item2 && rand < entry.Item3)
                    {
                        if (enemy == Enemy.EnemyType.Dodongo)
                        {
                            //magic numbers in here are just arbitrary--this just spawns in four bombs in a cluster instead of just one item when Dodongo dies
                            ItemSpriteFactory.Instance.CreateItem(entry.Item1, x, y);
                            ItemSpriteFactory.Instance.CreateItem(entry.Item1, x + 10, y + 14);
                            ItemSpriteFactory.Instance.CreateItem(entry.Item1, x - 8, y - 12);
                            ItemSpriteFactory.Instance.CreateItem(entry.Item1, x + 12, y - 4);
                        }
                        else 
                        {
                            ItemSpriteFactory.Instance.CreateItem(entry.Item1, x, y);
                        }
                        return;
                    }
                }
            }
            else 
            {
                foreach (Tuple<Item.ItemCategory, int, int> entry in ConvertToSpread(defaultProbabilityIndex))
                {
                    if (rand >= entry.Item2 && rand < entry.Item3)
                    {
                        ItemSpriteFactory.Instance.CreateItem(entry.Item1, x, y);
                        return;
                    }
                }
            }
        }

        public static void ResumeTime(Link link, Game1 game, GameTime timer) 
        {
            link.ResumeTime();
            link.LinkColor = Color.White;
            foreach(KeyValuePair<int, Enemy> entry in Enemy.EnemiesDictionary) 
            {
                if (entry.Value.Stunned) 
                {
                    entry.Value.Stunned = false;
                }
            }
            (game.Content.Load<SoundEffect>("LOZ_TimeResume")).Play();
            if (!ForceMuted)
            {
                MediaPlayer.IsMuted = false;
            }
            TimeStopped = false;
        }

        public static bool TimeStopped = false;
        public static void StopTime() 
        {
            MediaPlayer.IsMuted = true;
            TimeStopped = true;
        }

        public enum TopDoorFrameStyle { NormalLeft1, NormalTop1, NormalRight1, NormalBottom1, NormalLeft2, NormalTop2, NormalRight2, NormalBottom2 };
        public static Dictionary<string, List<TopDoorFrameStyle>> topOfDoorFrames1 = new Dictionary<string, List<TopDoorFrameStyle>>()
        {
            { "00", new List<TopDoorFrameStyle>(){  } },
            { "11", new List<TopDoorFrameStyle>(){  } },
            { "10", new List<TopDoorFrameStyle>(){ TopDoorFrameStyle.NormalRight1 } },
            { "20", new List<TopDoorFrameStyle>(){ TopDoorFrameStyle.NormalLeft1, TopDoorFrameStyle.NormalBottom1 } },
            { "21", new List<TopDoorFrameStyle>(){ TopDoorFrameStyle.NormalTop1, TopDoorFrameStyle.NormalBottom1 } },
            { "41", new List<TopDoorFrameStyle>(){ TopDoorFrameStyle.NormalRight1, TopDoorFrameStyle.NormalBottom1 } },
            { "51", new List<TopDoorFrameStyle>(){ TopDoorFrameStyle.NormalLeft1, } },
            { "02", new List<TopDoorFrameStyle>(){ TopDoorFrameStyle.NormalRight1 } },
            { "12", new List<TopDoorFrameStyle>(){ TopDoorFrameStyle.NormalLeft1, TopDoorFrameStyle.NormalRight1, TopDoorFrameStyle.NormalBottom1 } },
            { "22", new List<TopDoorFrameStyle>(){ TopDoorFrameStyle.NormalLeft1, TopDoorFrameStyle.NormalTop1, TopDoorFrameStyle.NormalRight1 } },
            { "32", new List<TopDoorFrameStyle>(){ TopDoorFrameStyle.NormalLeft1, TopDoorFrameStyle.NormalRight1} },
            { "42", new List<TopDoorFrameStyle>(){ TopDoorFrameStyle.NormalLeft1, TopDoorFrameStyle.NormalTop1 } },
            { "13", new List<TopDoorFrameStyle>(){ TopDoorFrameStyle.NormalTop1, TopDoorFrameStyle.NormalRight1 } },
            { "23", new List<TopDoorFrameStyle>(){ TopDoorFrameStyle.NormalLeft1, TopDoorFrameStyle.NormalRight1, TopDoorFrameStyle.NormalBottom1 } },
            { "33", new List<TopDoorFrameStyle>(){ TopDoorFrameStyle.NormalLeft1, } },
            { "24", new List<TopDoorFrameStyle>(){ TopDoorFrameStyle.NormalTop1, TopDoorFrameStyle.NormalBottom1} },
            { "15", new List<TopDoorFrameStyle>(){ TopDoorFrameStyle.NormalRight1, } },
            { "25", new List<TopDoorFrameStyle>(){ TopDoorFrameStyle.NormalLeft1, TopDoorFrameStyle.NormalTop1, TopDoorFrameStyle.NormalRight1, TopDoorFrameStyle.NormalBottom1 } },
            { "35", new List<TopDoorFrameStyle>(){ TopDoorFrameStyle.NormalLeft1, } },
        };

        public static Dictionary<string, List<TopDoorFrameStyle>> topOfDoorFrames2 = new Dictionary<string, List<TopDoorFrameStyle>>()
        {
            { "10", new List<TopDoorFrameStyle>(){ TopDoorFrameStyle.NormalRight2 } },
            { "20", new List<TopDoorFrameStyle>(){ TopDoorFrameStyle.NormalLeft2, TopDoorFrameStyle.NormalBottom2 } },
            { "21", new List<TopDoorFrameStyle>(){ TopDoorFrameStyle.NormalTop2, TopDoorFrameStyle.NormalBottom2 } },
            { "31", new List<TopDoorFrameStyle>(){ TopDoorFrameStyle.NormalBottom2 } },
            { "22", new List<TopDoorFrameStyle>(){ TopDoorFrameStyle.NormalTop2,TopDoorFrameStyle.NormalRight2,TopDoorFrameStyle.NormalBottom2 } },
            { "32", new List<TopDoorFrameStyle>(){ TopDoorFrameStyle.NormalLeft2,TopDoorFrameStyle.NormalTop2 } },
            { "23", new List<TopDoorFrameStyle>(){ TopDoorFrameStyle.NormalTop2, TopDoorFrameStyle.NormalRight2, TopDoorFrameStyle.NormalBottom2 } },
            { "33", new List<TopDoorFrameStyle>(){ TopDoorFrameStyle.NormalLeft2 } },
            { "24", new List<TopDoorFrameStyle>(){ TopDoorFrameStyle.NormalTop2, TopDoorFrameStyle.NormalRight2, TopDoorFrameStyle.NormalBottom2  } },
            { "34", new List<TopDoorFrameStyle>(){ TopDoorFrameStyle.NormalLeft2 } },
            { "25", new List<TopDoorFrameStyle>(){ TopDoorFrameStyle.NormalTop2, TopDoorFrameStyle.NormalRight2, TopDoorFrameStyle.NormalBottom2 } },
            { "35", new List<TopDoorFrameStyle>(){ TopDoorFrameStyle.NormalLeft2 } },
            { "06", new List<TopDoorFrameStyle>(){ TopDoorFrameStyle.NormalRight2 } },
            { "16", new List<TopDoorFrameStyle>(){ TopDoorFrameStyle.NormalLeft2, TopDoorFrameStyle.NormalBottom2, TopDoorFrameStyle.NormalRight2 } },
            { "26", new List<TopDoorFrameStyle>(){ TopDoorFrameStyle.NormalLeft2, TopDoorFrameStyle.NormalTop2, TopDoorFrameStyle.NormalRight2, TopDoorFrameStyle.NormalBottom2 } },
            { "36", new List<TopDoorFrameStyle>(){ TopDoorFrameStyle.NormalLeft2 } },
            { "17", new List<TopDoorFrameStyle>(){ TopDoorFrameStyle.NormalTop2, TopDoorFrameStyle.NormalRight2, TopDoorFrameStyle.NormalBottom2 } },
            { "27", new List<TopDoorFrameStyle>(){ TopDoorFrameStyle.NormalLeft2, TopDoorFrameStyle.NormalTop2 } },
        };
        public static Dictionary<TopDoorFrameStyle, Rectangle> doorFrameStyleToFrame = new Dictionary<TopDoorFrameStyle, Rectangle>()
        {
            { TopDoorFrameStyle.NormalLeft1, new Rectangle(515, 966, 17, 16) },
            { TopDoorFrameStyle.NormalTop1, new Rectangle(635, 886, 16, 17) },
            { TopDoorFrameStyle.NormalRight1, new Rectangle(754, 966, 17, 16) },
            { TopDoorFrameStyle.NormalBottom1, new Rectangle(635, 1045, 16, 17) },
            { TopDoorFrameStyle.NormalLeft2, new Rectangle(515, 1143, 17, 16) },
            { TopDoorFrameStyle.NormalTop2, new Rectangle(635, 1063, 16, 17) },
            { TopDoorFrameStyle.NormalRight2, new Rectangle(754, 1143, 17, 16) },
            { TopDoorFrameStyle.NormalBottom2, new Rectangle(635, 1222, 16, 17) },
        };
        public static Dictionary<TopDoorFrameStyle, Rectangle> doorFrameStyleToPosition = new Dictionary<TopDoorFrameStyle, Rectangle>()
        {
            { TopDoorFrameStyle.NormalLeft1, new Rectangle(0, 408, 51, 48) },
            { TopDoorFrameStyle.NormalTop1, new Rectangle(360, 168, 48, 51) },
            { TopDoorFrameStyle.NormalRight1, new Rectangle(717, 408, 51, 48) },
            { TopDoorFrameStyle.NormalBottom1, new Rectangle(360, 645, 48, 51) },
            { TopDoorFrameStyle.NormalLeft2, new Rectangle(0, 408, 51, 48) },
            { TopDoorFrameStyle.NormalTop2, new Rectangle(360, 168, 48, 51) },
            { TopDoorFrameStyle.NormalRight2, new Rectangle(717, 408, 51, 48) },
            { TopDoorFrameStyle.NormalBottom2, new Rectangle(360, 645, 48, 51) },
        };

        public static void DrawTopsOfDoors(int dungeonNumber, String roomNumber, ContentManager content, SpriteBatch sB) 
        {
            Texture2D spriteSheet;
            if(dungeonNumber == 1) 
            {
                spriteSheet = content.Load<Texture2D>("LevelSprites");
                foreach(TopDoorFrameStyle door in topOfDoorFrames1[roomNumber]) 
                {
                    sB.Draw(spriteSheet, doorFrameStyleToPosition[door], doorFrameStyleToFrame[door], Color.White);
                }
            }
            else if(dungeonNumber == 2) 
            {
                spriteSheet = content.Load<Texture2D>("dungeon2spritesheet");
                foreach (TopDoorFrameStyle door in topOfDoorFrames2[roomNumber])
                {
                    sB.Draw(spriteSheet, doorFrameStyleToPosition[door], doorFrameStyleToFrame[door], Color.White);
                }
            }
        }

    }
}
