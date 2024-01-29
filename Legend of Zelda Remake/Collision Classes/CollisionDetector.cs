using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
namespace Sprint0
{
    public class CollisionDetector
    {
        public enum CollisionType {Left, Right, Top, Bottom, Equal, None};
        private Game1 Game;
        
        private List<CollidableObject> CollidableObjects; //list of all objects with collision
        private List<CollidableObject[]> PotentiallyCollidingObjects; //list of colliding sprite pairs with collision type
        private List<Tuple<Tuple<CollidableObject.Type, int>, Tuple<CollidableObject.Type, int>, CollisionType>> CollidingObjects; //A list of objects where each object is a Tuple = ({Type, ID}, {Type, ID}, CollisionDirection)
        public CollisionDetector(Game1 game)
        {
            //place pairs of objects to check for collision based on x coord sorting in here
            Game = game;
            CollidableObjects = new List<CollidableObject>(Game.Collidables); //set to game object array on creation
            PotentiallyCollidingObjects = new List<CollidableObject[]>();
            CollidingObjects = new List<Tuple<Tuple<CollidableObject.Type, int>, Tuple<CollidableObject.Type, int>, CollisionType>>();
        }

        //checks a given pair of sprites for collision, returns them and their collision type if colliding
        CollisionType CheckForCollision(int i)
        {
            Rectangle Intersection = Microsoft.Xna.Framework.Rectangle.Intersect(PotentiallyCollidingObjects[i][0].CurrentHitbox, PotentiallyCollidingObjects[i][1].CurrentHitbox);

            if (!Intersection.IsEmpty)
            {
                return checkIntersection(Intersection,i);
            }
            else
            {
                return CollisionType.None;
            }
        }
        private CollisionType checkIntersection(Rectangle Intersection,int i)
        {
            if (Intersection.Width > Intersection.Height) //TopBottom Collision
            {
                if (PotentiallyCollidingObjects[i][0].CurrentHitbox.Top < PotentiallyCollidingObjects[i][1].CurrentHitbox.Top)
                {
                    return CollisionType.Bottom;
                }
                else
                {
                    return CollisionType.Top;
                }
            }
            else if (Intersection.Width < Intersection.Height) //LeftRight Collision
            {
                if (PotentiallyCollidingObjects[i][1].CurrentHitbox.Left > PotentiallyCollidingObjects[i][0].CurrentHitbox.Left)
                {
                    return CollisionType.Right;
                }
                else
                {
                    return CollisionType.Left;
                }
            }
            else //Intersection.Width == Intersection.Height
            {
                return CollisionType.Equal;
            }
        }
        //returns list of a sprite pair that needs handled alongside it's collision type
        private void CheckObjectPairs()
        {
            CollisionType colDirection;
            for (int i = 0; i < PotentiallyCollidingObjects.Count; i++)
            {
                colDirection = CheckForCollision(i);
                if (colDirection != CollisionType.None) //collision detected if true
                {
                    //System.Diagnostics.Debug.WriteLine(PotentiallyCollidingObjects[i][0].CollidableType + " "  + PotentiallyCollidingObjects[i][1].CollidableType);
                    CollidingObjects.Add(Tuple.Create(Tuple.Create(PotentiallyCollidingObjects[i][0].CollidableType, PotentiallyCollidingObjects[i][0].ID), Tuple.Create(PotentiallyCollidingObjects[i][1].CollidableType, PotentiallyCollidingObjects[i][1].ID), colDirection)); //COLLIDING OBJECTS HAS THE CORRECT ARGUMENTS
                }
            }
        }

        private void SortAndDetectPotentialCollision()
        {
            List<KeyValuePair<CollidableObject,int>> objectsInOrder = SortByX();
            for (int i = 0; i < objectsInOrder.Count; i++)
            {
                for (int j = i + 1; j < objectsInOrder.Count; j++)
                {
                    if (objectsInOrder[j].Value <= objectsInOrder[i].Key.CurrentHitbox.X + objectsInOrder[i].Key.CurrentHitbox.Width)
                    {
                        PotentiallyCollidingObjects.Add(new CollidableObject[] { objectsInOrder[i].Key, objectsInOrder[j].Key });
                    }
                }
            }
        }

        private List<KeyValuePair<CollidableObject,int>> SortByX()
        {
            List<KeyValuePair<CollidableObject, int>> objectList = new List<KeyValuePair<CollidableObject, int>>();

            foreach (CollidableObject obj in CollidableObjects)
            {
                objectList.Add(new KeyValuePair<CollidableObject, int>(obj, obj.CurrentHitbox.X));
            }
            objectList.Sort(delegate (KeyValuePair<CollidableObject, int> firstPair,
                KeyValuePair<CollidableObject, int> nextPair)
                            {
                                return firstPair.Value.CompareTo(nextPair.Value);
                            }
            );
            return objectList;
        }

        //returns dict to pass to handler, per update
        public List<Tuple<Tuple<CollidableObject.Type, int>, Tuple<CollidableObject.Type, int>, CollisionType>> Update()
        {
            CollidableObjects = new List<CollidableObject>(Game.Collidables);

            PotentiallyCollidingObjects = new List<CollidableObject[]>();
            CollidingObjects = new List<Tuple<Tuple<CollidableObject.Type, int>, Tuple<CollidableObject.Type, int>, CollisionType>>();
            SortAndDetectPotentialCollision();

            CheckObjectPairs();
            PotentiallyCollidingObjects.Clear();

            return CollidingObjects;        
        }

    }
}