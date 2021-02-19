using System.Collections;
using System.Collections.Generic;
using EventSystem;
using UnityEngine;

public class SaveObjectPicked : MonoBehaviour
{

    public GameEvent saveObjectPickedEvent;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("ObjPinzabili"))
        {
            ObjectPinzabili objectPinzabili = other.GetComponent<ObjectPinzabili>();
            if (objectPinzabili != null && objectPinzabili.isActive)
            {
                saveObjectPickedEvent.Raise();
                other.GetComponent<MeshRenderer>().material.color=Color.green;
                objectPinzabili.isActive = false;
            }
            
        }
        

    }
}
