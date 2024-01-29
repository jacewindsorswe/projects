using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.Design;
using System.Security.Cryptography;
using Microsoft.Xna.Framework;
using Sprint2.Commands;
using Sprint2.Commands.InventoryCommands;
using Sprint2;

namespace Sprint0
{
    public class KeyboardController : IController
    {
        public Dictionary<Keys, ICommand> keyboardCommandMap, inventoryCommandMap, pausedCommandMap, menuCommandMap;
        private GameTime timer;
        private int lastRecordedTime;

        private static KeyboardState currentState;
        private static KeyboardState previousState = new KeyboardState();

        //List contains all keys that act as toggles (i.e., once they're pressed, they should not remain "pressed")
        private readonly List<Keys> pressOnceKeys = new List<Keys>()
        {
            Keys.T,
            Keys.R,
            Keys.Y,
            Keys.P,
            Keys.O,
            Keys.I,
            Keys.U,
            Keys.D1,
            Keys.D2,
            Keys.D3,
            Keys.D4,
            Keys.D5,
            Keys.D6,
            Keys.D7,
            Keys.D8,
            Keys.D9,
            Keys.D0,
            Keys.Space,
            Keys.Z,
            Keys.N,
            Keys.OemComma,
            Keys.B,
            Keys.Right,
            Keys.Left,
            Keys.Up,
            Keys.Down,
            Keys.Enter,
            Keys.D0,
            Keys.L,
            Keys.M,
            Keys.X,
            Keys.LeftShift,
            Keys.NumPad0,
            Keys.NumPad1,
            Keys.NumPad2,
            Keys.NumPad3,
            Keys.NumPad4,
            Keys.NumPad5,
        };

        private readonly List<Keys> CheatKeys = new List<Keys>()
        {
            Keys.D6,
            Keys.D7,
            Keys.D8,
            Keys.D9,
            Keys.D0,
            Keys.T,
            Keys.NumPad0,
            Keys.NumPad1,
            Keys.NumPad2,
            Keys.NumPad3,
            Keys.NumPad4,
            Keys.NumPad5
        };
        void IController.Update(GameTime gameTime)
        {
            KeyboardState currentState = Keyboard.GetState();

            Keys[] pressedKeys = currentState.GetPressedKeys();

            foreach (Keys key in pressedKeys)
            {
                if (Game1.GameState == Game1.GameStateType.Normal)
                {
                    if (keyboardCommandMap.ContainsKey(key))
                    {
                        if (pressOnceKeys.Contains(key))
                        {
                            if (!HasBeenPressed(key))
                            {
                                if (key == Keys.Z || key == Keys.N)
                                {
                                    if (gameTime.TotalGameTime.TotalSeconds - GlobalUtilities.lastATime >= GlobalUtilities.DELAY_TIME_A)
                                    {
                                        keyboardCommandMap[key].Execute(gameTime);
                                        GlobalUtilities.lastATime = gameTime.TotalGameTime.TotalSeconds;
                                    }
                                }
                                else if (key == Keys.X)
                                {
                                    if (gameTime.TotalGameTime.TotalSeconds - GlobalUtilities.lastBTime >= GlobalUtilities.DELAY_TIME_B)
                                    {
                                        keyboardCommandMap[key].Execute(gameTime);
                                        GlobalUtilities.lastBTime = gameTime.TotalGameTime.TotalSeconds;
                                    }
                                }
                                else if (CheatKeys.Contains(key) && !Game1.CheatsEnabled) { }
                                else
                                {
                                    keyboardCommandMap[key].Execute(gameTime);
                                }
                            }
                        }
                        else
                        {
                            keyboardCommandMap[key].Execute(gameTime);
                        }
                    }
                }
                else if(Game1.GameState == Game1.GameStateType.Inventory) 
                {
                    if (inventoryCommandMap.ContainsKey(key))
                    {
                        if (pressOnceKeys.Contains(key))
                        {
                            if (!HasBeenPressed(key))
                            {
                                inventoryCommandMap[key].Execute(gameTime);
                            }
                        }
                        else
                        {
                            inventoryCommandMap[key].Execute(gameTime);
                        }
                    }
                }
                else if(Game1.GameState == Game1.GameStateType.Paused) 
                {
                    if (pausedCommandMap.ContainsKey(key)) 
                    {
                        if (pressOnceKeys.Contains(key))
                        {
                            if (!HasBeenPressed(key))
                            {
                                pausedCommandMap[key].Execute(gameTime);
                            }
                        }
                        else
                        {
                            pausedCommandMap[key].Execute(gameTime);
                        }
                    }
                }
                else if (Game1.GameState == Game1.GameStateType.MainMenu)
                {
                    if (menuCommandMap.ContainsKey(key))
                    {
                        if (pressOnceKeys.Contains(key))
                        {
                            if (!HasBeenPressed(key))
                            {
                                menuCommandMap[key].Execute(gameTime);
                            }
                        }
                        else
                        {
                            menuCommandMap[key].Execute(gameTime);
                        }
                    }
                }
            }

            previousState = currentState;
        }
        void AssignCommand(Keys key, ICommand command)
         {
            keyboardCommandMap.Add(key, command);
         }

