﻿using System;
using System.Collections;
using UnityEngine;

namespace RE
{
    public class CandleLight : MonoBehaviour, IInteractable, ISelectable
    {
        public bool IsSelected { get; set; }
        public event Action Interacted;

        private Animator _animator;
        private bool _clickable = true;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        public void Interact()
        {
            Interacted.Invoke();
        }

        private void OnMouseDown()
        {
            if (_clickable)
                StartCoroutine(Co_Interact());
        }

        private void OnMouseOver()
        {
            if (!IsSelected)
            {
                IsSelected = true;
                _animator.SetBool("isSelected", true);
            }
        }

        private void OnMouseExit()
        {
            IsSelected = false;
            _animator.SetBool("isSelected", false);
        }

        private IEnumerator Co_Interact()
        {
            _clickable = false;
            Interact();
            yield return new WaitForSeconds(1.3f);
            _clickable = true;
        }
    }
}
