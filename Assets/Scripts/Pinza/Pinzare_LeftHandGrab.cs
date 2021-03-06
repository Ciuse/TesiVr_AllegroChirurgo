﻿using System;
using System.Collections;
using System.Collections.Generic;
using EventSystem2;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;
using UnityEngine.XR.Interaction.Toolkit;
using Vector3 = UnityEngine.Vector3;

public class Pinzare_LeftHandGrab : MonoBehaviour
{
    public List<GameObject> sphereListPinza1;
    public List<GameObject> sphereListPinza2;

    public List<GameObject> sphereOutsideListPinza1;
    public List<GameObject> sphereOutsideListPinza2;
    
    
    public Animator animatorPinza1;
    public Animator animatorPinza2;
    private float _smoothClosePinza1=0f;
    private float _smoothClosePinza2=0f;

    public float collisionRadius;
    public float collisionOutsideRadius;

    public Collider[] _colliders = new Collider[10];
    public LayerMask sphereCollisionMask;
    
    public bool pinza1Collided;
    public bool pinza2Collided;
    public bool pinza1CollidedOutside;
    public bool pinza2CollidedOutside;
    
    public GameObject objectWithPinza1 = null;
    public GameObject objectWithPinza2 = null;
    public bool collided;
   
    public XRGrabInteractable m_InteractableBase;
    public bool m_gripDown;    

    public InputActionReference triggerPressingLeftHand;
    private bool resetting;

    public Material pinza1Material;

    public Material pinza2Material;

    private Color startPinza1Color;
    private Color startPinza2Color;
    
    public GameObject leftHandMesh;
    public bool pinzaInHand;
    
    // Start is called before the first frame update
    void Start()
    {
        m_InteractableBase = GetComponent<XRGrabInteractable>();
        m_InteractableBase.onSelectEntered.AddListener(GripPulled);
        m_InteractableBase.onSelectExited.AddListener(GripReleased);

        startPinza1Color = pinza1Material.color;
        startPinza2Color = pinza2Material.color;
    }

  
    void GripPulled(XRBaseInteractor obj)
    {
        m_gripDown = true;
    }

    void GripReleased(XRBaseInteractor obj)
    {
        m_gripDown = false;
    }


    public void Update()
    {
        if (pinza1CollidedOutside)
        {
            pinza1Material.color=Color.red;
        }
        else
        {
            pinza1Material.color = startPinza1Color;
        }
        
        if (pinza2CollidedOutside)
        {
            pinza2Material.color=Color.red;
        }
        else
        {
            pinza2Material.color = startPinza2Color;
        }
    }

    public void FixedUpdate()
    {
        if (m_gripDown)
        {
            leftHandMesh.SetActive(false);
            pinzaInHand = true;
            float triggerValue = triggerPressingLeftHand.action.ReadValue<float>();
            // check collision with tweezer and object
            if (triggerValue>0.05f)
            {  
                animatorPinza1.SetBool("ClosePinza1",true);
                animatorPinza2.SetBool("ClosePinza2",true);

                if(!collided)
                {
                    if(!pinza1Collided)
                    {
                        if (_smoothClosePinza1 <= triggerValue)
                        {
                            _smoothClosePinza1 = _smoothClosePinza1 + 0.02f;
                        }
                        else
                        {
                            _smoothClosePinza1 = _smoothClosePinza1 - 0.02f;
                        }

                        animatorPinza1.SetFloat("TriggerValue", _smoothClosePinza1);
                    }
                    
                    if(!pinza2Collided)
                    {
                        if (_smoothClosePinza2 <= triggerValue)
                        {
                            _smoothClosePinza2 = _smoothClosePinza2 + 0.02f;
                        }
                        else
                        {
                            _smoothClosePinza2 = _smoothClosePinza2 - 0.02f;
                        }

                        animatorPinza2.SetFloat("TriggerValue", _smoothClosePinza2); 
                    }
                   
                    //animatorPinza1.SetFloat("TriggerValue", Random.Range(0f,1f));    
                    //ClosePinze();
                    CheckCollidersWhileNoObject();
                }
            }
            else
            {
                ResetPinze();
            }       
        }
        else{
            if (pinzaInHand)
            {
                leftHandMesh.SetActive(true);
                pinzaInHand = false;
            }
            ResetPinze();

        }
    }


    private bool IsPinza1Collided()
    {
           
        foreach (GameObject sphere in sphereListPinza1)
        {
            var sphereMovableCollisions =
                Physics.OverlapSphereNonAlloc(sphere.transform.position, collisionRadius, _colliders,
                    sphereCollisionMask);
            if (sphereMovableCollisions > 0)
            {
                return true;
            }
        }
        return false;

    }

