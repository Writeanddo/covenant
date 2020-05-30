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
        [SerializeField] GameState _gameState;

        private Character _character;
        private CityCamera _cityCamera;
        private NPC _actualNPC;

        private bool tempZeroBool = true;

        private void Awake()
        {
            _character = FindObjectOfType<Character>();
            _cityCamera = FindObjectOfType<CityCamera>();
        }

        private void Start()
        {
            _gameState.TempZeroGameState(); /// REMOVER DEPOIS

            SetCharacterPosition();
            _cityCamera.SetNewPos();
            SetNPCAnimationAndDialog();
        }

        private void SetCharacterPosition()
        {
            _character.transform.position = _gameState.characterPosition;
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

        public void SetNPC(NPC npc)
        {
            _actualNPC = npc;
        }

        public void SetTutorialScene()
        {
            _gameState.SetActualId(_actualNPC._state.id);
            _gameState.SetCharacterPosition(_character.transform.position);
            SceneManager.LoadScene(_actualNPC._state.tutorialLevelIndex);
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
