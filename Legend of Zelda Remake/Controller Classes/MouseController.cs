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

namespace Sprint0
{
    public class MouseController : IController
    {
        private Dictionary<MouseButton, ICommand> mouseCommandDictionary;

        private static MouseState currentState;
        private static MouseState previousState = new MouseState();

        private enum MouseButton
        {
            RightButton,
            LeftButton
        }

        void IController.Update(GameTime gameTime)
        {
            previousState = currentState;
            currentState = Mouse.GetState();
            if (currentState.RightButton == ButtonState.Pressed)
            {
                if(previousState.RightButton != ButtonState.Pressed)
                {
                    //mouseCommandDictionary[MouseButton.RightButton].Execute(gameTime);

                }
            }
            if (currentState.LeftButton == ButtonState.Pressed)
            {
                if (previousState.LeftButton != ButtonState.Pressed)
                {
                    //mouseCommandDictionary[MouseButton.LeftButton].Execute(gameTime);

                }

            }

        }

        void AssignCommand(MouseButton button, ICommand command)
        {
            mouseCommandDictionary.Add(button, command);
        }

        public MouseController(Game1 myGame)
        {
            mouseCommandDictionary = new Dictionary<MouseButton, ICommand>();
            //AssignCommand(MouseButton.RightButton, new ChangeRoomUpCommand(myGame));
            //AssignCommand(MouseButton.LeftButton, new ChangeRoomDownCommand(myGame));
        }

    }

}
