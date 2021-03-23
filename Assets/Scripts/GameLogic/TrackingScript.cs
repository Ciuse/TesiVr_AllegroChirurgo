using System;
using System.Collections;
using System.Collections.Generic;
using EventSystem;
using UnityEngine;

public class TrackingScript : MonoBehaviour
{
    float elapsed = 0f;
    
    public int idObject=-1;
    public bool isRecording=false;
    public bool isActive=false;
    private bool errorObject=false;
    private bool errorPinza = false;

    private DateTime lastTime; 
    private JsonObject jsonObjectToSave = new JsonObject();
    private TrackObject trackObject = new TrackObject {id = 0};
    public Transform objectPinzabileTransform;



    public void Update()
    {
        elapsed += Time.deltaTime;
        if (elapsed >= 0.1f)
        {
            elapsed = elapsed % 0.1f;
            if (isRecording)
            {
                trackObject.positionList.Add(objectPinzabileTransform.position);
                trackObject.rotationList.Add(objectPinzabileTransform.rotation);
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

                
            }
        }
    }

    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        if (isActive && !isRecording && other.gameObject.layer == LayerMask.NameToLayer("Pinza"))
        {
            jsonObjectToSave.startTime = DateTime.Now;
            lastTime=DateTime.Now;
            isRecording = true;
        }
    }
    
    public void ActiveCollider(Interactable interactable)
    {
        if (idObject == interactable.id)
        {
            isActive = true;
            jsonObjectToSave.idObject = interactable.id;
        }
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
    }
}
