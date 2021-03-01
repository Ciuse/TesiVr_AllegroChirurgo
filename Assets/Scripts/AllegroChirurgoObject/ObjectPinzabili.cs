using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPinzabili  : DynamicObjectAbstract
{


    public bool isActive;
    private Material material;
    
    
    
    
    public void Start()
    {
        StartHash();
        isActive = false;
       
    }

    

    private void OnTriggerEnter(Collider other)
    {
        Interact();
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
        
    }
}

