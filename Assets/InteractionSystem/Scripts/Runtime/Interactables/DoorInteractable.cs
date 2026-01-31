using UnityEngine;
using InteractionSystem.Scripts.Runtime.Core;



    public class DoorInteractable : MonoBehaviour, IInteractable
    {
        public bool isOpen = true;

        public void InteractLogic()
        {
            Debug.Log("Interacting with door");

            if (isOpen) return;
            isOpen = true;
            
            Debug.Log("Kapı Açıldı");
        }
    }
