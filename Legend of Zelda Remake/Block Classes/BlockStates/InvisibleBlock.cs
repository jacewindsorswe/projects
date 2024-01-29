using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Sprint0;
using Sprint2.Interfaces;
using Sprint2.Item_Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sprint2.Block_Classes.BlockStates
{
    public class InvisibleBlock : IBlockState
    {
        //Not useful for this class
        public bool IsLocked
        {
            get { return false; }
        }
        //Used for color if the wall needs to be revealed for debugging
        private readonly Color WALL_COLOR = Color.Black;
        private Color color = Color.White;

        //Organized by { width, height }
        private Dictionary<BlockSpriteFactory.BlockKind, int[]> invisibleWallSizes = new Dictionary<BlockSpriteFactory.BlockKind, int[]>()
        {
            { BlockSpriteFactory.BlockKind.LongWall, new int[]{ 40, 40 } },
            { BlockSpriteFactory.BlockKind.ShortWall, new int[]{ 20, 20 } }
        };

        //Collidable information
        private CollidableObject.Type collidableType = CollidableObject.Type.StandardBlock;
        public CollidableObject.Type CollidableType
        {
            get { return collidableType; }
        }

        //Location data
        private int x;
        private int y;
        private int width = GlobalUtilities.STANDARD_BLOCK_WIDTH;
        private int height = GlobalUtilities.STANDARD_BLOCK_HEIGHT;

        public int X { get { return x; } set { x = value; } }
        public int Y { get { return y; } set { y = value; } }
        public int Width { get { return width; } set { width = value; } }
        public int Height { get { return height; } set { height = value; } }

        private bool gap = false;
        private bool teleport = false;
        public bool IsGap
        {
            get { return gap; }
            set { gap = value; }
        }
        public bool CanTeleport
        {
            get { return teleport; }
            set { teleport = value; }
        }

        //Existence
        private bool exists = true;
        public bool Exists { get { return exists; } set { exists = value; } }

        //Revealed
        private bool revealed = false;

        //Frame information
        private BlockSpriteFactory.BlockKind frameType;
        private Rectangle frame;
        Texture2D texture;
        public BlockSpriteFactory.BlockKind FrameType { get { return frameType; } set { frameType = value; } }
        public Rectangle Frame { get { return frame; } set { frame = value; } }

        //Constructor
        public InvisibleBlock() { }

        //Methods to be called from a BlockStateMachine
        public void Update(GameTime gT) 
        {
            if (!Exists) 
            {
                X = 0;
                Y = 0;
                Width = 0;
                Height = 0;
            }
        }
        public void Move(int numPixels, LinkStateMachine.Direction direction) { }
        public void Unlock() { }
        public void Lock() { }
        public void Reveal() 
        {
            if (!revealed)
            {
                color = WALL_COLOR;
                revealed = true;
            }
            else 
            {
                revealed = false;
                color = Color.White;
            }
        }

        public void Draw(SpriteBatch sB, Color col)
        {
            if (revealed)
            {
                sB.Draw(texture, new Rectangle(X, Y, Width, Height), color);
            }
        }
        public Rectangle GetHitbox()
        {
            return new Rectangle(X, Y, width, height);
        }

        public void UpdateTexture(Texture2D text) 
        {
            texture = text;
        }
        public void UpdateKind(BlockSpriteFactory.BlockKind kind) 
        {
            if (invisibleWallSizes.ContainsKey(kind)) 
            {
                Width = invisibleWallSizes[kind][0];
                Height = invisibleWallSizes[kind][1];
            }
        }

        //Miscellaneous private methods
        private void MoveRight(int numPixels)
        {
            X += numPixels;
        }
        private void MoveLeft(int numPixels)
        {
            X -= numPixels;
        }
        private void MoveUp(int numPixels)
        {
            Y -= numPixels;
        }
        private void MoveDown(int numPixels)
        {
            Y += numPixels;
        }

        private void UpdateFrame()
        {
            int[] frameCoords = BlockSpriteFactory.blockFrameDictionary[FrameType];
            Frame = new Rectangle(frameCoords[0], frameCoords[1], frameCoords[2], frameCoords[3]);
        }
    }
}
