using System;
using System.Collections;
using System.Collections.Generic;
using EventSystem;
using UnityEngine;

public class TrackingScript : MonoBehaviour
{
    float elapsed = 0f;
    
    private int idObject=-1;
    private bool isRecording=false;
    private bool isActive=false;
    private bool errorObject=false;

    private DateTime lastTime; 
    private JsonObject jsonObjectToSave = new JsonObject();
    private TrackObject trackObject = new TrackObject {id = 0};
    public Transform objectPinzabileTransform;
    


    public void Update()
    {     
        elapsed += Time.deltaTime;
        if (elapsed >= 1f)
        {
            elapsed = elapsed % 1f;
            if (isRecording)
            {
                if (!errorObject)
                {
                    trackObject.positionList.Add(objectPinzabileTransform.position);
                    trackObject.rotationList.Add(objectPinzabileTransform.rotation);
                }
                else
                {
                    jsonObjectToSave.numberOfErrorObject++;
                    DateTime currentTime = DateTime.Now;
                    trackObject.duration = currentTime.Subtract(lastTime);
                    jsonObjectToSave.TrackObjects.Add(trackObject);
                    trackObject = new TrackObject {id = jsonObjectToSave.TrackObjects.Count};
                    lastTime = currentTime;
                    errorObject = false;
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
    
    
    public void StopRecording()
    {
        isRecording = false;
    }
}
