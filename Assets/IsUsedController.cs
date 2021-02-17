using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EventSystem;
using UnityEngine.Serialization;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class IsUsedController : MonoBehaviour
{

   
    public GameObject controllerUsed;
    public XRBaseController leftControllerHand;
    public XRBaseController rightControllerHand;
    public bool isEventLeftSend = false;
    public bool isEventRightSend = false;
   
    
   
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
//        if (controllerUsed.layer == LayerMask.NameToLayer("ControllerLeft")&& isEventLeftSend==false){
//           
//            leftControllerHand.SendHapticImpulse(0.5f, 2);
//            print("LEFT");
//            isEventLeftSend = true;
//        }
//        if (controllerUsed.layer == LayerMask.NameToLayer("ControllerRight")&& isEventRightSend==false){
//            rightControllerHand.SendHapticImpulse(0.5f, 2);
//            print("RIGHT");
//            isEventRightSend = true;
//        }
    }

    public void PinzaTouchedElectricEdge()
    {
        if (controllerUsed.layer == LayerMask.NameToLayer("ControllerLeft")){
           
            leftControllerHand.SendHapticImpulse(0.5f, 2);
        }
        if (controllerUsed.layer == LayerMask.NameToLayer("ControllerRight")){
            rightControllerHand.SendHapticImpulse(0.5f, 2);
            
        }
    }
}
