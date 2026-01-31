using UnityEngine;
using InteractionSystem.Scripts.Runtime.Core;

namespace InteractionSystem.Scripts.Runtime.Interactables
{
    public class SimpleInteractable : InteractableBase
    {
        public override void Interact(GameObject interactor)
        {
            Debug.Log($"{name} ile etkileşime girildi!");
        }
    }
}