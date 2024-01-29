using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Sprint0;
using Sprint2.Block_Classes;
using Sprint2.Item_Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sprint2.Interfaces
{
    public interface IBlockState
    {
        public const int DEFAULT_FRAME_WIDTH = 91;
        public const int DEFAULT_FRAME_HEIGHT = 91;
        public CollidableObject.Type CollidableType { get; }
        public int X { get; set; }
        public int Y { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public bool Exists { get; set; }
        public bool IsGap { get; set; }
        public bool CanTeleport { get; set; }

        public bool IsLocked { get; }

        public BlockSpriteFactory.BlockKind FrameType { get; set; }
        public Rectangle Frame { get; set; }
        public void Update(GameTime gT);
        public void Move(int numPixels, LinkStateMachine.Direction direction);

        //Door --> unlocks, Pushable blocks --> allows movement
        public void Unlock();
        //Pushable blocks --> disallows movement
        public void Lock();
        public void Reveal();
        public void Draw(SpriteBatch sB, Color col);
        public Rectangle GetHitbox();
        public void UpdateTexture(Texture2D texture);
        public void UpdateKind(BlockSpriteFactory.BlockKind kind);
    }
}
