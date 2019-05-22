using UnityEngine;
using UnityEngine.UI;

namespace DroidDigital.Characters.Enemies.Enemies
{
    public class EnemyUI: MonoBehaviour
    {
        public Sprite ScoreSprite;

        public SpriteRenderer SpriteRenderer => _spriteRenderer ?? (_spriteRenderer = GetComponent<SpriteRenderer>());

        private SpriteRenderer _spriteRenderer;

        public void SetSprite()
        {
            SpriteRenderer.sprite = ScoreSprite;
        }
    }
}