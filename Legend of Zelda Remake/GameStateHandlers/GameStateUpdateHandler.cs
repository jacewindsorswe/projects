using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework;
using Sprint0;
using Sprint2.Block_Classes;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using static Sprint0.Game1;
using Sprint2.Commands;

namespace Sprint2.GameStateHandlers
{
    public class GameStateUpdateHandler{
        public static void UpdateNormalState(Game1 g, GameTime gameTime)
        {
            if (MediaPlayer.IsMuted && justPlayed)
            {
                g._song = g.Content.Load<Song>("BackgroundMusic");
                MediaPlayer.Play(g._song);
                MediaPlayer.IsMuted = false;
                justPlayed = false;
            }
            g.entityUpdateHandler.Update(gameTime);
            g.overheadDisplay.Update(gameTime, g.dungeon.RoomPos[0], g.dungeon.RoomPos[1]);
            g.link1.Update(gameTime);
            g.dungeon.Update();
            g.RCH.Update(gameTime);

            if (Game1.GameState.Equals(GameStateType.Inventory))
            {
                g.itemScreen.Update(gameTime);
                g.overheadDisplay.Update(gameTime, g.dungeon.RoomPos[0], g.dungeon.RoomPos[1]);
            }
            g.deathAnimationHandler.Update(gameTime);

            if (!GlobalUtilities.EnemiesLeft())
            {
                foreach (int id in idList)
                {
                    BlockStateMachine.RemoveBlock(id);
                }
                idList.Clear();
            }
            else if (GlobalUtilities.LinkInDoorway(g.link1))
            {
                GlobalUtilities.DeactivateClosedDoors(idList);
                foreach (int id in idList)
                {
                    GlobalUtilities.GetCollidable(CollidableObject.Type.StandardBlock, id).IsActive = false;
                }
            }
            else
            {
                foreach (int id in idList)
                {
                    GlobalUtilities.GetCollidable(CollidableObject.Type.StandardBlock, id).IsActive = true;
                }
            }

            g.collisionDetectionHandler.Update(gameTime);
        }
        public static void UpdateGameOverState(Game1 g, GameTime gameTime)
        {
            GlobalUtilities.DoGameOverTransition(gameTime, g.link1, g.dungeon);
            if (gameTime.TotalGameTime.TotalSeconds - deathTime > GlobalUtilities.DEATH_TRANSITION_TIME)
            {
                Game1.gameOver = true;
            }
        }
        public static void UpdateGameWinState(Game1 g, GameTime gameTime)
        {
            GlobalUtilities.DoGameWinTransition(gameTime, g.link1, g.dungeon);
            if (gameTime.TotalGameTime.TotalSeconds - winTime > GlobalUtilities.WIN_TRANSITION_TIME)
            {
                Game1.gameWin = true;
            }
        }
    }   
}
