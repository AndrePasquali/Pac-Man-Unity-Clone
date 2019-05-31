using UnityEngine;

namespace Aquiris.PacMan.Input
{
    public class Button
    {
        public string ButtonID { get; protected set; }
        
        public ButtonState State { get; protected set; }

        public KeyCode Key { get; protected set; }

        public delegate void ButtonDown();

        public delegate void ButtonUp();

        public delegate void ButtonPressed();

        public ButtonDown ButtonDownAction;

        public ButtonUp ButtonUpAction;

        public ButtonPressed ButtonPressedAction;

        public Button(string id, KeyCode code, ButtonDown buttonDown, ButtonUp buttonUp, ButtonPressed buttonPressed, ButtonState state)
        {
            ButtonID = id;
            Key = code;
            ButtonDownAction = buttonDown;
            ButtonUpAction = buttonUp;
            ButtonPressedAction = buttonPressed;
            State = ButtonState.Off;
        }

        public virtual void OnButtonDown()
        {
            ButtonDownAction();
        }

        public virtual void OnButtonUp()
        {
            ButtonUpAction();
        }

        public virtual void OnButtonPressed()
        {
            ButtonPressedAction();
        }
    }
}
