using System;
using System.Collections;
using System.Collections.Generic;
using EventSystem2;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class StartingSimulationVr : MonoBehaviour
{
    public InputActionReference pressingA;
    public InputActionReference pressingX;
    public GameEvent startingVocalTrainingVR;
    public bool isStarted = false;
    public GameObject objectToPick;
    public GameObject collider;
    public GameObject box;
    public GameObject textStart;
    public GameEvent startingTraining;

    private void Update()
    {
        float buttonValueA = pressingA.action.ReadValue<float>();
        float buttonValueX = pressingX.action.ReadValue<float>();
        //if ((buttonValueA > 0.5f || buttonValueX > 0.5f ) && !isStarted)
        if ((Keyboard.current.aKey.wasPressedThisFrame||Keyboard.current.xKey.wasPressedThisFrame )&& !isStarted)
        {
            startingVocalTrainingVR.Raise();
            textStart.SetActive(false);
            isStarted = true;
        }
    }

    public void BoxActivate()
    {
        objectToPick.SetActive(true);
        box.SetActive(true);
       collider.SetActive(true);
       StartCoroutine(WaitBeforeStartTraining());
    }
    
    IEnumerator WaitBeforeStartTraining()
    {
        yield return new WaitForSeconds(0.3f);
        startingTraining.Raise();
        
    }
}
