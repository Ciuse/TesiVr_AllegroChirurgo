using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Inputs;

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
    public bool m_TriggerDown;
    public bool b_Button;
    public float m_TriggerHeldTime;
    
    public float speed;
   

    // Start is called before the first frame update
    void Start()
    {
        m_InteractableBase = GetComponent<XRGrabInteractable>();
        m_InteractableBase.onActivate.AddListener(TriggerPulled);
        m_InteractableBase.onDeactivate.AddListener(TriggerReleased);
        isMoving = false;
        Vector3 localPos1 = pinza1.localPosition;
        startPosPinza1 = new Vector3(localPos1.x, localPos1.y, localPos1.z);
        Vector3 localPos2 = pinza2.localPosition;
        startPosPinza2 = new Vector3(localPos2.x, localPos2.y, localPos2.z);

    }

    void TriggerReleased(XRBaseInteractor obj)
    {

        m_TriggerDown = false;
        m_TriggerHeldTime = 0;

    }

    void TriggerPulled(XRBaseInteractor obj)
    {
        m_TriggerDown = true;
    }

    public void FixedUpdate()
    {
        // check collision with tweezer and object

        if (Keyboard.current.aKey.isPressed)
        {
            ClosePinze();
        }
        else
        {
            ResetPinze();
        }

        if (isMoving)
        {
            if(IsPinza1Collided()){
                pinza1Collided = true;
                if(objectWithPinza1==null)
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
                if(objectWithPinza2==null) 
                    objectWithPinza2 = _colliders[0].gameObject;
            }
            else
            {
                pinza2Collided = false;
                objectWithPinza2 = null;
            }

            if (pinza1Collided && pinza2Collided)
            {
                StopMovement();
                if( objectWithPinza1 !=null && objectWithPinza2 !=null)
                { 
                    if (objectWithPinza1 == objectWithPinza2)
                    {
                        objectWithPinza1.transform.SetParent(this.gameObject.transform);
                        objectWithPinza1.GetComponent<Collider>().attachedRigidbody.useGravity = false;
                    }
                }
            }
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

    void StopMovement()
    {

        collided = true; 
        isMoving = false;

    }

    void ClosePinze()
    {
        if (!collided)
        {
            isMoving = true;
            pinza1.localPosition = Vector3.MoveTowards(pinza1.localPosition, endPosPinza1.localPosition, Time.deltaTime * speed);
            pinza2.localPosition = Vector3.MoveTowards(pinza2.localPosition, endPosPinza2.localPosition, Time.deltaTime * speed);
        }


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