using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StatusScore : MonoBehaviour
{
    [SerializeField] TextMeshPro _textCorrect;
    [SerializeField] TextMeshPro _textWrong;

    int correctNumber = 0;
    int wrongNumber = 0;

    public void SetScore(bool success)
    {
        if (success)
        {
            correctNumber++;
            _textCorrect.text = correctNumber.ToString();
        }
        else
        {
            wrongNumber++;
            _textWrong.text = wrongNumber.ToString();
        }

    }
}
