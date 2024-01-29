using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Sprint0;
using Sprint2.Item_Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sprint2.Interfaces
{
    public interface IItemState
    {
        public bool Exists { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public int Speed { get; }

        public int Damage { get; }

        public bool IsActive { get; set; }
        public Item.Direction FrameDirection { get; set; }
        public void Pickup();
        public void Update(GameTime gT);
        public bool Update(GameTime gT, double cT, bool flip);
        public void Draw(SpriteBatch spriteBatch, Color color);
        public void UpdateTexture(Texture2D text);
        public Rectangle GetHitbox();
    }
}
