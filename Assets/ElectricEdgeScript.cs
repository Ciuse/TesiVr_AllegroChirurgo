using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XR.Haptics;
using UnityEngine.XR.Interaction.Toolkit;

public class ElectricEdgeScript : MonoBehaviour
{
    private XRBaseController controller;
    private XRBaseInteractor interactor;
    
    
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
          
        }
    }

    public void GetXrController()
    {
        XRGrabInteractable grabInteractable;
    }
}
