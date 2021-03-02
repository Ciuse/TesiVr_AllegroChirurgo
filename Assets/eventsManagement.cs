using System;
using EventSystem;
using UnityEngine;
using System.Collections;

public class eventsManagement : MonoBehaviour
{


    public GameObject objectToPick;
    public GameObject cubeAssociated;
    private ObjectPinzabili objectPinzabili;
    public GameEvent saveObjectPickedEvent;
    public GameEvent objectPickedWithSuccess;
    public float health;
    public bool startDissolveEffect;
    
    private void Start()
    {
        objectPinzabili = objectToPick.GetComponent<ObjectPinzabili>();
    }

    private void Update()
    {
        if (startDissolveEffect)
        {
           
                health += 0.005f;
                cubeAssociated.GetComponent<Renderer>().material.SetFloat("_DissolveValue", health);
           
            if (cubeAssociated.GetComponent<Renderer>().material.GetFloat("_DissolveValue") >= 1)
            {
                startDissolveEffect = false;
            }
        }
    }

    IEnumerator objectPickedWithSuccessWait()
    {
        yield return new WaitForSeconds(0.5f);
       objectPickedWithSuccess.Raise();
   
    }
    
    
    public void OnTriggerEnter(Collider other)
    {
       
        if (other.CompareTag(objectToPick.tag))
        {
           
            if (objectPinzabili != null && objectPinzabili.isActive)
            {
                saveObjectPickedEvent.Raise();
                objectPinzabili.isActive = false;
                startDissolveEffect = true;
                StartCoroutine(objectPickedWithSuccessWait());
                
            }else{
                cubeAssociated.GetComponent<MeshRenderer>().material.color=Color.red;
               
            }
        }
        

    }
}
