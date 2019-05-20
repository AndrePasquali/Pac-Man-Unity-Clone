using System.Linq;
using DroidDigital.Core;
using DroidDigital.Core.Constants;
using DroidDigital.PacMan.Helpers;
using UnityEngine;
using UnityEngine.UI;

namespace DroidDigital.PacMan.UI
{
    public class UIController: Singleton<UIController>
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

        public void OnGameOverUI()
        {
            _score.text = "0";
            
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

    }
}