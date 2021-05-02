using System;
using System.Collections;
using System.Collections.Generic;
using EventSystem2;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class StartingSimulationVr : MonoBehaviour
{
    public InputActionReference pressingA;
    public InputActionReference pressingX;
    public InputActionReference pressingGrip;
    public InputActionReference pressingTrigger;
    public GameEvent startingVocalTrainingVR;
    public bool isStarted = false;
    public GameObject objectToPick;
    public GameObject collider;
    public GameObject box;
    public TextMeshProUGUI text;
    public Image imageMappingButton;
    public GameEvent startingTraining;
    public float valueGrip;
    public float valueTrigger;

    private void Update()
    {
        valueGrip = pressingGrip.action.ReadValue<float>();
        valueTrigger = pressingTrigger.action.ReadValue<float>();
        float buttonValueA = pressingA.action.ReadValue<float>();
        float buttonValueX = pressingX.action.ReadValue<float>();
        if ((buttonValueA > 0.5f || buttonValueX > 0.5f ) && !isStarted)
        //if ((Keyboard.current.aKey.wasPressedThisFrame||Keyboard.current.xKey.wasPressedThisFrame )&& !isStarted)
        {
            startingVocalTrainingVR.Raise();
            text.enabled = false;
            imageMappingButton.enabled = false;
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
