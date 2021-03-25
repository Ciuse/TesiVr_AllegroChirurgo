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
    private TrajectoryObject singleTrajectory = new TrajectoryObject {id = 0};
    public Transform handTransform;
    public Transform pinzaTransform;

    public ObjectEvent saveJsonObject;
    private int executionNumber=-1;


    public void Update()
    {
        elapsed += Time.deltaTime;
        if (elapsed >= 0.2f)
        {
            elapsed = elapsed % 0.2f;
            if (isRecording)
            {
                singleTrajectory.positionList.Add(handTransform.position);
                singleTrajectory.rotationList.Add(handTransform.rotation);
                if (errorObject)
                {
                    jsonObjectToSave.numberOfErrorObject++;
                    DateTime currentTime = DateTime.Now;
                    singleTrajectory.duration = currentTime.Subtract(lastTime);
                    jsonObjectToSave.trajectoryList.Add(singleTrajectory);
                    singleTrajectory = new TrajectoryObject {id = jsonObjectToSave.trajectoryList.Count};
                    lastTime = currentTime;
                    errorObject = false;
                }
                else if (errorPinza)
                {
                    jsonObjectToSave.numberOfErrorPinza++;
                    DateTime currentTime = DateTime.Now;
                    singleTrajectory.duration = currentTime.Subtract(lastTime);
                    jsonObjectToSave.trajectoryList.Add(singleTrajectory);
                    singleTrajectory = new TrajectoryObject {id = jsonObjectToSave.trajectoryList.Count};
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
        executionNumber++;
        jsonObjectToSave = new JsonObject();
        singleTrajectory = new TrajectoryObject {id = 0};
        idObject = interactable.id;
        transform.GetChild(idObject).GetComponent<BoxCollider>().enabled=true;
        jsonObjectToSave.idObject = idObject;
        jsonObjectToSave.executionNumber = executionNumber;
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
        jsonObjectToSave.endTime = DateTime.Now;
        transform.GetChild(idObject).GetComponent<BoxCollider>().enabled=false;
        Interactable objectToSave = new Interactable{ interactedObject= this.gameObject};
        saveJsonObject.Raise(objectToSave);
    }
    
}
