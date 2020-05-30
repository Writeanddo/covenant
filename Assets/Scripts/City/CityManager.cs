using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace RE.City
{
    public class CityManager : MonoBehaviour
    {
        [SerializeField] List<NPC> _npcs;
        public GameState _gameState;

        private Character _character;
        private CityCamera _cityCamera;
        private int _actualNPC;
        private MainManager _mainManager;

        private bool tempZeroBool = true;

        private void Awake()
        {
            _character = FindObjectOfType<Character>();
            _cityCamera = FindObjectOfType<CityCamera>();
            _mainManager = FindObjectOfType<MainManager>();
            _gameState = _mainManager._gameState;
        }

        private void Start()
        {
            _mainManager._gameState.TempZeroGameState(); /// REMOVER DEPOIS

            SetCharacterPosition();
            _cityCamera.SetNewPos();
            SetNPCAnimationAndDialog();
        }

        private void SetCharacterPosition()
        {
            _character.transform.position = _mainManager._gameState.characterPosition;
        }

        private void SetNPCAnimationAndDialog()
        {
            if (_gameState.actualId == null)
                return;

            _character.SetIsNotClickable();
           /* foreach (NPC npc in _npcs)
            {
                NPC actualNpc = npc.SetStatus();
                if (actualNpc != null)
                {
                    _actualNPC = actualNpc;
                }
            }*/
        }

        public void SetNPC(int index)
        {
            _actualNPC = index;
        }

        public void SetTutorialScene()
        {
            Debug.Log("index" + _actualNPC);
            Debug.Log("length" + _mainManager._npcStates);
            Debug.Log(_mainManager._npcStates[_actualNPC].id);
            Debug.Log(_mainManager._npcStates[_actualNPC].tutorialLevelIndex);
            _mainManager._gameState.SetActualId(_mainManager._npcStates[_actualNPC].id);
            _mainManager._gameState.SetCharacterPosition(_character.transform.position);
            SceneManager.LoadScene(_mainManager._npcStates[_actualNPC].tutorialLevelIndex);
        }

        public void ChangeEndScene(int sceneIndex)
        {
            StartCoroutine(Co_ChangeEndScene(sceneIndex));
        }

        private IEnumerator Co_ChangeEndScene(int sceneIndex)
        {
            yield return new WaitForSeconds(4f);
            SceneManager.LoadScene(sceneIndex);
        }
    }
}
