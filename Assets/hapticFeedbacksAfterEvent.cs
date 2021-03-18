using System.Collections;
using System.Collections.Generic;
using EventSystem;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class hapticFeedbacksAfterEvent : MonoBehaviour
{
    public XRBaseController controllerUsedToPickObject;
    public  AudioClip pinzaTouchedElectricEdgeSound;

    public GameObject handLeft;
    public GameObject handRight;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void activateGameEventListenersVibrationLeftHand()
    {
        GameEventListener eventListenerLeftHand = handLeft.GetComponent<GameEventListener>();
        eventListenerLeftHand.enabled = !eventListenerLeftHand.enabled;
        print("attivata vibrazione mano sinistra");
        print(eventListenerLeftHand.enabled);
    }

    public void activateGameEventListenersVibrationRightHand()
    {
        GameEventListener eventListenerRightHand = handRight.GetComponent<GameEventListener>();
        eventListenerRightHand.enabled = !eventListenerRightHand.enabled;
        print("attivata vibrazione mano destra");
        print(eventListenerRightHand.enabled);
    }
    
   public  void hapticFeedbacks()
    {
        controllerUsedToPickObject.SendHapticImpulse(0, 0.5f);
    }
   
}
