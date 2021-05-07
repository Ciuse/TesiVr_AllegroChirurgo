using System.Collections;
using System.Collections.Generic;
using EventSystem2;
using UnityEngine;

public class Training_Haptic : MonoBehaviour
{
    private bool buttonStatus = false;
    public  GameObject hapticDevice = null;
    public int buttonID = 0;

    public GameEvent startingTrainingVocal;

    public bool startingVocal = false;
    public GameObject objectToPick;
    public GameObject collider;
    public GameObject box;
    public GameEvent startingTraining;
    public Canvas start_Training_Canvas;
    public Canvas objectToPickCanvas;

    public void Start()
    {
        start_Training_Canvas.enabled = true;
        objectToPickCanvas.enabled = false;
    }
    // Update is called once per frame
    void Update()
    {
        bool newButtonStatus = hapticDevice.GetComponent<HapticPlugin>().Buttons [buttonID] == 1;
        buttonStatus = newButtonStatus;
        if (buttonStatus && !startingVocal)
        {
            startingTrainingVocal.Raise();
            startingVocal = true;
            start_Training_Canvas.enabled = false;
            objectToPickCanvas.enabled = true;
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
