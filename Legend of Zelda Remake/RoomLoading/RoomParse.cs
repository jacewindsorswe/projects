using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Microsoft.Xna.Framework;
using System.Security.Cryptography.X509Certificates;
using System.ComponentModel;
using Sprint2.Interfaces;
using static Sprint0.BlockSpriteFactory;
using static Sprint2.Block_Classes.BlockStateMachine;
using Sprint2.Block_Classes;
using Sprint2.Item_Classes;
using Sprint2;
using System.Numerics;
using Microsoft.Xna.Framework.Input;

namespace Sprint0
{
    public class RoomParse
    {
        DataTable roomTable;
        Room room;
        BackgroundSprite background = (BackgroundSprite)BackgroundSpriteFactory.Instance.CreateRoom(BackgroundSpriteFactory.RoomType.Room25);
        String roomName;
        public List<Tuple<CollidableObject.Type, int>> roomContent;
        public Dictionary<int[], String> itemLocations;
        Game1 game;
        private static int ROW_SIZE = 48;
        private static int COLUMN_SIZE = 48;

        private static int TABLE_START_X = 96;
        private static int TABLE_START_Y = 96;

        private static int RL_WALL_WIDTH = 96;
        private static int RL_WALL_HEIGHT = 145;

        private static int TB_WALL_WIDTH = 264;
        private static int TB_WALL_HEIGHT = 96;

        private static int CORNER_SIZE = 96;
        public static RoomParse instance = new RoomParse();

        private readonly Dictionary<string, BackgroundSpriteFactory.RoomType> RoomTypeDictionary = new()
        {
            { "Room210", BackgroundSpriteFactory.RoomType.Room210 },
            { "Room220", BackgroundSpriteFactory.RoomType.Room220 },
            { "Room221", BackgroundSpriteFactory.RoomType.Room221 },
            { "Room231", BackgroundSpriteFactory.RoomType.Room231 },
            { "Room222", BackgroundSpriteFactory.RoomType.Room222 },
            { "Room232", BackgroundSpriteFactory.RoomType.Room232 },
            { "Room223", BackgroundSpriteFactory.RoomType.Room223 },
            { "Room233", BackgroundSpriteFactory.RoomType.Room233 },
            { "Room224", BackgroundSpriteFactory.RoomType.Room224 },
            { "Room234", BackgroundSpriteFactory.RoomType.Room234 },
            { "Room225", BackgroundSpriteFactory.RoomType.Room225 },
            { "Room235", BackgroundSpriteFactory.RoomType.Room235 },
            { "Room206", BackgroundSpriteFactory.RoomType.Room206 },
            { "Room216", BackgroundSpriteFactory.RoomType.Room216 },
            { "Room226", BackgroundSpriteFactory.RoomType.Room226 },
            { "Room236", BackgroundSpriteFactory.RoomType.Room236 },
            { "Room217", BackgroundSpriteFactory.RoomType.Room217 },
            { "Room227", BackgroundSpriteFactory.RoomType.Room227 },
        };
        
        public static RoomParse Instance
        {
            get
            {
                return instance;
            }
        }

        public Room ParseRoom(DataTable Room, Game1 game1)
        {
            roomContent = new List<Tuple<CollidableObject.Type, int>>();
            itemLocations = new Dictionary<int[], String>();
            this.roomTable = Room;
            this.game = game1;
            LoadCurrentContent();
            MainContent();
            HandleDoors();

            room = new Room(background, roomContent);
            return room;
        }
        private void LoadCurrentContent()
        {
            roomName = roomTable.Rows[1][13].ToString();
            if (RoomTypeDictionary.ContainsKey(roomName))
            {
                background = (BackgroundSprite)BackgroundSpriteFactory.Instance.CreateRoom(RoomTypeDictionary[roomName]);
                GenerateWalls();
            }
            else
            {
                switch (roomName)
                {
                    case "Room11":
                        background = (BackgroundSprite)BackgroundSpriteFactory.Instance.CreateRoom(BackgroundSpriteFactory.RoomType.Room00);
                        Generate00Walls();
                        break;
                    case "Room10":
                        background = (BackgroundSprite)BackgroundSpriteFactory.Instance.CreateRoom(BackgroundSpriteFactory.RoomType.Room10);
                        GenerateWalls();
                        break;
                    case "Room20":
                        background = (BackgroundSprite)BackgroundSpriteFactory.Instance.CreateRoom(BackgroundSpriteFactory.RoomType.Room20);
                        GenerateWalls();
                        break;
                    case "Room21":
                        background = (BackgroundSprite)BackgroundSpriteFactory.Instance.CreateRoom(BackgroundSpriteFactory.RoomType.Room21);
                        GenerateWalls();
                        break;
                    case "Room41":
                        background = (BackgroundSprite)BackgroundSpriteFactory.Instance.CreateRoom(BackgroundSpriteFactory.RoomType.Room41);
                        GenerateWalls();
                        break;
                    case "Room51":
                        background = (BackgroundSprite)BackgroundSpriteFactory.Instance.CreateRoom(BackgroundSpriteFactory.RoomType.Room51);
                        GenerateWalls();
                        break;
                    case "Room02":
                        background = (BackgroundSprite)BackgroundSpriteFactory.Instance.CreateRoom(BackgroundSpriteFactory.RoomType.Room02);
                        GenerateWalls();
                        break;
                    case "Room12":
                        background = (BackgroundSprite)BackgroundSpriteFactory.Instance.CreateRoom(BackgroundSpriteFactory.RoomType.Room12);
                        GenerateWalls();
                        break;
                    case "Room22":
                        background = (BackgroundSprite)BackgroundSpriteFactory.Instance.CreateRoom(BackgroundSpriteFactory.RoomType.Room22);
                        GenerateWalls();
                        break;
                    case "Room32":
                        background = (BackgroundSprite)BackgroundSpriteFactory.Instance.CreateRoom(BackgroundSpriteFactory.RoomType.Room32);
                        GenerateWalls();
                        break;
                    case "Room42":
                        background = (BackgroundSprite)BackgroundSpriteFactory.Instance.CreateRoom(BackgroundSpriteFactory.RoomType.Room42);
                        GenerateWalls();
                        break;
                    case "Room13":
                        background = (BackgroundSprite)BackgroundSpriteFactory.Instance.CreateRoom(BackgroundSpriteFactory.RoomType.Room13);
                        GenerateWalls();
                        break;
                    case "Room23":
                        background = (BackgroundSprite)BackgroundSpriteFactory.Instance.CreateRoom(BackgroundSpriteFactory.RoomType.Room23);
                        GenerateWalls();
                        break;
                    case "Room33":
                        background = (BackgroundSprite)BackgroundSpriteFactory.Instance.CreateRoom(BackgroundSpriteFactory.RoomType.Room33);
                        GenerateWalls();
                        break;
                    case "Room24":
                        background = (BackgroundSprite)BackgroundSpriteFactory.Instance.CreateRoom(BackgroundSpriteFactory.RoomType.Room24);
                        GenerateWalls();
                        break;
                    case "Room15":
                        background = (BackgroundSprite)BackgroundSpriteFactory.Instance.CreateRoom(BackgroundSpriteFactory.RoomType.Room15);
                        GenerateWalls();
                        break;
                    case "Room25":
                        background = (BackgroundSprite)BackgroundSpriteFactory.Instance.CreateRoom(BackgroundSpriteFactory.RoomType.Room25);
                        GenerateWalls();
                        break;
                    case "Room35":
                        background = (BackgroundSprite)BackgroundSpriteFactory.Instance.CreateRoom(BackgroundSpriteFactory.RoomType.Room35);
                        GenerateWalls();
                        break;
                    case "Room26":
                        background = (BackgroundSprite)BackgroundSpriteFactory.Instance.CreateRoom(BackgroundSpriteFactory.RoomType.Room26);
                        GenerateWalls();
                        break;
                }
            }

        }

