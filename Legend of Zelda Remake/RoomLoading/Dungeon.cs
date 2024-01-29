using Microsoft.Xna.Framework;
using Sprint0;
using Sprint2;
using Sprint2.Block_Classes;
using Sprint2.Item_Classes;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sprint0
{
    public class Dungeon
    {
        private Game1 game;
        private Room[,] dungeon = new Room[7, 8];
        private Room room;
        private Room prevRoom;
        private int prevRoomFrames = 0;
        private int offSet = 0;
        private int curRoomNum = 17;
        private int[] roomPos = new int[2] { 2, 6 };
        private BackgroundSprite background;
        private int screenX;
        private int screenY;
        //--------------------------------------Left---Right---Up----Down
        private bool[] direction = new bool[4] { false, false, false, false };

        public Room[,] RoomArray { get { return dungeon; } }

        public int[] RoomPos 
        {
            get { return roomPos; }
            set { roomPos = value; }
        }
        public int RoomPosX
        {
            get { return roomPos[0]; }
            set { roomPos[0] = value; }
        }
        public int RoomPosY
        {
            get { return roomPos[1]; }
            set { roomPos[1] = value; }
        }

        private bool moving = false;
        public bool Moving 
        {
            get { return moving; }
            set { moving = value; }
        }
        private Dictionary<String, int[]> fileNames1 = new Dictionary<String, int[]>()
        {
            {"Room00.csv", new int[] {0,0}},
            {"Room11.csv", new int[] {1,1}},
            {"Room10.csv", new int[] {1,0}},
            {"Room20.csv", new int[] {2,0}},
            {"Room21.csv", new int[] {2,1}},
            {"Room41.csv", new int[] {4,1}},
            {"Room51.csv", new int[] {5,1}},
            {"Room02.csv", new int[] {0,2}},
            {"Room12.csv", new int[] {1,2}},
            {"Room22.csv", new int[] {2,2}},
            {"Room32.csv", new int[] {3,2}},
            {"Room42.csv", new int[] {4,2}},
            {"Room13.csv", new int[] {1,3}},
            {"Room23.csv", new int[] {2,3}},
            {"Room33.csv", new int[] {3,3}},
            {"Room24.csv", new int[] {2,4}},
            {"Room15.csv", new int[] {1,5}},
            {"Room25.csv", new int[] {2,5}},
            {"Room35.csv", new int[] {3,5} },
            {"Room26.csv", new int[] {2,6} }
        };
        private Dictionary<String, int[]> fileNames2 = new Dictionary<String, int[]>()
        {
            {"Dungeon2Files\\Room210.csv", new int[] {1,0}},
            {"Dungeon2Files\\Room220.csv", new int[] {2,0}},
            {"Dungeon2Files\\Room221.csv", new int[] {2,1}},
            {"Dungeon2Files\\Room231.csv", new int[] {3,1}},
            {"Dungeon2Files\\Room222.csv", new int[] {2,2}},
            {"Dungeon2Files\\Room232.csv", new int[] {3,2}},
            {"Dungeon2Files\\Room223.csv", new int[] {2,3}},
            {"Dungeon2Files\\Room233.csv", new int[] {3,3}},
            {"Dungeon2Files\\Room224.csv", new int[] {2,4}},
            {"Dungeon2Files\\Room234.csv", new int[] {3,4}},
            {"Dungeon2Files\\Room225.csv", new int[] {2,5}},
            {"Dungeon2Files\\Room235.csv", new int[] {3,5}},
            {"Dungeon2Files\\Room206.csv", new int[] {0,6}},
            {"Dungeon2Files\\Room216.csv", new int[] {1,6}},
            {"Dungeon2Files\\Room226.csv", new int[] {2,6}},
            {"Dungeon2Files\\Room236.csv", new int[] {3,6}},
            {"Dungeon2Files\\Room217.csv", new int[] {1,7}},
            {"Dungeon2Files\\Room227.csv", new int[] {2,7}}
        };
        public Dungeon(Game1 game)
        {
            this.game = game;
            screenX = game.MiddleX * 2;
            screenY = game.MiddleY * 2; //Do these do anything? --Nolan

        }
        /*public void Load()
        {
            int[] posHold = new int[2];
            foreach(String roomName in fileNames1.Keys)
            {
                fileNames1.TryGetValue(roomName, out posHold);
                room = LoadCSVs.Instance.LoadRoom(roomName, posHold, game);
                dungeon[posHold[0], posHold[1]] = room;
            }
            room = dungeon[2, 5];
            RoomPos = new int[2] { 2, 5 };
            Spawn();
        }*/

        public void Load(int dNum)
        {
            int[] posHold = new int[2];
            switch (dNum)
            {
                case 1:
                    foreach (String roomName in fileNames1.Keys)
                    {
                        fileNames1.TryGetValue(roomName, out posHold);
                        room = LoadCSVs.Instance.LoadRoom(roomName, posHold, game);
                        dungeon[posHold[0], posHold[1]] = room;
                    }
                    room = dungeon[2, 5];
                    RoomPos = new int[2] { 2, 5 };
                    Spawn();
                    break;
                case 2:
                    foreach (String roomName in fileNames2.Keys)
                    {
                        fileNames2.TryGetValue(roomName, out posHold);
                        room = LoadCSVs.Instance.LoadRoom(roomName, posHold, game);
                        dungeon[posHold[0], posHold[1]] = room;
                        //System.Diagnostics.Debug.WriteLine(roomName);
                        //System.Diagnostics.Debug.WriteLine(posHold[0] + " " + posHold[1]);
                    }
                    room = dungeon[1, 7];
                    RoomPos = new int[2] { 1, 7 };
                    Spawn();
                    break;
            }
        }
        public void Update()
        {
            room = dungeon[roomPos[0], roomPos[1]];
        }

        public void Draw()
        {
            if (prevRoomFrames > 0)
            {
                if (direction[0] || direction[1] || direction[2] || direction[3]) 
                {
                    Moving = true;
                }
                if (direction[0])
                {
                    prevRoom.MoveRight(30 - prevRoomFrames);
                    room.MoveLeft(prevRoomFrames);
                    room.Draw(game._spriteBatch);
                    prevRoom.Draw(game._spriteBatch);
                }
                else if (direction[1])
                {
                    prevRoom.MoveLeft(30 - prevRoomFrames);
                    room.MoveRight(prevRoomFrames);
                    room.Draw(game._spriteBatch);
                    prevRoom.Draw(game._spriteBatch);
                }
                else if (direction[2])
                {
                    prevRoom.MoveDown(30 - prevRoomFrames);
                    room.MoveUp(prevRoomFrames);
                    room.Draw(game._spriteBatch);
                    prevRoom.Draw(game._spriteBatch);
                }
                else if (direction[3])
                {
                    prevRoom.MoveUp(30 - prevRoomFrames);
                    room.MoveDown(prevRoomFrames);
                    room.Draw(game._spriteBatch);
                    prevRoom.Draw(game._spriteBatch);
                }
                prevRoomFrames--;
            }
            else 
            {
                direction[0] = false;
                direction[1] = false;
                direction[2] = false;
                direction[3] = false;
                room.MoveOrignial();
                Spawn();
                room.Draw(game._spriteBatch);
                game.link1.Invisible = false;
                Moving = false;
            }
        }
        public void Draw(Color col)
        {
            Moving = true;
            room.Draw(game._spriteBatch, col);
        }

        public void ChangeRoomUp()
        {
            if (RoomPosX != 0 || RoomPosY != 0)
            {
                prevRoomFrames = 30;
                prevRoom = room;
                Despawn();
                //DespawnAndSpawn(0, -1); //dungeon built w coord system similar to monogame coord -
                                        //leftmost x, bottommost y (therefore subtract y to go up and x to go left)
                direction[2] = true;
                RoomPosY = RoomPos[1] - 1;
            }
        }
        public void ChangeRoomDown()
        {
            if (RoomPosX != 0 || RoomPosY != 0)
            {
                prevRoomFrames = 30;
                prevRoom=room;
                Despawn();
                //DespawnAndSpawn(0, 1);
                direction[3] = true;
                RoomPosY = RoomPos[1] + 1;
            }
        }
        public void ChangeRoomLeft()
        {
            prevRoomFrames = 30;
            prevRoom = room;
            Despawn();
            //DespawnAndSpawn(-1, 0);
            direction[0] = true;
            RoomPosX = RoomPos[0] - 1;
        }
        public void ChangeRoomRight()
        {
            prevRoomFrames = 30;
            prevRoom = room;
            Despawn();
            //DespawnAndSpawn(1, 0);
            direction[1] = true;
            RoomPosX = RoomPos[0] + 1;

        }
        public void Despawn()
        {
            foreach (Tuple<CollidableObject.Type, int> obj in dungeon[RoomPos[0], RoomPos[1]].RemainingObjects)
            {
                if (ObjectExists(obj.Item1, obj.Item2))
                {
                    GlobalUtilities.GetCollidable(obj.Item1, obj.Item2).IsActive = false;
                }
            }

            for(int i = 0; i < (Projectile.ProjectileDictionary.Keys.ToList()).Count; i++) 
            {
                Projectile.RemoveProjectile(Projectile.ProjectileDictionary.Keys.ToList()[i]);
                i -= 1;
            }
            for (int i = 0; i < (Item.ItemDictionary.Keys.ToList()).Count; i++)
            {
                GlobalUtilities.GetCollidable(CollidableObject.Type.Item, (Item.ItemDictionary.Keys.ToList())[i]).IsActive = false;
            }

        }
        private void Spawn2(int xMod, int yMod)
        {
            RoomPosX = RoomPos[0] + xMod;
            RoomPosY = RoomPos[1] + yMod;
            foreach (Tuple<CollidableObject.Type, int> obj in dungeon[RoomPos[0], RoomPos[1]].RemainingObjects)
            {
                if (ObjectExists(obj.Item1, obj.Item2))
                {
                    //If included, this if statement will break the door generation--it has a skeleton in this method as to how to deal with getting rid of
                    //doors if Link unlocked the one on the other side
                    //if (DoesDependentExist(obj))
                    //{
                    GlobalUtilities.GetCollidable(obj.Item1, obj.Item2).IsActive = true;
                    //}
                }
            }
        }
        //private void DespawnAndSpawn(int xModifier, int yModifier) 
        //{
        //    foreach (Tuple<CollidableObject.Type, int> obj in dungeon[RoomPos[0], RoomPos[1]].RemainingObjects)
        //    {
        //        if (ObjectExists(obj.Item1, obj.Item2))
        //        {
        //            GlobalUtilities.GetCollidable(obj.Item1, obj.Item2).IsActive = false;
        //        }
        //    }
        //    RoomPosX = RoomPos[0] + xModifier;
        //    RoomPosY = RoomPos[1] + yModifier;
        //    foreach (Tuple<CollidableObject.Type, int> obj in dungeon[RoomPos[0], RoomPos[1]].RemainingObjects)
        //    {
        //        if (ObjectExists(obj.Item1, obj.Item2))
        //        {
        //            //If included, this if statement will break the door generation--it has a skeleton in this method as to how to deal with getting rid of
        //            //doors if Link unlocked the one on the other side
        //            //if (DoesDependentExist(obj))
        //            //{
        //                GlobalUtilities.GetCollidable(obj.Item1, obj.Item2).IsActive = true;
        //            //}
        //        }
        //    }
        //    
        //}
        private void Spawn()
        {
            foreach (Tuple<CollidableObject.Type, int> obj in dungeon[RoomPos[0], RoomPos[1]].RemainingObjects)
            {
                if (ObjectExists(obj.Item1, obj.Item2) && !Game1.idList.Contains(obj.Item2))
                {
                    GlobalUtilities.GetCollidable(obj.Item1, obj.Item2).IsActive = true;
                }
            }
        }
        public static bool ObjectExists(CollidableObject.Type type, int id) 
        {
            bool existence = false;
            switch (type) 
            {
                case CollidableObject.Type.PushableBlock:
                    if (BlockStateMachine.BlockDictionary.ContainsKey(id)) 
                    {
                        existence = true;
                    }
                    break;
                case CollidableObject.Type.StandardBlock:
                    if (BlockStateMachine.BlockDictionary.ContainsKey(id))
                    {
                        existence = true;
                    }
                    break;
                case CollidableObject.Type.Enemy:
                    if (Enemy.EnemiesDictionary.ContainsKey(id))
                    {
                        existence = true;
                    }
                    break;
                case CollidableObject.Type.Link:
                    if (Link.LinkDictionary.ContainsKey(id))
                    {
                        existence = true;
                    }
                    break;
                case CollidableObject.Type.Item:
                    if (Item.ItemDictionary.ContainsKey(id))
                    {
                        existence = true;
                    }
                    break;
                case CollidableObject.Type.Projectile:
                    if (Projectile.ProjectileDictionary.ContainsKey(id)) 
                    {
                        existence = true;
                    }
                    break;
            }
            return existence;
        }

        public static LinkStateMachine.Direction CoordinatesToDirection(int x, int y, int height, int width) 
        {
            int middleX = GlobalUtilities.Resolution[0] / 2;
            int middleY = (GlobalUtilities.Resolution[1] - GlobalUtilities.HEADS_UP_DISPLAY_SIZE) / 2 + GlobalUtilities.HEADS_UP_DISPLAY_SIZE;

            if(height > width) 
            {
                if(x < middleX) 
                {
                    return LinkStateMachine.Direction.Left;
                }
                else 
                {
                    return LinkStateMachine.Direction.Right;
                }
            }
            else 
            {
                if(y < middleY) 
                {
                    return LinkStateMachine.Direction.Up;
                }
                else 
                {
                    return LinkStateMachine.Direction.Down;
                }
            }
        }

        private int[] DirectionToModifiers(LinkStateMachine.Direction direction) 
        {
            int x, y;
            switch (direction) 
            {
                case LinkStateMachine.Direction.Left:
                    x = -1;
                    y = 0;
                    break;
                case LinkStateMachine.Direction.Right:
                    x = 1;
                    y = 0;
                    break;
                case LinkStateMachine.Direction.Up:
                    x = 0;
                    y = -1;
                    break;
                default:
                    x = 0;
                    y = 1;
                    break;
            }
            return new[] { x, y };
        }

        










    }
}
