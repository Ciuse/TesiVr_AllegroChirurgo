using System;
using System.Collections;
using System.Collections.Generic;
using EventSystem2;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class StartingSimulationVrRightHand : MonoBehaviour
{
    public InputActionReference pressingA;
    public GameEvent startingVocalTrainingVR;
    public bool isStarted = false;
    public GameObject objectToPick;
    public GameObject collider;
    public GameObject box;
    public GameObject textStart;
    public GameEvent startingTraining;

    private void Update()
    {
        float buttonValue = pressingA.action.ReadValue<float>();
        //if (buttonValue > 0.5f && !isStarted)
        if (Keyboard.current.aKey.wasPressedThisFrame && !isStarted)
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
