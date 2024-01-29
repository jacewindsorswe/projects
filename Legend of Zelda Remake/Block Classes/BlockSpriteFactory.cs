using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Sprint2.Block_Classes;
using Sprint2.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Sprint2.Block_Classes.BlockStateMachine;

namespace Sprint0
{
	public class BlockSpriteFactory 
	{
		public enum BlockKind 
		{
			Stairs,
			FlatSquare,
			Statue,
			Statue2,
			BlueGap,
			Pushable,
			WhiteBrick,
			BlueSand,
			WhitePanes,
			LockedDoorLeft,
			LockedDoorRight,
			LockedDoorUp,
			LockedDoorDown,
			ClosedDoorUp,
			ClosedDoorDown,
			ClosedDoorLeft,
			ClosedDoorRight,
			LongWall,
			ShortWall,
			WallLeft,
			WallRight,
			WallTop,
			WallBottom,
			OldMan,

            DarkLockedDoorLeft,
            DarkLockedDoorRight,
            DarkLockedDoorUp,
            DarkLockedDoorDown,
            DarkClosedDoorUp,
            DarkClosedDoorDown,
            DarkClosedDoorLeft,
            DarkClosedDoorRight,
            DarkWallLeft,
            DarkWallRight,
            DarkWallTop,
            DarkWallBottom,
        }

		// In order: { x, y, width, height }
        public readonly static Dictionary<BlockKind, int[]> blockFrameDictionary = new Dictionary<BlockKind, int[]>()
        {
            { BlockKind.Stairs, new int[] { 296, 107, IBlockState.DEFAULT_FRAME_WIDTH, IBlockState.DEFAULT_FRAME_HEIGHT } },
            { BlockKind.FlatSquare, new int[] { 10, 11, IBlockState.DEFAULT_FRAME_WIDTH, IBlockState.DEFAULT_FRAME_HEIGHT } },
            { BlockKind.Statue, new int[] { 202, 11, IBlockState.DEFAULT_FRAME_WIDTH, IBlockState.DEFAULT_FRAME_HEIGHT } },
            { BlockKind.Statue2, new int[] { 296, 10, IBlockState.DEFAULT_FRAME_WIDTH, IBlockState.DEFAULT_FRAME_HEIGHT } },
            { BlockKind.WhitePanes, new int[] { 107, 204, IBlockState.DEFAULT_FRAME_WIDTH, IBlockState.DEFAULT_FRAME_HEIGHT } },
            { BlockKind.BlueGap, new int[] { 201, 107, IBlockState.DEFAULT_FRAME_WIDTH, IBlockState.DEFAULT_FRAME_HEIGHT } },
            { BlockKind.Pushable, new int[] { 105, 11, IBlockState.DEFAULT_FRAME_WIDTH, IBlockState.DEFAULT_FRAME_HEIGHT } },
            { BlockKind.WhiteBrick, new int[] { 10, 202, IBlockState.DEFAULT_FRAME_WIDTH, IBlockState.DEFAULT_FRAME_HEIGHT } },
            { BlockKind.BlueSand, new int[] { 105, 108, IBlockState.DEFAULT_FRAME_WIDTH, IBlockState.DEFAULT_FRAME_HEIGHT } },
			{ BlockKind.LockedDoorUp, new int[] { 243, 86, 32, 20 } },
            { BlockKind.LockedDoorDown, new int[] { 279, 86, 32, 20 } },
            { BlockKind.LockedDoorRight, new int[] { 314, 74, 20, 32 } },
            { BlockKind.LockedDoorLeft, new int[] { 337, 74, 20, 32 } },
            { BlockKind.ClosedDoorUp, new int[] { 243, 49, 32, 20 } },
            { BlockKind.ClosedDoorDown, new int[] { 279, 49, 32, 20 } },
            { BlockKind.ClosedDoorRight, new int[] { 314, 37, 20, 32 } },
            { BlockKind.ClosedDoorLeft, new int[] { 337, 37, 20, 32 } },
            { BlockKind.WallTop, new int[] { 96, 12, 48, 24 } },
            { BlockKind.WallLeft, new int[] { 0, 68, 24, 48 } },
            { BlockKind.WallBottom, new int[] { 96, 148, 48, 24 } },
            { BlockKind.WallRight, new int[] { 216, 68, 24, 48 } },
			{ BlockKind.OldMan, new int[] { 0, 0, 16, 16 } },


            { BlockKind.DarkLockedDoorUp, new int[] { 243, 261, 32, 20 } },
            { BlockKind.DarkLockedDoorDown, new int[] { 279, 261, 32, 20 } },
            { BlockKind.DarkLockedDoorRight, new int[] { 314, 249, 20, 32 } },
            { BlockKind.DarkLockedDoorLeft, new int[] { 337, 249, 20, 32 } },
            { BlockKind.DarkClosedDoorUp, new int[] { 243, 224, 32, 20 } },
            { BlockKind.DarkClosedDoorDown, new int[] { 279, 224, 32, 20 } },
            { BlockKind.DarkClosedDoorRight, new int[] { 314, 212, 20, 32 } },
            { BlockKind.DarkClosedDoorLeft, new int[] { 337, 212, 20, 32 } },
            { BlockKind.DarkWallTop, new int[] { 96, 187, 48, 24 } },
            { BlockKind.DarkWallLeft, new int[] { 0, 243, 24, 48 } },
            { BlockKind.DarkWallBottom, new int[] { 96, 323, 48, 24 } },
            { BlockKind.DarkWallRight, new int[] { 216, 243, 24, 48 } }
        };