    private bool IsPinza1CollidedOutside()
    {
           
        foreach (GameObject sphere in sphereOutsideListPinza1)
        {
            var sphereMovableCollisions =
                Physics.OverlapSphereNonAlloc(sphere.transform.position, collisionOutsideRadius, _colliders,
                    sphereCollisionMask);
            if (sphereMovableCollisions > 0)
            {
                return true;
            }
        }
        return false;

    }
    
    private bool IsPinza2CollidedOutside()
    {
           
        foreach (GameObject sphere in sphereOutsideListPinza2)
        {
            var sphereMovableCollisions =
                Physics.OverlapSphereNonAlloc(sphere.transform.position, collisionOutsideRadius, _colliders,
                    sphereCollisionMask);
            if (sphereMovableCollisions > 0)
            {
                return true;
            }
        }
        return false;

    }
    
    public void CheckCollidersWhileNoObject()
    {
        if (!resetting)
        {
            if (IsPinza1Collided())
        {
            pinza1Collided = true;
            if (objectWithPinza1 == null)
                objectWithPinza1 = _colliders[0].gameObject;
        }
        else
        {
            pinza1Collided = false;
            objectWithPinza1 = null;

        }



        if (IsPinza2Collided())
        {
            pinza2Collided = true;
            if (objectWithPinza2 == null)
                objectWithPinza2 = _colliders[0].gameObject;
        }
        else
        {
            pinza2Collided = false;
            objectWithPinza2 = null;
        }
        
        pinza1CollidedOutside = IsPinza1CollidedOutside(); 
        
        pinza2CollidedOutside = IsPinza2CollidedOutside();
  

        if (pinza1Collided && pinza2Collided && !pinza1CollidedOutside && !pinza2CollidedOutside)
        {
            if (objectWithPinza1 != null && objectWithPinza2 != null)
            {
                if (objectWithPinza1 == objectWithPinza2)
                {
                    collided = true;
                    objectWithPinza1.gameObject.transform.SetParent(transform);
                    objectWithPinza1.gameObject.transform.GetComponent<Rigidbody>().isKinematic = true;

                }
            }
        }
    }
    }
    private bool IsPinza2Collided()
    {
        foreach (GameObject sphere in sphereListPinza2)
        {
            var sphereMovableCollisions =
                Physics.OverlapSphereNonAlloc(sphere.transform.position, collisionRadius, _colliders,
                    sphereCollisionMask);
            if (sphereMovableCollisions > 0)
            {
                return true;
            }
        }
        return false;
        
  
    }  

    public void ResetPinze()
    {
        animatorPinza1.SetBool("ClosePinza1",false);
        animatorPinza2.SetBool("ClosePinza2",false);
        
        if (objectWithPinza1 != null)
        {
            objectWithPinza1.gameObject.transform.SetParent(null);
            objectWithPinza1.gameObject.transform.GetComponent<Rigidbody>().isKinematic = false;
        }

        pinza1CollidedOutside = false;
        pinza2CollidedOutside = false;
        pinza1Collided = false;
        pinza2Collided = false;
        objectWithPinza1 = null;
        objectWithPinza2 = null;
        
        collided = false;
        
        _smoothClosePinza1 = 0f;
        _smoothClosePinza2= 0f;
        
    }

    public void RemoveObjectPinzato()
    {
        resetting = true;
        if (objectWithPinza1 != null)
        {
            objectWithPinza1.gameObject.transform.SetParent(null);
            objectWithPinza1.gameObject.transform.GetComponent<Rigidbody>().isKinematic = false;
        }
        
        pinza1CollidedOutside = false;
        pinza2CollidedOutside = false;
        pinza1Collided = false;
        pinza2Collided = false;
        objectWithPinza1 = null;
        objectWithPinza2 = null;
        
        collided = false;
        
        StartCoroutine(WaitReset());

    }
    
    public void RemoveObjectSuccess()
    {
        resetting = true;
        if (objectWithPinza1 != null)
        {
            objectWithPinza1.gameObject.transform.SetParent(null);
            objectWithPinza1.gameObject.transform.GetComponent<Rigidbody>().isKinematic = true;
        }
        
        pinza1CollidedOutside = false;
        pinza2CollidedOutside = false;
        pinza1Collided = false;
        pinza2Collided = false;
        objectWithPinza1 = null;
        objectWithPinza2 = null;
        
        collided = false;
        
        StartCoroutine(WaitReset());

    }
    IEnumerator WaitReset()
    {
        yield return new WaitForSeconds(0.1f);
        resetting = false;
    }
   
    private void OnDrawGizmos()
    {
        foreach (GameObject sphere in sphereListPinza1)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(sphere.transform.position, collisionRadius);

        }

        foreach (GameObject sphere in sphereListPinza2)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(sphere.transform.position, collisionRadius);
        }
        
        foreach (GameObject sphere in sphereOutsideListPinza1)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(sphere.transform.position, collisionOutsideRadius);

        }

        foreach (GameObject sphere in sphereOutsideListPinza2)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(sphere.transform.position, collisionOutsideRadius);
        }
    }
}
