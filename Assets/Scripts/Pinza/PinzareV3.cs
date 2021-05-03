using System;
using System.Collections;
using System.Collections.Generic;
using EventSystem2;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;
using UnityEngine.XR.Interaction.Toolkit;
using Vector3 = UnityEngine.Vector3;

public class PinzareV3 : MonoBehaviour
{
    public bool detectPinzaCollisions;
    public bool resetObjectAfterObjectTouch;
    public List<GameObject> sphereListPinza1;
    public List<GameObject> sphereListPinza2;

    public List<GameObject> sphereOutsideListPinza1;
    public List<GameObject> sphereOutsideListPinza2;
   
    public List<GameObject> sphereTableListPinza1;
    public List<GameObject> sphereTableListPinza2;

    public Animator animatorPinza1;
    public Animator animatorPinza2;
    private float _smoothClosePinza1=0f;
    private float _smoothClosePinza2=0f;

    public float collisionRadius;
    public float collisionOutsideRadius;

    public Collider[] _colliders = new Collider[10];
    public LayerMask sphereCollisionMask;
    public LayerMask sphereCollisionTableMask;
    
    public bool pinza1Collided;
    public bool pinza2Collided;
    
    public bool pinza1CollidedOutside;
    public bool pinza2CollidedOutside;

    public GameObject objectWithPinza1 = null;
    public GameObject objectWithPinza2 = null;
    public bool collided;
   
    public XRGrabInteractable m_InteractableBase;
    public bool m_gripDown;    

    public InputActionReference triggerPressing;
    private bool resetting;

    public SkinnedMeshRenderer pinza1MateriaMeshRender;
    public SkinnedMeshRenderer pinza2MateriaMeshRender;


    private Color startPinza1Color;
    private Color startPinza2Color;
    
    public GameObject handMesh;
    public bool pinzaInHand;
   
    public bool disableHand;
    public bool activateVisualEffectPinza;

    public bool pinzaIsCollideElectricEdge;
    public bool pinza1ColorChanged = false;
    public bool pinza2ColorChanged = false;
    
    // Start is called before the first frame update
    void Start()
    {
        m_InteractableBase = GetComponent<XRGrabInteractable>();
        m_InteractableBase.onSelectEntered.AddListener(GripPulled);
        m_InteractableBase.onSelectExited.AddListener(GripReleased);
        startPinza1Color = pinza1MateriaMeshRender.material.color;
        startPinza2Color = pinza2MateriaMeshRender.material.color;
        if (GameObject.Find("ManageJsonToSaveDB") != null)
        {
            ManageJsonAndSettingsVR manageJsonAndSettings = GameObject.Find("ManageJsonToSaveDB").GetComponent<ManageJsonAndSettingsVR>();
            activateVisualEffectPinza = manageJsonAndSettings.visualPinzaSetting;
            disableHand = manageJsonAndSettings.hideHandSetting;
            detectPinzaCollisions = manageJsonAndSettings.detectPinzaCollision;
            resetObjectAfterObjectTouch = manageJsonAndSettings.detectObjectCollision;
        }
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
        if (!pinzaIsCollideElectricEdge)
        {
            if (pinza1CollidedOutside && activateVisualEffectPinza)
            {
                pinza1MateriaMeshRender.material.color = Color.yellow;
                pinza1ColorChanged=true;
            }
            else
            {
                if(pinza1ColorChanged)
                {
                    pinza1MateriaMeshRender.material.color = startPinza1Color;
                    pinza1ColorChanged=false;

                }            
            }

            if (pinza2CollidedOutside && activateVisualEffectPinza)
            {
                pinza2MateriaMeshRender.material.color = Color.yellow;
                pinza2ColorChanged=true;

            }
            else
            {
                if(pinza2ColorChanged)
                {
                    pinza2MateriaMeshRender.material.color = startPinza2Color;
                    pinza2ColorChanged=false;
                }            
            }
        }
    }


    public void FixedUpdate()
    {
        if (m_gripDown)
        {
            if (disableHand)
            {
                handMesh.SetActive(false);
                pinzaInHand = true;
            }

           
            float triggerValue = triggerPressing.action.ReadValue<float>();

            if (triggerValue > 0.05)
            {
                // check collision with tweezer and object
                animatorPinza1.SetBool("CloseRight", true);
                animatorPinza2.SetBool("CloseLeft", true);
            }
            else
            {
                if (_smoothClosePinza1 < 0.05 & _smoothClosePinza2 < 0.05)
                {
                    animatorPinza1.SetBool("CloseRight", false);
                    animatorPinza2.SetBool("CloseLeft", false);
                }
                else
                {
                    SmoothMovePinza1(triggerValue);
                    SmoothMovePinza2(triggerValue);
                }

            }


            if (!collided)
                {
                    if (!pinza1Collided && !pinza1CollidedOutside)
                    {
                        SmoothMovePinza1(triggerValue);
                    }

                    if (!pinza2Collided && !pinza2CollidedOutside)
                    {
                        SmoothMovePinza2(triggerValue);
                    }

                    //animatorPinza1.SetFloat("TriggerValue", Random.Range(0f,1f));    
                    //ClosePinze();
                    CheckCollidersWhileNoObject();
                }
          
                else
                {
                    if (triggerValue<0.05)
                    {
                        ResetPinze();
                    }
                    
                }
        }
        else{
            if (disableHand)
            {
                if (pinzaInHand)
                {
                    handMesh.SetActive(true);
                    pinzaInHand = false;
                }
            }

            ResetPinze();

        }
    }