		private List<BlockKind> doorTypes = new List<BlockKind>() { BlockKind.LockedDoorUp, BlockKind.LockedDoorDown, BlockKind.LockedDoorLeft, BlockKind.LockedDoorRight, BlockKind.ClosedDoorUp, BlockKind.ClosedDoorDown, BlockKind.ClosedDoorLeft, BlockKind.ClosedDoorRight, BlockKind.WallTop, BlockKind.WallLeft, BlockKind.WallBottom, BlockKind.WallRight, BlockKind.DarkLockedDoorUp, BlockKind.DarkLockedDoorDown, BlockKind.DarkLockedDoorLeft, BlockKind.DarkLockedDoorRight, BlockKind.DarkClosedDoorUp, BlockKind.DarkClosedDoorDown, BlockKind.DarkClosedDoorLeft, BlockKind.DarkClosedDoorRight, BlockKind.DarkWallTop, BlockKind.DarkWallLeft, BlockKind.DarkWallBottom, BlockKind.DarkWallRight };

        private Texture2D blockSpriteSheet, doorSpriteSheet, oldManSpriteSheet;

		private static BlockSpriteFactory instance = new BlockSpriteFactory();
		public static BlockSpriteFactory Instance 
		{
			get 
			{
				return instance;
			}
		}

		private BlockSpriteFactory() { }

		public void LoadAllTextures(ContentManager content) 
		{
			blockSpriteSheet = content.Load<Texture2D>("BlockSprites");
			doorSpriteSheet = content.Load<Texture2D>("tiles-dungeon");
			oldManSpriteSheet = content.Load<Texture2D>("OldMan");
		}

		public BlockStateMachine CreateBlock(int x, int y, BlockStateMachine.BlockType bT, BlockKind kind)
		{
			if (doorTypes.Contains(kind)) 
			{
                return new BlockStateMachine(doorSpriteSheet, bT, kind, x, y);
            }
			else if(kind == BlockKind.OldMan) 
			{
                return new BlockStateMachine(oldManSpriteSheet, bT, kind, x, y);
            }
			return new BlockStateMachine(blockSpriteSheet, bT, kind, x, y);
        }
        public BlockStateMachine CreateBlock(int x, int y, int width, int height, BlockStateMachine.BlockType bT, BlockKind kind)
        {
            if (doorTypes.Contains(kind))
            {
                return new BlockStateMachine(doorSpriteSheet, bT, kind, x, y, width, height);
            }
            else if (kind == BlockKind.OldMan)
            {
                return new BlockStateMachine(oldManSpriteSheet, bT, kind, x, y);
            }
            return new BlockStateMachine(blockSpriteSheet, bT, kind, x, y, width, height);
        }

    }
}