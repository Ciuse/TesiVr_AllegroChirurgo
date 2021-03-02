using System;
using System.Collections;
using System.Collections.Generic;
using EventSystem;
using UnityEngine;

public class ObjectPinzabili  : DynamicObjectAbstract
{


    public bool isActive;
    public bool resetting;
    public Material material;
    public GameEvent electricEdgeTouched;
    public void Start()
    {
        StartHash();
        SaveState();
        isActive = false;
        material = gameObject.GetComponent<MeshRenderer>().material;
    }
    

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Pinza"))
        {
            Interact();
        }
        
        if(hasInteract && other.gameObject.layer == LayerMask.NameToLayer("Electric Edge"))
        {
            resetting = true;
            electricEdgeTouched.Raise();
            ResetState();
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
        ResetColor();
    }

    public void ResetColor()
    {
        
       gameObject.GetComponent<MeshRenderer>().material.color=material.color;
    }
}

