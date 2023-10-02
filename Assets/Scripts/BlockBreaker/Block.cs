using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using TMPro;

namespace Leedong.BlockBreaker
{
    public class Block : MonoBehaviour
    {
        [SerializeField]
        private SpriteRenderer _spriteRenderer;

        [SerializeField]
        private TextMeshProUGUI _textCount;

        private int _count = 10;

        public void SetCount(int count)
        {
            _count = count;

            _textCount.text = _count.ToString();
        }

        public void SetColor(Color color)
        {
            _spriteRenderer.color = color;
        }

        void OnCollisionEnter2D(Collision2D col)
        {
            _count--;

            _textCount.text = _count.ToString();

            if (_count <= 0)
            {
                Destroy(gameObject);
            }
        }

    }

}
