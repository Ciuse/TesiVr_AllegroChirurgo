﻿using System;
using System.Collections;
using System.Collections.Generic;
using EventSystem2;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XR.Haptics;
using UnityEngine.XR.Interaction.Toolkit;

public class HapticFeedbackTouchEdge : MonoBehaviour
{

    public GameEvent pinzaTouchElectricEdge;
    public GameEvent pinzaStopTouchElectricEdge;

    public int countCollision;


    private void OnTriggerEnter(Collider other)
    {
        
            if (other.gameObject.layer == LayerMask.NameToLayer("Electric Edge"))
            {
           
                if (countCollision == 0)
                {
                    pinzaTouchElectricEdge.Raise();
                }
                countCollision++;
            }
            
        
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Electric Edge"))
        {
            countCollision--;
            if (countCollision == 0)
            {
                pinzaStopTouchElectricEdge.Raise();
            }
        }

       

    }


}
