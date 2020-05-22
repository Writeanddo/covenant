using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System;

namespace RE
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] Texture2D _arrow;
        [SerializeField] TextMeshPro _successTextStatus;
        [SerializeField] TextMeshPro _failTextStatus;

        [SerializeField] int _successNumber;
        [SerializeField] int _failNumber;
        [SerializeField] TextMeshPro _successText;
        [SerializeField] TextMeshPro _failText;
        [SerializeField] Demon _demon;
        [SerializeField] AltarCanvas _altarCanvas;

        private MainCamera _mainCamera;

        private Scene _actualScene;
        private int _successStatus = 0;
        private int _failStatus = 0;

        private void Awake()
        {
            _mainCamera = FindObjectOfType<MainCamera>();
            _actualScene = SceneManager.GetActiveScene();
        }

        private void Start()
        {
            Cursor.SetCursor(_arrow, Vector2.zero, CursorMode.ForceSoftware);
            _successText.text = _successNumber.ToString();
            _failText.text = _failNumber.ToString();
        }

        public void GameEnd() ///REFATORAR
        {
            _altarCanvas.gameObject.SetActive(true);
            _altarCanvas.SetStatus(CheckSuccess(), SetText(_successStatus, _successNumber), SetText(_failStatus, _failNumber));
        }

        private string SetText(int status, int number) => status.ToString() + " / " + number.ToString();

        private bool CheckSuccess()
        {
            if (_failStatus >= _failNumber)
                return false;
            else if (_successStatus >= _successNumber)
                return true;
            return false;

        }

        public void SetScore(bool success)
        {
            if (success)
                SetSuccess();
            else
                SetFail();

        }

        public void SetSuccess()
        {
            _successStatus++;
            _successTextStatus.text = _successStatus.ToString();
        }

        public void SetFail()
        {
            _failStatus++;
            _failTextStatus.text = _failStatus.ToString();
            _demon.SetStatus();

            _mainCamera.ChromaticPulse();
            _mainCamera.ShakeCamera();

            if (_failStatus >= _failNumber)
                GameEnd();
        }

        public void SetChangeScene()
        {
            SceneManager.LoadScene(_actualScene.buildIndex + 1);
        }

        public void ResetScene()
        {
            SceneManager.LoadScene(_actualScene.buildIndex);
        }
        

    }
}
