using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

namespace RE
{
    public class NPC : MonoBehaviour
    {
        [SerializeField] public NPCState _state;
        [SerializeField] UnityEvent _setInitialDialogue;
        [SerializeField] UnityEvent _setFinalDialogue;

        private Animator _animator;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        private void Start()
        {
            if (CheckFinalStatus())
            {
                _state.ChangeStatus(NPCStatus.Ended);
                SetNPCConclusion();
            }
            else if (CheckEndedStatus())
                SetEndAnimation();
        }

        

        private void OnTriggerEnter2D(Collider2D collision)
        {
            //if (_gameState.actualId != null && _gameState.actualId.Contains(_state.id))
            //return;
            if (CheckFinalStatus() || CheckEndedStatus())
                return;

            if (_state.greetingAnimation)
            {
                _animator.SetTrigger("greet");
            }
            Debug.Log("Entrou" + _state.npcStatus);
            _state.ChangeStatus(NPCStatus.Final);
            Debug.Log("Mudou" + _state.npcStatus);

            _setInitialDialogue?.Invoke();
            collision.GetComponent<Character>().StopMove();
        }

        public NPC SetStatus()
        {
            if (CheckFinalStatus())
            {
                _state.ChangeStatus(NPCStatus.Ended);
                SetNPCConclusion();
                return this;
            }
            else if (CheckEndedStatus())
                SetEndAnimation();
            return null;
        }

        public void SetNPCConclusion()
        {
            StartCoroutine(Co_SetNPCConclusion());
        }

        private IEnumerator Co_SetNPCConclusion()
        {
            SetEndAnimation();
            yield return new WaitForSeconds(2f);
            _setFinalDialogue?.Invoke();
        }

        public void SetEndAnimation()
        {
            _animator.SetTrigger("concluded");
        }

        public bool CheckFinalStatus() => _state.npcStatus == NPCStatus.Final;
        public bool CheckEndedStatus() => _state.npcStatus == NPCStatus.Ended;

    }

    public enum NPCStatus
    {
        Initial,
        Final,
        Ended
    }
}
