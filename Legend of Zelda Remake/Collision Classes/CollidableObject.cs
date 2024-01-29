using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Sprint0
{
	public interface CollidableObject : ICloneable
	{
		public Rectangle CurrentHitbox { get; set; }
        public int ID { get; }

        public enum Type
        {
            Enemy,
            Link,
            PushableBlock,
            StandardBlock,
            Projectile,
            Item,
        }
        public Type CollidableType { get; }

        public bool IsActive { get; set; }

        public void Update(GameTime gameTime);
        public void Draw(SpriteBatch spriteBatch, Color col);
    }
}