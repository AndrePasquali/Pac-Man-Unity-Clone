namespace DroidDigital.Core.Constants
{
    internal sealed class GameConstants
    {
        #region Input
        public const string HORIZONTAL_AXIS = "Horizontal";
        public const string VERTICAL_AXIS = "Vertical";
        public const string MOVE_UP = "MoveUp";
        public const string MOVE_DOWN = "MoveDown";
        public const string MOVE_LEFT = "MoveLeft";
        public const string MOVE_RIGHT = "MoveRight";
        public const string HORIZONTAL_SPEED = "SpeedX";
        public const string VERTICAL_SPEED = "SpeedY";     
        #endregion

        #region State    
        public const string DEAD = "Dead";
        public const string ALIVE = "Alive";
        public const string VULNERABLE = "Vulnerable";
        public const string IDLE = "Idle";
        public const string FREEZE = "Freeze";
        #endregion

        #region UIState

        public const string IDLE_SCREEN = "Idle";
        public const string PREPARE_TO_START = "PrepareToStart";
        public const string START_GAME = "StartGame";
        public const string GAMEPLAY = "Gameplay";
        public const string LEVEL_COMPLETED = "LevelCompleted";
        public const string GAMEOVER = "GameOver";
        

        #endregion

        public const string PLAYER_TAG = "Player";    
        public const string ENEMY_TAG = "Enemy";
        
        public const string NODE_TAG = "Node";
        public const string WALL_TAG = "Wall";
        

        #region GameplaySettings

        public const int MAX_LIVES = 3;


        #endregion


    }
}