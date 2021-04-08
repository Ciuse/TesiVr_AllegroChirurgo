using System.Collections;
using System.Collections.Generic;
using EventSystem2;
using UnityEngine;

public class StartingHandEventRaise : MonoBehaviour
{
    public GameEvent startSceneWithSpecificHand;
    // Start is called before the first frame update
    void Start()
    {
        startSceneWithSpecificHand.Raise();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