        public void MainContent()
        {
            String currentTileValue;
            int x, y;
            if (roomTable.Rows[1][14].ToString().Equals("F"))
            {
                for (int row = 0; row < GlobalUtilities.CSV_NUM_ROWS; row++)
                {
                    for(int col = 0; col < GlobalUtilities.CSV_NUM_COLUMNS; col++)
                    {
                        currentTileValue = roomTable.Rows[row][col].ToString();

                        x = TABLE_START_X + (COLUMN_SIZE * col); // Starting position + (block_width * column_number)
                        y = TABLE_START_Y + (ROW_SIZE * row); // Starting position + (block_height * row_number)

                        itemLocations.Add(new int[2] { x, y }, currentTileValue);
                    }
                }
                if (roomTable.Rows[1][13].ToString().Equals("Room00") || roomTable.Rows[1][13].ToString().Equals("Room11"))
                {
                    itemLocations.Add(new int[2] {144,0-GlobalUtilities.STANDARD_BLOCK_HEIGHT},"InvisibleBlocktt");
                    Add11Content();
                }
            }
            DetermineType();
        }
        private void HandleDoors()
        {
            if (roomTable.Rows[1][12].ToString() == "Wall")
            {
                BlockStateMachine leftWall = BlockSpriteFactory.Instance.CreateBlock(0, 0, 66, 144, BlockStateMachine.BlockType.InvisibleBlock, BlockSpriteFactory.BlockKind.FlatSquare);
                leftWall.X = GlobalUtilities.DEFAULT_LEFT_WALL_X;
                leftWall.Y = GlobalUtilities.DEFAULT_LEFT_WALL_Y;
                leftWall.IsActive = false;
                roomContent.Add(Tuple.Create<CollidableObject.Type, int>(CollidableObject.Type.StandardBlock, leftWall.ID));
            }
            else if (roomTable.Rows[1][12].ToString() == "Locked")
            {
                BlockStateMachine leftWall = BlockSpriteFactory.Instance.CreateBlock(0, 0, 57, 96, BlockStateMachine.BlockType.HorizontalLockedDoor, GetBlockKind(BlockType.HorizontalLockedDoor, 0));
                leftWall.X = GlobalUtilities.LEFT_WALL_LOCKED_X;
                leftWall.Y = GlobalUtilities.DEFAULT_LEFT_WALL_Y;
                leftWall.IsActive = false;
                roomContent.Add(Tuple.Create<CollidableObject.Type, int>(CollidableObject.Type.StandardBlock, leftWall.ID));
            }
            if (roomTable.Rows[2][12].ToString() == "Wall")
            {
                BlockStateMachine topWall = BlockSpriteFactory.Instance.CreateBlock(0, 0, 144, 66, BlockStateMachine.BlockType.InvisibleBlock, BlockSpriteFactory.BlockKind.FlatSquare);
                topWall.X = GlobalUtilities.DEFAULT_TOP_WALL_X;
                topWall.Y = GlobalUtilities.DEFAULT_TOP_WALL_Y;
                topWall.IsActive = false;
                roomContent.Add(Tuple.Create<CollidableObject.Type, int>(CollidableObject.Type.StandardBlock, topWall.ID));
            }
            else if (roomTable.Rows[2][12].ToString() == "Locked")
            {
                BlockStateMachine topWall = BlockSpriteFactory.Instance.CreateBlock(0, 0, 96, 57, BlockStateMachine.BlockType.VerticalLockedDoor, GetBlockKind(BlockType.VerticalLockedDoor, 0));
                topWall.X = GlobalUtilities.TOP_WALL_LOCKED_X;
                topWall.Y = GlobalUtilities.TOP_WALL_LOCKED_Y;
                topWall.IsActive = false;
                roomContent.Add(Tuple.Create<CollidableObject.Type, int>(CollidableObject.Type.StandardBlock, topWall.ID));
            }
            else if(roomTable.Rows[2][12].ToString() == "Explode")
            {
                BlockStateMachine topWall = BlockSpriteFactory.Instance.CreateBlock(312,24,144, 72, BlockStateMachine.BlockType.ExplodableDoor, GetBlockKind(BlockType.ExplodableDoor, 0));
                topWall.IsActive = false;
                roomContent.Add(Tuple.Create<CollidableObject.Type, int>(CollidableObject.Type.StandardBlock, topWall.ID));
            }
            if (roomTable.Rows[3][12].ToString() == "Wall")
            {
                BlockStateMachine rightWall = BlockSpriteFactory.Instance.CreateBlock(0, 0, 66, 144, BlockStateMachine.BlockType.InvisibleBlock, BlockSpriteFactory.BlockKind.FlatSquare);
                rightWall.X = GlobalUtilities.Resolution[0] - 98;
                rightWall.Y = GlobalUtilities.DEFAULT_RIGHT_WALL_Y;
                rightWall.IsActive = false;
                roomContent.Add(Tuple.Create<CollidableObject.Type, int>(CollidableObject.Type.StandardBlock, rightWall.ID));
            }
            else if (roomTable.Rows[3][12].ToString() == "Locked")
            {
                BlockStateMachine rightWall = BlockSpriteFactory.Instance.CreateBlock(0, 0, 57, 96, BlockStateMachine.BlockType.HorizontalLockedDoor, GetBlockKind(BlockType.HorizontalLockedDoor, 1));
                rightWall.X = GlobalUtilities.Resolution[0] - 98;
                rightWall.Y = GlobalUtilities.RIGHT_WALL_LOCKED_Y;
                rightWall.IsActive = false;
                roomContent.Add(Tuple.Create<CollidableObject.Type, int>(CollidableObject.Type.StandardBlock, rightWall.ID));
            }
            if (roomTable.Rows[4][12].ToString() == "Wall")
            {
                BlockStateMachine bottomWall1 = BlockSpriteFactory.Instance.CreateBlock(100, 0, 144, 66, BlockStateMachine.BlockType.InvisibleBlock, BlockSpriteFactory.BlockKind.FlatSquare);
                bottomWall1.X = GlobalUtilities.DEFAULT_BOTTOM_WALL_X;
                bottomWall1.Y = GlobalUtilities.DEFAULT_BOTTOM_WALL_Y;
                bottomWall1.IsActive = false;
                roomContent.Add(Tuple.Create<CollidableObject.Type, int>(CollidableObject.Type.StandardBlock, bottomWall1.ID));
            }
            else if(roomTable.Rows[4][12].ToString() == "Locked")
            {
                BlockStateMachine bottomWall1 = BlockSpriteFactory.Instance.CreateBlock(100, 0, 96, 57, BlockStateMachine.BlockType.VerticalLockedDoor, GetBlockKind(BlockType.VerticalLockedDoor, 1));
                bottomWall1.X = GlobalUtilities.BOTTOM_WALL_LOCKED_X;
                bottomWall1.Y = GlobalUtilities.DEFAULT_BOTTOM_WALL_Y;
                bottomWall1.IsActive = false;
                roomContent.Add(Tuple.Create<CollidableObject.Type, int>(CollidableObject.Type.StandardBlock, bottomWall1.ID));
            }
            else if (roomTable.Rows[4][12].ToString() == "Explode")
            {
                BlockStateMachine bottomWall1 = BlockSpriteFactory.Instance.CreateBlock(312, 432,144, 72, BlockStateMachine.BlockType.ExplodableDoor, GetBlockKind(BlockType.ExplodableDoor, 1));
                bottomWall1.IsActive = false;
                roomContent.Add(Tuple.Create<CollidableObject.Type, int>(CollidableObject.Type.StandardBlock, bottomWall1.ID));
            }
            if (roomTable.Rows[1][14].ToString().Equals("F"))
            {
                if (roomTable.Rows[1][12].ToString() == "Closed")
                {
                    BlockStateMachine leftWall = BlockSpriteFactory.Instance.CreateBlock(0, 0, 57, 96, BlockStateMachine.BlockType.HorizontalClosedDoor, GetBlockKind(BlockType.HorizontalClosedDoor, 0));
                    leftWall.X = GlobalUtilities.LEFT_WALL_LOCKED_X;
                    leftWall.Y = GlobalUtilities.DEFAULT_LEFT_WALL_Y;
                    leftWall.IsActive = false;
                    roomContent.Add(Tuple.Create<CollidableObject.Type, int>(CollidableObject.Type.StandardBlock, leftWall.ID));
                }
                if (roomTable.Rows[2][12].ToString() == "Closed")
                {
                    BlockStateMachine topWall = BlockSpriteFactory.Instance.CreateBlock(0, 0, 96, 57, BlockStateMachine.BlockType.VerticalClosedDoor, GetBlockKind(BlockType.VerticalClosedDoor, 0));
                    topWall.X = GlobalUtilities.TOP_WALL_LOCKED_X;
                    topWall.Y = GlobalUtilities.TOP_WALL_LOCKED_Y;
                    topWall.IsActive = false;
                    roomContent.Add(Tuple.Create<CollidableObject.Type, int>(CollidableObject.Type.StandardBlock, topWall.ID));
                }
                if(roomTable.Rows[3][12].ToString() == "Closed")
                {
                    BlockStateMachine rightWall = BlockSpriteFactory.Instance.CreateBlock(0, 0, 57, 96, BlockStateMachine.BlockType.HorizontalClosedDoor, GetBlockKind(BlockType.HorizontalClosedDoor, 1));
                    rightWall.X = GlobalUtilities.Resolution[0] - 98;
                    rightWall.Y = GlobalUtilities.RIGHT_WALL_LOCKED_Y;
                    rightWall.IsActive = false;
                    roomContent.Add(Tuple.Create<CollidableObject.Type, int>(CollidableObject.Type.StandardBlock, rightWall.ID));
                }
                if (roomTable.Rows[4][12].ToString() == "Closed")
                {
                    BlockStateMachine bottomWall1 = BlockSpriteFactory.Instance.CreateBlock(100, 0, 96, 57, BlockStateMachine.BlockType.VerticalClosedDoor, GetBlockKind(BlockType.VerticalClosedDoor, 1));
                    bottomWall1.X = GlobalUtilities.BOTTOM_WALL_LOCKED_X;
                    bottomWall1.Y = GlobalUtilities.DEFAULT_BOTTOM_WALL_Y;
                    bottomWall1.IsActive = false;
                    roomContent.Add(Tuple.Create<CollidableObject.Type, int>(CollidableObject.Type.StandardBlock, bottomWall1.ID));
                }
            }

        }

