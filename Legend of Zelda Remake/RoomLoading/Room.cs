using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Sprint2;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sprint0
{
    public class Room
    {
        public List<Tuple<CollidableObject.Type, int>> roomContent;
        public Game1 game;
        public GraphicsDeviceManager graphics;
        public BackgroundSprite backgroundSprite;
        private List<Tuple<CollidableObject.Type, int>> remainingObjects = new List<Tuple<CollidableObject.Type, int>>();

        public List<Tuple<CollidableObject.Type, int>> RemainingObjects
        {
            get { return remainingObjects; }
            set { remainingObjects = value; }
        }
        public Tuple<CollidableObject.Type, int> AddRemainingObjects
        {
            set { remainingObjects.Add(value); }
        }
        private Color color = Color.White;
        private Rectangle screen;
        private Rectangle orignialScreen;

        public Room(BackgroundSprite background, List<Tuple<CollidableObject.Type, int>> content)
        {
            backgroundSprite = background;
            roomContent = content;
            
            foreach (Tuple<CollidableObject.Type, int> obj in roomContent)
            {
                remainingObjects.Add(obj);
            }      
            screen = new Rectangle(0, GlobalUtilities.HEADS_UP_DISPLAY_SIZE, (int)(GlobalUtilities.BASE_RESOLUTION_WIDTH * GlobalUtilities.Res_Scalar), (int)(GlobalUtilities.BASE_RESOLUTION_HEIGHT * GlobalUtilities.Res_Scalar));
            orignialScreen = screen;
        }
        public void Draw(SpriteBatch batch)
        {
            backgroundSprite.Draw(batch, screen, color);
        }
        public void Draw(SpriteBatch batch, Color col)
        {
            backgroundSprite.Draw(batch, screen, col);
        }
        public void MoveLeft(int offSet)
        {
            screen.X = -offSet * 25;
        }
        public void MoveRight(int offSet)
        {
            screen.X = offSet * 25;
        }
        public void MoveDown(int offSet)
        {
            screen.Y = GlobalUtilities.HEADS_UP_DISPLAY_SIZE + offSet * 20;
        }
        public void MoveUp(int offSet)
        {
            screen.Y = GlobalUtilities.HEADS_UP_DISPLAY_SIZE - offSet * 20;
        }
        public void MoveOrignial()
        {
            screen = orignialScreen;
        }
    }
}
