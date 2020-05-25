using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace RE
{
    public class StatusSpeech : MonoBehaviour
    {
        [SerializeField] Color _colorSuccess;
        [SerializeField] string _textSuccess;
        [SerializeField] Color _colorFail;
        [SerializeField] string _textFail;

        private Animation _animation;
        private SpriteRenderer _sprite;
        private TextMeshPro _text;

        private void Awake()
        {
            _animation = GetComponent<Animation>();
            _sprite = GetComponent<SpriteRenderer>();
            _text = GetComponentInChildren<TextMeshPro>();
        }

        public void SetMessage(bool success)
        {
            if (success)
            {
                _sprite.color = _colorSuccess;
                _text.text = _textSuccess;
                _animation.Play();
            }
            else
            {
                _sprite.color = _colorFail;
                _text.text = _textFail;
                _animation.Play();
            }
        }

    }
}
