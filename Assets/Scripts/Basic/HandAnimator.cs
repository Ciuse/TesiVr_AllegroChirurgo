    using System;
    using System.Collections.Generic;
using UnityEngine;
    using UnityEngine.InputSystem;
    using UnityEngine.XR.Interaction.Toolkit;
    using CommonUsages = UnityEngine.XR.CommonUsages;

    public class HandAnimator : MonoBehaviour
{
    public float speed = 5.0f;
    //public XRController controller = null;

    private Animator animator = null;
    public ActionBasedController controllerActionBase;

    public InputActionReference gripAction;
    public InputActionReference pointerAction;
    
    private readonly List<Finger> gripFingers = new List<Finger>()
    {
        new Finger(FingerType.Middle),
        new Finger(FingerType.Ring),
        new Finger(FingerType.Pinky),
    };
     
    private readonly List<Finger> pointFingers = new List<Finger>()
    {
        new Finger(FingerType.Index),
        new Finger(FingerType.Thumb),
    };
        
    void Start()
    {
        
       Debug.Log(controllerActionBase.selectAction.action.enabled);
       
    }    
        
        
        
        
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        //store input
        CheckGrip();
        CheckPointer();
      
        //smooth input values 
        SmoothFinger(pointFingers);
        SmoothFinger(gripFingers);
        
        //apply smoothed values
        AnimateFinger(pointFingers);
        AnimateFinger(gripFingers);
        
    }

    private void CheckGrip()
    {
        

        if (gripAction.action.enabled)
        {
            if (gripAction.action.triggered)
            {
                SetFingerTargets(gripFingers, gripAction.action.ReadValue<float>());
            }
                
        }
        
    }

    private void CheckPointer()
    {
        
        if (pointerAction.action.enabled)
        {
            if (pointerAction.action.triggered)
            {
                SetFingerTargets(pointFingers, pointerAction.action.ReadValue<float>());
            }
                
        }
        
        
    }

    private void SetFingerTargets(List<Finger> fingers, float value)
    {
        foreach (Finger finger in fingers)
        {
            finger.target = value;
        }
    }

    private void SmoothFinger(List<Finger> fingers)
    {
        foreach (Finger finger in fingers)
        {
            float time = speed * Time.unscaledDeltaTime;
            finger.current = Mathf.MoveTowards(finger.current, finger.target, time);
        }
    }

    private void AnimateFinger(List<Finger> fingers)
    {
        foreach (Finger finger in fingers)
        {
            AnimateFinger(finger.type.ToString(), finger.current);
        }
    }

    private void AnimateFinger(string finger, float blend)
    {
        animator.SetFloat(finger,blend);
    }
}