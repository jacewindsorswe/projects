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
    public class EnemySprite : ISprite
    {
        private Texture2D texture;
        private Rectangle source;
        private int sourceX, sourceY;
        public EnemySprite(Texture2D t, int x, int y, int width, int height)
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