using InteractionSystem.Scripts.Runtime.Core;
using UnityEngine;

public class LightInteractable : MonoBehaviour, IInteractable
{
    bool isOn = false;

    void Start()
    {
        GetComponent<Renderer>().material.color = Color.black;
    }
    public void InteractLogic()
    {
       isOn = !isOn;
       
       Debug.Log("Light currently is: " + (isOn ? "ON" : "OFF"));
       if(isOn == true)
       {
           GetComponent<Renderer>().material.color = Color.black;
       }
       if(isOn == false)
       {
           GetComponent<Renderer>().material.color = Color.yellow;
       }
    }
}