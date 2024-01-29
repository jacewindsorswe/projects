using Microsoft.Xna.Framework.Input;
using Sprint2.Item_Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Sprint2.Link_Classes
{
    public static class LinkInventory
    {
        private static int maxHealth = 60;
        public static int MaxHealth { get { return maxHealth; } set { maxHealth = value; } }

        public static int health = maxHealth;
        public static int Health { get { return health; } }

        private static int rupees = 0;

        public static int keys = 0;

        public static int bombs = 0;

        private static int bow = 0;

        private static int boomerang = 0;

        private static int map = 0;

        private static int compass = 0;

        private static List<int[]> exploredRooms = new List<int[]>() { };
        public static List<int[]> ExploredRooms 
        {
            get { return exploredRooms; }
        }

        //Print used for debugging
        /*
        public static void PrintExploredRooms() 
        {
            foreach (int[] arr in exploredRooms)
            {
                System.Diagnostics.Debug.WriteLine(arr[0] + " " + arr[1]);
            }
        }
        */
        public static void Clear() 
        {
            rupees = 0;

            keys = 0;

            bombs = 0;

            bow = 0;

            boomerang = 0;

            map = 0;

            compass = 0;
        }
        public static void AddRoomToMemory(int x, int y) 
        {
            bool exists = false;
            foreach (int[] arr in exploredRooms) 
            {
                if (arr[0] == x && arr[1] == y) 
                {
                    exists = true;
                }
            }
            if (!exists)
            {
                exploredRooms.Add(new int[] { x, y });
            }
        }

        public static Dictionary<Item.ItemCategory, int> Inventory 
        {
            get 
            {
                return new Dictionary<Item.ItemCategory, int>()
                {
                    {  Item.ItemCategory.Rupee, rupees },
                    {  Item.ItemCategory.Key, keys },
                    {  Item.ItemCategory.Bomb, bombs },
                    {  Item.ItemCategory.Bow, bow },
                    {  Item.ItemCategory.Boomerang, boomerang },
                    {  Item.ItemCategory.GoldenTicket, map },
                    {  Item.ItemCategory.Compass, compass },
                };
            }
        }

        public static void PickupMap() 
        {
            map = 1;
        }

        public static void PickupBow()
        {
            bow = 1;
        }

        public static void PickupCompass()
        {
            compass = 1;
        }



        public static List<bool> allItems()
        {
            return new List<bool>() { boomerang > 0, (bombs > 0), bow > 0, map > 0, compass > 0 };
        }

        private static OverheadDisplay.HUDItem aItem = OverheadDisplay.HUDItem.WoodenSword;
        public static OverheadDisplay.HUDItem AItem 
        {
            get { return aItem; }
            set { aItem = value; }
        }

        private static OverheadDisplay.HUDItem bItem = OverheadDisplay.HUDItem.None;
        public static OverheadDisplay.HUDItem BItem
        {
            get { return bItem; }
            set { bItem = value; }
        }

        public static void TakeDamage(int amount) 
        {
            health -= amount;
        }
        public static void Heal(int amount) 
        {
            health += amount;
            if(health > MaxHealth) 
            {
                health = MaxHealth;
            }
        }

        public static void FullHealth()
        {
            health = MaxHealth;
        }

        public static void PickupRupees(int amount) 
        {
            rupees += amount;
        }
        public static bool SpendRupees(int amount) 
        {
            if(amount > rupees) 
            {
                return false;
            }
            rupees -= amount;
            return true;
        }

        public static void UnlockDoor() 
        {
            keys--;
        }

        public static void PickupKey() 
        {
            keys++;
        }

        public static void MaxKeys()
        {
            keys = 99;
        }

        public static void MaxBombs()
        {
            bombs = 99;
        }

        public static void ThrowBomb() 
        {
            bombs--;
        }

        public static void PickupBomb() 
        {
            bombs++;
        }
    }
}
