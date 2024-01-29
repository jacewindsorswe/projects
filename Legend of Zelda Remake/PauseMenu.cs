using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sprint2
{
    public static class PauseMenu
    {
        private static readonly Rectangle PAUSED_TEXT_LOCATION = new Rectangle((GlobalUtilities.Resolution[0] / 2) - (LinkFont.GetSizeOfText("PAUSED", 50, 50).Item1 / 2), (GlobalUtilities.HEADS_UP_DISPLAY_SIZE + ((GlobalUtilities.Resolution[1] - GlobalUtilities.HEADS_UP_DISPLAY_SIZE) / 2)) - (LinkFont.GetSizeOfText("PAUSED", 50, 50)).Item2 / 2, 50, 50);

        private static readonly List<Rectangle> selectionPositions = new List<Rectangle>()
        {
            new Rectangle(PAUSED_TEXT_LOCATION.X - 48, PAUSED_TEXT_LOCATION.Y + PAUSED_TEXT_LOCATION.Height + 10, 48, 21),
            new Rectangle(PAUSED_TEXT_LOCATION.X - 48, PAUSED_TEXT_LOCATION.Y + PAUSED_TEXT_LOCATION.Height + 50, 48, 21),
            new Rectangle(PAUSED_TEXT_LOCATION.X - 48, PAUSED_TEXT_LOCATION.Y + PAUSED_TEXT_LOCATION.Height + 90, 48, 21)
        };

        private static Rectangle selectionArrowFrame = new Rectangle(161, 192, 48, 21);
        private static int currentSelection = 0;

        private static bool selected = false;
        public static bool Selected 
        {
            get { return selected; }
        }

        public static int Selection 
        {
            get { return currentSelection; }
        }

        public static void Draw(SpriteBatch sB, Texture2D cursorTexture) 
        {
            Rectangle cursorPosition = new Rectangle(PAUSED_TEXT_LOCATION.X, PAUSED_TEXT_LOCATION.Y, PAUSED_TEXT_LOCATION.Width, PAUSED_TEXT_LOCATION.Height);
            LinkFont.DrawText(sB, "PAUSED", cursorPosition);
            cursorPosition = new Rectangle(cursorPosition.X, cursorPosition.Y + cursorPosition.Height + 10, 30, 30);
            LinkFont.DrawText(sB, "CONTINUE", cursorPosition);
            cursorPosition = new Rectangle(cursorPosition.X, cursorPosition.Y + cursorPosition.Height + 10, 30, 30);
            LinkFont.DrawText(sB, "MAIN MENU", cursorPosition);
            cursorPosition = new Rectangle(cursorPosition.X, cursorPosition.Y + cursorPosition.Height + 10, 30, 30);
            LinkFont.DrawText(sB, "TOGGLE CHEATS", cursorPosition);

            sB.Draw(cursorTexture, selectionPositions[Selection], selectionArrowFrame, Color.White);
        }

        public static void MoveNext() 
        {
            if(currentSelection == selectionPositions.Count - 1) 
            {
                currentSelection = 0;
            }
            else 
            {
                currentSelection++;
            }
        }

        public static void MoveBack() 
        {
            if(currentSelection == 0) 
            {
                currentSelection = selectionPositions.Count - 1;
            }
            else 
            {
                currentSelection--;
            }
        }

        public static void Select() 
        {
            selected = true;
        }
    }
}