        private static int INVISIBLE_WALL_HEIGHT = 114;
        private static int INVISIBLE_WALL_HEIGHT2 = 65;
        private void Generate00Walls()
        {
            BlockStateMachine leftWall = BlockSpriteFactory.Instance.CreateBlock(0, 0, 96, 528, BlockType.InvisibleBlock, BlockKind.FlatSquare);
            leftWall.IsActive = false;
            roomContent.Add(Tuple.Create<CollidableObject.Type, int>(CollidableObject.Type.StandardBlock, leftWall.ID));

            BlockStateMachine leftStairs = BlockSpriteFactory.Instance.CreateBlock(96, 0, 48, 313, BlockType.InvisibleBlock, BlockKind.FlatSquare);
            leftStairs.IsActive = false;
            roomContent.Add(Tuple.Create<CollidableObject.Type, int>(CollidableObject.Type.StandardBlock, leftStairs.ID));

            BlockStateMachine bottomSection = BlockSpriteFactory.Instance.CreateBlock(96, 422, 672, 106, BlockType.InvisibleBlock, BlockKind.FlatSquare);
            bottomSection.IsActive = false;
            roomContent.Add(Tuple.Create<CollidableObject.Type, int>(CollidableObject.Type.StandardBlock, bottomSection.ID));

            BlockStateMachine middleSection = BlockSpriteFactory.Instance.CreateBlock(192, 264, 336, 49, BlockType.InvisibleBlock, BlockKind.FlatSquare);
            middleSection.IsActive = false;
            roomContent.Add(Tuple.Create<CollidableObject.Type, int>(CollidableObject.Type.StandardBlock, middleSection.ID));

            BlockStateMachine middleSection2 = BlockSpriteFactory.Instance.CreateBlock(192, 102, 144, 162, BlockType.InvisibleBlock, BlockKind.FlatSquare);
            middleSection2.IsActive = false;
            roomContent.Add(Tuple.Create<CollidableObject.Type, int>(CollidableObject.Type.StandardBlock, middleSection2.ID));

            BlockStateMachine middleSection3 = BlockSpriteFactory.Instance.CreateBlock(576, 264, 96, 49, BlockType.InvisibleBlock, BlockKind.FlatSquare);
            middleSection3.IsActive = false;
            roomContent.Add(Tuple.Create<CollidableObject.Type, int>(CollidableObject.Type.StandardBlock, middleSection3.ID));

            BlockStateMachine topSection = BlockSpriteFactory.Instance.CreateBlock(192, 0, 576, 102, BlockType.InvisibleBlock, BlockKind.FlatSquare);
            topSection.IsActive = false;
            roomContent.Add(Tuple.Create<CollidableObject.Type, int>(CollidableObject.Type.StandardBlock, topSection.ID));

            BlockStateMachine rightWall = BlockSpriteFactory.Instance.CreateBlock(672, 0, 96, 422, BlockType.InvisibleBlock, BlockKind.FlatSquare);
            rightWall.IsActive = false;
            roomContent.Add(Tuple.Create<CollidableObject.Type, int>(CollidableObject.Type.StandardBlock, rightWall.ID));

            //Invisible boundaries
            BlockStateMachine bowRoom = BlockSpriteFactory.Instance.CreateBlock(337, 103, 336, INVISIBLE_WALL_HEIGHT, BlockType.InvisibleBlock, BlockKind.FlatSquare);
            bowRoom.IsActive = false;
            bowRoom.IsGap = true;
            roomContent.Add(Tuple.Create<CollidableObject.Type, int>(CollidableObject.Type.StandardBlock, bowRoom.ID));

            BlockStateMachine largeRoom1 = BlockSpriteFactory.Instance.CreateBlock(97, 314, 48, INVISIBLE_WALL_HEIGHT2, BlockType.InvisibleBlock, BlockKind.FlatSquare);
            largeRoom1.IsActive = false;
            largeRoom1.IsGap = true;
            roomContent.Add(Tuple.Create<CollidableObject.Type, int>(CollidableObject.Type.StandardBlock, largeRoom1.ID));

            BlockStateMachine largeRoom2 = BlockSpriteFactory.Instance.CreateBlock(193, 314, 336, INVISIBLE_WALL_HEIGHT2, BlockType.InvisibleBlock, BlockKind.FlatSquare);
            largeRoom2.IsActive = false;
            largeRoom2.IsGap = true;
            roomContent.Add(Tuple.Create<CollidableObject.Type, int>(CollidableObject.Type.StandardBlock, largeRoom2.ID));

            BlockStateMachine largeRoom3 = BlockSpriteFactory.Instance.CreateBlock(576, 314, 95, INVISIBLE_WALL_HEIGHT2, BlockType.InvisibleBlock, BlockKind.FlatSquare);
            largeRoom3.IsActive = false;
            largeRoom3.IsGap = true;
            roomContent.Add(Tuple.Create<CollidableObject.Type, int>(CollidableObject.Type.StandardBlock, largeRoom3.ID));
        }
        private void GenerateWalls()
        {
            BlockStateMachine topLeftCorner = BlockSpriteFactory.Instance.CreateBlock(0, 0, CORNER_SIZE, CORNER_SIZE, BlockStateMachine.BlockType.InvisibleBlock, BlockSpriteFactory.BlockKind.FlatSquare);
            topLeftCorner.IsActive = false;
            topLeftCorner.IsWall = true;
            roomContent.Add(Tuple.Create<CollidableObject.Type, int>(CollidableObject.Type.StandardBlock, topLeftCorner.ID));

            BlockStateMachine topRightCorner = BlockSpriteFactory.Instance.CreateBlock(672, 0, CORNER_SIZE, CORNER_SIZE, BlockStateMachine.BlockType.InvisibleBlock, BlockSpriteFactory.BlockKind.FlatSquare);
            topRightCorner.IsActive = false;
            topRightCorner.IsWall = true;
            roomContent.Add(Tuple.Create<CollidableObject.Type, int>(CollidableObject.Type.StandardBlock, topRightCorner.ID));

            BlockStateMachine bottomLeftCorner = BlockSpriteFactory.Instance.CreateBlock(0, 432, CORNER_SIZE, CORNER_SIZE, BlockStateMachine.BlockType.InvisibleBlock, BlockSpriteFactory.BlockKind.FlatSquare);
            bottomLeftCorner.IsActive = false;
            bottomLeftCorner.IsWall = true;
            roomContent.Add(Tuple.Create<CollidableObject.Type, int>(CollidableObject.Type.StandardBlock, bottomLeftCorner.ID));

            BlockStateMachine bottomRightCorner = BlockSpriteFactory.Instance.CreateBlock(672, 432, CORNER_SIZE, CORNER_SIZE, BlockStateMachine.BlockType.InvisibleBlock, BlockSpriteFactory.BlockKind.FlatSquare);
            bottomRightCorner.IsActive = false;
            bottomRightCorner.IsWall = true;
            roomContent.Add(Tuple.Create<CollidableObject.Type, int>(CollidableObject.Type.StandardBlock, bottomRightCorner.ID));

            BlockStateMachine leftWall = BlockSpriteFactory.Instance.CreateBlock(0, 95, RL_WALL_WIDTH, RL_WALL_HEIGHT, BlockStateMachine.BlockType.InvisibleBlock, BlockSpriteFactory.BlockKind.FlatSquare);
            leftWall.IsActive = false;
            leftWall.IsWall = true;
            roomContent.Add(Tuple.Create<CollidableObject.Type, int>(CollidableObject.Type.StandardBlock, leftWall.ID));

            BlockStateMachine leftWall2 = BlockSpriteFactory.Instance.CreateBlock(0, 289, RL_WALL_WIDTH, RL_WALL_HEIGHT, BlockStateMachine.BlockType.InvisibleBlock, BlockSpriteFactory.BlockKind.FlatSquare);
            leftWall2.IsActive = false;
            leftWall2.IsWall = true;
            roomContent.Add(Tuple.Create<CollidableObject.Type, int>(CollidableObject.Type.StandardBlock, leftWall2.ID));

            BlockStateMachine topWall = BlockSpriteFactory.Instance.CreateBlock(CORNER_SIZE, 0, TB_WALL_WIDTH, TB_WALL_HEIGHT, BlockStateMachine.BlockType.InvisibleBlock, BlockSpriteFactory.BlockKind.FlatSquare);
            topWall.IsActive = false;
            topWall.IsWall = true;
            roomContent.Add(Tuple.Create<CollidableObject.Type, int>(CollidableObject.Type.StandardBlock, topWall.ID));

            BlockStateMachine topWall2 = BlockSpriteFactory.Instance.CreateBlock(CORNER_SIZE + TB_WALL_WIDTH + 48, 0, TB_WALL_WIDTH, TB_WALL_HEIGHT, BlockStateMachine.BlockType.InvisibleBlock, BlockSpriteFactory.BlockKind.FlatSquare);
            topWall2.IsActive = false;
            topWall2.IsWall = true;
            roomContent.Add(Tuple.Create<CollidableObject.Type, int>(CollidableObject.Type.StandardBlock, topWall2.ID));

            BlockStateMachine rightWall = BlockSpriteFactory.Instance.CreateBlock(672, 95, RL_WALL_WIDTH, RL_WALL_HEIGHT, BlockStateMachine.BlockType.InvisibleBlock, BlockSpriteFactory.BlockKind.FlatSquare);
            rightWall.IsActive = false;
            rightWall.IsWall = true;
            roomContent.Add(Tuple.Create<CollidableObject.Type, int>(CollidableObject.Type.StandardBlock, rightWall.ID));

            BlockStateMachine rightWall2 = BlockSpriteFactory.Instance.CreateBlock(672, 289, RL_WALL_WIDTH, RL_WALL_HEIGHT, BlockStateMachine.BlockType.InvisibleBlock, BlockSpriteFactory.BlockKind.FlatSquare);
            rightWall2.IsActive = false;
            rightWall2.IsWall = true;
            roomContent.Add(Tuple.Create<CollidableObject.Type, int>(CollidableObject.Type.StandardBlock, rightWall2.ID));

            BlockStateMachine bottomWall1 = BlockSpriteFactory.Instance.CreateBlock(CORNER_SIZE, 432, TB_WALL_WIDTH, TB_WALL_HEIGHT, BlockStateMachine.BlockType.InvisibleBlock, BlockSpriteFactory.BlockKind.FlatSquare);
            bottomWall1.IsActive = false;
            bottomWall1.IsWall = true;
            roomContent.Add(Tuple.Create<CollidableObject.Type, int>(CollidableObject.Type.StandardBlock, bottomWall1.ID));

            BlockStateMachine bottomWall2 = BlockSpriteFactory.Instance.CreateBlock(CORNER_SIZE + TB_WALL_WIDTH + 48, 432, TB_WALL_WIDTH, TB_WALL_HEIGHT, BlockStateMachine.BlockType.InvisibleBlock, BlockSpriteFactory.BlockKind.FlatSquare);
            bottomWall2.IsActive = false;
            bottomWall2.IsWall = true;
            roomContent.Add(Tuple.Create<CollidableObject.Type, int>(CollidableObject.Type.StandardBlock, bottomWall2.ID));
        }

