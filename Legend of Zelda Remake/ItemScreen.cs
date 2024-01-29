using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Sprint2.Link_Classes;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using static Sprint2.Item_Classes.Item;

namespace Sprint2
{
    public class ItemScreen
    {
        private OverheadDisplay overhead;
        private Texture2D overheadSpriteSheet;

        public const int MAX_X_COORD = 528;
        public const int MIN_X_COORD = 384;

        private static int[] bItemCoords = { 384, 144, 48, 48 };
        public static int[] BItemCoords 
        {
            get { return bItemCoords; }
        }

        private static Dictionary<int[], OverheadDisplay.HUDItem> coordsToItem = new(new CompareArrays())
        {
            { new int[]{ 384, 144 }, OverheadDisplay.HUDItem.WoodenBoomerang },
            { new int[]{ 456, 144 }, OverheadDisplay.HUDItem.Bomb },
            { new int[]{ 528, 144 }, OverheadDisplay.HUDItem.Bow }
        };

        private class CompareArrays : IEqualityComparer<int[]>
        {
            public bool Equals(int[] x, int[] y)
            {
                if (x[0] == y[0] && x[1] == y[1]) 
                {
                    return true;
                }
                return false;
            }

            public int GetHashCode([DisallowNull] int[] obj)
            {
                return 0;
            }
        }

        //Coords stored here are for red selection box
        private static Dictionary<OverheadDisplay.HUDItem, int[]> inventoryBoxes = new()
        {
            { OverheadDisplay.HUDItem.WoodenBoomerang, new int[]{ 384, 144 } },
            { OverheadDisplay.HUDItem.Bomb, new int[]{ 456, 144 } },
            { OverheadDisplay.HUDItem.Bow, new int[]{ 528, 144 } },
            { OverheadDisplay.HUDItem.WoodenArrow, new int[]{ 528, 144 } }
        };

        //Coords stored here are for the actual rendering of items in the menu
        private static Dictionary<OverheadDisplay.HUDItem, Rectangle> itemToCoords = new()
        {
            { OverheadDisplay.HUDItem.WoodenBoomerang, new Rectangle ( 396, 144, 24, 48 ) },
            { OverheadDisplay.HUDItem.Bomb, new Rectangle(468, 144, 24, 48) },
            { OverheadDisplay.HUDItem.WoodenArrow, new Rectangle(528, 144, 24, 48) },
            { OverheadDisplay.HUDItem.Bow, new Rectangle(552, 144, 24, 48) },
            { OverheadDisplay.HUDItem.Map, new Rectangle(144, 336, 24, 48) },
            { OverheadDisplay.HUDItem.Compass, new Rectangle(132, 456, 45, 48) }
        };

        private int[] redFrame = new int[] { 519, 137, 16, 16 };
        private int[] blueFrame = new int[] { 536, 137, 16, 16 };

        public Dictionary<int[], OverheadDisplay.HUDItem> CoordsToItem 
        {
            get { return coordsToItem; }
        }
        public Dictionary<OverheadDisplay.HUDItem, Rectangle> ItemToCoords
        {
            get { return itemToCoords; }
        }

        public ItemScreen(OverheadDisplay oD, ContentManager content) 
        {
            overhead = oD;
            overheadSpriteSheet = content.Load<Texture2D>("overheadhud");
        }

        public void Update(GameTime gameTime) 
        {

        }

        public void Draw(SpriteBatch sB) 
        { //528 <-- Beginning y
            overhead.Draw(sB, 0, 528); //draws the overhead display on the bottom of the screen

            sB.Draw(overheadSpriteSheet, new Rectangle(0, 0, 768, 264), new Rectangle(1, 11, 256, 88), Color.White); //Draws the top half of the item screen
            sB.Draw(overheadSpriteSheet, new Rectangle(0, 264, 768, 264), new Rectangle(258, 112, 256, 88), Color.White); //Draws the bottom half of the item screen

            DrawItemsInInventory(sB); 

            Rectangle frame = OverheadDisplay.HUDFrames[LinkInventory.BItem];
            sB.Draw(overheadSpriteSheet, new Rectangle(204, 144, 24, 45), frame, Color.White); //Draws the BItem in the appropriate position in the item screen (near top left)

            sB.Draw(overheadSpriteSheet, new Rectangle(bItemCoords[0], bItemCoords[1], bItemCoords[2], bItemCoords[3]), new Rectangle(redFrame[0], redFrame[1], redFrame[2], redFrame[3]), Color.White); //Draws the red selection frame appropriately
            
            DrawMap(sB);
        }
        private void DrawItemsInInventory(SpriteBatch sB)
        {
            List<bool> items = LinkInventory.allItems();
            for(int i = 0; i < items.Count; i++) 
            {
                if (items[i])
                {
                    DrawItem(sB, i);
                }
            }
        }
        private void DrawItem(SpriteBatch sB, int i) 
        {
            Rectangle pos, pos2, frame, frame2;
            OverheadDisplay.HUDItem itemType, itemType2 = OverheadDisplay.HUDItem.WoodenArrow;
            switch (i) 
            {
                case 0:
                    itemType = OverheadDisplay.HUDItem.WoodenBoomerang;
                    break;
                case 1:
                    itemType = OverheadDisplay.HUDItem.Bomb;
                    break;
                case 2:
                    itemType = OverheadDisplay.HUDItem.WoodenArrow;
                    itemType2 = OverheadDisplay.HUDItem.Bow;
                    break;
                case 3:
                    itemType = OverheadDisplay.HUDItem.Map;
                    break;
                default:
                    itemType = OverheadDisplay.HUDItem.Compass;
                    break;
            }
            if (i != 2)
            {
                pos = itemToCoords[itemType];
                frame = OverheadDisplay.HUDFrames[itemType];
                sB.Draw(overheadSpriteSheet, pos, frame, Color.White);
            }
            else 
            {
                pos = itemToCoords[itemType];
                frame = OverheadDisplay.HUDFrames[itemType];
                sB.Draw(overheadSpriteSheet, pos, frame, Color.White);

                pos2 = itemToCoords[itemType2];
                frame2 = OverheadDisplay.HUDFrames[itemType2];
                sB.Draw(overheadSpriteSheet, pos2, frame2, Color.White);
            }
        }

