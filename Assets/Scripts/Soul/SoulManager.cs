using System.Collections.Generic;
using UnityEngine;

namespace RE.Soul
{
    public class SoulManager : MonoBehaviour
    {
        [SerializeField] List<SoulState> _souls;
        [SerializeField] Transform[] _waypoints;
        [SerializeField] Queue<GameObject> _soulQueue = new Queue<GameObject>();

        private SoulSpawner _soulSpawner;
        private int _soulIndex = 0;

        private void Awake()
        {
            _soulSpawner = GetComponent<SoulSpawner>();
        }

        private void Update()
        {

            if (Input.GetKeyDown(KeyCode.Space))
            {
                Debug.Log(_souls[_soulIndex].name);
                SetSoulProps();
            }
        }

        private void SetSoulProps()
        {
            SoulState soulState = _souls[_soulIndex];
            Transform waypoint = _waypoints[_soulIndex];
            GameObject soul = _soulSpawner.SpawnSoul(soulState, waypoint);
            _soulQueue.Enqueue(soul);
            Debug.Log(_soulQueue.Count);
        }

    }
}
