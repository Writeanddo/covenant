using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RE
{
    public abstract class NPC : MonoBehaviour, IInteractable
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
