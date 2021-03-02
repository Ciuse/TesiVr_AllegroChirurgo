using System;
using System.Collections;
using System.Collections.Generic;
using EventSystem;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XR.Haptics;
using UnityEngine.XR.Interaction.Toolkit;

public class ElectricEdgeScript : MonoBehaviour
{

    public GameEvent pinzaTouchElectricEdge;

    public int countCollision;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
       
        if (other.gameObject.layer == LayerMask.NameToLayer("Electric Edge"))
        {
            if (countCollision == 0)
            {
                pinzaTouchElectricEdge.Raise();
                print("bordo elettrico toccato");  
            }

            countCollision++;

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Electric Edge"))
        {
            countCollision--;

        }

    }
}
