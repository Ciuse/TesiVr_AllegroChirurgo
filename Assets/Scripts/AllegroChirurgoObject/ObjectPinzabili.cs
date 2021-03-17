using System;
using System.Collections;
using System.Collections.Generic;
using EventSystem;
using UnityEngine;

public class ObjectPinzabili  : DynamicObjectAbstract
{


    public bool isActive;
    public GameEvent electricEdgeTouched;
    public int idObject;
    public void Start()
    {
        StartHash();
        SaveState();
        isActive = false;

    }


    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Pinza"))
        {
            Interact();
        }

        if(hasInteract && other.gameObject.layer == LayerMask.NameToLayer("Electric Edge"))
        {
            ResetState();
            electricEdgeTouched.Raise();

        }
    }

    private void OnTriggerStay(Collider other)
    {
        if(hasInteract && other.gameObject.layer == LayerMask.NameToLayer("Electric Edge"))
        {
            ResetState();
            electricEdgeTouched.Raise();

        }
    }

    public override void SaveState()
    {
        SaveStatePosition();
    }

    public override void ResetState()
    {
        ResetStatePosition();
    }

    public void ActivateObject()
    {
        print("activate object");
        isActive = true;
    }

    public void DeActivateObject()
    {
        print("enter deactivate object");
        isActive = false;
    }
    
    public void ObjectEventCard(Interactable interactable)
    {
        int cardId = interactable.id;
        if (idObject == cardId)
        {
            print("è quello giusto"+interactable.id.ToString());
        }
        
        

    }
    
}