    private void SmoothMovePinza1(float triggerValue)
    {
        if (_smoothClosePinza1 <= triggerValue)
        {
            _smoothClosePinza1 = _smoothClosePinza1 + 0.02f;
        }
        else
        {
            _smoothClosePinza1 = _smoothClosePinza1 - 0.02f;
        }

        animatorPinza1.SetFloat("Right", _smoothClosePinza1);
    }

    private void SmoothMovePinza2(float triggerValue)
    {
        if (_smoothClosePinza2 <= triggerValue)
        {
            _smoothClosePinza2 = _smoothClosePinza2 + 0.02f;
        }
        else
        {
            _smoothClosePinza2 = _smoothClosePinza2 - 0.02f;
        }

        animatorPinza2.SetFloat("Left", _smoothClosePinza2);
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
    
    private bool IsPinza2CollidedTable()
    {
           
        foreach (GameObject sphere in sphereTableListPinza2)
        {
            var sphereMovableCollisions =
                Physics.OverlapSphereNonAlloc(sphere.transform.position, collisionOutsideRadius, _colliders,
                    sphereCollisionTableMask);
            if (sphereMovableCollisions > 0)
            {
                return true;
            }
        }
        return false;

    }
    
    private bool IsPinza1CollidedTable()
    {
           
        foreach (GameObject sphere in sphereTableListPinza1)
        {
            var sphereMovableCollisions =
                Physics.OverlapSphereNonAlloc(sphere.transform.position, collisionOutsideRadius, _colliders,
                    sphereCollisionTableMask);
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
                    objectWithPinza1 = _colliders[0].gameObject.transform.root.gameObject;
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
                    objectWithPinza2 = _colliders[0].gameObject.transform.root.gameObject;
            }
            else
            {
                pinza2Collided = false;
                objectWithPinza2 = null;
            }

            if (detectPinzaCollisions)
            {


                if (IsPinza1CollidedOutside() || IsPinza1CollidedTable())
                {
                    pinza1CollidedOutside = true;

                }
                else
                {
                    pinza1CollidedOutside = false;
                }

                if (IsPinza2CollidedOutside() || IsPinza2CollidedTable())
                {
                    pinza2CollidedOutside = true;
                }
                else
                {
                    pinza2CollidedOutside = false;
                }
            }

            if (pinza1Collided && pinza2Collided && !pinza1CollidedOutside && !pinza2CollidedOutside &&
                !pinzaIsCollideElectricEdge)
            {
                if (objectWithPinza1 != null && objectWithPinza2 != null)
                {
                    if (objectWithPinza1 == objectWithPinza2)
                    {
                        if(!objectWithPinza1.GetComponent<ObjectPinzabile>().objectPicked)
                        {
                            collided = true;
                            objectWithPinza1.gameObject.transform.SetParent(transform);
                            objectWithPinza1.gameObject.transform.GetComponent<Rigidbody>().isKinematic = true;
                            objectWithPinza1.gameObject.GetComponent<ObjectPinzabile>().SetHasInteract();
                        }

                    }
                }
            }
        }
    }


    public void ResetPinze()
    {                        
        animatorPinza1.SetBool("CloseRight", false);
        animatorPinza2.SetBool("CloseLeft", false);
        animatorPinza1.SetFloat("Right", 0f);
        animatorPinza2.SetFloat("Left", 0f);
       // animatorPinza2.SetBool("ClosePinza2",false);
        
        if (objectWithPinza1 != null)
        {
            objectWithPinza1.gameObject.transform.SetParent(null);
            objectWithPinza1.gameObject.transform.GetComponent<Rigidbody>().isKinematic = false;
            objectWithPinza1.gameObject.GetComponent<ObjectPinzabile>().ResetHasInteracted();
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

    public void RemoveObjectPinzatoPinzaTouch()
    {
        resetting = true;
        if (objectWithPinza1 != null)
        {
            objectWithPinza1.gameObject.transform.SetParent(null);
            objectWithPinza1.gameObject.transform.GetComponent<Rigidbody>().isKinematic = false;
            objectWithPinza1.gameObject.GetComponent<ObjectPinzabile>().ResetState();
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
    
    public void RemoveObjectPinzatoObjectTouch()
    {
        if (resetObjectAfterObjectTouch)
        {
            resetting = true;
            if (objectWithPinza1 != null)
            {
                objectWithPinza1.gameObject.transform.SetParent(null);
                objectWithPinza1.gameObject.transform.GetComponent<Rigidbody>().isKinematic = false;
                objectWithPinza1.gameObject.GetComponent<ObjectPinzabile>().ResetState();
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
        
        foreach (GameObject sphere in sphereTableListPinza1)
        {
            Gizmos.color = Color.magenta;
            Gizmos.DrawWireSphere(sphere.transform.position, collisionOutsideRadius);

        }

        foreach (GameObject sphere in sphereTableListPinza2)
        {
            Gizmos.color = Color.magenta;
            Gizmos.DrawWireSphere(sphere.transform.position, collisionOutsideRadius);
        }
    }
    
    public void ActiveVisualEffectPinza()
    {
        activateVisualEffectPinza = !activateVisualEffectPinza;
    }

   

    public void SetPinzaColorError()
    {
        if (activateVisualEffectPinza)
        {
            pinzaIsCollideElectricEdge = true;
            pinza1MateriaMeshRender.material.color=Color.red;
            pinza2MateriaMeshRender.material.color=Color.red;
        }
    }
    
    public void RemoveSetPinzaColorError()
    {
        if (activateVisualEffectPinza)
        {
            pinza1MateriaMeshRender.material.color = startPinza1Color;
            pinza2MateriaMeshRender.material.color = startPinza2Color;
            pinzaIsCollideElectricEdge = false;

        }
    }
}