        private BlockSpriteFactory.BlockKind GetBlockKind(BlockStateMachine.BlockType type, int direction) //direction = {0 -> left, 1 -> right, 0 -> up, 1 -> down} 
        {
            switch (type) 
            {
                case BlockType.HorizontalLockedDoor:
                    switch (direction) 
                    {
                        case 0:
                            if(MainMenu.Selection == 1) 
                            {
                                return BlockKind.LockedDoorLeft;
                            }
                            else 
                            {
                                return BlockKind.DarkLockedDoorLeft;
                            }
                        default:
                            if (MainMenu.Selection == 1)
                            {
                                return BlockKind.LockedDoorRight;
                            }
                            else
                            {
                                return BlockKind.DarkLockedDoorRight;
                            }
                    }
                case BlockType.VerticalLockedDoor:
                    switch (direction)
                    {
                        case 0:
                            if (MainMenu.Selection == 1)
                            {
                                return BlockKind.LockedDoorUp;
                            }
                            else
                            {
                                return BlockKind.DarkLockedDoorUp;
                            }
                        default:
                            if (MainMenu.Selection == 1)
                            {
                                return BlockKind.LockedDoorDown;
                            }
                            else
                            {
                                return BlockKind.DarkLockedDoorDown;
                            }
                    }
                case BlockType.HorizontalClosedDoor:
                    switch (direction)
                    {
                        case 0:
                            if (MainMenu.Selection == 1)
                            {
                                return BlockKind.ClosedDoorLeft;
                            }
                            else
                            {
                                return BlockKind.DarkClosedDoorLeft;
                            }
                        default:
                            if (MainMenu.Selection == 1)
                            {
                                return BlockKind.ClosedDoorRight;
                            }
                            else
                            {
                                return BlockKind.DarkClosedDoorRight;
                            }
                    }
                case BlockType.VerticalClosedDoor:
                    switch (direction)
                    {
                        case 0:
                            if (MainMenu.Selection == 1)
                            {
                                return BlockKind.ClosedDoorUp;
                            }
                            else
                            {
                                return BlockKind.DarkClosedDoorUp;
                            }
                        default:
                            if (MainMenu.Selection == 1)
                            {
                                return BlockKind.ClosedDoorDown;
                            }
                            else
                            {
                                return BlockKind.DarkClosedDoorDown;
                            }
                    }
                case BlockType.ExplodableDoor:
                    switch (direction)
                    {
                        case 0:
                            if (MainMenu.Selection == 1)
                            {
                                return BlockKind.WallTop;
                            }
                            else
                            {
                                return BlockKind.DarkWallTop;
                            }
                        default:
                            if (MainMenu.Selection == 1)
                            {
                                return BlockKind.WallBottom;
                            }
                            else
                            {
                                return BlockKind.DarkWallBottom;
                            }
                    }
                default:
                    return BlockKind.LockedDoorRight;
            }
        }

