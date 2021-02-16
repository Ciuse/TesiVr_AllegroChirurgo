using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.Serialization;

public class eventsManagement : MonoBehaviour
{


    public GameObject objectToPick;
    public Material validationMaterial;
    public BoxCollider colliderOverObject;
    
    
    public void activateCollider()
    {
        colliderOverObject.enabled = true;
    }
    
    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(objectToPick.tag))
        {
            objectToPick.GetComponent<MeshRenderer>().material=validationMaterial;
            print("Oggetto uscito dal collider");
        }
        

    }
}
