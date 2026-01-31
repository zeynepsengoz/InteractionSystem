using InteractionSystem.Scripts.Runtime.Core;
using UnityEngine;

public class ChestInteractable : MonoBehaviour, IInteractable
{
    
    public bool isOpen = false;
    public PlayerInteractor playerInteractor;

    void Start()
    {
        GetComponent<Renderer>().material.color = Color.red;
    }
    
    
    public void InteractLogic()
    {
        if (isOpen == false)
        {
            if (playerInteractor.m_KeyValue >= 1 && playerInteractor.m_Timer >= 3f)
            {
                Debug.Log("Sandık Açıldı");
                isOpen = true;
                GetComponent<Renderer>().material.color = Color.green;
                playerInteractor.m_Timer = 0;
            }
            if (playerInteractor.m_KeyValue <= 0)
            {
                Debug.Log("Sandık Kilitli, Anahtar Yok");
                playerInteractor.m_Timer = 0;
            }
     
        }
    }
}
