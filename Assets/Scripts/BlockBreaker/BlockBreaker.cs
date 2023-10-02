using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Leedong.BlockBreaker
{
    public class BlockBreaker : MonoBehaviour
    {
        [SerializeField]
        private Camera _camera;

        [SerializeField]
        private InputHandler _handler;

        [Header("Map")]

        [SerializeField]
        private Grid _grid;

        [SerializeField]
        private Tilemap _tilemap;

        [Header("Block")]

        [SerializeField]
        private Block _block;

        [SerializeField]
        private Color[] _blockColors;

        [Header("Ball")]

        [SerializeField]
        private GameObject _startBall;

        [SerializeField]
        private BallGuide _ballGuide;

        [SerializeField]
        private Transform _muzzle;

        [SerializeField]
        private BallPool _ballPool;

        private float _rotateSpeed = 10f;

        private float _valBefore;
        private float _pivot;

        private void Start()
        {
            if (_handler != null)
            {
                _handler.OnInputEvent += InputCallback;
            }

            if (_ballPool != null)
            {
                _ballPool.FinishEvent += FinishCallback;
            }

            // Init Blocks
            for (int y = 0; y <= 3; y++)
            {
                for (int x = -3; x <= 2; x++)
                {
                    Block block = Instantiate(_block, _block.transform.parent);

                    Vector3Int tilePosition = new Vector3Int(x, y, 0);
                    Vector3 pos = _grid.CellToWorld(tilePosition) + new Vector3(0.5f, 0.5f, 0);
                    block.transform.position = pos;

                    // Set Count
                    block.SetCount(10 + (y * 10));

                    // Set Color
                    block.SetColor(_blockColors[y]);

                    block.gameObject.SetActive(true);
                }
            }
        }

        private void InputCallback(Vector2 screenVec, TouchPhase touchPhase)
        {
            if (!_startBall.activeSelf) return;

            if (touchPhase == TouchPhase.Began)
            {
                Vector2 vec = _camera.ScreenToWorldPoint(screenVec);
                _valBefore = vec.x;
            }
            else if (touchPhase == TouchPhase.Moved)
            {
                Vector2 vec = _camera.ScreenToWorldPoint(screenVec);

                float touchDistance = vec.x - _valBefore;
                float move = touchDistance * _rotateSpeed;

                _pivot += move;
                _pivot = Mathf.Clamp(_pivot, -45f, 45f);

                _muzzle.rotation = Quaternion.Euler(0, 0, -_pivot);

                _valBefore = vec.x;
            }
            else if (touchPhase == TouchPhase.Ended)
            {
                Shoot();
            }
        }

        private void FinishCallback()
        {
            _startBall.SetActive(true);
        }

        private void Shoot()
        {
            _startBall.SetActive(false);

            Vector2 dir = (_ballGuide.Aim.transform.position - _muzzle.transform.position).normalized;

            _ballPool.Shoot(dir);
        }

    }
}


