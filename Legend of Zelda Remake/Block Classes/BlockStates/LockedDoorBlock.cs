using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Sprint0;
using Sprint2.Item_Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sprint2.Interfaces;

namespace Sprint2.Block_Classes.BlockStates
{
    public class LockedDoorBlock : IBlockState
    {
        //Not useful for this class
        public bool IsLocked
        {
            get { return true; }
        }
        //Collidable information
        private CollidableObject.Type collidableType = CollidableObject.Type.StandardBlock;
        public CollidableObject.Type CollidableType
        {
            get { return collidableType; }
        }
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

        //Location data
        private int x;
        private int y;
        private int width = GlobalUtilities.STANDARD_DOOR_WIDTH;
        private int height = GlobalUtilities.STANDARD_DOOR_HEIGHT;

        public int X { get { return x; } set { x = value; } }
        public int Y { get { return y; } set { y = value; } }
        public int Width { get { return width; } set { width = value; } }
        public int Height { get { return height; } set { height = value; } }

        //Existence
        private bool exists = true;
        public bool Exists { get { return exists; } set { exists = value; } }

        //Frame information
        private BlockSpriteFactory.BlockKind frameType;
        private Rectangle frame;
        Texture2D texture;
        public BlockSpriteFactory.BlockKind FrameType { get { return frameType; } set { frameType = value; } }
        public Rectangle Frame { get { return frame; } set { frame = value; } }

        //Constructor
        public LockedDoorBlock() { }

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
        public void Unlock() 
        {
            Exists = false;
        }
        public void Lock() { }
        public void Reveal() { }
        public void Draw(SpriteBatch sB, Color col)
        {
            Rectangle rect = new Rectangle(X, Y, width, height);
            sB.Draw(texture, rect, Frame, col);
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
            frameType = kind;
            UpdateFrame();
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