        private void Add11Content() 
        {
            HandleItem(385, 220 + GlobalUtilities.HEADS_UP_DISPLAY_SIZE, Item.ItemCategory.Bow);
        }

        private Tuple<string, int[], bool, bool> ParseCSVEntry(string tileName) 
        {
            string name;

            string[] unparsedSizes = new string[2];
            int[] sizes = new int[2];

            bool gap = false;
            bool teleportable = false;

            if (tileName.EndsWith("gg")) 
            {
                gap = true;
                tileName = tileName.Substring(0, tileName.Length - 2);
            }

            if (tileName.EndsWith("tt")) 
            {
                teleportable = true;
                tileName = tileName.Substring(0, tileName.Length - 2);
            }

            if (tileName.StartsWith("In") && tileName.Length > 14) 
            {
                name = tileName.Substring(0, 14);

                unparsedSizes[0] = tileName.Substring(15, tileName.IndexOf("z") - 15);
                unparsedSizes[1] = tileName.Substring(tileName.IndexOf("z") + 1, (tileName.Length - 1) - (tileName.IndexOf("z") + 1));

                sizes[0] = int.Parse(unparsedSizes[0]);
                sizes[1] = int.Parse(unparsedSizes[1]);

                return Tuple.Create<string, int[], bool, bool>(name, sizes, gap, teleportable);
            }
            return Tuple.Create<string, int[], bool, bool>(tileName, new int[] { 1, 1 }, gap, teleportable);
        }
        
