namespace DroidDigital.PacMan.Gameplay.State
{
    public static class GamePlayStateController
    {
        public static GamePlayState CurrentGamePlayState = GamePlayState.Start;

        public static void ChangeGamePlayState(GamePlayState newState)
        {
            CurrentGamePlayState = newState;
            
            GameplayController.ProcessState();
        }

    }
}