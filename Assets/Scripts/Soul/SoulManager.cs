﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RE.Soul
{
    public class SoulManager : MonoBehaviour
    {
        [SerializeField] List<SoulState> _souls;
        [SerializeField] Transform[] _waypoints;
        [SerializeField] Transform _portalWaypoint;
        [SerializeField] Queue<ISoul> _soulQueue = new Queue<ISoul>();
        [SerializeField] int _queueLimit;
        [SerializeField] GameObject _portalPrefab;

        private SoulCheck _soulCheck;
        private CandleLight _candleLight;
        private Pen _pen;

        private SoulSpawner _soulSpawner;
        Transform _actualWaypoint;
        SoulState _actualSoulState;

        static System.Random rnd = new System.Random();

        private void Awake()
        {
            _soulCheck = GetComponent<SoulCheck>();
            _candleLight = FindObjectOfType<CandleLight>();
            _candleLight.Interacted += Burn;
            _pen = FindObjectOfType<Pen>();
            _pen.Interacted += Sign;
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

        private void Sign()
        {
            RemoveSoul(true);
        }

        private void Burn()
        {
            RemoveSoul(false);
        }

        private void RemoveSoul(bool sign)
        {
            ISoul soulBody = _soulQueue.Dequeue();
            Destroy(soulBody.SoulGameObject);
            _soulCheck.ItemsToCheck(soulBody.SoulState, sign);

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
            StartCoroutine(Co_OpenPortal());
            StartCoroutine(Co_InstantiatePaper());
            StartCoroutine(Co_SpawnSoul());
        }

        private IEnumerator Co_InstantiatePaper()
        {
            yield return new WaitForSeconds(1.2f);
            _soulSpawner.SpawnPaper(_soulQueue.Peek().SoulState);
        }

        private IEnumerator Co_SpawnSoul()
        {
            yield return new WaitForSeconds(0.2f);
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

        private IEnumerator Co_OpenPortal()
        {
            GameObject portal = Instantiate(_portalPrefab, new Vector2(_portalWaypoint.position.x, _portalWaypoint.position.y - 0.2f), Quaternion.identity);
            yield return new WaitForSeconds(2.3f);
            Destroy(portal);
        }
    }
}
