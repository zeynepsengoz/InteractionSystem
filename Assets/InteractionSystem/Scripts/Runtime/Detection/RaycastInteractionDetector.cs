// Assets/InteractionSystem/Scripts/Runtime/Detection/RaycastInteractionDetector.cs
using UnityEngine;
using InteractionSystem.Scripts.Runtime.Core;

namespace InteractionSystem.Scripts.Runtime.Detection
{
    /// <summary>
    /// Simple raycast-based interaction detector.
    /// </summary>
    public class RaycastInteractionDetector : MonoBehaviour
    {
        [SerializeField] private Camera m_Camera;
        [SerializeField] private float m_MaxDistance = 3f;
        [SerializeField] private LayerMask m_LayerMask;

        /// <summary>
        /// Returns the interactable the camera is looking at, or null.
        /// </summary>
        
        public IInteractable Detect()
        {
            if (m_Camera == null) return null;

            Ray ray = new Ray(
                m_Camera.transform.position,
                m_Camera.transform.forward
            );

            if (Physics.Raycast(ray, out RaycastHit hit, m_MaxDistance, m_LayerMask))
            {
                return hit.collider.GetComponentInParent<IInteractable>();
            }

            return null;
        }

    }
}