using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace Aquiris.Characters.Enemies.Enemies
{
    public class EnemyScoreFeedBack: MonoBehaviour
    {
        public List<Sprite> ScoreSprite;

        private int CurrentCombo => PacMan.Gameplay.Gameplay.GetCurrentCombo();

        public SpriteRenderer SpriteRenderer => _spriteRenderer ?? (_spriteRenderer = GetComponent<SpriteRenderer>());

        private SpriteRenderer _spriteRenderer;
       

        public void OnEnemyDie()
        {
            SetSprite();
        }

        public async void SetSprite()
        {
            var timeDuration = 1.0F;
            var counter = 0F;
            
            Debug.Log("Sprite is Set");

            while (counter < timeDuration)
            {
                counter += Time.time;
                
                SpriteRenderer.sprite = ScoreSprite[CurrentCombo];
                await Task.Delay(TimeSpan.FromSeconds(0.025F));
            }
        }
    }
}