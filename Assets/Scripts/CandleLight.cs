using System;
using UnityEngine;

namespace RE
{
    public class CandleLight : MonoBehaviour, IInteractable
    {
        public event Action Interacted;

        public void Interact()
        {
            Interacted.Invoke();
        }

        private void OnMouseDown()
        {
            Interact();
        }
    }
}
