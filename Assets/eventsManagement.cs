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
    public Transform positionObjectAfterSuccess;
    
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

    IEnumerator ObjectPickedWithSuccessWait()
    {
        yield return new WaitForSeconds(0.2f);
       objectPickedWithSuccess.Raise();
   
    }
    
    IEnumerator StartDissolve()
    {
        yield return new WaitForSeconds(0.1f);
        startDissolveEffect = true;
    }
    
    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(objectToPick.tag))
        {
           
            if (objectPinzabili != null && objectPinzabili.isActive)
            {
                objectToPick.transform.position = new Vector3(positionObjectAfterSuccess.position.x,positionObjectAfterSuccess.position.y,positionObjectAfterSuccess.position.z);
                saveObjectPickedEvent.Raise();
                objectPinzabili.isActive = false;
                StartCoroutine(StartDissolve());
                StartCoroutine(ObjectPickedWithSuccessWait());
                
            }else{
                cubeAssociated.GetComponent<MeshRenderer>().material.SetColor("_Albedo", Color.red);
               
            }
        }
        

    }
}