        private void DetermineType()
        {
            int x, y, width, height;
            string tileName;
            bool gap, teleportable;
            foreach (int[] location in itemLocations.Keys)
            {
                x = location[0];
                y = location[1];

                tileName = itemLocations[location];

                width = GlobalUtilities.STANDARD_BLOCK_WIDTH * ParseCSVEntry(tileName).Item2[0];
                height = GlobalUtilities.STANDARD_BLOCK_HEIGHT * ParseCSVEntry(tileName).Item2[1];

                gap = ParseCSVEntry(tileName).Item3;
                teleportable = ParseCSVEntry(tileName).Item4;

                tileName = ParseCSVEntry(tileName).Item1;

                switch (tileName)
                {
                    case "Stalfos":
                        HandleStalfos(x, y);
                        break;
                    case "Darknut":
                        HandleDarknut(x, y);
                        break;
                    case "Keese":
                        HandleKeese(x, y);
                        break;
                    case "Aquamentus":
                        HandleAquamentus(x, y);
                        break;
                    case "Dodongo":
                        HandleDodongo(x, y);
                        break;
                    case "Gel":
                        HandleGel(x, y);
                        break;
                    case "BlueGel":
                        HandleBlueGel(x, y);
                        break;
                    case "Wallmaster":
                        HandleWallmaster(x, y);
                        break;
                    case "Blade":
                        HandleBlade(x, y);
                        break;
                    case "Rope":
                        HandleRope(x, y);
                        break;
                    case "MoldormSection":
                        HandleMoldormSection(x, y);
                        break;
                    case "Goriya":
                        HandleGoriya(x, y);
                        break;
                    case "BlueGoriya":
                        HandleBlueGoriya(x, y);
                        break;
                    case "Wizzrobe":
                        HandleWizzrobe(x, y);
                        break;
                    case "Pushable Block":
                        HandlePushableBlock(x, y);
                        break;
                    case "InvisibleBlock":
                        HandleInvisibleBlock(x, y, width, height, gap, teleportable);
                        break;
                    case "Key":
                        HandleItem(x, y + GlobalUtilities.HEADS_UP_DISPLAY_SIZE, Item.ItemCategory.Key);
                        break;
                    case "Bow":
                        HandleItem(x, y + GlobalUtilities.HEADS_UP_DISPLAY_SIZE, Item.ItemCategory.Bow);
                        break;
                    case "Compass":
                        HandleItem(x, y + GlobalUtilities.HEADS_UP_DISPLAY_SIZE, Item.ItemCategory.Compass);
                        break;
                    case "OrangeTriangle":
                        HandleItem(x, y + GlobalUtilities.HEADS_UP_DISPLAY_SIZE, Item.ItemCategory.OrangeTriangle);
                        break;
                    case "GoldenTicket":
                        HandleItem(x, y + GlobalUtilities.HEADS_UP_DISPLAY_SIZE, Item.ItemCategory.GoldenTicket);
                        break;
                    case "BigHeart":
                        HandleItem(x, y + GlobalUtilities.HEADS_UP_DISPLAY_SIZE, Item.ItemCategory.BigHeart);
                        break;
                    case "OldMan":
                        HandleOldMan(x, y);
                        break;
                    case "Fire":
                        HandleItem(x, y + GlobalUtilities.HEADS_UP_DISPLAY_SIZE, Item.ItemCategory.Fire);
                        break;
                }
            }
            
        }

