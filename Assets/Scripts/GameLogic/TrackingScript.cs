using System;
using System.Collections;
using System.Collections.Generic;
using EventSystem;
using UnityEngine;

public class TrackingScript : MonoBehaviour
{
    float elapsed = 0f;
    
    private int idObject=-1;
    public bool isRecording=false;
    private bool errorObject=false;
    private bool errorPinza = false;

    private DateTime lastTime; 
    public JsonObject jsonObjectToSave = new JsonObject();
    private TrackObject trackObject = new TrackObject {id = 0};
    public Transform handTransform;
    public Transform pinzaTransform;

    public ObjectEvent saveJsonObject;



    public void Update()
    {
        elapsed += Time.deltaTime;
        if (elapsed >= 0.2f)
        {
            elapsed = elapsed % 0.2f;
            if (isRecording)
            {
                trackObject.positionList.Add(handTransform.position);
                trackObject.rotationList.Add(handTransform.rotation);
                if (errorObject)
                {
                    jsonObjectToSave.numberOfErrorObject++;
                    DateTime currentTime = DateTime.Now;
                    trackObject.duration = currentTime.Subtract(lastTime);
                    jsonObjectToSave.TrackObjects.Add(trackObject);
                    trackObject = new TrackObject {id = jsonObjectToSave.TrackObjects.Count};
                    lastTime = currentTime;
                    errorObject = false;
                }
                else if (errorPinza)
                {
                    jsonObjectToSave.numberOfErrorPinza++;
                    DateTime currentTime = DateTime.Now;
                    trackObject.duration = currentTime.Subtract(lastTime);
                    jsonObjectToSave.TrackObjects.Add(trackObject);
                    trackObject = new TrackObject {id = jsonObjectToSave.TrackObjects.Count};
                    lastTime = currentTime;
                    errorPinza = false;
                }
                Debug.Log(handTransform.position.x);

                
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!isRecording && (other.gameObject.layer == LayerMask.NameToLayer("Pinza")))
        {
            jsonObjectToSave.startTime = DateTime.Now;
            lastTime=DateTime.Now;
            isRecording = true;
        }
    }
    
    public void ActiveCollider(Interactable interactable)
    {
        jsonObjectToSave = new JsonObject();
        idObject = interactable.id;
        transform.GetChild(idObject).GetComponent<BoxCollider>().enabled=true;
        jsonObjectToSave.idObject = idObject;
   
    }

    public void SetErrorObject()
    {
        errorObject = true;
    }
   
    public void SetErrorPinza()
    {
        errorPinza = true;
    }
    
    public void StopRecording()
    {
        isRecording = false;
        transform.GetChild(idObject).GetComponent<BoxCollider>().enabled=false;
        Interactable objectToSave = new Interactable{ interactedObject= this.gameObject};
        saveJsonObject.Raise(objectToSave);
    }
    
}
