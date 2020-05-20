using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

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

        private int _successStatus = 0;
        private int _failStatus = 0;

        private void Awake()
        {

        }

        private void Start()
        {
            Cursor.SetCursor(_arrow, Vector2.zero, CursorMode.ForceSoftware);
            _successText.text = _successNumber.ToString();
            _failText.text = _failNumber.ToString();
        }

        public void SetFail()
        {
            _failStatus++;
            _failTextStatus.text = _failStatus.ToString();
            _demon.SetStatus();

            if(_failStatus >= _failNumber)
                GameEnd();
        }

        public void SetSuccess()
        {
            _successStatus++;
            _successTextStatus.text = _successStatus.ToString();
        }

        public void GameEnd()
        {
            Debug.Log("Acbou!!");
        }

        public void SetScore(bool success)
        {
            if (success)
                SetSuccess();
            else
                SetFail();

        }

    }
}