        private const int frameX = 519, frameY = 108, frameSize = 8;

        public static Dictionary<int[], int> pairToCoords1 = new(new CompareArrays())
        {
            { new int[]{ 1, 0 }, 1 },
            { new int[]{ 1, 1 }, 0 },
            { new int[]{ 2, 0 }, 6 },
            { new int[]{ 2, 1 }, 12 },
            { new int[]{ 4, 1 }, 5 },
            { new int[]{ 5, 1 }, 2 },
            { new int[]{ 0, 2 }, 1 },
            { new int[]{ 1, 2 }, 7 },
            { new int[]{ 2, 2 }, 15 },
            { new int[]{ 3, 2 }, 7 },
            { new int[]{ 4, 2 }, 10 },
            { new int[]{ 1, 3 }, 9 },
            { new int[]{ 2, 3 }, 15 },
            { new int[]{ 3, 3 }, 10 },
            { new int[]{ 2, 4 }, 12 },
            { new int[]{ 1, 5 }, 1 },
            { new int[]{ 2, 5 }, 11 },
            { new int[]{ 3, 5 }, 2 },
        };

        public static Dictionary<int[], int> pairToCoords2 = new(new CompareArrays())
        {
            { new int[]{ 1, 0 }, 1 },
            { new int[]{ 2, 0 }, 0 },
            { new int[]{ 2, 1 }, 6 },
            { new int[]{ 3, 1 }, 4 },
            { new int[]{ 2, 2 }, 13 },
            { new int[]{ 3, 2 }, 14 },
            { new int[]{ 2, 3 }, 13 },
            { new int[]{ 3, 3 }, 14 },
            { new int[]{ 2, 4 }, 13 },
            { new int[]{ 3, 4 }, 14 },
            { new int[]{ 2, 5 }, 13 },
            { new int[]{ 3, 5 }, 14 },
            { new int[]{ 0, 6 }, 1 },
            { new int[]{ 1, 6 }, 7 },
            { new int[]{ 2, 6 }, 15 },
            { new int[]{ 3, 6 }, 10 },
            { new int[]{ 1, 7 }, 13 },
            { new int[]{ 2, 7 }, 10 },
        };

        private void DrawMap(SpriteBatch sB) 
        {
            int startX = 384, startY = 288;
            int mapSize = 24;

            Rectangle pos, frame;
            foreach (int[] coords in LinkInventory.ExploredRooms) 
            {
                pos = new Rectangle(startX + (mapSize * (coords[0] + 1)), startY + (mapSize * (coords[1] + 1)), mapSize, mapSize);
                if (MainMenu.Selection == 1)
                {
                    frame = new Rectangle((frameX + (pairToCoords1[coords] * (frameSize + 1))), frameY, frameSize, frameSize);
                }
                else 
                {
                    frame = new Rectangle((frameX + (pairToCoords2[coords] * (frameSize + 1))), frameY, frameSize, frameSize);
                }
                sB.Draw(overheadSpriteSheet, pos, frame, Color.White);
            }
        }

        //Move the selection box around
        public static void MoveRight() 
        {
            if (bItemCoords[0] < MAX_X_COORD)
            {
                bItemCoords[0] += 72;
            }
        }
        public static void MoveLeft() 
        {
            if (bItemCoords[0] > MIN_X_COORD) 
            {
                bItemCoords[0] -= 72;
            }
        }

        public static void Select() 
        {
            //System.Diagnostics.Debug.WriteLine(bItemCoords[0] + " " + bItemCoords[1]);
            if (doesLinkHaveItem(coordsToItem[new int[] { bItemCoords[0], bItemCoords[1] }])) 
            {
                LinkInventory.BItem = coordsToItem[new int[] { bItemCoords[0], bItemCoords[1] }];
            }
        }

        private static bool doesLinkHaveItem(OverheadDisplay.HUDItem item) 
        {
            switch (item) 
            {
                case OverheadDisplay.HUDItem.WoodenBoomerang:
                    return LinkInventory.Inventory[Item_Classes.Item.ItemCategory.Boomerang] > 0;
                case OverheadDisplay.HUDItem.Bomb:
                    return LinkInventory.Inventory[Item_Classes.Item.ItemCategory.Bomb] > 0;
                case OverheadDisplay.HUDItem.Bow:
                    return LinkInventory.Inventory[Item_Classes.Item.ItemCategory.Bow] > 0;
                default:
                    return false;
            }
        }
    }
}
