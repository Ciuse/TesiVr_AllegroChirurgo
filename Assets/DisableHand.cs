using System.Collections;
using System.Collections.Generic;
using EventSystem;
using UnityEngine;

public class DisableHand : MonoBehaviour
{
    public GameEvent disableHandEvent;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void DisableHandAfterButtonClick()
    {
        disableHandEvent.Raise();
    }
}
