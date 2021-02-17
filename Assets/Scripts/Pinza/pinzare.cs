using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using System.Runtime.CompilerServices;
using EventSystem;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Inputs;
using Vector3 = UnityEngine.Vector3;

public class pinzare : MonoBehaviour
{
    public Transform pinza1;
    public Transform pinza2;
    public Transform endPosPinza1;
    public Transform endPosPinza2;
    public Vector3 startPosPinza1;
    public Vector3 startPosPinza2;
    
    public List<GameObject> sphereListPinza1;
    public List<GameObject> sphereListPinza2;
    public float collisionRadius;
    public Collider[] _colliders = new Collider[10];
    public LayerMask sphereCollisionMask;
    
    public bool pinza1Collided = false;
    public bool pinza2Collided = false;
    public GameObject objectWithPinza1 = null;
    public GameObject objectWithPinza2 = null;
    public bool isMoving;
    public bool collided;
   
    XRGrabInteractable m_InteractableBase;
    public bool m_gripDown;
    public bool m_TriggerDown;
    public float m_TriggerHeldTime;
    
    public float speed;
    const float k_HeldThreshold = 0.1f;

    public GameEvent raiseError;

    // Start is called before the first frame update
    void Start()
    {
        m_InteractableBase = GetComponent<XRGrabInteractable>();
        m_InteractableBase.onActivate.AddListener(TriggerPulled);
        m_InteractableBase.onDeactivate.AddListener(TriggerReleased);
        m_InteractableBase.onSelectEntered.AddListener(GripPulled);
        m_InteractableBase.onSelectExited.AddListener(GripReleased);

        isMoving = false;
        Vector3 localPos1 = pinza1.localPosition;
        startPosPinza1 = new Vector3(localPos1.x, localPos1.y, localPos1.z);
        Vector3 localPos2 = pinza2.localPosition;
        startPosPinza2 = new Vector3(localPos2.x, localPos2.y, localPos2.z);

    }

    void TriggerPulled(XRBaseInteractor obj)
    {
        m_TriggerDown = true;
    }
    
    void TriggerReleased(XRBaseInteractor obj)
    {

        m_TriggerDown = false;
        m_TriggerHeldTime = 0;

    }

  
    void GripPulled(XRBaseInteractor obj)
    {
        m_gripDown = true;
    }

    void GripReleased(XRBaseInteractor obj)
    {
        m_gripDown = false;
        m_TriggerDown = false;
        m_TriggerHeldTime = 0;
    }


    public void FixedUpdate()
    {
        if (m_gripDown)
        {


            // check collision with tweezer and object
            if (m_TriggerDown)
            {
                m_TriggerHeldTime += Time.deltaTime;

                if (m_TriggerHeldTime >= k_HeldThreshold)
                {

                    
                    if(!collided)
                    {
                        ClosePinze();
                        CheckCollidersWhileNoObject();
                    }
                    else
                    {
                        RemoveVelocity();
                        CheckCollidersWhileObjectPinzato();
                    }
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

    public void RemoveVelocity()
    {
        if (objectWithPinza1 != null && objectWithPinza2 != null )
        {
            print("reset velocity");
            objectWithPinza1.GetComponent<Collider>().attachedRigidbody.velocity=Vector3.zero;
            objectWithPinza1.GetComponent<Collider>().attachedRigidbody.angularVelocity = Vector3.zero;
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

    public void CheckCollidersWhileObjectPinzato()
    {
        if (IsPinza1Collided())
        {
            pinza1Collided = true;
        }else
        {
            pinza1Collided = false;   
        }

        if (IsPinza2Collided())
        {
            pinza2Collided = true;          
        }else
        {
            pinza2Collided = false;           
        }

        if (!pinza1Collided || !pinza2Collided)
        {
            print("entrato");
            collided = false;
            if (objectWithPinza1 != null)
            {
                objectWithPinza1.transform.SetParent(null);
                objectWithPinza1.GetComponent<Collider>().attachedRigidbody.useGravity = true;
                objectWithPinza1 = null;
            }
            if (objectWithPinza2 != null)
            {
                objectWithPinza2.transform.SetParent(null);
                objectWithPinza2.GetComponent<Collider>().attachedRigidbody.useGravity = true;
                objectWithPinza2 = null;
            }
        }
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
            
        }

        if (pinza1Collided && pinza2Collided)
        {
            if (objectWithPinza1 != null && objectWithPinza2 != null)
            {
                if (objectWithPinza1 == objectWithPinza2)
                {
                    collided = true;
                    objectWithPinza1.transform.SetParent(this.gameObject.transform);
                    objectWithPinza1.GetComponent<Collider>().attachedRigidbody.useGravity = false;
                    objectWithPinza1.GetComponent<Collider>().attachedRigidbody.velocity = Vector3.zero;
                    objectWithPinza1.GetComponent<Collider>().attachedRigidbody.angularVelocity = Vector3.zero;
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

    void ClosePinze()
    {
    
            isMoving = true;
            pinza1.localPosition = Vector3.MoveTowards(pinza1.localPosition, endPosPinza1.localPosition, Time.deltaTime * speed);
            pinza2.localPosition = Vector3.MoveTowards(pinza2.localPosition, endPosPinza2.localPosition, Time.deltaTime * speed);

    }

    void ResetPinze()
    {
        //il reset delle pinze viene invocato solo se le due posizioni sono diverse perchè vuol dire che la pinza era stata compressa e deve ritornare
        //allo stato iniziale 
        if ((pinza1.localPosition != startPosPinza1) || (pinza2.localPosition != startPosPinza2))
        {
            pinza1.localPosition = Vector3.MoveTowards(pinza1.localPosition, startPosPinza1, Time.deltaTime * speed);
            pinza2.localPosition = Vector3.MoveTowards(pinza2.localPosition, startPosPinza2, Time.deltaTime * speed);
           
        }

        if (pinza2Collided || pinza1Collided)
        {
            pinza2Collided = false;
            pinza1Collided = false;
            if (objectWithPinza1 != null)
            {
                objectWithPinza1.transform.SetParent(null);
                objectWithPinza1.GetComponent<Collider>().attachedRigidbody.useGravity = true;
                objectWithPinza1 = null;
            }
            if (objectWithPinza2 != null)
            {
                objectWithPinza2.transform.SetParent(null);
                objectWithPinza2.GetComponent<Collider>().attachedRigidbody.useGravity = true;
                objectWithPinza2 = null;
            }
        }

        collided = false;
        isMoving = false;
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