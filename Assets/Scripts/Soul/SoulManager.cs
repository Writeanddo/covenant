using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RE.Soul
{
    public class SoulManager : MonoBehaviour
    {
        [SerializeField] SoulChecker _soulChecker;
        [SerializeField] List<SoulState> _souls;
        [SerializeField] Transform[] _waypoints;
        [SerializeField] Transform _portalWaypoint;
        [SerializeField] Queue<ISoul> _soulQueue = new Queue<ISoul>();
        [SerializeField] int _queueLimit;

        private CandleLight _candleLight;
        private Pen _pen;

        private SoulSpawner _soulSpawner;
        Transform _actualWaypoint;
        SoulState _actualSoulState;

        static System.Random rnd = new System.Random();

        private void Awake()
        {
            _candleLight = FindObjectOfType<CandleLight>();
            _candleLight.Interacted += PaperBurn;
            _pen = FindObjectOfType<Pen>();
            _pen.Interacted += PaperSign;
            _soulSpawner = GetComponent<SoulSpawner>();
        }

        private void Start()
        {
            SetInitialSoulQueue();
        }

        private void SetInitialSoulQueue()
        {
            for (int i = 0; i < _queueLimit; i++)
            {
                _actualWaypoint = _waypoints[i];
                GetRandomSoul();
                EnqueueSoul();
                if (_queueLimit - 1 == i)
                    _soulSpawner.SpawnPaper(_actualSoulState);
            }
        }

        private void Update()
        {

            if (Input.GetKeyDown(KeyCode.Space))
            {
                EnqueueSoul();
            }
        }

        private void EnqueueSoul()
        {
            _soulQueue.Enqueue(SetSoulProps());

        }

        private ISoul SetSoulProps()
        {
            return new ISoul
            {
                SoulGameObject = _soulSpawner.SpawnSoul(_actualSoulState, _actualWaypoint),
                SoulState = _actualSoulState
            };
        }

        private void GetRandomSoul()
        {
            int r = rnd.Next(_souls.Count);
            _actualSoulState = _souls[r];
        }

        private void PaperSign()
        {
            RemoveSoul();
        }

        private void PaperBurn()
        {
            RemoveSoul();
        }

        private void RemoveSoul()
        {
            ISoul soulBody = _soulQueue.Dequeue();
            Destroy(soulBody.SoulGameObject);
            _soulChecker.ItemsToCheck(soulBody.SoulState);

            MoveAndAddSoul();
        }

        private void MoveAndAddSoul()
        {
            List<GameObject> souls = new List<GameObject>();
            int index = 0;
            foreach (ISoul soul in _soulQueue)
            {
                StartCoroutine(MoveToWaypoint(soul.SoulGameObject, index));
                index++;
            }
            StartCoroutine(Co_InstantiatePaper());
            StartCoroutine(Co_SpawnSoul());

        }

        private IEnumerator Co_InstantiatePaper()
        {
            yield return new WaitForSeconds(2f);
            _soulSpawner.SpawnPaper(_soulQueue.Peek().SoulState);
        }

        private IEnumerator Co_SpawnSoul()
        {
            _actualWaypoint = _portalWaypoint;
            GetRandomSoul();
            ISoul soul = SetSoulProps();
            yield return new WaitForSeconds(0.4f);
            StartCoroutine(MoveToWaypoint(soul.SoulGameObject, _waypoints.Length - 1));
            _soulQueue.Enqueue(soul);
        }

        private IEnumerator MoveToWaypoint(GameObject soul, int index)
        {
            Transform soulT = soul.transform;
            float speed = 2.8f;
            Transform _nextWaypoint = _waypoints[index];

            while (_nextWaypoint.position != soulT.position)
            {
                soulT.position = Vector2.MoveTowards(soulT.position, _nextWaypoint.position, speed * Time.deltaTime);
                yield return null;
            }
        }
    }
}
