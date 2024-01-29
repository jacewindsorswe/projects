using Microsoft.Xna.Framework;
using Sprint2;
using Sprint2.Block_Classes;
using Sprint2.Commands.CollisionCommands;
using Sprint2.Commands.CollisionCommands.EnemyBlockCollisions;
using Sprint2.Commands.CollisionCommands.EnemyEnemyCollisions;
using Sprint2.Commands.CollisionCommands.LinkBlockCollisions;
using Sprint2.Commands.CollisionCommands.LinkEnemyCollisions;
using Sprint2.Commands.CollisionCommands.LinkItemCollisions;
using Sprint2.Interfaces;
using Sprint2.Item_Classes;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Security.Cryptography.X509Certificates;
using static Sprint0.CollisionDetector;

namespace Sprint0
{
    public class CollisionHandler
    {
        public List<CollidableObject[]> ObjectsToHandle;
        public Game1 game;

        //dictionary of all handling commands
        private Dictionary<Tuple<CollidableObject.Type, CollidableObject.Type, CollisionDetector.CollisionType>, ICollisionCommand> testDict = new Dictionary<Tuple<CollidableObject.Type, CollidableObject.Type, CollisionDetector.CollisionType>, ICollisionCommand>(new ListEqualityComparer());

        public CollisionHandler(Game1 g)
        {
            game = g;
            FillDictionary();
        }

        public void Update(List<Tuple<Tuple<CollidableObject.Type, int>, Tuple<CollidableObject.Type, int>, CollisionType>> ObjectsToHandle, GameTime gameTime)
        {

            foreach(Tuple<Tuple<CollidableObject.Type, int>, Tuple<CollidableObject.Type, int>, CollisionType> collidingObjectTriple in ObjectsToHandle) 
            {
                CollisionType direction = HandleItemDirection(collidingObjectTriple.Item2.Item1, collidingObjectTriple.Item1.Item1, collidingObjectTriple.Item3);
                Tuple<CollidableObject.Type, CollidableObject.Type, CollisionType> collideDictLookup = Tuple.Create(collidingObjectTriple.Item1.Item1, collidingObjectTriple.Item2.Item1, direction);

                if (testDict.ContainsKey(collideDictLookup))
                {
                    if (Dungeon.ObjectExists(collidingObjectTriple.Item1.Item1, collidingObjectTriple.Item1.Item2) && Dungeon.ObjectExists(collidingObjectTriple.Item2.Item1, collidingObjectTriple.Item2.Item2))
                    {
                        testDict[collideDictLookup].Execute(gameTime, GlobalUtilities.GetCollidable(collidingObjectTriple.Item1.Item1, collidingObjectTriple.Item1.Item2), GlobalUtilities.GetCollidable(collidingObjectTriple.Item2.Item1, collidingObjectTriple.Item2.Item2));
                        RemoveInverse(collidingObjectTriple, ObjectsToHandle);
                    }
                }
                else 
                {
                    direction = HandleItemDirection(collidingObjectTriple.Item1.Item1, collidingObjectTriple.Item2.Item1, GlobalUtilities.GetCollisionOpposite(collidingObjectTriple.Item3));
                    collideDictLookup = Tuple.Create(collidingObjectTriple.Item2.Item1, collidingObjectTriple.Item1.Item1, direction);
                    if (testDict.ContainsKey(collideDictLookup))
                    {
                        if (Dungeon.ObjectExists(collidingObjectTriple.Item1.Item1, collidingObjectTriple.Item1.Item2) && Dungeon.ObjectExists(collidingObjectTriple.Item2.Item1, collidingObjectTriple.Item2.Item2))
                        {
                            testDict[collideDictLookup].Execute(gameTime, GlobalUtilities.GetCollidable(collidingObjectTriple.Item2.Item1, collidingObjectTriple.Item2.Item2), GlobalUtilities.GetCollidable(collidingObjectTriple.Item1.Item1, collidingObjectTriple.Item1.Item2));
                            RemoveInverse(collidingObjectTriple, ObjectsToHandle);
                        }
                    }
                }
            }
        }

        //If Link collides with a non-damaging item, the direction the collision occurred in is changed to "Equal"
        private CollisionDetector.CollisionType HandleItemDirection(CollidableObject.Type type2, CollidableObject.Type type, CollisionDetector.CollisionType cT) 
        {
            if (type2 == CollidableObject.Type.Item) 
            {
                return CollisionDetector.CollisionType.Equal;
            }

            if(type2 == CollidableObject.Type.StandardBlock || type2 == CollidableObject.Type.PushableBlock) 
            {
                if(type == CollidableObject.Type.Projectile) 
                {
                    return CollisionDetector.CollisionType.Equal;
                }
            }
            return cT;
        }

