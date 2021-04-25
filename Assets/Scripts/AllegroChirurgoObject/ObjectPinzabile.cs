﻿using System;
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
    
    private float health=-1;
    private bool startDissolveEffect;

    private Color defaultMeshColor;
    
    private static readonly int Albedo = Shader.PropertyToID("_Albedo");
    private static readonly int DissolveValue = Shader.PropertyToID("_DissolveValue");

    private List<Transform> childList = new List<Transform>();
    public bool visualEffectObject;

    public void Start()
    {
        StartHash();
        SaveState();
        isActive = false;
        FindLeaves(transform, childList);
        defaultMeshColor = childList[0].GetComponent<MeshRenderer>().material.GetColor(Albedo);
        if (GameObject.Find ("SceneLoader_Haptic")!=null)
        {
            Scene_Loader_Haptic sceneLoaderHaptic = GameObject.Find ("SceneLoader_Haptic").GetComponent<Scene_Loader_Haptic>();
            visualEffectObject = sceneLoaderHaptic.visualObjectSetting;
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

    public void Update()
    {
        if (startDissolveEffect)
        {
            health += 0.008f;
            foreach (Transform child in childList)
            {
                child.GetComponent<Renderer>().material.SetFloat(DissolveValue, health);
                if (child.GetComponent<Renderer>().material.GetFloat(DissolveValue) >= 1)
                {
                    startDissolveEffect = false;
                    Destroy(gameObject);
                }
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
        
    public void CorrectPickEvents()
    {

        if (visualEffectObject)
        {
            startDissolveEffect = true;
        }
        else
        {
            Destroy(gameObject);
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
                child.GetComponent<MeshRenderer>().material.SetColor(Albedo, Color.red);
            }
        }
       
            Interactable interactable = new Interactable {id = idObject};
            objectWrongPicked.Raise(interactable);

        

    }
    
    public void ResetMesh()
    {
        foreach (Transform child in childList)
        {
            child.GetComponent<MeshRenderer>().material.SetColor(Albedo, defaultMeshColor);
        }
    }

    public void ActivateVisualEffectObjects()
    {
        visualEffectObject = !visualEffectObject;
    }
}
