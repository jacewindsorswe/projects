using Microsoft.Xna.Framework;
using Sprint2.Block_Classes;
using System;
using System.Collections.Generic;
using static Sprint0.CollisionDetector;

namespace Sprint0
{
    public class EntityUpdateHandler
    {
        Game1 game;

        public EntityUpdateHandler(Game1 g)
        {
            game = g;
        }

        public void Update(GameTime gameTime)
        {
            bool isClear = true;
            foreach (CollidableObject entity in Sprint2.GlobalUtilities.AllEntities)
            {
                if (entity.IsActive)
                {
                    if (entity.CollidableType == CollidableObject.Type.Enemy)
                    {
                        isClear = false;
                        if (((Enemy)entity).CurrentEnemyType == Enemy.EnemyType.Keese)
                        {
                            ((Keese)entity).Update(gameTime, game.link1.CurrentXPos, game.link1.CurrentYPos);
                        }
                        else if (((Enemy)entity).CurrentEnemyType == Enemy.EnemyType.Blade)
                        {
                            ((Blade)entity).Update(gameTime, game.link1.CurrentXPos, game.link1.CurrentYPos);
                        }
                        else if (((Enemy)entity).CurrentEnemyType == Enemy.EnemyType.Death)
                        {
                            ((Death)entity).Update(gameTime, 2);
                        }
                        else if(((Enemy)entity).CurrentEnemyType == Enemy.EnemyType.Rope) 
                        {
                            ((Rope)entity).Update(gameTime, game.link1.CurrentHitbox);
                        }
                        else
                        {
                            entity.Update(gameTime);
                        }
                    }
                    else
                    {
                        entity.Update(gameTime);
                    }
                }
            }
            if (isClear)
            {
                List<BlockStateMachine> toRemove = new();
                foreach (KeyValuePair<int, BlockStateMachine> pair in BlockStateMachine.BlockDictionary)
                {
                    if (pair.Value.Type == BlockStateMachine.BlockType.HorizontalClosedDoor || pair.Value.Type == BlockStateMachine.BlockType.VerticalClosedDoor)
                    {
                        toRemove.Add(pair.Value);
                    }
                }

                foreach (BlockStateMachine block in toRemove)
                {
                    block.IsActive = false;
                }
            }
        }
    }
}