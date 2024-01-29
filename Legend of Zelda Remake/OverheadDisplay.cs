using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Sprint0;
using Sprint2.Item_Classes;
using Sprint2.Link_Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Sprint2
{
    public class OverheadDisplay
    {
        //Constants for overhead display
        public readonly int[][] X_LOCATIONS = new int[][] { new int[] { 288, 48 }, new int[] { 288, 96 }, new int[] { 288, 120 } };
        public readonly int X_SIZE = 24;

        public readonly int[][] NUMBER_LOCATIONS = new int[][] { new int[] { 312, 48 }, new int[] { 312, 96 }, new int[] { 312, 120 } };
        public readonly int NUMBER_SIZE = 24;

        public readonly int[] A_ITEM_LOCATION = new int[] { 384 , 72, 24, 48 };
        public readonly int[] B_ITEM_LOCATION = new int[] { 456, 72, 24, 48 };

        Texture2D overheadSpriteSheet;

        private static int[] roomNum = new int[2];
        public static int[] RoomNum 
        {
            get { return roomNum; }
        }

        public static List<int> xCoords1 = new(){ 55, 82, 110, 137, 165, 192 }; //Associated with the x coordinates where each green square can be drawn on the overhead map
        public static List<int> yCoords1 = new() { 62, 75, 89, 103, 117, 130 }; //Same as above but with y coords

        public static List<int> xCoords2 = new() { 66, 90, 114, 138 }; //Associated with the x coordinates where each green square can be drawn on the overhead map
        public static List<int> yCoords2 = new() { 55, 67, 79, 91, 103, 115, 127, 139 }; //Same as above but with y coords

        public static HUDItem AItem = LinkInventory.AItem, BItem = LinkInventory.BItem;

        //FRAME INFORMATION:
        public enum HUDItem 
        {
            EmptyHeart,
            HalfHeart,
            FullHeart,
            X,
            Zero,
            One,
            Two,
            Three,
            Four,
            Five,
            Six,
            Seven,
            Eight,
            Nine,
            WoodenSword,
            WoodenBoomerang,
            Bomb,
            WoodenArrow,
            MetalArrow,
            Bow,
            Compass,
            Map,
            None
        }

        public static Dictionary<HUDItem, Rectangle> HUDFrames = new()
        {
            { HUDItem.EmptyHeart, new Rectangle( 627, 117, 8, 8 ) },
            { HUDItem.HalfHeart, new Rectangle(636, 117, 8, 8) },
            { HUDItem.FullHeart, new Rectangle(645, 117, 8, 8) },
            { HUDItem.X, new Rectangle(519, 117, 8, 8) },
            { HUDItem.Zero, new Rectangle(528, 117, 8, 8) },
            { HUDItem.One, new Rectangle(537, 117, 8, 8) },
            { HUDItem.Two, new Rectangle(546, 117, 8, 8) },
            { HUDItem.Three, new Rectangle(555, 117, 8, 8) },
            { HUDItem.Four, new Rectangle(564, 117, 8, 8) },
            { HUDItem.Five, new Rectangle(573, 117, 8, 8) },
            { HUDItem.Six, new Rectangle(582, 117, 8, 8) },
            { HUDItem.Seven, new Rectangle(591, 117, 8, 8) },
            { HUDItem.Eight, new Rectangle(600, 117, 8, 8) },
            { HUDItem.Nine, new Rectangle(609, 117, 8, 8) },
            { HUDItem.WoodenSword, new Rectangle( 555, 137, 8, 16 ) },
            { HUDItem.WoodenBoomerang, new Rectangle(584, 137, 8, 16) },
            { HUDItem.Bomb, new Rectangle(604, 137, 8, 16) },
            { HUDItem.WoodenArrow, new Rectangle(615, 137, 8, 16) },
            { HUDItem.MetalArrow, new Rectangle(624, 137, 8, 16) },
            { HUDItem.Bow, new Rectangle(633, 137, 8, 16) },
            { HUDItem.Compass, new Rectangle(612, 156, 15, 16) },
            { HUDItem.Map, new Rectangle(601, 156, 8, 16) },
            { HUDItem.None, new Rectangle() }
        };
        //END FRAME INFORMATION

        public OverheadDisplay(ContentManager content)
        {
            overheadSpriteSheet = content.Load<Texture2D>("overheadhud");
        }

        public void Update(GameTime gameTime, int roomX, int roomY) 
        {
            RoomNum[0] = roomX;
            RoomNum[1] = roomY;

            BItem = LinkInventory.BItem;
        }
        public void Draw(SpriteBatch spriteBatch, int x, int y) 
        {
            spriteBatch.Draw(overheadSpriteSheet, new Rectangle(x, y, 768, 168), new Rectangle(258, 11, 256, 56), Color.White); //Draws background

            spriteBatch.Draw(overheadSpriteSheet, new Rectangle(X_LOCATIONS[0][0] + x, X_LOCATIONS[0][1] + y, X_SIZE, X_SIZE), HUDFrames[HUDItem.X], Color.White); //Draws the rupee 'X'
            spriteBatch.Draw(overheadSpriteSheet, new Rectangle(X_LOCATIONS[1][0] + x, X_LOCATIONS[1][1] + y, X_SIZE, X_SIZE), HUDFrames[HUDItem.X], Color.White); //Keys 'X'
            spriteBatch.Draw(overheadSpriteSheet, new Rectangle(X_LOCATIONS[2][0] + x, X_LOCATIONS[2][1] + y, X_SIZE, X_SIZE), HUDFrames[HUDItem.X], Color.White); //Bombs 'X'

            DrawNumber(spriteBatch, LinkInventory.Inventory[Item.ItemCategory.Rupee], NUMBER_LOCATIONS[0][0] + x, NUMBER_LOCATIONS[0][1] + y);
            DrawNumber(spriteBatch, LinkInventory.Inventory[Item.ItemCategory.Key], NUMBER_LOCATIONS[1][0] + x, NUMBER_LOCATIONS[1][1] + y);
            DrawNumber(spriteBatch, LinkInventory.Inventory[Item.ItemCategory.Bomb], NUMBER_LOCATIONS[2][0] + x, NUMBER_LOCATIONS[2][1] + y);

            spriteBatch.Draw(overheadSpriteSheet, new Rectangle(A_ITEM_LOCATION[0] + x, A_ITEM_LOCATION[1] + y, A_ITEM_LOCATION[2], A_ITEM_LOCATION[3]), HUDFrames[BItem], Color.White); //Draws BItem
            spriteBatch.Draw(overheadSpriteSheet, new Rectangle(B_ITEM_LOCATION[0] + x, B_ITEM_LOCATION[1] + y, B_ITEM_LOCATION[2], B_ITEM_LOCATION[3]), HUDFrames[AItem], Color.White); //Draws AItem

            DrawHealth(spriteBatch, (int)((LinkInventory.MaxHealth / 20.0) + 0.5), 528 + x, 120 + y);

            DrawMap(spriteBatch, x , y);

            if (MainMenu.Selection == 1) //Draws level number
            {
                spriteBatch.Draw(overheadSpriteSheet, new Rectangle(192, 24, 24, 24), HUDFrames[HUDItem.One], Color.White);
            }
            else
            {
                spriteBatch.Draw(overheadSpriteSheet, new Rectangle(192, 24, 24, 24), HUDFrames[HUDItem.Two], Color.White);
            }
        }

        private void DrawMap(SpriteBatch sB, int xOffset, int yOffset) 
        {
            if (LinkInventory.Inventory[Item.ItemCategory.GoldenTicket] > 0)
            {
                if (MainMenu.Selection == 1)
                {
                    if (roomNum[1] != 6)
                    {
                        sB.Draw(overheadSpriteSheet, new Rectangle(48 + xOffset, 48 + yOffset, 164, 96), new Rectangle(354, 74, 48, 28), Color.White); //Map background

                        sB.Draw(overheadSpriteSheet, new Rectangle(xCoords1[roomNum[0]] + xOffset, yCoords1[roomNum[1]] + yOffset, 10, 10), new Rectangle(519, 126, 3, 3), Color.White); //Green square
                    }
                    if (LinkInventory.Inventory[Item.ItemCategory.Compass] > 0)
                    {
                        sB.Draw(overheadSpriteSheet, new Rectangle(xCoords1[4] + xOffset, yCoords1[1] + yOffset, 10, 10), new Rectangle(528, 126, 3, 3), Color.White);
                    }
                }
                else 
                {
                    sB.Draw(overheadSpriteSheet, new Rectangle(60 + xOffset, 55 + yOffset, 96, 96), new Rectangle(441, 72, 32, 32), Color.White); //Map background
                    sB.Draw(overheadSpriteSheet, new Rectangle(xCoords2[roomNum[0]] + xOffset, yCoords2[roomNum[1]] + yOffset, 9, 9), new Rectangle(519, 126, 3, 3), Color.White); //Green square
                    if (LinkInventory.Inventory[Item.ItemCategory.Compass] > 0)
                    {
                        sB.Draw(overheadSpriteSheet, new Rectangle(xCoords2[2] + xOffset, yCoords2[0] + yOffset, 9, 9), new Rectangle(528, 126, 3, 3), Color.White);
                    }
                }
            }
        }

        private void DrawNumber(SpriteBatch sB, int number, int x, int y) 
        {
            Rectangle positionRectangle = new (x, y, 24, 24);
            Rectangle frameRectangle = HUDFrames[parseInt(number)[0]];
            sB.Draw(overheadSpriteSheet, positionRectangle, frameRectangle, Color.White);

            positionRectangle = new (x + 24, y, 24, 24);
            frameRectangle = HUDFrames[parseInt(number)[1]];
            sB.Draw(overheadSpriteSheet, positionRectangle, frameRectangle, Color.White);
        }
        private void DrawHealth(SpriteBatch sB, int numHearts, int x, int y) 
        {
            Rectangle positionRectangle;
            Rectangle frameRectangle;

            int originalX = x;

            int fullHeartAmount = LinkInventory.MaxHealth / numHearts;
            int halfHeartAmount = fullHeartAmount / 2;

            int i;

            for (i = fullHeartAmount; i <= LinkInventory.Health; i += fullHeartAmount) 
            {
                x = (((i / fullHeartAmount) - 1) * 24) + originalX;
                positionRectangle = new(x, y, 24, 24);
                frameRectangle = HUDFrames[HUDItem.FullHeart];
                sB.Draw(overheadSpriteSheet, positionRectangle, frameRectangle, Color.White);
            }
            if(i - LinkInventory.Health > 0 && i - LinkInventory.Health <= halfHeartAmount) 
            {
                x = (((i / fullHeartAmount) - 1) * 24) + originalX;
                positionRectangle = new(x, y, 24, 24);
                frameRectangle = HUDFrames[HUDItem.HalfHeart];
                sB.Draw(overheadSpriteSheet, positionRectangle, frameRectangle, Color.White);
                i += fullHeartAmount;
            }
            while(i <= LinkInventory.MaxHealth) 
            {
                x = (((i / fullHeartAmount) - 1) * 24) + originalX;
                positionRectangle = new(x, y, 24, 24);
                frameRectangle = HUDFrames[HUDItem.EmptyHeart];
                sB.Draw(overheadSpriteSheet, positionRectangle, frameRectangle, Color.White);
                i += fullHeartAmount;
            }
        }

        private HUDItem[] parseInt(int number) 
        {
            return new HUDItem[] { getDigit(number / 10), getDigit(number - ((number / 10) * 10)) };
        }

        private HUDItem getDigit(int digit) 
        {
            switch (digit) 
            {
                case 0:
                    return HUDItem.Zero;
                case 1:
                    return HUDItem.One;
                case 2:
                    return HUDItem.Two;
                case 3:
                    return HUDItem.Three;
                case 4:
                    return HUDItem.Four;
                case 5:
                    return HUDItem.Five;
                case 6:
                    return HUDItem.Six;
                case 7:
                    return HUDItem.Seven;
                case 8:
                    return HUDItem.Eight;
                default:
                    return HUDItem.Nine;
            }
        }
    }
}