        private void HandleWizzrobe(int x, int y)
        {
            Enemy wizzrobe = new Wizzrobe(game);
            wizzrobe.CurrentPosX = (int)(x * GlobalUtilities.Res_Scalar);
            wizzrobe.CurrentPosY = (int)(y * GlobalUtilities.Res_Scalar) + GlobalUtilities.HEADS_UP_DISPLAY_SIZE;
            wizzrobe.IsActive = false;
            roomContent.Add(Tuple.Create<CollidableObject.Type, int>(CollidableObject.Type.Enemy, wizzrobe.ID));
        }
        private void HandleOldMan(int x, int y)
        {
            BlockStateMachine block = BlockSpriteFactory.Instance.CreateBlock(x, y, BlockStateMachine.BlockType.OldMan, BlockSpriteFactory.BlockKind.OldMan);
            block.X = x;
            block.Y = y;
            block.IsActive = false;
            roomContent.Add(Tuple.Create<CollidableObject.Type, int>(CollidableObject.Type.StandardBlock, block.ID));
        }
        private void HandleStalfos(int x, int y)
        {
            Enemy stalfos = new Stalfos(game);
            stalfos.CurrentPosX = (int)(x * GlobalUtilities.Res_Scalar);
            stalfos.CurrentPosY = (int)(y * GlobalUtilities.Res_Scalar) + GlobalUtilities.HEADS_UP_DISPLAY_SIZE;
            stalfos.IsActive = false;
            roomContent.Add(Tuple.Create<CollidableObject.Type, int>(CollidableObject.Type.Enemy, stalfos.ID));
        }

        private void HandleDarknut(int x, int y)
        {
            Enemy darknut = new Darknut(game);
            darknut.CurrentPosX = (int)(x * GlobalUtilities.Res_Scalar);
            darknut.CurrentPosY = (int)(y * GlobalUtilities.Res_Scalar) + GlobalUtilities.HEADS_UP_DISPLAY_SIZE;
            darknut.IsActive = false;
            roomContent.Add(Tuple.Create<CollidableObject.Type, int>(CollidableObject.Type.Enemy, darknut.ID));
        }

        private void HandleKeese(int x, int y)
        {
            Enemy keese = new Keese(game);
            keese.CurrentPosX = (int)(x * GlobalUtilities.Res_Scalar);
            keese.CurrentPosY = (int)(y * GlobalUtilities.Res_Scalar) + GlobalUtilities.HEADS_UP_DISPLAY_SIZE;
            keese.IsActive = false;
            roomContent.Add(Tuple.Create<CollidableObject.Type, int>(CollidableObject.Type.Enemy, keese.ID));
        }

        private void HandleAquamentus(int x, int y)
        {
            Enemy dragon = new Aquamentus(game);
            dragon.CurrentPosX = (int)(x * GlobalUtilities.Res_Scalar);
            dragon.CurrentPosY = (int)(y * GlobalUtilities.Res_Scalar) + GlobalUtilities.HEADS_UP_DISPLAY_SIZE;
            dragon.IsActive = false;
            roomContent.Add(Tuple.Create<CollidableObject.Type, int>(CollidableObject.Type.Enemy, dragon.ID));
        }

        private void HandleDodongo(int x, int y)
        {
            Enemy dodongo = new Dodongo(game);
            dodongo.CurrentPosX = (int)(x * GlobalUtilities.Res_Scalar);
            dodongo.CurrentPosY = (int)(y * GlobalUtilities.Res_Scalar) + GlobalUtilities.HEADS_UP_DISPLAY_SIZE;
            dodongo.IsActive = false;
            roomContent.Add(Tuple.Create<CollidableObject.Type, int>(CollidableObject.Type.Enemy, dodongo.ID));
        }

        private void HandleBlade(int x, int y)
        {
            Enemy blade = new Blade(game);
            blade.CurrentPosX = (int)(x * GlobalUtilities.Res_Scalar);
            blade.CurrentPosY = (int)(y * GlobalUtilities.Res_Scalar) + GlobalUtilities.HEADS_UP_DISPLAY_SIZE;

            if (blade.CurrentPosY > GlobalUtilities.Resolution[1] - ((int)(GlobalUtilities.BASE_RESOLUTION_HEIGHT * GlobalUtilities.Res_Scalar) / 2)) 
            {
                blade.CurrentDirection = Enemy.Direction.Down;
            }
            else 
            {
                blade.CurrentDirection = Enemy.Direction.Up;
            }
            blade.IsActive = false;
            roomContent.Add(Tuple.Create<CollidableObject.Type, int>(CollidableObject.Type.Enemy, blade.ID));
        }

