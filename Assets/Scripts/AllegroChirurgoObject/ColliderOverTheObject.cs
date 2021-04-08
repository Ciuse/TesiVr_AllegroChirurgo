using System;
using EventSystem2;
using UnityEngine;
using System.Collections;

public class ColliderOverTheObject : MonoBehaviour
{


    public GameObject objectToPick;
    private ObjectPinzabile objectPinzabile;
    public GameEvent saveObjectPickedEvent;
    public GameEvent objectPickedWithSuccess;
    public Transform positionObjectAfterSuccess;
    

    private bool done=false;
    private void Start()
    {
        objectPinzabile = objectToPick.GetComponent<ObjectPinzabile>();
    }

    IEnumerator ObjectPickedWithSuccessWait()
    {
        yield return new WaitForSeconds(0.2f);
        objectPickedWithSuccess.Raise();
        Destroy(gameObject);
   
    }

   
    
    IEnumerator StartDissolve()
    {
        yield return new WaitForSeconds(0.1f);
        objectPinzabile.CorrectPickEvents();
    }
    
    public void OnTriggerEnter(Collider other)
    {
        if (!done && other.CompareTag(objectToPick.tag))
        {

            if (objectPinzabile != null && objectPinzabile.isActive)
            {
                done = true;
                objectToPick.transform.position = new Vector3(positionObjectAfterSuccess.position.x,positionObjectAfterSuccess.position.y,positionObjectAfterSuccess.position.z);
                saveObjectPickedEvent.Raise();
                StartCoroutine(StartDissolve());
                StartCoroutine(ObjectPickedWithSuccessWait());

            }else{

                objectPinzabile.WrongPickEvents();

            }
        }

    }
}
