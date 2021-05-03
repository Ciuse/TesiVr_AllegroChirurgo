using System;
using System.Collections;
using System.Collections.Generic;
using EventSystem2;
using UnityEngine;

public class TrackingScript : MonoBehaviour
{
    float elapsed = 0f;
    
    private int idObject=-1;
    public bool isRecording=false;
    private bool errorObject=false;
    private bool errorPinza = false;

    public List<GameObject> objectPinzabiliList = new List<GameObject>();

    private DateTime lastTime; 
    public JsonObject jsonObjectToSave = new JsonObject();
    private TrajectoryObject singleTrajectory = new TrajectoryObject {id = 0};
    public Transform handTransform;
    public Transform pinzaTransform;

    public ObjectEvent saveJsonObject;
    private int executionNumber=-1;

    private Vector3 objectPosition;

    public void Update()
    {
        elapsed += Time.deltaTime;
        if (elapsed >= 0.2f)
        {
            elapsed = elapsed % 0.2f;
            if (isRecording)
            {
                singleTrajectory.positionList.Add(GetNormalizedPinzaPosition());
                singleTrajectory.rotationList.Add(pinzaTransform.rotation);
                if (errorObject)
                {
                    jsonObjectToSave.numberOfErrorObject++;
                    DateTime currentTime = DateTime.Now;
                    singleTrajectory.duration = currentTime.Subtract(lastTime).TotalMilliseconds;
                    jsonObjectToSave.trajectoryList.Add(singleTrajectory);
                    singleTrajectory = new TrajectoryObject {id = jsonObjectToSave.trajectoryList.Count};
                    lastTime = currentTime;
                    errorObject = false;
                }
                else if (errorPinza)
                {
                    jsonObjectToSave.numberOfErrorPinza++;
                    DateTime currentTime = DateTime.Now;
                    singleTrajectory.duration = currentTime.Subtract(lastTime).TotalMilliseconds;
                    jsonObjectToSave.trajectoryList.Add(singleTrajectory);
                    singleTrajectory = new TrajectoryObject {id = jsonObjectToSave.trajectoryList.Count};
                    lastTime = currentTime;
                    errorPinza = false;
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!isRecording && (other.gameObject.layer == LayerMask.NameToLayer("Pinza")))
        {
            singleTrajectory = new TrajectoryObject {id = 0};
            jsonObjectToSave.startTime = DateTime.Now;
            lastTime=DateTime.Now;
            isRecording = true;
        }
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (isRecording && (other.gameObject.layer == LayerMask.NameToLayer("Pinza")))
        {
            isRecording = false;
        }
    }

    
    public void ActiveCollider(Interactable interactable)
    {
        executionNumber++;
        jsonObjectToSave = new JsonObject();
        idObject = interactable.id;
        transform.GetChild(idObject).GetComponent<BoxCollider>().enabled=true;
        jsonObjectToSave.idObject = idObject;
        jsonObjectToSave.executionNumber = executionNumber;
        objectPosition = objectPinzabiliList[idObject].transform.position;
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
        jsonObjectToSave.trajectoryList.Add(singleTrajectory);
        jsonObjectToSave.endTime = DateTime.Now;
        jsonObjectToSave.duration = (int) jsonObjectToSave.endTime.Subtract(jsonObjectToSave.startTime).TotalMilliseconds;
        transform.GetChild(idObject).GetComponent<BoxCollider>().enabled=false;
        Interactable objectToSave = new Interactable{ interactedObject= this.gameObject};
        saveJsonObject.Raise(objectToSave);
    }

    private Vector3 GetNormalizedPinzaPosition()
    {
        Vector3 pinzaPos = pinzaTransform.position;
        return new Vector3(
            pinzaPos.x-objectPosition.x, 
            pinzaPos.y-objectPosition.y, 
            pinzaPos.z-objectPosition.z);
        
    }

    public void SaveWrongObjectPickedNumber(Interactable interactable)
    {
        if(!jsonObjectToSave.wrongObjectPicked.Exists(x=> x == interactable.id)) 
            jsonObjectToSave.wrongObjectPicked.Add(interactable.id);
    }
 
    
}
