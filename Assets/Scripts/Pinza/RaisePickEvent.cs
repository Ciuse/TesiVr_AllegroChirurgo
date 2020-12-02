using System.Collections;
using System.Collections.Generic;
using EventSystem;
using UnityEngine;

public class RaisePickEvent : MonoBehaviour
{
    public GameEvent pickEvent;
    public GameEvent unPickEvent;

    // Start is called before the first frame update
    public void DoPickEvent()
    {
        pickEvent.Raise();
    }
    public void DoUnPickEvent()
    {
        unPickEvent.Raise();
    }
}
