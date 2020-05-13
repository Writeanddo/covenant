using UnityEngine;
using System.Collections.Generic;
using System;

namespace RE
{
    public class SoulManager : MonoBehaviour
    {
        [SerializeField] SoulDictionary _soulDictionary;
        [SerializeField] List<SoulState> _souls;

        private int _soulIndex = 0;

        private void Update()
        {

            if (Input.GetKeyDown(KeyCode.Space))
            {
                Debug.Log(_souls[_soulIndex].name);
                InstantiateSoul();
            }
        }

        private void InstantiateSoul()
        {
            SoulState soulState = _souls[_soulIndex];
            //GameObject soulBody = _soulDictionary.soulBodyDictionary[soulState.soulBody];

            //GameObject soul = Instantiate(soulBody, transform.position, Quaternion.identity);
        }
    }
}
