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
    public Sprite gripButton;
    public Sprite triggerButton;
    public GameEvent startingTraining;
    public float valueGrip;
    public float valueTrigger;
    public Training_VR_SFX trainingVRSfx;
    public Canvas infoTraining;
    public Canvas objectCanvas;

    private void Start()
    {
        objectCanvas.enabled = false;
        infoTraining.enabled = true;
    }

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
            text.text = "";
            imageMappingButton.sprite = null;
            isStarted = true;
        }

        if (trainingVRSfx.startedVocal1)
        {
            StartCoroutine(WaitBeforeShowGripButton());
        }
        if (trainingVRSfx.startedVocal2)
        {
            StartCoroutine(WaitBeforeShowTriggerButton());
        }
    }

    public void BoxActivate()
    {
        objectToPick.SetActive(true);
        box.SetActive(true); 
        collider.SetActive(true);
        infoTraining.enabled = false;
        objectCanvas.enabled = true;
        StartCoroutine(WaitBeforeStartTraining());
    }
    
    IEnumerator WaitBeforeStartTraining()
    {
        yield return new WaitForSeconds(0.3f);
        startingTraining.Raise();
        
    }
    IEnumerator WaitBeforeShowGripButton()
    {
        yield return new WaitForSeconds(6f);
        imageMappingButton.sprite = gripButton;
        imageMappingButton.preserveAspect = true;
        text.text = "PRESS AND HOLD GRIP";

    }
    IEnumerator WaitBeforeShowTriggerButton()
    {
        yield return new WaitForSeconds(4f);
        imageMappingButton.sprite = triggerButton;
        imageMappingButton.preserveAspect = true;
        text.text = "PRESS AND HOLD TRIGGER";
    }
    
    
}
