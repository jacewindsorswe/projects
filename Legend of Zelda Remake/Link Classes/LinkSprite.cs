using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Sprint0;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Sprint0
{
    public class LinkSprite : ISprite
    {
        Texture2D texture;
        Rectangle source;
        int sourceX, sourceY;
        public LinkSprite(Texture2D t, int x, int y, int width, int height)
        {
            texture = t;
            sourceX = x;
            sourceY = y;
            source = new Rectangle(sourceX, sourceY, width, height);
        }

        public void Draw(SpriteBatch spriteBatch, Rectangle rect, Color color)
        {
            spriteBatch.Draw(texture, rect, source, color);
        }

    }
}
