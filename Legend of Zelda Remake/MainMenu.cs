using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using Sprint0;
using Sprint2.Link_Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sprint2
{
    public static class MainMenu
    {
        private static readonly double scalar = 1.5;

        private static readonly int DUNGEON_WIDTH = (int)(200 * scalar);
        private static readonly int DUNGEON_HEIGHT = (int)(133 * scalar);

        private static readonly int CURSOR_WIDTH = (int)(206 * scalar);
        private static readonly int CURSOR_HEIGHT = (int)(139 * scalar);

        private static bool selected = false;
        public static bool Selected 
        {
            get { return selected; }
            set { selected = value; }
        }

        private static readonly int ANIMATE_FRAME = 2;
        private static int frameCount = 0;
        private static int curFrame = 1;

        private static readonly double SELECT_DELAY = 1.7;
        private static double timeBegin = 0;

        private static Dictionary<int, Rectangle> frameDictionary = new Dictionary<int, Rectangle>()
        {
            { 1, new Rectangle(16, 19, 200, 133) },
            { 2, new Rectangle(232, 19, 200, 133) },
        };

        private static Dictionary<int, Rectangle> selectionFrameDictionary = new Dictionary<int, Rectangle>()
        {
            { 1, new Rectangle(14, 166, 206, 139) },
            { 2, new Rectangle(232, 166, 206, 139) },
        };

        private static Texture2D dungeonSheet;

        private static int numDungeons = 2;
        private static int startX = 50;
        private static int startY = GlobalUtilities.HEADS_UP_DISPLAY_SIZE + 20;

        private static int selectionLocation = 1;
        public static int Selection 
        {
            get { return selectionLocation; }
        }

        public static void LoadContent(ContentManager content) 
        {
            dungeonSheet = content.Load<Texture2D>("mainmenu");
        }
        public static void Update(GameTime gameTime) 
        {
            if (selected) 
            {
                if(gameTime.TotalGameTime.TotalSeconds - timeBegin >= SELECT_DELAY) 
                {
                    Game1.GameState = Game1.GameStateType.Normal;
                    switch (Selection) 
                    {
                        case 1:
                            LinkInventory.AddRoomToMemory(2, 5);
                            break;
                        case 2:
                            LinkInventory.AddRoomToMemory(1, 7);
                            break;
                    }
                }
            }
        }
        public static void Draw(SpriteBatch sB) 
        {
            LinkFont.DrawText(sB, "SELECT DUNGEON", new Rectangle(((GlobalUtilities.Resolution[0] / 2) - (LinkFont.GetSizeOfText("SELECT DUNGEON", 30, 30).Item1 / 2)), 50, 30, 30));
            for(int i = 0; i < numDungeons; i++) 
            {
                sB.Draw(dungeonSheet, GetPositionRectangle(i + 1), frameDictionary[i + 1], Color.White);
                DrawLevelText(sB, i + 1);
            }
            sB.Draw(dungeonSheet, GetSelectionRectangle(), selectionFrameDictionary[GetFrame()], Color.White);
        }
        public static void MoveNext() 
        {
            if (!selected)
            {
                if (selectionLocation < numDungeons)
                {
                    selectionLocation++;
                }
            }
        }
        public static void MoveBack() 
        {
            if (!selected)
            {
                if (selectionLocation > 1)
                {
                    selectionLocation--;
                }
            }
        }

        public static void Select(GameTime gameTime) 
        {
            selected = true;
            timeBegin = gameTime.TotalGameTime.TotalSeconds;
            MediaPlayer.IsMuted = true;
        }

        private static int GetFrame() 
        {
            if(selected) 
            {
                if(frameCount % ANIMATE_FRAME == 0) 
                {
                    if(curFrame < 2) 
                    {
                        curFrame++;
                    }
                    else 
                    {
                        curFrame = 1;
                    }
                }
                frameCount++;
                return curFrame;
            }
            return 1;
        }

        private static void DrawLevelText(SpriteBatch sB, int i) 
        {
            Rectangle cursorPosition = Rectangle.Empty;
            if (i % 2 == 0)
            {
                cursorPosition = new Rectangle(GlobalUtilities.Resolution[0] - startX - DUNGEON_WIDTH + ((DUNGEON_WIDTH / 2) - (LinkFont.GetSizeOfText("LEVEL - " + i, 20, 20).Item1 / 2)), startY + (((i / 2) - 1) * DUNGEON_HEIGHT) + DUNGEON_HEIGHT + 10, 20, 20);
            }
            else
            {
                cursorPosition = new Rectangle(startX + ((DUNGEON_WIDTH / 2) - (LinkFont.GetSizeOfText("LEVEL - " + i, 20, 20).Item1 / 2)), startY + (((i / 2)) * DUNGEON_HEIGHT) + DUNGEON_HEIGHT + 10, 20, 20);
            }
            LinkFont.DrawText(sB, "LEVEL - " + i, cursorPosition);
        }

        private static Rectangle GetPositionRectangle(int rectangleNum) 
        {
            if(rectangleNum % 2 == 0) 
            {
                return new Rectangle(GlobalUtilities.Resolution[0] - startX - DUNGEON_WIDTH, startY + (((rectangleNum / 2) - 1) * DUNGEON_HEIGHT), DUNGEON_WIDTH, DUNGEON_HEIGHT);
            }
            return new Rectangle(startX, startY + (((rectangleNum / 2)) * DUNGEON_HEIGHT), DUNGEON_WIDTH, DUNGEON_HEIGHT);
        }
        private static Rectangle GetSelectionRectangle()
        {
            if (selectionLocation % 2 == 0)
            {
                return new Rectangle((GlobalUtilities.Resolution[0] - startX - DUNGEON_WIDTH) - (int)(3 * scalar), (startY + (((selectionLocation / 2) - 1) * CURSOR_HEIGHT)) - (int)(3 * scalar), CURSOR_WIDTH, CURSOR_HEIGHT);
            }
            return new Rectangle(startX - (int)(3 * scalar), startY + (((selectionLocation / 2)) * CURSOR_HEIGHT) - (int)(3 * scalar), CURSOR_WIDTH, CURSOR_HEIGHT);
        }
    }
}
