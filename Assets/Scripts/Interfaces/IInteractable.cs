using System;

namespace RE
{
    public interface IInteractable
    {
        event Action Interacted;
        void Interact();
    }
}
