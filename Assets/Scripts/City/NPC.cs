using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace RE
{

    public class NPC : MonoBehaviour
    {
        [SerializeField] int _index;
        [SerializeField] UnityEvent _setInitialDialogue;
        [SerializeField] UnityEvent _setFinalDialogue;

        //private NPCState _state;
        private Animator _animator;
        private MainManager _mainManager;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
            _mainManager = FindObjectOfType<MainManager>();
            //_state = _mainManager._npcStates[_index];
        }

        private void Start()
        {
            if (CheckFinalStatus())
            {
                _mainManager._npcStates[_index].ChangeStatus(NPCStatus.Ended);
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

            _mainManager._npcStates[_index].ChangeStatus(NPCStatus.Final);


            _setInitialDialogue?.Invoke();
            collision.GetComponent<Character>().StopMove();
        }

        public NPC SetStatus()
        {
            if (CheckFinalStatus())
            {
                _mainManager._npcStates[_index].ChangeStatus(NPCStatus.Ended);
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

        public bool CheckFinalStatus() => _mainManager._npcStates[_index].npcStatus == NPCStatus.Final;
        public bool CheckEndedStatus() => _mainManager._npcStates[_index].npcStatus == NPCStatus.Ended;

    }

    [Serializable]
    public enum NPCStatus
    {
        Initial,
        Final,
        Ended
    }
}
