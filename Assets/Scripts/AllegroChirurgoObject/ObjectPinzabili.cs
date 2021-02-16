using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPinzabili  : DynamicObjectAbstract
{
    public void Start()
    {
        StartHash();
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
}

