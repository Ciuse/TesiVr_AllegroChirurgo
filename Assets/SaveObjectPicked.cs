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
        print("qualcosa ha colliso");
        if (other.gameObject.layer == LayerMask.NameToLayer("ObjPinzabili"))
        { 
            print("ha colliso un oggetto pinzabile");
            ObjectPinzabili objectPinzabili = other.GetComponent<ObjectPinzabili>();
            if (objectPinzabili != null && objectPinzabili.isActive)
            {
                print("entrato nella save platform");
                saveObjectPickedEvent.Raise();
                other.GetComponent<MeshRenderer>().material.color=Color.green;
                objectPinzabili.isActive = false;
            }
            
        }
        

    }
}
