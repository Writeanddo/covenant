using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RE.City
{

    public class CityEnviroment : MonoBehaviour
    {
        [SerializeField] Walker[] _shadowPrefabs;
        [SerializeField] Transform _startWaypoint;
        [SerializeField] Transform _endWaypoint;
        [SerializeField] float maxTime = 5;
        [SerializeField] float minTime = 2;

        private Walker _spawnObject;
        private float _time;
        private float _spawnTime;

        void Start()
        {
            SetRandomTime();
            _time = minTime;
        }

        void FixedUpdate()
        {

            _time += Time.deltaTime;

            if (_time >= _spawnTime)
            {
                SpawnObject();
                SetRandomTime();
            }

        }

        void SpawnObject()
        {
            _time = 0;
            _spawnObject = SetRandomObject();
            _spawnObject._startWaypoint = _startWaypoint;
            _spawnObject._endWaypoint = _endWaypoint;
            Instantiate(_spawnObject, _spawnObject._startWaypoint.position, Quaternion.identity);
        }

        private void SetRandomTime() => _spawnTime = Random.Range(minTime, maxTime);

        private Walker SetRandomObject() => _shadowPrefabs[Random.Range(0, _shadowPrefabs.Length)];
    }
}
