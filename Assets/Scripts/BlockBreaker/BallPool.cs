using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Leedong.BlockBreaker
{
    public class BallPool : MonoBehaviour
    {
        public event Action FinishEvent;

        [SerializeField]
        private Ball _initBall;

        private List<Ball> _balls;

        private void Start()
        {
            _balls = new List<Ball>() { _initBall };

            // 30 balls
            for (int i = 0; i < 29; i++)
            {
                Ball ball = Instantiate(_initBall, _initBall.transform.parent);
                _balls.Add(ball);
            }
        }

        public void Shoot(Vector2 dir)
        {
            StartCoroutine(StartShoot(dir));
        }

        private IEnumerator StartShoot(Vector2 dir)
        {
            for (int i = 0; i < _balls.Count; i++)
            {
                Ball ball = _balls[i];
                ball.Shoot(dir);

                yield return new WaitForSeconds(0.1f);
            }

            yield return new WaitForSeconds(2f);

            while (true)
            {
                bool isFinished = true;

                for (int i = 0; i < _balls.Count; i++)
                {
                    if (_balls[i].gameObject.activeSelf)
                    {
                        isFinished = false;
                        break;
                    }
                }

                if (isFinished) break;

                yield return new WaitForSeconds(0.25f);
            }

            FinishEvent?.Invoke();
        }

    }
}