        private void FillDictionary() 
        {
            //Enemy-Enemy Collisions
            testDict.Add(Tuple.Create(CollidableObject.Type.Enemy, CollidableObject.Type.Enemy, CollisionDetector.CollisionType.Left), new ResolveEnemyEnemyLeftCollision(game));
            testDict.Add(Tuple.Create(CollidableObject.Type.Enemy, CollidableObject.Type.Enemy, CollisionDetector.CollisionType.Right), new ResolveEnemyEnemyRightCollision(game));
            testDict.Add(Tuple.Create(CollidableObject.Type.Enemy, CollidableObject.Type.Enemy, CollisionDetector.CollisionType.Equal), new ResolveEnemyEnemyLeftCollision(game));
            testDict.Add(Tuple.Create(CollidableObject.Type.Enemy, CollidableObject.Type.Enemy, CollisionDetector.CollisionType.Top), new ResolveEnemyEnemyTopCollision(game));
            testDict.Add(Tuple.Create(CollidableObject.Type.Enemy, CollidableObject.Type.Enemy, CollisionDetector.CollisionType.Bottom), new ResolveEnemyEnemyBottomCollision(game));

            //Enemy-Block Collisions
            testDict.Add(Tuple.Create(CollidableObject.Type.Enemy, CollidableObject.Type.StandardBlock, CollisionDetector.CollisionType.Bottom), new ResolveEnemyBlockBottomCollision(game));
            testDict.Add(Tuple.Create(CollidableObject.Type.Enemy, CollidableObject.Type.StandardBlock, CollisionDetector.CollisionType.Top), new ResolveEnemyBlockTopCollision(game));
            testDict.Add(Tuple.Create(CollidableObject.Type.Enemy, CollidableObject.Type.StandardBlock, CollisionDetector.CollisionType.Left), new ResolveEnemyBlockLeftCollision(game));
            testDict.Add(Tuple.Create(CollidableObject.Type.Enemy, CollidableObject.Type.StandardBlock, CollisionDetector.CollisionType.Right), new ResolveEnemyBlockRightCollision(game));
            testDict.Add(Tuple.Create(CollidableObject.Type.Enemy, CollidableObject.Type.StandardBlock, CollisionDetector.CollisionType.Equal), new ResolveEnemyBlockEqualCollision(game));

            testDict.Add(Tuple.Create(CollidableObject.Type.Enemy, CollidableObject.Type.PushableBlock, CollisionDetector.CollisionType.Bottom), new ResolveEnemyBlockBottomCollision(game));
            testDict.Add(Tuple.Create(CollidableObject.Type.Enemy, CollidableObject.Type.PushableBlock, CollisionDetector.CollisionType.Top), new ResolveEnemyBlockTopCollision(game));
            testDict.Add(Tuple.Create(CollidableObject.Type.Enemy, CollidableObject.Type.PushableBlock, CollisionDetector.CollisionType.Left), new ResolveEnemyBlockLeftCollision(game));
            testDict.Add(Tuple.Create(CollidableObject.Type.Enemy, CollidableObject.Type.PushableBlock, CollisionDetector.CollisionType.Right), new ResolveEnemyBlockRightCollision(game));
            testDict.Add(Tuple.Create(CollidableObject.Type.Enemy, CollidableObject.Type.PushableBlock, CollisionDetector.CollisionType.Equal), new ResolveEnemyBlockEqualCollision(game));

            //Link-Block Collisions
            testDict.Add(Tuple.Create(CollidableObject.Type.Link, CollidableObject.Type.StandardBlock, CollisionDetector.CollisionType.Bottom), new ResolveLinkBlockBottomCollision(game));
            testDict.Add(Tuple.Create(CollidableObject.Type.Link, CollidableObject.Type.StandardBlock, CollisionDetector.CollisionType.Top), new ResolveLinkBlockTopCollision(game));
            testDict.Add(Tuple.Create(CollidableObject.Type.Link, CollidableObject.Type.StandardBlock, CollisionDetector.CollisionType.Left), new ResolveLinkBlockLeftCollision(game));
            testDict.Add(Tuple.Create(CollidableObject.Type.Link, CollidableObject.Type.StandardBlock, CollisionDetector.CollisionType.Right), new ResolveLinkBlockRightCollision(game));
            testDict.Add(Tuple.Create(CollidableObject.Type.Link, CollidableObject.Type.StandardBlock, CollisionDetector.CollisionType.Equal), new ResolveLinkBlockEqualCollision(game));
            testDict.Add(Tuple.Create(CollidableObject.Type.Link, CollidableObject.Type.PushableBlock, CollisionDetector.CollisionType.Bottom), new ResolveLinkPushableBottomCollision(game));
            testDict.Add(Tuple.Create(CollidableObject.Type.Link, CollidableObject.Type.PushableBlock, CollisionDetector.CollisionType.Top), new ResolveLinkPushableTopCollision(game));
            testDict.Add(Tuple.Create(CollidableObject.Type.Link, CollidableObject.Type.PushableBlock, CollisionDetector.CollisionType.Left), new ResolveLinkPushableLeftCollision(game));
            testDict.Add(Tuple.Create(CollidableObject.Type.Link, CollidableObject.Type.PushableBlock, CollisionDetector.CollisionType.Right), new ResolveLinkPushableRightCollision(game));
            testDict.Add(Tuple.Create(CollidableObject.Type.Link, CollidableObject.Type.PushableBlock, CollisionDetector.CollisionType.Equal), new ResolveLinkPushableEqualCollision(game));

            //Projectile-Block Collisions
            testDict.Add(Tuple.Create(CollidableObject.Type.Projectile, CollidableObject.Type.StandardBlock, CollisionDetector.CollisionType.Equal), new ResolveProjectileBlockCollision(game));
            testDict.Add(Tuple.Create(CollidableObject.Type.Projectile, CollidableObject.Type.PushableBlock, CollisionDetector.CollisionType.Equal), new ResolveProjectileBlockCollision(game));

            //Link-Item Collisions
            testDict.Add(Tuple.Create(CollidableObject.Type.Link, CollidableObject.Type.Item, CollisionDetector.CollisionType.Equal), new ResolveLinkItemCollision(game));

            //Enemy-Projectile Collisions
            testDict.Add(Tuple.Create(CollidableObject.Type.Enemy, CollidableObject.Type.Projectile, CollisionDetector.CollisionType.Bottom), new ResolveEnemyProjectileBottomCollision(game));
            testDict.Add(Tuple.Create(CollidableObject.Type.Enemy, CollidableObject.Type.Projectile, CollisionDetector.CollisionType.Top), new ResolveEnemyProjectileTopCollision(game));
            testDict.Add(Tuple.Create(CollidableObject.Type.Enemy, CollidableObject.Type.Projectile, CollisionDetector.CollisionType.Left), new ResolveEnemyProjectileLeftCollision(game));
            testDict.Add(Tuple.Create(CollidableObject.Type.Enemy, CollidableObject.Type.Projectile, CollisionDetector.CollisionType.Right), new ResolveEnemyProjectileRightCollision(game));
            testDict.Add(Tuple.Create(CollidableObject.Type.Enemy, CollidableObject.Type.Projectile, CollisionDetector.CollisionType.Equal), new ResolveEnemyProjectileEqualCollision(game));

            //Enemy-Item Collisions
            testDict.Add(Tuple.Create(CollidableObject.Type.Enemy, CollidableObject.Type.Item, CollisionDetector.CollisionType.Bottom), new ResolveEnemyItemBottomCollision(game));
            testDict.Add(Tuple.Create(CollidableObject.Type.Enemy, CollidableObject.Type.Item, CollisionDetector.CollisionType.Top), new ResolveEnemyItemTopCollision(game));
            testDict.Add(Tuple.Create(CollidableObject.Type.Enemy, CollidableObject.Type.Item, CollisionDetector.CollisionType.Left), new ResolveEnemyItemLeftCollision(game));
            testDict.Add(Tuple.Create(CollidableObject.Type.Enemy, CollidableObject.Type.Item, CollisionDetector.CollisionType.Right), new ResolveEnemyItemRightCollision(game));
            testDict.Add(Tuple.Create(CollidableObject.Type.Enemy, CollidableObject.Type.Item, CollisionDetector.CollisionType.Equal), new ResolveEnemyItemEqualCollision(game));

            //Link-Projectile Collisions
            testDict.Add(Tuple.Create(CollidableObject.Type.Link, CollidableObject.Type.Projectile, CollisionDetector.CollisionType.Bottom), new ResolveLinkProjectileBottomCollision(game));
            testDict.Add(Tuple.Create(CollidableObject.Type.Link, CollidableObject.Type.Projectile, CollisionDetector.CollisionType.Top), new ResolveLinkProjectileTopCollision(game));
            testDict.Add(Tuple.Create(CollidableObject.Type.Link, CollidableObject.Type.Projectile, CollisionDetector.CollisionType.Left), new ResolveLinkProjectileLeftCollision(game));
            testDict.Add(Tuple.Create(CollidableObject.Type.Link, CollidableObject.Type.Projectile, CollisionDetector.CollisionType.Right), new ResolveLinkProjectileRightCollision(game));
            testDict.Add(Tuple.Create(CollidableObject.Type.Link, CollidableObject.Type.Projectile, CollisionDetector.CollisionType.Equal), new ResolveLinkProjectileEqualCollision(game));

            //Link-Enemy Collisions
            testDict.Add(Tuple.Create(CollidableObject.Type.Link, CollidableObject.Type.Enemy, CollisionDetector.CollisionType.Bottom), new ResolveLinkEnemyBottomCollision(game));
            testDict.Add(Tuple.Create(CollidableObject.Type.Link, CollidableObject.Type.Enemy, CollisionDetector.CollisionType.Top), new ResolveLinkEnemyTopCollision(game));
            testDict.Add(Tuple.Create(CollidableObject.Type.Link, CollidableObject.Type.Enemy, CollisionDetector.CollisionType.Left), new ResolveLinkEnemyLeftCollision(game));
            testDict.Add(Tuple.Create(CollidableObject.Type.Link, CollidableObject.Type.Enemy, CollisionDetector.CollisionType.Right), new ResolveLinkEnemyRightCollision(game));
            testDict.Add(Tuple.Create(CollidableObject.Type.Link, CollidableObject.Type.Enemy, CollisionDetector.CollisionType.Equal), new ResolveLinkEnemyEqualCollision(game));

            //PushableBlock-StaticBlock Collisions
            testDict.Add(Tuple.Create(CollidableObject.Type.PushableBlock, CollidableObject.Type.StandardBlock, CollisionDetector.CollisionType.Bottom), new ResolvePushableBlockBottomCollision(game));
            testDict.Add(Tuple.Create(CollidableObject.Type.PushableBlock, CollidableObject.Type.StandardBlock, CollisionDetector.CollisionType.Top), new ResolvePushableBlockTopCollision(game));
            testDict.Add(Tuple.Create(CollidableObject.Type.PushableBlock, CollidableObject.Type.StandardBlock, CollisionDetector.CollisionType.Left), new ResolvePushableBlockLeftCollision(game));
            testDict.Add(Tuple.Create(CollidableObject.Type.PushableBlock, CollidableObject.Type.StandardBlock, CollisionDetector.CollisionType.Right), new ResolvePushableBlockRightCollision(game));

            //PushableBlock-PushableBlock Collisions
            testDict.Add(Tuple.Create(CollidableObject.Type.PushableBlock, CollidableObject.Type.PushableBlock, CollisionDetector.CollisionType.Bottom), new ResolvePushableBlockBottomCollision(game));
            testDict.Add(Tuple.Create(CollidableObject.Type.PushableBlock, CollidableObject.Type.PushableBlock, CollisionDetector.CollisionType.Top), new ResolvePushableBlockTopCollision(game));
            testDict.Add(Tuple.Create(CollidableObject.Type.PushableBlock, CollidableObject.Type.PushableBlock, CollisionDetector.CollisionType.Left), new ResolvePushableBlockLeftCollision(game));
            testDict.Add(Tuple.Create(CollidableObject.Type.PushableBlock, CollidableObject.Type.PushableBlock, CollisionDetector.CollisionType.Right), new ResolvePushableBlockRightCollision(game));
        }

