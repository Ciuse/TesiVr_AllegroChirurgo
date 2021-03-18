using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EventSystem;
using UnityEngine.Serialization;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class PinzaHapticFeedback : MonoBehaviour
{

   
    public XRBaseController leftControllerHand;
    public XRBaseController rightControllerHand;
    public bool enableVibration;

    public void PinzaTouchedElectricEdge()
    {

        if (enableVibration)
        {
            if (gameObject.layer == LayerMask.NameToLayer("ControllerLeft")){
           
                leftControllerHand.SendHapticImpulse(0.5f, 2);
            }
            if (gameObject.layer == LayerMask.NameToLayer("ControllerRight")){
                rightControllerHand.SendHapticImpulse(0.5f, 2);
            
            }
        }

    }
    
    public void ActivateVibration()
    {
        enableVibration = !enableVibration;
    }
}
