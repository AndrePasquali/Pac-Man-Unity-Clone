namespace Aquiris.PacMan.Gameplay.State
{
    public static class GamePlayStateManager
    {
        public static GamePlayState CurrentGamePlayState = GamePlayState.StartScreen;

        public static void ChangeGamePlayState(GamePlayState newState)
        {
            CurrentGamePlayState = newState;
            
            Gameplay.ProcessState();
        }

    }
}