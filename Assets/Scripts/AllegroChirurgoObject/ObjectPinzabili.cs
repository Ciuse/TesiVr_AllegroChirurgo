using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPinzabili  : DynamicObjectAbstract
{


    public bool isActive;
    public Material material;
    public void Start()
    {
        StartHash();
        isActive = false;
        material = gameObject.GetComponent<MeshRenderer>().material;
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

