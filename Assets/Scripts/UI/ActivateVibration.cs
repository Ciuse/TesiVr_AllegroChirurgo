using System.Collections;
using System.Collections.Generic;
using EventSystem;
using UnityEngine;

public class ActivateVibration : MonoBehaviour
{
    public GameEvent activateVibrationEvent;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ActivateVibrationAfterButtonClick()
    {
        activateVibrationEvent.Raise();
    }
}