        private void RemoveInverse(Tuple<Tuple<CollidableObject.Type, int>, Tuple<CollidableObject.Type, int>, CollisionType> collidingObjects, List<Tuple<Tuple<CollidableObject.Type, int>, Tuple<CollidableObject.Type, int>, CollisionType>> objToHandle) 
        {
            Tuple<Tuple<CollidableObject.Type, int>, Tuple<CollidableObject.Type, int>, CollisionType> inverseCollidableObject = Tuple.Create<Tuple<CollidableObject.Type, int>, Tuple<CollidableObject.Type, int>, CollisionType>(collidingObjects.Item2, collidingObjects.Item1, collidingObjects.Item3);

            if (objToHandle.Contains(inverseCollidableObject)) 
            {
                objToHandle.Remove(inverseCollidableObject);
            }
        }

        //Necessary so the dictionary can reference each object/object/direction pair with the appropriate key in the dictionary
        private class ListEqualityComparer : IEqualityComparer<Tuple<CollidableObject.Type, CollidableObject.Type, CollisionDetector.CollisionType>>
        {
            public bool Equals(Tuple<CollidableObject.Type, CollidableObject.Type, CollisionDetector.CollisionType> x, Tuple<CollidableObject.Type, CollidableObject.Type, CollisionDetector.CollisionType> y)
            {
                if(x.Item1 != y.Item1 || x.Item2 != y.Item2 || x.Item3 != y.Item3) 
                {
                    return false;
                }
                return true;
            }

            //Never necessary to get a hashcode, no need to implement
            public int GetHashCode([DisallowNull] Tuple<CollidableObject.Type, CollidableObject.Type, CollisionDetector.CollisionType> obj)
            {
                return 0;
            }
        }
    }
}