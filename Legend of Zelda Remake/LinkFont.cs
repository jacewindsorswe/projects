using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sprint2
{
    public static class LinkFont
    {
        private static Texture2D fontSheet;

        private static Dictionary<string, Rectangle> frameDictionary = new Dictionary<string, Rectangle>()
        {
            { "0", new Rectangle(12, 915, 9, 9) },
            { "1", new Rectangle(28, 915, 9, 9) },
            { "2", new Rectangle(44, 915, 9, 9) },
            { "3", new Rectangle(60, 915, 9, 9) },
            { "4", new Rectangle(76, 915, 9, 9) },
            { "5", new Rectangle(92, 915, 9, 9) },
            { "6", new Rectangle(108, 915, 9, 9) },
            { "7", new Rectangle(124, 915, 9, 9) },
            { "8", new Rectangle(140, 915, 9, 9) },
            { "9", new Rectangle(156, 915, 9, 9) },
            { "A", new Rectangle(172, 915, 9, 9) },
            { "B", new Rectangle(188, 915, 9, 9) },
            { "C", new Rectangle(204, 915, 9, 9) },
            { "D", new Rectangle(220, 915, 9, 9) },
            { "E", new Rectangle(236, 915, 9, 9) },
            { "F", new Rectangle(252, 915, 9, 9) },
            { "G", new Rectangle(12, 931, 9, 9) },
            { "H", new Rectangle(28, 931, 9, 9) },
            { "I", new Rectangle(44, 931, 9, 9) },
            { "J", new Rectangle(60, 931, 9, 9) },
            { "K", new Rectangle(76, 931, 9, 9) },
            { "L", new Rectangle(92, 931, 9, 9) },
            { "M", new Rectangle(108, 931, 9, 9) },
            { "N", new Rectangle(124, 931, 9, 9) },
            { "O", new Rectangle(140, 931, 9, 9) },
            { "P", new Rectangle(156, 931, 9, 9) },
            { "Q", new Rectangle(172, 931, 9, 9) },
            { "R", new Rectangle(188, 931, 9, 9) },
            { "S", new Rectangle(204, 931, 9, 9) },
            { "T", new Rectangle(220, 931, 9, 9) },
            { "U", new Rectangle(236, 931, 9, 9) },
            { "V", new Rectangle(252, 931, 9, 9) },
            { "W", new Rectangle(12, 947, 9, 9) },
            { "X", new Rectangle(28, 947, 9, 9) },
            { "Y", new Rectangle(44, 947, 9, 9) },
            { "Z", new Rectangle(60, 947, 9, 9) },
            { ",", new Rectangle(76, 947, 9, 9) },
            { "!", new Rectangle(92, 947, 9, 9) },
            { "'", new Rectangle(108, 947, 9, 9) },
            { "&", new Rectangle(124, 947, 9, 9) },
            { ".", new Rectangle(140, 947, 9, 9) },
            { "\"", new Rectangle(156, 947, 9, 9) },
            { "?", new Rectangle(172, 947, 9, 9) },
            { "-", new Rectangle(188, 947, 9, 9) },
            { " ", new Rectangle(195, 947, 9, 9) },
        };
        private static Rectangle cursorLocation = Rectangle.Empty;
        private static int startingX;
        public static Rectangle CursorLocation 
        {
            get { return cursorLocation; }
            set { cursorLocation= value; }
        }

        private static int numReturns = 1;
        private static int numPixelsBetween = 3;
        public static void LoadSheet(ContentManager content) 
        {
            fontSheet = content.Load<Texture2D>("mainmenu2");
        }

        public static void DrawText(SpriteBatch sB, string txt, Rectangle cursorPosition) 
        {
            cursorLocation.X = cursorPosition.X;
            cursorLocation.Y = cursorPosition.Y;
            cursorLocation.Width = cursorPosition.Width;
            cursorLocation.Height = cursorPosition.Height;
            for(int i = 0; i < txt.Length; i++)
            {
                sB.Draw(fontSheet, cursorLocation, frameDictionary[txt.Substring(i, 1)], Color.White);
                UpdateCursor(txt.Substring(i, 1));
            }
        }

        public static Tuple<int, int> GetSizeOfText(string txt, int width, int height) 
        {
            return Tuple.Create<int,int>((width + numPixelsBetween) * txt.Length, height);
        }
        public static void UpdateCursor(string character) 
        {
            if (character.Equals("\n")) 
            {
                cursorLocation.X = startingX;
                cursorLocation.Y = cursorLocation.Y + (cursorLocation.Height * numReturns);
                numReturns++;
            }
            else 
            {
                cursorLocation.X = cursorLocation.X + cursorLocation.Width + numPixelsBetween;
            }
        }
    }
}
