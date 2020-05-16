using System;
using UnityEngine;

namespace RE
{
    public class CandleLight : MonoBehaviour, IInteractable, ISelectable
    {
        public bool IsSelected { get; set; }
        public event Action Interacted;

        private Animator _animator;

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
            Interact();
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
    }
}
