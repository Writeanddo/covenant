using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RE
{
    public class TutorialManager : MonoBehaviour
    {
        [SerializeField] List<GameObject> _tutorialParts;

        private GameManager _gameManager;
        private GameObject _actualPart;
        private int _index = 0;

        private void Awake()
        {
            _gameManager = FindObjectOfType<GameManager>();
        }

        private void Start()
        {
            _actualPart = _tutorialParts[_index];
        }

        public void ChangeTutorialPart()
        {
            _index++;
            if (_index >= _tutorialParts.Count)
            {
                _gameManager.SetChangeScene();
            }
            else
            {
                _actualPart.SetActive(false);
                _actualPart = _tutorialParts[_index];
                _actualPart.SetActive(true);
            }
        }
    }
}
