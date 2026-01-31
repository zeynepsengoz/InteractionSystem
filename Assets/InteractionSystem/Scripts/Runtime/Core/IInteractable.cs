using UnityEngine;

namespace InteractionSystem.Scripts.Runtime.Core
{
   
    public interface IInteractable
    {
       
        string Prompt { get; }

        
        Transform PromptAnchor { get; }

       
        bool CanInteract(GameObject interactor);

       
        void OnFocusEnter();

        
        void OnFocusExit();

        
        void Interact(GameObject interactor);
    }
} 