using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EventSystem2;
using UnityEngine.Serialization;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class PinzaHapticFeedback : MonoBehaviour
{

   
    public XRBaseController leftControllerHand;
    public XRBaseController rightControllerHand;
    public bool enableVibration;
    public bool startVibration;
    private float elapsed=0f;

    private void Start()
    {
        if (GameObject.Find("ManageJsonToSaveDB") != null)
        {
            ManageJsonAndSettingsVR manageJsonAndSettings = GameObject.Find("ManageJsonToSaveDB").GetComponent<ManageJsonAndSettingsVR>();
            enableVibration = manageJsonAndSettings.vibrationSetting;
        }
    }

    private void Update()
    {
        elapsed += Time.deltaTime;
        if (elapsed >= 0.2f)
        {
            elapsed = elapsed % 0.2f;
            if (startVibration)
            {
                if (gameObject.layer == LayerMask.NameToLayer("ControllerLeft"))
                {

                    leftControllerHand.SendHapticImpulse(0.5f, 0.2f);
                }

                if (gameObject.layer == LayerMask.NameToLayer("ControllerRight"))
                {
                    rightControllerHand.SendHapticImpulse(0.5f, 0.2f);
                }
            }
        }
    }

    public void PinzaTouchedElectricEdge()
    {

        if (enableVibration)
        {
            startVibration = true;
        }

    }
    
    public void PinzaStopTouchedElectricEdge()
    {
        if (enableVibration)
        {
            startVibration = false;
        }
    }
    
}
