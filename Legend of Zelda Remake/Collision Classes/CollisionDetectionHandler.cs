using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using static Sprint0.CollisionDetector;

namespace Sprint0
{
    public class CollisionDetectionHandler
    {
        List<Tuple<Tuple<CollidableObject.Type, int>, Tuple<CollidableObject.Type, int>, CollisionType>> CollidingObjects;
        CollisionDetector Detector;
        CollisionHandler Handler;
        Game1 game;

        public CollisionDetectionHandler(Game1 g)
        {
            game = g;
            Detector = new CollisionDetector(game);
            Handler = new CollisionHandler(game);
        }

        public void Update(GameTime gameTime)
        {
            CollidingObjects = new List<Tuple<Tuple<CollidableObject.Type, int>, Tuple<CollidableObject.Type, int>, CollisionType>>(Detector.Update());

            Handler.Update(CollidingObjects, gameTime);
            CollidingObjects.Clear();
        }
    }
}