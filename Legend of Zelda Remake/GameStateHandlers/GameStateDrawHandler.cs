using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Sprint0;
using Sprint2.Enemy_Classes;
using Sprint2.Item_Classes;
using Sprint2.Link_Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Sprint0.Game1;

namespace Sprint2.GameStateHandlers
{
    public class GameStateDrawHandler
    {
        public static void DrawNormalPausedState(Game1 g,GameTime gameTime, SpriteBatch _spriteBatch,SoundEffectGenerator _soundEffectGenerator)
        {
            g.dungeon.Draw();

            g.overheadDisplay.Draw(_spriteBatch, 0, 0);


            foreach (CollidableObject entity in GlobalUtilities.AllEntities)
            {
                if (entity.IsActive && entity.CollidableType != CollidableObject.Type.Projectile)
                {
                    entity.Draw(_spriteBatch, Microsoft.Xna.Framework.Color.White);
                    /*if (idList.Count >= 1 && entity.CollidableType == CollidableObject.Type.StandardBlock && (((BlockStateMachine)entity).Type == BlockStateMachine.BlockType.VerticalClosedDoor || ((BlockStateMachine)entity).Type == BlockStateMachine.BlockType.HorizontalClosedDoor))
                    {
                        System.Diagnostics.Debug.WriteLine("ID LIST: " + idList[0]);
                        System.Diagnostics.Debug.WriteLine("ID: " + entity.ID);
                    }*/
                }
            }

            g.link1.Draw(_spriteBatch, Microsoft.Xna.Framework.Color.White);

            for (int i = 0; i < Projectile.ProjectileList.Count; i++)
            {
                if (gameTime.TotalGameTime.TotalSeconds - Projectile.ProjectileList[i].CallTime >= GlobalUtilities.disappearTime[Projectile.ProjectileList[i].ItemClass])
                {
                    if (g.explodablesP.Contains(Projectile.ProjectileList[i].ItemClass))
                    {
                        if (Projectile.ProjectileList[i].ItemClass != Item.ItemCategory.Bomb)
                        {
                            Explosion explosives = new Explosion(Item.ItemCategory.OrangeParticle, Projectile.ProjectileList[i].X + Projectile.ProjectileList[i].Width, Projectile.ProjectileList[i].Y + Projectile.ProjectileList[i].Height, Projectile.ProjectileList[i].X, Projectile.ProjectileList[i].Y, gameTime);
                            List<Projectile> explodingParticles = explosives.ParticleList;

                            foreach (Projectile particle in explodingParticles)
                            {
                                if (Projectile.ProjectileList[i].LinkThrow == true)
                                {
                                    particle.LinkThrow = true;
                                }
                            }
                        }
                        else 
                        {
                            _soundEffectGenerator.Play("LOZ_Bomb_Blow");
                            Explosion explosives = new Explosion(Item.ItemCategory.ExplosiveCloud, Projectile.ProjectileList[i].X + Projectile.ProjectileList[i].Width, Projectile.ProjectileList[i].Y + Projectile.ProjectileList[i].Height, Projectile.ProjectileList[i].X, Projectile.ProjectileList[i].Y, gameTime);
                            List<Projectile> explodingParticles = explosives.ParticleList;

                            foreach (Projectile particle in explodingParticles)
                            {
                                if (Projectile.ProjectileList[i].LinkThrow == true)
                                {
                                    particle.LinkThrow = true;
                                }
                            }
                        }
                    }
                    if ((Projectile.ProjectileList[i]).ItemClass != Item.ItemCategory.OrangeParticle)
                    {
                        g.deathAnimations.Add(new DeathAnimation(GlobalUtilities.convertItemtoDeathType((Projectile.ProjectileList[i]).ItemClass), new Microsoft.Xna.Framework.Rectangle((Projectile.ProjectileList[i]).X, (Projectile.ProjectileList[i]).Y, GlobalUtilities.PROJECTILE_DEATH_ANIMATION_SIZE, GlobalUtilities.PROJECTILE_DEATH_ANIMATION_SIZE), gameTime.TotalGameTime.TotalSeconds));
                    }
                    Projectile.ProjectileList[i].Exists = false;
                }
                else
                {
                    if (Projectile.ProjectileList[i].IsActive)
                    {
                        Projectile.ProjectileList[i].Draw(_spriteBatch, Microsoft.Xna.Framework.Color.White);
                    }
                }
            }

            if (Game1.gameState == GameStateType.Paused)
            {
                PauseMenu.Draw(_spriteBatch, g.Content.Load<Texture2D>("Enemies Fixed"));
            }
        }
        public static void DrawGameOverState(Game1 g,GameTime gameTime,SpriteBatch _spriteBatch)
        {
            if (!Game1.gameOver) //Game over sequence is happening
            {
                //Draw link, dungeon tinted red
                g.dungeon.Draw(Microsoft.Xna.Framework.Color.Lerp(Microsoft.Xna.Framework.Color.White, Microsoft.Xna.Framework.Color.Red, 0.95f));
                g.link1.Draw(_spriteBatch, Microsoft.Xna.Framework.Color.White);
                g.GameEndTime = gameTime.TotalGameTime.TotalSeconds;
            }
            else //Game over sequence is over, should show text that reads "game over"
            {
                if (gameTime.TotalGameTime.TotalSeconds - g.GameEndTime >= GlobalUtilities.GAME_OVER_DELAY)
                {
                    int width = LinkFont.GetSizeOfText("GAME OVER", GlobalUtilities.GAME_OVER_TEXT_WIDTH, GlobalUtilities.GAME_OVER_TEXT_HEIGHT).Item1;
                    int height = LinkFont.GetSizeOfText("GAME OVER", GlobalUtilities.GAME_OVER_TEXT_WIDTH, GlobalUtilities.GAME_OVER_TEXT_HEIGHT).Item2;
                    Microsoft.Xna.Framework.Rectangle cursorPosition = new Microsoft.Xna.Framework.Rectangle((GlobalUtilities.Resolution[0] / 2) - (width / 2), (GlobalUtilities.Resolution[1] / 2) - (height / 2), GlobalUtilities.GAME_OVER_TEXT_WIDTH, GlobalUtilities.GAME_OVER_TEXT_HEIGHT);
                    LinkFont.DrawText(_spriteBatch, "GAME OVER", cursorPosition);

                    if (gameTime.TotalGameTime.TotalSeconds - g.GameEndTime > GlobalUtilities.GAME_OVER_DELAY + GlobalUtilities.GAME_OVER_DISPLAY)
                    {
                        ICommand Reset = new ResetCommand(g);
                        Reset.Execute(gameTime);
                        LinkInventory.Clear();
                        g.link1.Dead = false;
                    }
                }
            }
        }
        public static void DrawGameWinState(Game1 g, GameTime gameTime, SpriteBatch _spriteBatch)
        {
            if (!Game1.gameWin) //Game win sequence is happening
            {
                //Draw link, dungeon
                g.dungeon.Draw();
                g.link1.Draw(_spriteBatch, Microsoft.Xna.Framework.Color.White);
                foreach (Item item in Item.ItemList)
                {
                    if (item.IsActive)
                    {
                        item.Draw(_spriteBatch, Color.White);
                    }
                }
                g.GameEndTime = gameTime.TotalGameTime.TotalSeconds;
            }
            else //Game win sequence is over, begin transition to black
            {
                if (gameTime.TotalGameTime.TotalSeconds - g.GameEndTime <= GlobalUtilities.GAME_WIN_TRANSITION_TIME / 3)
                {
                    g.dungeon.Draw();
                    _spriteBatch.Draw(g.blackRectangle, new Rectangle(0, 0, (GlobalUtilities.Resolution[0] / 2) / 3, GlobalUtilities.Resolution[1]), Color.Black);
                    _spriteBatch.Draw(g.blackRectangle, new Rectangle(GlobalUtilities.Resolution[0] - ((GlobalUtilities.Resolution[0] / 2) / 3), 0, (GlobalUtilities.Resolution[0] / 2) / 3, GlobalUtilities.Resolution[1]), Color.Black);
                    g.link1.Draw(_spriteBatch, Microsoft.Xna.Framework.Color.White);
                    foreach (Item item in Item.ItemList)
                    {
                        if (item.IsActive)
                        {
                            item.Draw(_spriteBatch, Color.White);
                        }
                    }
                }
                else if (gameTime.TotalGameTime.TotalSeconds - g.GameEndTime <= (GlobalUtilities.GAME_WIN_TRANSITION_TIME * 2) / 3)
                {
                    g.dungeon.Draw();
                    _spriteBatch.Draw(g.blackRectangle, new Rectangle(0, 0, GlobalUtilities.Resolution[0] / 3, GlobalUtilities.Resolution[1]), Color.Black);
                    _spriteBatch.Draw(g.blackRectangle, new Rectangle(GlobalUtilities.Resolution[0] - (GlobalUtilities.Resolution[0] / 3), 0, GlobalUtilities.Resolution[0] / 3, GlobalUtilities.Resolution[1]), Color.Black);
                    g.link1.Draw(_spriteBatch, Microsoft.Xna.Framework.Color.White);
                    foreach (Item item in Item.ItemList)
                    {
                        if (item.IsActive)
                        {
                            item.Draw(_spriteBatch, Color.White);
                        }
                    }
                }
                else if (gameTime.TotalGameTime.TotalSeconds - g.GameEndTime <= GlobalUtilities.GAME_WIN_TRANSITION_TIME)
                {
                    g.link1.Draw(_spriteBatch, Microsoft.Xna.Framework.Color.White);
                    foreach (Item item in Item.ItemList)
                    {
                        if (item.IsActive)
                        {
                            item.Draw(_spriteBatch, Color.White);
                        }
                    }
                }
                else 
                {
                    ICommand Reset = new ResetCommand(g);
                    Reset.Execute(gameTime);
                    g.link1.Win = false;
                    g.link1.CurrentAction = LinkStateMachine.ActionType.Idle;
                    Game1.gameWin = false;
                }
            }
        }
    }
}
