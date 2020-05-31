using UnityEngine;
using System.Collections;

namespace RE.City
{
    public class Walker : MonoBehaviour
    {
        [SerializeField] float _maxMoveSpeed;
        [SerializeField] float _minMoveSpeed;

        public Transform _startWaypoint;
        public Transform _endWaypoint;
        private float _moveSpeed;

        private void Start()
        {
            _moveSpeed = Random.Range(_minMoveSpeed, _maxMoveSpeed);
        }

        private void FixedUpdate()
        {
            transform.position = Vector2.MoveTowards(transform.position, _endWaypoint.position, _moveSpeed * Time.deltaTime);

            if (Vector2.Distance(transform.position, _endWaypoint.position) < 1f)
                Destroy(gameObject);
        }

    }
}
