using InteractionSystem.Scripts.Runtime.Core;
using UnityEngine;

public class KeyInteractable : MonoBehaviour, IInteractable
{
    public PlayerInteractor playerInteractor;

    public void InteractLogic()
    {
        playerInteractor.m_KeyValue += 1;
        Destroy(gameObject);
        Debug.Log("Key Received!");

    }
}
