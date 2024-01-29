using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Sprint0;
using Sprint2.Block_Classes.BlockStates;
using Sprint2.Interfaces;
using Sprint2.Item_Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sprint2.Block_Classes
{
    public class BlockStateMachine : CollidableObject
    {
        private const int MOVEMENT_SPEED = 10; //Number of ticks it takes to move a pushable block
        private int movementTicks = 0;
        private LinkStateMachine.Direction linkDirection;

        private static SortedDictionary<int, BlockStateMachine> existingBlocks = new SortedDictionary<int, BlockStateMachine>();
        private int[] teleportLocation = new int[2];
        public int[] TeleportLocation { get{ return teleportLocation;} set{teleportLocation = value;}}
        private static List<BlockStateMachine> blockList = new List<BlockStateMachine>();
        public static SortedDictionary<int, BlockStateMachine> BlockDictionary
        {
            get { return existingBlocks; }
        }
        public static void RemoveBlock(int key)
        {
            existingBlocks.Remove(key);
        }

        public bool IsGap 
        {
            get { return blockState.IsGap; }
            set { blockState.IsGap = value; }
        }
        public bool CanTeleport
        {
            get { return blockState.CanTeleport; }
            set { blockState.CanTeleport = value; }
        }

        private bool wall = false;
        public bool IsWall 
        {
            get { return wall; }
            set { wall = value; }
        }

        public static List<BlockStateMachine> BlockList 
        {
            get 
            {
                blockList.Clear();
                foreach(KeyValuePair<int, BlockStateMachine> pair in existingBlocks) 
                {
                    blockList.Add(pair.Value);
                }
                return blockList;
            }
        }

        private bool active = true;
        public bool IsActive 
        {
            get { return active; }
            set { active = value; }
        }

        private const double DOOR_SCALING_FACTOR = 2.5;

        public static int numberBlocks = 0;

        private int blockID;
        
        public int ID
        {
            get { return blockID; }
        }

        private IBlockState blockState;
        public IBlockState BlockState 
        {
            get { return blockState; }
            set { blockState = value; }
        }

        private BlockType blockType;
        public BlockType Type
        {
            get { return blockType; }
        }
        public BlockSpriteFactory.BlockKind Kind
        {
            get { return blockState.FrameType; }
        }
        public bool Exists 
        {
            get { return blockState.Exists; }
            set 
            { 
                blockState.Exists = value;
                if (!blockState.Exists)
                {
                    existingBlocks.Remove(ID);
                }
            }
        }
        private bool moving = false;
        public bool Moving 
        {
            get { return moving; }
            set { moving = value; }
        }
        public enum BlockType 
        {
            StandardBlock,
            PushableBlock,
            InvisibleBlock,
            TeleportBlock,
            HorizontalLockedDoor,
            VerticalLockedDoor,
            HorizontalClosedDoor,
            VerticalClosedDoor,
            ExplodableDoor,
            OldMan
        }

        private Dictionary<BlockStateMachine.BlockType, IBlockState> possibleStates = new Dictionary<BlockStateMachine.BlockType, IBlockState>()
        {
            { BlockStateMachine.BlockType.StandardBlock, new StaticBlock() },
            { BlockStateMachine.BlockType.PushableBlock, new PushableBlock() },
            { BlockStateMachine.BlockType.InvisibleBlock, new InvisibleBlock() },
            { BlockStateMachine.BlockType.TeleportBlock, new InvisibleBlock() },
            { BlockStateMachine.BlockType.HorizontalLockedDoor, new LockedDoorBlock() },
            { BlockStateMachine.BlockType.VerticalLockedDoor, new LockedDoorBlock() },
            { BlockStateMachine.BlockType.HorizontalClosedDoor, new StaticBlock() },
            { BlockStateMachine.BlockType.VerticalClosedDoor, new StaticBlock() },
            { BlockStateMachine.BlockType.ExplodableDoor, new ExplodeDoorBlock() },
            { BlockStateMachine.BlockType.OldMan, new StaticBlock() }
        };

        //Constructors
        public BlockStateMachine(Texture2D texture, BlockStateMachine.BlockType type, BlockSpriteFactory.BlockKind kind, int startX, int startY)
        {
            blockType = type;

            blockState = possibleStates[type];
            blockState.UpdateTexture(texture);
            blockState.UpdateKind(kind);
            X = startX;
            Y = startY;

            if (type == BlockStateMachine.BlockType.HorizontalLockedDoor || type == BlockStateMachine.BlockType.HorizontalClosedDoor)
            {
                Width = (int)(GlobalUtilities.STANDARD_DOOR_WIDTH * DOOR_SCALING_FACTOR);
                Height = (int)(GlobalUtilities.STANDARD_DOOR_HEIGHT * DOOR_SCALING_FACTOR);
            }
            else if (type == BlockStateMachine.BlockType.VerticalLockedDoor || type == BlockStateMachine.BlockType.VerticalClosedDoor)
            {
                Height = (int)(GlobalUtilities.STANDARD_DOOR_WIDTH * DOOR_SCALING_FACTOR);
                Width = (int)(GlobalUtilities.STANDARD_DOOR_HEIGHT * DOOR_SCALING_FACTOR);
            }

            numberBlocks++;
            blockID = numberBlocks;

            existingBlocks.Add(ID, this);
        }
        public BlockStateMachine(Texture2D texture, BlockStateMachine.BlockType type, BlockSpriteFactory.BlockKind kind, int startX, int startY, int startW, int startH) : this(texture, type, kind, startX, startY)
        {
            Height = startH;
            Width = startW;
        }

        //Block positional properties
        public int X 
        {
            get { return blockState.X; }
            set { blockState.X = (int)(value * GlobalUtilities.Res_Scalar); }
        }
        public int Y
        {
            get { return blockState.Y; }
            set { blockState.Y = (int)(value * GlobalUtilities.Res_Scalar) + GlobalUtilities.HEADS_UP_DISPLAY_SIZE; }
        }
        public int Width
        {
            get { return blockState.Width; }
            set { blockState.Width = (int)(value * GlobalUtilities.Res_Scalar); }
        }
        public int Height
        {
            get { return blockState.Height; }
            set { blockState.Height = (int)(value * GlobalUtilities.Res_Scalar); }
        }

        private int moved = 0;
        //Calling blockState methods
        public void Update(GameTime gT) 
        {
            if (moving && movementTicks < MOVEMENT_SPEED) 
            {
                Move((blockState.Width) / MOVEMENT_SPEED, linkDirection);
                moved += ((blockState.Width) / MOVEMENT_SPEED);
                movementTicks++;
            }
            else if(moving)
            {
                moving = false;

                if(moved != blockState.Width) //If the block hasn't moved its width in the time allotted, this will move it slightly faster on the last tick so it gets to its destination
                {
                    Move((blockState.Width - moved), linkDirection);
                    moved = 0;
                }
                movementTicks = 0;
            }
            blockState.Update(gT);
        }
        public void Move(int numPixels, LinkStateMachine.Direction direction) 
        {
            blockState.Move(numPixels, direction);
        }

        public void Push(LinkStateMachine.Direction direction) 
        {
            moving = true;
            movementTicks = 0;
            linkDirection = direction;
        }
        public void Unlock() 
        {
            blockState.Unlock();
            Exists = false;
        }
        public void Reveal() 
        {
            blockState.Reveal();
        }
        public void Draw(SpriteBatch sB, Color col) 
        {
            blockState.Draw(sB, col);
        }
        public void UpdateTexture(Texture2D text) 
        {
            blockState.UpdateTexture(text);
        }

        public void UpdateKind(BlockSpriteFactory.BlockKind kind) 
        {
            blockState.UpdateKind(kind);
        }

        //CollidableObject properties
        public Rectangle CurrentHitbox 
        { 
            get { return blockState.GetHitbox(); } 
            set 
            {
                blockState.X = value.X;
                blockState.Y = value.Y;
                blockState.Width = value.Width;
                blockState.Height = value.Height;
            } 
        }

        public CollidableObject.Type CollidableType { get { return blockState.CollidableType; } }

        public object Clone()
        {
            return BlockSpriteFactory.Instance.CreateBlock(X, Y, blockType, blockState.FrameType);
        }
    }
}
