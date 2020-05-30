using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;

namespace RE
{
    public class AltarCanvas : MonoBehaviour
    {
        [SerializeField] TextMeshPro _textFail;
        [SerializeField] TextMeshPro _textSuccess;
        [SerializeField] TextMeshPro _status;
        [SerializeField] TextMeshPro _BtText;
        [SerializeField] GameObject _ui;

        private Animator _animator;
        private GameManager _gameManager;
        private bool _success = false;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
            _gameManager = FindObjectOfType<GameManager>();
        }

        private void OnMouseDown() => ChangeScene();

        public void SetStatus(bool success, string successText, string failText)
        {
            StartCoroutine(Co_SetStatus(success, successText, failText));
        }

        private IEnumerator Co_SetStatus(bool success, string successText, string failText)
        {
            yield return new WaitForSeconds(.75f);
            _ui.SetActive(true);
            _success = success;
            _textFail.text = failText;
            _textSuccess.text = successText;
            if (success)
            {
                _status.text = "Successful covenant!";
                _BtText.text = "Next >";
            }
            else
            {
                _status.text = "Failure!";
                _BtText.text = "Try again >";
            }
        }

        public void ChangeScene()
        {
            if (_success)
            {
                _animator.SetTrigger("close");
                _ui.SetActive(false);
                _gameManager.SetChangeScene(true); //True se tem transição
            }
            else
                StartCoroutine(Co_ChangeScene());
        }

        private IEnumerator Co_ChangeScene()
        {
            _animator.SetTrigger("close");
            _ui.SetActive(false);
            yield return new WaitForSeconds(0.75f);
            _gameManager.ResetScene();
        }
    }
}
