using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Leedong.BlockBreaker
{
    public class BallGuide : MonoBehaviour
    {
        [SerializeField]
        private Transform _aim;

        public Transform Aim => _aim;

        private int _layerMask;

        private void OnEnable()
        {
            _aim.gameObject.SetActive(true);
        }

        private void Start()
        {
            _layerMask = LayerMask.GetMask("Wall", "Block");
        }

        private void FixedUpdate()
        {
            RaycastHit2D hit = Physics2D.CircleCast(transform.position, 0.125f /* (Bullet Scale / 2) */, transform.up, Mathf.Infinity, _layerMask);

            if (hit.collider != null)
            {
                _aim.position = hit.point;
            }
            else
            {
                _aim.position = new Vector2(0, -999f);
            }
        }

        private void OnDisable()
        {
            _aim.gameObject.SetActive(false);
        }
    }

}

