using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace RE
{
    public class AltarCanvas : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI _textFail;
        [SerializeField] TextMeshProUGUI _textSuccess;
        [SerializeField] TextMeshProUGUI _status;
        [SerializeField] Button _buttonSuccess;
        [SerializeField] Button _buttonFail;
        [SerializeField] Animator _animator;

        private GameManager _gameManager;

        private void Awake()
        {
            _gameManager = FindObjectOfType<GameManager>();
        }

        public void SetStatus(bool success, string successText, string failText)
        {
            _textFail.text = failText;
            _textSuccess.text = successText;
            if (success)
            {
                _status.text = "Sucesso!";
                _buttonSuccess.gameObject.SetActive(true);
            }
            else
            {
                _status.text = "Falhou!";
                _buttonFail.gameObject.SetActive(true);
            }
        }

        public void ChangeScene()
        {
            StartCoroutine(Co_ChangeScene());
        }

        private IEnumerator Co_ChangeScene()
        {
            _animator.SetTrigger("close");
            yield return new WaitForSeconds(0.4f);
            _gameManager.ResetScene();
        }
    }
}
