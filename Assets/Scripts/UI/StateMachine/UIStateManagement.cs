using Aquiris.PacMan.Gameplay;

namespace Aquiris.PacMan.UI.StateMachine
{
    internal static class UIStateManagement
    {
        public static UIState CurrentUIState;

        public static void ChangeUIState(UIState newUiState)
        {
            CurrentUIState = newUiState;
        }
    }
}