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

        #region Character States    
        public const string DEAD = "Dead";
        public const string ALIVE = "Alive";
        public const string VULNERABLE = "Vulnerable";
        public const string IDLE = "Idle";
        public const string FREEZE = "Freeze";
        #endregion

        #region UIState

        public const string START_SCREEN = "StartScreen";
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
        public const float PLAYER_RESPAWN_TIME = 3.0F;

        #endregion

        #region Animations

        public const float PLAYER_DEAD_DURATION = 3.0F;

        public const float GAMEOVER_SCREEN_DURATION = 2.5F;

        public const float GAMEPLAY_SCREEN_DURATION = 2.5F;

        public const float GAMEPLAY_SCREEN_PRE_START_DURATION = 2.0F;

        #endregion
    }
}