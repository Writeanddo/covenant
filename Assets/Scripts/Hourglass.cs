using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;
using System;

namespace RE
{
    public class Hourglass : MonoBehaviour
    {
        [SerializeField] float _sceneTime;
        [SerializeField] UnityEvent TimeUp;

        private Animator _animator;
        private TextMeshPro _text;
        private float _eachLevelTime;
        private float _reachLevelTime;
        private int _level = 1;
        private bool _isTimeUp;
        private bool _showText;


        private void Awake()
        {
            _animator = GetComponent<Animator>();
            _text = GetComponentInChildren<TextMeshPro>();
            _eachLevelTime = Helpers.DivideNumberByLevel(_sceneTime, 4); ;
            _reachLevelTime = _eachLevelTime * 3;
        }

        private void Update()
        {
            _sceneTime -= Time.deltaTime;
            SetLevel();

            if (_sceneTime > 0 && _showText)
                ShowText();

            if (_sceneTime <= 0 && !_isTimeUp)
            {
                _isTimeUp = true;
                SetTimeUp();
            }
        }

        private void ShowText() => _text.text = Convert.ToInt32(_sceneTime).ToString();

        private void SetLevel()
        {
            if (_sceneTime < _reachLevelTime)
            {
                _reachLevelTime -= _eachLevelTime;
                _level += 1;
                _animator.SetInteger("level", _level);
            }
            if (_level > 3)
                _showText = true;
        }

        private void SetTimeUp() => TimeUp?.Invoke();
    }
}
