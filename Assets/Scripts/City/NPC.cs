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
        [SerializeField] public string id;
        [SerializeField] Scene _puzzleLevel;
        [SerializeField] UnityEvent _setInitialDialogue;
        [SerializeField] UnityEvent _setFinalDialogue;

        private Animator _animator;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            _setInitialDialogue?.Invoke();
        }

        public void SetNPCConclusion()
        {
            StartCoroutine(Co_SetNPCConclusion());
        }

        private IEnumerator Co_SetNPCConclusion()
        {
            _animator.SetTrigger("concluded");
            yield return new WaitForSeconds(2f);
            _setFinalDialogue?.Invoke();
        }

    }
}