        private void HandleWallmaster(int x, int y)
        {
            Enemy wallmaster = new Wallmaster(game);
            wallmaster.CurrentPosX = (int)(x * GlobalUtilities.Res_Scalar);
            wallmaster.CurrentPosY = (int)(y * GlobalUtilities.Res_Scalar) + GlobalUtilities.HEADS_UP_DISPLAY_SIZE;
            wallmaster.IsActive = false;
            roomContent.Add(Tuple.Create<CollidableObject.Type, int>(CollidableObject.Type.Enemy, wallmaster.ID));
        }

        private void HandleGel(int x, int y)
        {
            Enemy gel = new Gel(game);
            gel.CurrentPosX = (int)(x * GlobalUtilities.Res_Scalar);
            gel.CurrentPosY = (int)(y * GlobalUtilities.Res_Scalar) + GlobalUtilities.HEADS_UP_DISPLAY_SIZE;
            gel.IsActive = false;
            roomContent.Add(Tuple.Create<CollidableObject.Type, int>(CollidableObject.Type.Enemy, gel.ID));
        }

        private void HandleBlueGel(int x, int y)
        {
            Enemy gel = new BlueGel(game);
            gel.CurrentPosX = (int)(x * GlobalUtilities.Res_Scalar);
            gel.CurrentPosY = (int)(y * GlobalUtilities.Res_Scalar) + GlobalUtilities.HEADS_UP_DISPLAY_SIZE;
            gel.IsActive = false;
            roomContent.Add(Tuple.Create<CollidableObject.Type, int>(CollidableObject.Type.Enemy, gel.ID));
        }

        private void HandleRope(int x, int y)
        {
            Enemy rope = new Rope(game);
            rope.CurrentPosX = (int)(x * GlobalUtilities.Res_Scalar);
            rope.CurrentPosY = (int)(y * GlobalUtilities.Res_Scalar) + GlobalUtilities.HEADS_UP_DISPLAY_SIZE;
            rope.IsActive = false;
            roomContent.Add(Tuple.Create<CollidableObject.Type, int>(CollidableObject.Type.Enemy, rope.ID));
        }

        private void HandleMoldormSection(int x, int y)
        {
            Enemy moldormsection = new MoldormSection(game);
            moldormsection.CurrentPosX = (int)(x * GlobalUtilities.Res_Scalar);
            moldormsection.CurrentPosY = (int)(y * GlobalUtilities.Res_Scalar) + GlobalUtilities.HEADS_UP_DISPLAY_SIZE;
            moldormsection.IsActive = false;
            roomContent.Add(Tuple.Create<CollidableObject.Type, int>(CollidableObject.Type.Enemy, moldormsection.ID));
        }

        private void HandleGoriya(int x, int y)
        {
            Enemy goriya = new Goriya(game);
            goriya.CurrentPosX = (int)(x * GlobalUtilities.Res_Scalar);
            goriya.CurrentPosY = (int)(y * GlobalUtilities.Res_Scalar) + GlobalUtilities.HEADS_UP_DISPLAY_SIZE;
            goriya.IsActive = false;
            roomContent.Add(Tuple.Create<CollidableObject.Type, int>(CollidableObject.Type.Enemy, goriya.ID));

        }

        private void HandleBlueGoriya(int x, int y)
        {
            Enemy goriya = new BlueGoriya(game);
            goriya.CurrentPosX = (int)(x * GlobalUtilities.Res_Scalar);
            goriya.CurrentPosY = (int)(y * GlobalUtilities.Res_Scalar) + GlobalUtilities.HEADS_UP_DISPLAY_SIZE;
            goriya.IsActive = false;
            roomContent.Add(Tuple.Create<CollidableObject.Type, int>(CollidableObject.Type.Enemy, goriya.ID));

        }

        private void HandlePushableBlock(int x, int y)
        {
            BlockStateMachine block = BlockSpriteFactory.Instance.CreateBlock(x, y, 48, 48, BlockStateMachine.BlockType.PushableBlock, BlockSpriteFactory.BlockKind.Pushable);
            block.X = x;
            block.Y = y;
            block.TeleportLocation = new int[2] {0,0};
            block.IsActive = false;
            roomContent.Add(Tuple.Create<CollidableObject.Type, int>(CollidableObject.Type.PushableBlock, block.ID));

        }

        private void HandleInvisibleBlock(int x, int y, int w, int h, bool gap, bool teleportable)
        {
            BlockStateMachine block = BlockSpriteFactory.Instance.CreateBlock(x, y, w, h, BlockStateMachine.BlockType.InvisibleBlock, BlockSpriteFactory.BlockKind.FlatSquare);
            block.X = x;
            block.Y = y;
            block.IsActive = false;
            block.IsGap = gap;
            block.CanTeleport = teleportable;
            block.TeleportLocation = new int[2] {GlobalUtilities.BASE_RESOLUTION_WIDTH/2-(GlobalUtilities.LINK_WIDTH), ((GlobalUtilities.BOUNDARY_BOTTOM_Y-GlobalUtilities.BOUNDARY_UP_Y)/2)+GlobalUtilities.HEADS_UP_DISPLAY_SIZE};
            if (roomName.Equals("Room10"))
            {
                block.TeleportLocation = new int[2] {140, GlobalUtilities.HEADS_UP_DISPLAY_SIZE+15};
            }
            roomContent.Add(Tuple.Create<CollidableObject.Type, int>(CollidableObject.Type.PushableBlock, block.ID));
        }

        private void HandleItem(int x, int y, Item.ItemCategory itemType)
        {
            Item item = ItemSpriteFactory.Instance.CreateItem(itemType, x, y);
            item.X = x;
            item.Y = y;
            item.IsActive = false;
            roomContent.Add(Tuple.Create<CollidableObject.Type, int>(CollidableObject.Type.Item, item.ID));
        }



    }
}
