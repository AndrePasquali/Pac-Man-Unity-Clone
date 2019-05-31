using System.Linq;
using Aquiris.Core;
using Aquiris.Core.Constants;
using Aquiris.PacMan.Helpers;
using Aquiris.PacMan.UI.StateMachine;
using UnityEngine;
using UnityEngine.UI;

namespace Aquiris.PacMan.UI
{
    public class HUDManager: Singleton<HUDManager>
    {
        #region Components

        [SerializeField]
        private Text _score;

        [SerializeField]
        private Text _highScore;

        [SerializeField]
        private Image[] _playerLives;

        #endregion

        private void Start()
        {
            Initialize.InitalizeGame();
        }

        public void UpdateScoreUI(int newScore)
        {
            var score = newScore.ToString();
            
            _score.text = score;
        }

        public void UpdateHighScoreUI(int newHighScore)
        {
            var highscore = newHighScore.ToString();

            _highScore.text = highscore;
        }

        public void OnGameStart()
        {
            ResetScore();
            ResetLivesUI();
        }

        public void UpdateLiveUI(int amount)
        {
            var totalLives = GameConstants.MAX_LIVES;
                  
            for (var i = amount; i < totalLives ; i++)
                _playerLives[i].enabled = false;
        }

        public void ResetLivesUI()
        {
            _playerLives.ToList().All(e => e.enabled = true);
        }

        public void ResetScore()
        {
            _score.text = "0";
        }
    }
}