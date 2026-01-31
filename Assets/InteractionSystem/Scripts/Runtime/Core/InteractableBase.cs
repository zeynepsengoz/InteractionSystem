
using UnityEngine;

namespace InteractionSystem.Scripts.Runtime.Core
{
    /// <summary>
    /// Minimal base class for interactable objects.
    /// </summary>
    public abstract class InteractableBase : MonoBehaviour, IInteractable
    {
        [SerializeField] private string m_Prompt = "Interact";
        [SerializeField] private Transform m_PromptAnchor;
        [SerializeField] private float m_MaxInteractDistance = 2f;

        public string Prompt => m_Prompt;

        public Transform PromptAnchor =>
            m_PromptAnchor != null ? m_PromptAnchor : transform;

        public bool CanInteract(GameObject interactor)
        {
            if (!enabled || interactor == null)
                return false;

            return Vector3.Distance(
                interactor.transform.position,
                transform.position
            ) <= m_MaxInteractDistance;
        }

        // MVP: boş bırakıldı, türeyen sınıflar isterse override eder
        public virtual void OnFocusEnter() { }

        public virtual void OnFocusExit() { }

        public abstract void Interact(GameObject interactor);
    }
}
