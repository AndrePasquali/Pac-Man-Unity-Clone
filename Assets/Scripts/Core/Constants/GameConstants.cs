using System.IO;
using UnityEngine;

namespace Aquiris.Core.Constants
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
        public const string BLUE = "Blue";
        public const string FLASHBLUE = "Flashblue";
        public const string IDLE = "Idle";
        public const string FREEZE = "Freeze";

        public const string COMBO = "Combo";

        public const float LOW_SPEED = 2.5F;
        public const float NORMAL_SPEED = 5.0F;
        public const float MODERATE_SPEED = 6.5F;
        public const float FAST_SPEED = 8.0F;
        public const float SUPER_FAST_SPEED = 9.0F;
        public const float ULTRA_FAST_SPEED = 10.0F;

        public const string ISPACMAN = "IsPacMan";

        #endregion

        #region UIState

        public const string START_SCREEN = "StartScreen";
        public const string PREPARE_TO_START = "PrepareToStart";
        public const string LEVEL_START = "StartGame";
        public const string GAMESTART = "Gameplay";
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
        public const int MAX_DOTS = 293;
        public const float FREEZE_TIME = 1.0F;
        
        //ENEMIES
        public static readonly Vector3 INITIAL_BLINKY_POSITION = new Vector3(0, 4, 0);
        public static readonly Vector3 INITIAL_PINKY_POSITION = new Vector3(0, 1.8F, 0);
        public static readonly Vector3 INITAL_INKY_POSITION = new Vector3(-2, 0.5F, 0);
        public static readonly Vector3 INITIAL_CLYDE_POSITION = new Vector3(2, 0.5F, 0);
        //PLAYER
        public static readonly Vector3 INITIAL_PACMAN_POSITION = new Vector3(0, -8, 0);

        #endregion

        #region IO

        public static readonly string SAVE_FILE_NAME = "gamesave.sav";

        public static readonly string SAVE_PATH = Application.persistentDataPath;

        public static readonly string FULL_SAVE_PATH = Application.persistentDataPath + Path.DirectorySeparatorChar + SAVE_FILE_NAME;

        public static readonly string WAY_DIRECTIONS_PATH = Application.persistentDataPath;

        public static readonly string WAY_DIRECTIONS_FILE_NAME = "way_paths.path";

        public static readonly string WAY_DIRECTIONS_FULL_SAVE_PATH =
            Application.persistentDataPath + Path.DirectorySeparatorChar + WAY_DIRECTIONS_FILE_NAME;

        #endregion

        #region Animations

        public const float PLAYER_DEAD_DURATION = 3.0F;

        public const float GAMEOVER_SCREEN_DURATION = 2.5F;

        public const float GAMEPLAY_SCREEN_DURATION = 2.5F;

        public const float GAMEPLAY_SCREEN_PRE_START_DURATION = 2.0F;

        public const float LEVEL_COMPLETED_ANIMATION_DURATION = 5.0F;

        #endregion

        #region LevelAnimator

        public const string ENDGAME_ANIMATION = "EndGame";

        public const string GAMESTART_ANIMATION = "LevelStart";

        #endregion
    }
}