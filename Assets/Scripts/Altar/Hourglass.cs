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

        private float _time;


        private void Awake()
        {
            _animator = GetComponent<Animator>();
            _text = GetComponentInChildren<TextMeshPro>();
            _eachLevelTime = Helpers.DivideNumberByLevel(_sceneTime, 4); ;
            _reachLevelTime = _eachLevelTime * 3;
        }

        private void Start()
        {
            InitializeTime();
        }


        private void Update()
        {
            _time -= Time.deltaTime;
            SetLevel();

            if (_time > 0)
                ShowText();

            if (_time <= 0 && !_isTimeUp)
            {
                _isTimeUp = true;
                SetTimeUp();
            }
        }

        public void InitializeTime () => _time = _sceneTime;

        private void ShowText() => _text.text = FloatToMinutes(_time);

        private void SetLevel()
        {
            if (_sceneTime < _reachLevelTime)
            {
                _reachLevelTime -= _eachLevelTime;
                _level += 1;
                _animator.SetInteger("level", _level);
            }
           // if (_level > 3) //Usar caso precise de timer apenas no fim da cena
                //_showText = true;
        }

        private void SetTimeUp() => TimeUp?.Invoke();

        private static string FloatToMinutes(float time) => TimeSpan.FromSeconds(time).ToString("mm':'ss");

    }
}
