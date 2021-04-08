using System.Collections;
using System.Collections.Generic;
using EventSystem2;
using UnityEngine;

public class VisualEffectObjects : MonoBehaviour
{
    public GameEvent visualEffectObjectsEvent;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ActivateVisualEffectObjects()
    {
        visualEffectObjectsEvent.Raise();
    }
}
