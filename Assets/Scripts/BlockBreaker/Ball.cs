using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Leedong.BlockBreaker
{
    public class Ball : MonoBehaviour
    {
        private Rigidbody2D _rb;

        private float _lifeTime = 20f;
        private float _speed = 10f;

        public void Shoot(Vector2 dir)
        {
            gameObject.SetActive(true);

            if (_rb == null) _rb = GetComponent<Rigidbody2D>();

            _rb.velocity = dir * _speed;
        }

        private void Update()
        {
            if (transform.position.y < -5f)
            {
                gameObject.SetActive(false);
            }

            _lifeTime -= Time.deltaTime;

            if (_lifeTime < 0)
            {
                gameObject.SetActive(false);
            }
        }

        private void OnDisable()
        {
            _lifeTime = 20f;
            transform.localPosition = Vector2.zero;
        }
    }
}


