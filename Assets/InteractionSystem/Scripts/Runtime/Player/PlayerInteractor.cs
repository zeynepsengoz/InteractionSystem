using InteractionSystem.Scripts.Runtime.Core;
using UnityEngine;


    public class PlayerInteractor : MonoBehaviour
    {
        [SerializeField] private LayerMask m_InteractLayer;
        [SerializeField] private float m_InteractRadius = 1.5f;
        [SerializeField] private KeyCode m_InteractKey = KeyCode.E;

        void Update()
        {
            if (Input.GetKeyDown(m_InteractKey))
            {
                Collider hit = Physics.OverlapSphere(
                    transform.position,
                    m_InteractRadius,
                    m_InteractLayer
                )[0];

                if (hit != null)
                {
                    Debug.Log("Hit: " + hit.name);
                    IInteractable interactable = hit.GetComponent<IInteractable>();

                    if (interactable != null)
                    {
                        Debug.Log("Interact Bulundu");
                        interactable.InteractLogic();
                    }
                }
            }
        }
        
        
        
        
        
        
        
    }
