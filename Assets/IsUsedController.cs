using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EventSystem;
using UnityEngine.Serialization;
using UnityEngine.XR;

public class IsUsedController : MonoBehaviour
{

    public GameEvent leftControllerUsedEvent;
    public GameEvent rightControllerUsedEvent;
    public GameObject controllerUsed;
    public bool isEventLeftSend = false;
    public bool isEventRightSend = false;
   
    
   
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (controllerUsed.layer == LayerMask.NameToLayer("ControllerLeft")&& isEventLeftSend==false){
           
            leftControllerUsedEvent.Raise();
            print("LEFT");
            isEventLeftSend = true;
        }
        if (controllerUsed.layer == LayerMask.NameToLayer("ControllerRight")&& isEventRightSend==false){
            rightControllerUsedEvent.Raise();
            print("RIGHT");
            isEventRightSend = true;
        }
    }
}