        void AssignCommandInventory(Keys key, ICommand command) 
        {
            inventoryCommandMap.Add(key, command);
        }

        void AssignCommandPaused(Keys key, ICommand command)
        {
            pausedCommandMap.Add(key, command);
        }

        void AssignCommandMenu(Keys key, ICommand command) 
        {
            menuCommandMap.Add(key, command);
        }

        public KeyboardController(Game1 myGame)
        {
            keyboardCommandMap = new Dictionary<Keys, ICommand>();
            AssignCommand(Keys.Q, new QuitCommand(myGame));
            AssignCommand(Keys.R, new ResetCommand(myGame));
            //AssignCommand(Keys.T, new BlockTypeDownCommand(myGame));
            AssignCommand(Keys.Y, new BlockTypeUpCommand(myGame));
            AssignCommand(Keys.W, new MoveLinkUpCommand(myGame));
            AssignCommand(Keys.A, new MoveLinkLeftCommand(myGame));
            AssignCommand(Keys.S, new MoveLinkDownCommand(myGame));
            AssignCommand(Keys.D, new MoveLinkRightCommand(myGame));
            AssignCommand(Keys.Z, new AttackSwordLinkCommand(myGame));
            AssignCommand(Keys.N, new AttackSwordLinkCommand(myGame));
            AssignCommand(Keys.E, new DamageLinkCommand(myGame));
            AssignCommand(Keys.P, new EnemyTypeUpCommand(myGame));
            AssignCommand(Keys.O, new EnemyTypeDownCommand(myGame));
            AssignCommand(Keys.I, new ItemTypeUpCommand(myGame));
            AssignCommand(Keys.U, new ItemTypeDownCommand(myGame));
            AssignCommand(Keys.D1, new AttackArrowLinkCommand(myGame));
            AssignCommand(Keys.D2, new AttackBombLinkCommand(myGame));
            AssignCommand(Keys.D3, new AttackFireLinkCommand(myGame));
            AssignCommand(Keys.D4, new AttackBoomerangLinkCommand(myGame));
            AssignCommand(Keys.D5, new AttackBlueBoomerangLinkCommand(myGame));
            AssignCommand(Keys.B, new OpenInventoryCommand(myGame));
            AssignCommand(Keys.OemComma, new RevealInvisibleBlocksCommand(myGame)); //Used for debugging
            AssignCommand(Keys.M, new MuteSoundCommand(myGame));
            AssignCommand(Keys.X, new AttackSecondaryCommand(myGame));
            AssignCommand(Keys.LeftShift, new TogglePauseCommand(myGame));
            AssignCommand(Keys.D6, new GodModeCheat(myGame));
            AssignCommand(Keys.D7, new MaxKeysCheat(myGame));
            AssignCommand(Keys.D8, new MaxBombsCheat(myGame));
            AssignCommand(Keys.D9, new MaxHealthCheat(myGame));
            AssignCommand(Keys.D0, new MassMurderCheat(myGame));
            AssignCommand(Keys.T, new TimeStopCheat(myGame));
            AssignCommand(Keys.NumPad0, new GodModeCheat(myGame));
            AssignCommand(Keys.NumPad1, new MaxKeysCheat(myGame));
            AssignCommand(Keys.NumPad2, new MaxBombsCheat(myGame));
            AssignCommand(Keys.NumPad3, new MaxHealthCheat(myGame));
            AssignCommand(Keys.NumPad4, new MassMurderCheat(myGame));
            AssignCommand(Keys.NumPad5, new EasyDubCheat(myGame));



            inventoryCommandMap = new Dictionary<Keys, ICommand>();
            AssignCommandInventory(Keys.B, new ExitCommand(myGame));
            AssignCommandInventory(Keys.Left, new MoveRedBoxLeftCommand(myGame));
            AssignCommandInventory(Keys.Right, new MoveRedBoxRightCommand(myGame));
            AssignCommandInventory(Keys.Enter, new SelectCommand(myGame));

            pausedCommandMap = new Dictionary<Keys, ICommand>();
            AssignCommandPaused(Keys.LeftShift, new TogglePauseCommand(myGame));
            AssignCommandPaused(Keys.Left, new PauseSelectionBack(myGame));
            AssignCommandPaused(Keys.Right, new PauseSelectionNext(myGame));
            AssignCommandPaused(Keys.Up, new PauseSelectionBack(myGame));
            AssignCommandPaused(Keys.Down, new PauseSelectionNext(myGame));
            AssignCommandPaused(Keys.Enter, new PauseSelect(myGame));

            menuCommandMap = new Dictionary<Keys, ICommand>();
            AssignCommandMenu(Keys.Q, new QuitCommand(myGame));
            AssignCommandMenu(Keys.Left, new MoveCursorLeftCommand(myGame));
            AssignCommandMenu(Keys.Right, new MoveCursorRightCommand(myGame));
            AssignCommandMenu(Keys.Enter, new MenuSelectCommand(myGame));
        }
        public static bool HasBeenPressed(Keys key) 
        {
            return previousState.IsKeyDown(key);
        }

    }
}
