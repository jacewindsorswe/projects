using Microsoft.Xna.Framework;
using Sprint0;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sprint2.Interfaces
{
    public interface ICollisionCommand
    {
        void Execute(GameTime gameTime, CollidableObject c1, CollidableObject c2);
    }
}
