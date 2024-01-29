using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Sprint2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sprint0
{
    public class BackgroundSprite : ISprite
    {

        Rectangle source;
        Texture2D texture;
        int x;
        int y;


        public BackgroundSprite(Texture2D texture, int x, int y, int width, int height)
        {
            this.texture = texture;
            this.x = x;
            this.y = y;
            source = new Rectangle(x, y, width, height);
        }
        public void Draw(SpriteBatch spriteBatch, Rectangle dest, Color color)
        {
            spriteBatch.Draw(texture, dest, source, color);

        }
    }
}
