using System.Collections;
using System.Collections.Generic;
using EventSystem2;
using UnityEngine;

public class Training_Haptic : MonoBehaviour
{
    private bool buttonStatus = false;
    private  GameObject hapticDevice = null;
    public int buttonID = 0;

    public GameEvent startingTrainingVocal;

    public bool startingVocal = false;
    // Start is called before the first frame update
    void Start()
    {
        if (hapticDevice == null)
        {

            HapticPlugin[] HPs = (HapticPlugin[])Object.FindObjectsOfType(typeof(HapticPlugin));
            foreach (HapticPlugin HP in HPs)
            {
                if (HP.hapticManipulator == this.gameObject)
                {
                    hapticDevice = HP.gameObject;
                }
            }

        }
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
        }
    }
    
    
}
