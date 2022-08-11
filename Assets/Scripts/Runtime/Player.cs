using System;
using System.Collections.Generic;
using UnityEngine;

namespace Runtime
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private MazeAction mazeAction;
        [SerializeField] private Rigidbody2D rb;
        [SerializeField] private float moveSpeed = 5f;

        [SerializeField] private VictoryMenu victoryMenu;

        private Stack<Vector2Int> _path = new Stack<Vector2Int>();
        private bool _isMoving = false;
        private float _time;

        private Vector3 _startPos, _endPos;


        private void FixedUpdate()
        {
            if (_path.Count == 0) return;

            _time += Time.fixedDeltaTime * moveSpeed;

            if (_time >= 1f)
            {
                _path.Pop();
                if (_path.Count == 0)
                {
                    _isMoving = false;
                    return;
                }

                var pos = _path.Peek();

                _startPos = transform.localPosition;
                _endPos = new Vector3(pos.x, pos.y);
                _time = 0f;
                _isMoving = true;

                var dir = (_endPos - _startPos).normalized;

                float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90f;
                transform.rotation = Quaternion.AngleAxis(angle, Vector3.back);
            }

            transform.localPosition = Vector3.Lerp(_startPos, _endPos, _time);
        }

        public void Move()
        {
            if (_isMoving) return;
            var path = mazeAction.GetPath();
            _path = new Stack<Vector2Int>(path);
            transform.localPosition = Vector3.zero;
            _time = 1f;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!other.CompareTag("Target")) return;
            victoryMenu.Open();
        }
    }
}