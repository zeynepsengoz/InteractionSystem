using UnityEngine;
using InteractionSystem.Scripts.Runtime.Core;
using InteractionSystem.Scripts.Runtime.Detection;

namespace InteractionSystem.Scripts.Runtime.Player
{
    public class PlayerInteractor : MonoBehaviour
    {
        [SerializeField] private RaycastInteractionDetector m_Detector;
        [SerializeField] private KeyCode m_InteractKey = KeyCode.E;

        private IInteractable m_CurrentTarget;

        private void Update()
        {
            if (m_Detector == null) return;

            var target = m_Detector.Detect();

            if (target != m_CurrentTarget)
            {
                if (m_CurrentTarget != null) m_CurrentTarget.OnFocusExit();

                m_CurrentTarget = target;

                if (m_CurrentTarget != null) m_CurrentTarget.OnFocusEnter();
            }

            if (Input.GetKeyDown(m_InteractKey) && m_CurrentTarget != null && m_CurrentTarget.CanInteract(gameObject))
            {
                m_CurrentTarget.Interact(gameObject);
            }
        }
    }
}