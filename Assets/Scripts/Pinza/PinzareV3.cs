﻿using System.Collections.Generic;
using EventSystem;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;
using Vector3 = UnityEngine.Vector3;

public class PinzareV3 : MonoBehaviour
{
    public List<GameObject> sphereListPinza1;
    public List<GameObject> sphereListPinza2;

    public Animator animatorPinza1;
    public Animator animatorPinza2;
    private float _smoothTriggerValue=0f;

    public float collisionRadius;
    public Collider[] _colliders = new Collider[10];
    public LayerMask sphereCollisionMask;
    
    public bool pinza1Collided = false;
    public bool pinza2Collided = false;
    public GameObject objectWithPinza1 = null;
    public GameObject objectWithPinza2 = null;
    public bool collided;
   
    public XRGrabInteractable m_InteractableBase;
    public bool m_gripDown;    

    public InputActionReference triggerPressing;
    public InputActionReference gripPressing;

    // Start is called before the first frame update
    void Start()
    {
        m_InteractableBase = GetComponent<XRGrabInteractable>();
        m_InteractableBase.onSelectEntered.AddListener(GripPulled);
        m_InteractableBase.onSelectExited.AddListener(GripReleased);

    }

  
    void GripPulled(XRBaseInteractor obj)
    {
        m_gripDown = true;
    }

    void GripReleased(XRBaseInteractor obj)
    {
        m_gripDown = false;
    }


    public void FixedUpdate()
    {
        if (m_gripDown)
        {
        
            float triggerValue = triggerPressing.action.ReadValue<float>();
            // check collision with tweezer and object
            if (triggerValue>0.05f)
            {  
                animatorPinza1.SetBool("ClosePinza1",true);
                animatorPinza2.SetBool("ClosePinza2",true);

                if(!collided)
                {
                    if (_smoothTriggerValue <= triggerValue)
                    {
                        _smoothTriggerValue = _smoothTriggerValue + 0.02f;
                    }
                    else
                    {
                        _smoothTriggerValue = _smoothTriggerValue - 0.02f;
                    }
                    animatorPinza2.SetFloat("TriggerPressing", _smoothTriggerValue); 
                    animatorPinza1.SetFloat("TriggerValue", _smoothTriggerValue); 
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
            ResetPinze();

        }
    }


    public void ClosePinze()
    {
       
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

    
    
    public void CheckCollidersWhileNoObject()
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

        if (pinza1Collided && pinza2Collided)
        {
            print("collidono");
            if (objectWithPinza1 != null && objectWithPinza2 != null)
            {
                if (objectWithPinza1 == objectWithPinza2)
                {
                    if(objectWithPinza1.gameObject.GetComponent<ObjectPinzabili>().resetting==false)
                    {
                        collided = true;
                        objectWithPinza1.gameObject.transform.SetParent(transform);
                        objectWithPinza1.gameObject.transform.GetComponent<Rigidbody>().isKinematic = true;
                        print(objectWithPinza1.gameObject.transform.position);
                        print("oggetto pinzato");

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
        //il reset delle pinze viene invocato solo se le due posizioni sono diverse perchè vuol dire che la pinza era stata compressa e deve ritornare
        //allo stato iniziale 
        
        animatorPinza1.SetBool("ClosePinza1",false);
        animatorPinza2.SetBool("ClosePinza2",false);
        
        if (objectWithPinza1 != null)
        {
            objectWithPinza1.gameObject.transform.SetParent(null);
            objectWithPinza1.gameObject.transform.GetComponent<Rigidbody>().isKinematic = false;

        }
     

        pinza2Collided = false;
        pinza1Collided = false;
        objectWithPinza1 = null;
        objectWithPinza2 = null;
        collided = false;
        _smoothTriggerValue = 0f;
    }

    public void RemoveObjectPinzato()
    {
        print("inizio remove");
//        animatorPinza1.SetTrigger("Resetting");
//        animatorPinza2.SetTrigger("Resetting");
        if (objectWithPinza1 != null)
        {
            objectWithPinza1.gameObject.transform.SetParent(null);
            objectWithPinza1.gameObject.transform.GetComponent<Rigidbody>().isKinematic = false;

        }

        pinza2Collided = false;
        pinza1Collided = false;
        objectWithPinza1 = null;
        objectWithPinza2 = null;
        collided = false;
        print("fine remove");

//        _smoothTriggerValue = 0f;

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

    }
}