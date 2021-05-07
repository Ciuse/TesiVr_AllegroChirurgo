using System;
using System.Collections;
using System.Collections.Generic;
using EventSystem2;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ObjectPinzabile  : DynamicObjectAbstract
{
    public bool isActive;
    public ObjectEvent objectTouchBox;
    public ObjectEvent objectWrongPicked;
    public ObjectEvent objectCorrectPickedSound;

    public int idObject;
    
    private Color defaultMeshColor;
    
    private List<Transform> childList = new List<Transform>();
    public bool visualEffectObject;

    public bool objectPicked;

    public void Start()
    {
        StartHash();
        SaveState();
        isActive = false;
        FindLeaves(transform, childList);
        defaultMeshColor = childList[0].GetComponent<MeshRenderer>().material.color;
        if (GameObject.Find ("SceneLoader_Haptic")!=null)
        {
            Scene_Loader_Haptic sceneLoaderHaptic = GameObject.Find ("SceneLoader_Haptic").GetComponent<Scene_Loader_Haptic>();
            visualEffectObject = sceneLoaderHaptic.visualCorrectObjectSetting;
        }
        if (GameObject.Find("ManageJsonToSaveDB") != null)
        {
            ManageJsonAndSettingsVR manageJsonAndSettings = GameObject.Find("ManageJsonToSaveDB").GetComponent<ManageJsonAndSettingsVR>();
            visualEffectObject= manageJsonAndSettings.visualPickingUpObjectSetting;
        }

    }
    
    private void FindLeaves(Transform parent, List<Transform> leafArray)
    {
        
        if (parent.childCount == 0)
        {
            leafArray.Add(parent);
        }
        else
        {
            foreach (Transform child in parent)
            {
                FindLeaves(child, leafArray);
            }
        }
    }
    

    private void OnCollisionEnter(Collision other)
    {
        if (SceneManager.GetActiveScene().name == "Haptic_Scene2")
        {

            if (other.gameObject.layer == LayerMask.NameToLayer("Pinza"))
            {
                Interact();
            }

            if (hasInteract && other.gameObject.layer == LayerMask.NameToLayer("Object"))
            {
                Interactable interactable = new Interactable {id = idObject};
                objectTouchBox.Raise(interactable);
            }

        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Pinza"))
        {
            Interact();
        }

        if(hasInteract && (other.gameObject.layer == LayerMask.NameToLayer("HoleEdge") || other.gameObject.layer == LayerMask.NameToLayer("Electric Edge")) )
        {
            Interactable interactable = new Interactable {id = idObject};
            objectTouchBox.Raise(interactable);

        }

        if (!hasInteract && (other.gameObject.layer == LayerMask.NameToLayer("HoleEdge") || other.gameObject.layer == LayerMask.NameToLayer("Electric Edge")))
        {
            ResetState();
        }

    
    }

    private void OnTriggerStay(Collider other)
    {
        
        if (other.gameObject.layer == LayerMask.NameToLayer("Pinza"))
        {
            Interact();
        }

        if(hasInteract && (other.gameObject.layer == LayerMask.NameToLayer("HoleEdge") || other.gameObject.layer == LayerMask.NameToLayer("Electric Edge")) )
        {
            Interactable interactable = new Interactable {id = idObject};
            objectTouchBox.Raise(interactable);

        }

        if (!hasInteract && (other.gameObject.layer == LayerMask.NameToLayer("HoleEdge") || other.gameObject.layer == LayerMask.NameToLayer("Electric Edge")))
        {
            ResetState();
        }

    }

    public override void SaveState()
    {
        SaveStatePosition();
    }

    public override void ResetState()
    {
        ResetStatePosition();
        ResetHasInteracted();
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
    }


    public void ObjectEventCard(Interactable interactable)
    {
        if (idObject == interactable.id)
        {
            isActive = true;
        }
    }
    
    public void ResetObjectAfterDraw()
    {
        hasInteract = false;
        if (isActive == false)
        {
            ResetMesh();
            ResetState();
        }
   
    }
    
    IEnumerator WaitXSec()
    {
        yield return new WaitForSeconds(1.5f);
        Destroy(gameObject);

    }
        
    public void CorrectPickEvents()
    {
        objectPicked = true;
        if (visualEffectObject)
        {
            foreach (Transform child in childList)
            {
                child.GetComponent<MeshRenderer>().material.color= Color.green;
                
                StartCoroutine(WaitXSec());

            }
        }
        else
        {
            StartCoroutine(WaitXSec());
        }
        
     
        Interactable interactable = new Interactable {id = idObject};
        objectCorrectPickedSound.Raise(interactable);

        
    }
    
    
    public void WrongPickEvents()
    {
        if (visualEffectObject)
        {
            foreach (Transform child in childList)
            {
                child.GetComponent<MeshRenderer>().material.color= Color.red;
            }
        }
       
            Interactable interactable = new Interactable {id = idObject};
            objectWrongPicked.Raise(interactable);

        

    }
    
    public void ResetMesh()
    {
        foreach (Transform child in childList)
        {
            child.GetComponent<MeshRenderer>().material.color= defaultMeshColor;
        }
    }
    
}
