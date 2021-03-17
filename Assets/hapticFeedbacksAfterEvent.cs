using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class hapticFeedbacksAfterEvent : MonoBehaviour
{
    public XRBaseController controllerUsedToPickObject;
    public  AudioClip pinzaTouchedElectricEdgeSound;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

   public  void hapticFeedbacks()
    {
        controllerUsedToPickObject.SendHapticImpulse(0, 0.5f);
    }
}
