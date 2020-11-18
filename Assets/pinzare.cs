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
    private Vector3 startPosPinza1;
    private Vector3 startPosPinza2;
    public List<GameObject> sphereListPinza1;
    public List<GameObject> sphereListPinza2;
    public float collisionRadius;
    private Collider[] _colliders = new Collider[10];
    public LayerMask sphereCollisionMask;
    private bool pinza1Collided = false;
    private bool pinza2Collided = false;
    bool m_TriggerDown;
    bool b_Button;
    XRGrabInteractable m_InteractableBase;
    float m_TriggerHeldTime;
    public float speed;


    private bool isMoving;

    private bool collided;

    // Start is called before the first frame update
    void Start()
    {
        m_InteractableBase = GetComponent<XRGrabInteractable>();
        m_InteractableBase.onActivate.AddListener(TriggerPulled);
        m_InteractableBase.onDeactivate.AddListener(TriggerReleased);
        isMoving = false;
        startPosPinza1 = pinza1.position;
        startPosPinza2 = pinza2.position;

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
            OpenPinze();
        }

        if (isMoving)
        {
            foreach (GameObject sphere in sphereListPinza1)
            {
                var sphereMovableCollisions =
                    Physics.OverlapSphereNonAlloc(sphere.transform.position, collisionRadius, _colliders,
                        sphereCollisionMask);
                if (sphereMovableCollisions > 0)
                {
                    pinza1Collided = true;
                }
            }

            foreach (GameObject sphere in sphereListPinza2)
            {
                var sphereMovableCollisions =
                    Physics.OverlapSphereNonAlloc(sphere.transform.position, collisionRadius, _colliders,
                        sphereCollisionMask);
                if (sphereMovableCollisions > 0)
                {
                    pinza2Collided = true;
                }
            }

            if (pinza1Collided && pinza2Collided)
            {
                StopMovement();
            }
        }

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
            pinza1.position = Vector3.MoveTowards(pinza1.position, endPosPinza1.position, Time.deltaTime * speed);
            pinza2.position = Vector3.MoveTowards(pinza2.position, endPosPinza2.position, Time.deltaTime * speed);
        }


    }

    void OpenPinze()
    {
        pinza1.position = Vector3.MoveTowards(pinza1.position, startPosPinza1, Time.deltaTime * speed);
        pinza2.position = Vector3.MoveTowards(pinza2.position, startPosPinza2, Time.deltaTime * speed);
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
