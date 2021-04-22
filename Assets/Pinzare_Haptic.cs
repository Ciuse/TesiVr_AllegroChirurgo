using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Object = UnityEngine.Object;

public class Pinzare_Haptic : MonoBehaviour
{
    
    public Animator animatorPinza1;
    public Animator animatorPinza2;
    private float _smoothClosePinza1=0f;
    private float _smoothClosePinza2 = 0f;
    public bool collided;
    public bool pinza1Collided;
    public bool pinza2Collided;
    public List<GameObject> sphereListPinza1;
    public List<GameObject> sphereListPinza2;
    public float collisionRadius;
    public LayerMask sphereCollisionMask;
    public Collider[] _colliders = new Collider[10];
    public bool jointCreated;
    
    
    public int buttonID = 0;		//!< index of the button assigned to grabbing.  Defaults to the first button
	public bool ButtonActsAsToggle = false;	//!< Toggle button? as opposed to a press-and-hold setup?  Defaults to off.
	public enum PhysicsToggleStyle{ none, onTouch, onGrab };
	public PhysicsToggleStyle physicsToggleStyle = PhysicsToggleStyle.none;   //!< Should the grabber script toggle the physics forces on the stylus? 

	public bool DisableUnityCollisionsWithTouchableObjects = true;

	private  GameObject hapticDevice = null;   //!< Reference to the GameObject representing the Haptic Device
	private bool buttonStatus = false;			//!< Is the button currently pressed?
	private GameObject touching = null;			//!< Reference to the object currently touched
	private GameObject grabbing = null;			//!< Reference to the object currently grabbed
	private FixedJoint joint = null;			//!< The Unity physics joint created between the stylus and the object being grabbed.
	
	
	//! Automatically called for initialization
	void Start () 
	{
		if (hapticDevice == null)
		{

			HapticPlugin[] HPs = (HapticPlugin[])Object.FindObjectsOfType(typeof(HapticPlugin));
			foreach (HapticPlugin HP in HPs)
			{
				if (HP.hapticManipulator == this.gameObject)
				{
					hapticDevice = HP.gameObject;
				}
			}

		}

		if ( physicsToggleStyle != PhysicsToggleStyle.none)
			hapticDevice.GetComponent<HapticPlugin>().PhysicsManipulationEnabled = false;

		if (DisableUnityCollisionsWithTouchableObjects)
			disableUnityCollisions();
	}

	void disableUnityCollisions()
	{
		GameObject[] touchableObjects;
		touchableObjects =  GameObject.FindGameObjectsWithTag("Touchable") as GameObject[];  //FIXME  Does this fail gracefully?

		// Ignore my collider
		Collider myC = gameObject.GetComponent<Collider>();
		if (myC != null)
			foreach (GameObject T in touchableObjects)
			{
				Collider CT = T.GetComponent<Collider>();
				if (CT != null)
					Physics.IgnoreCollision(myC, CT);
			}
		
		// Ignore colliders in children.
		Collider[] colliders = gameObject.GetComponentsInChildren<Collider>();
		foreach (Collider C in colliders)
			foreach (GameObject T in touchableObjects)
			{
				Collider CT = T.GetComponent<Collider>();
				if (CT != null)
					Physics.IgnoreCollision(C, CT);
			}

	}

	//! Update is called once per frame
	void FixedUpdate () 
	{
		if (jointCreated)
		{
			if (joint == null)
			{
				jointCreated = false;
				release();
				ResetPinze();
			}
		}
	
		bool newButtonStatus = hapticDevice.GetComponent<HapticPlugin>().Buttons [buttonID] == 1;
		buttonStatus = newButtonStatus;
		// check collision with tweezer and object
		if (buttonStatus)
		{
			animatorPinza1.SetBool("CloseLeft", true);
			animatorPinza2.SetBool("CloseRight", true);

			if (!collided)
			{
				if (!pinza1Collided && _smoothClosePinza1<=1)
				{
					if (buttonStatus)
					{
						_smoothClosePinza1 = _smoothClosePinza1 + 0.02f;
					}
					else
					{
						_smoothClosePinza1 = _smoothClosePinza1 - 0.02f;
					}

					animatorPinza1.SetFloat("Left", _smoothClosePinza1);
				}

				if (!pinza2Collided && _smoothClosePinza2<=1)
				{
					if (buttonStatus)
					{
						_smoothClosePinza2 = _smoothClosePinza2 + 0.02f;
					}
					else
					{
						_smoothClosePinza2 = _smoothClosePinza2 - 0.02f;
					}

					animatorPinza2.SetFloat("Right", _smoothClosePinza2);
				}
				CheckCollidersWhileNoObject();
			}
		} else
		{
			ResetPinze();
		}    
		
		
		if (buttonStatus)
		{ 
			if(collided) 
				grab();
		}
		if (!buttonStatus)
		{
			release();
		}

		// Make sure haptics is ON if we're grabbing
		if( grabbing && physicsToggleStyle != PhysicsToggleStyle.none)
			hapticDevice.GetComponent<HapticPlugin>().PhysicsManipulationEnabled = true;
		if (!grabbing && physicsToggleStyle == PhysicsToggleStyle.onGrab)
			hapticDevice.GetComponent<HapticPlugin>().PhysicsManipulationEnabled = false;

		/*
		if (grabbing)
			hapticDevice.GetComponent<HapticPlugin>().shapesEnabled = false;
		else
			hapticDevice.GetComponent<HapticPlugin>().shapesEnabled = true;
			*/
			
	}

	public void ResetPinze()
	{
		animatorPinza1.SetBool("CloseLeft",false);
		animatorPinza2.SetBool("CloseRight",false);
		
		pinza1Collided = false;
		pinza2Collided = false;

		collided = false;

		if (_smoothClosePinza1 > 0)
		{
			_smoothClosePinza1 = _smoothClosePinza1 - 0.02f;
			animatorPinza1.SetFloat("Left", _smoothClosePinza1);
	
		}

		if (_smoothClosePinza2 > 0)
		{
			_smoothClosePinza2 = _smoothClosePinza2 - 0.02f;
			animatorPinza2.SetFloat("Right", _smoothClosePinza2);
		}
	}

	public void CheckCollidersWhileNoObject()
	{
	
		if (IsPinza1Collided())
		{
			pinza1Collided = true;
		}
		else
		{
			pinza1Collided = false;
		}
		
		if (IsPinza2Collided())
		{
			pinza2Collided = true;
		}
		else
		{
			pinza2Collided = false;
		}
		
		if (pinza1Collided && pinza2Collided)
		{
			collided = true;
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
	
	private void hapticTouchEvent( bool isTouch )
	{
		if (physicsToggleStyle == PhysicsToggleStyle.onGrab)
		{
			if (isTouch)
				hapticDevice.GetComponent<HapticPlugin>().PhysicsManipulationEnabled = true;
			else			
				return; // Don't release haptics while we're holding something.
		}
			
		if( physicsToggleStyle == PhysicsToggleStyle.onTouch )
		{
			hapticDevice.GetComponent<HapticPlugin>().PhysicsManipulationEnabled = isTouch;
			GetComponentInParent<Rigidbody>().velocity = Vector3.zero;
			GetComponentInParent<Rigidbody>().angularVelocity = Vector3.zero;

		}
	}

	void OnCollisionEnter(Collision collisionInfo)
	{
		Collider other = collisionInfo.collider;
		//Debug.unityLogger.Log("OnCollisionEnter : " + other.name);
		GameObject that = other.gameObject;
		Rigidbody thatBody = that.GetComponent<Rigidbody>();

		// If this doesn't have a rigidbody, walk up the tree. 
		// It may be PART of a larger physics object.
		while (thatBody == null)
		{
			//Debug.logger.Log("Touching : " + that.name + " Has no body. Finding Parent. ");
			if (that.transform == null || that.transform.parent == null)
				break;
			GameObject parent = that.transform.parent.gameObject;
			if (parent == null)
				break;
			that = parent;
			thatBody = that.GetComponent<Rigidbody>();
		}

		if( collisionInfo.rigidbody != null )
			hapticTouchEvent(true);

		if (thatBody == null)
			return;

		if (thatBody.isKinematic)
			return;
	
		touching = that;
	}
	void OnCollisionExit(Collision collisionInfo)
	{
		Collider other = collisionInfo.collider;
		//Debug.unityLogger.Log("onCollisionrExit : " + other.name);

		if( collisionInfo.rigidbody != null )
			hapticTouchEvent( false );

		if (touching == null)
			return; // Do nothing

		if (other == null ||
		    other.gameObject == null || other.gameObject.transform == null)
			return; // Other has no transform? Then we couldn't have grabbed it.

		if( touching == other.gameObject || other.gameObject.transform.IsChildOf(touching.transform))
		{
			touching = null;
		}
	}
		
	//! Begin grabbing an object. (Like closing a claw.) Normally called when the button is pressed. 
	void grab()
	{
		GameObject touchedObject = touching;
		if (touchedObject == null) // No Unity Collision? 
		{
			// Maybe there's a Haptic Collision
			touchedObject = hapticDevice.GetComponent<HapticPlugin>().touching;
		}

		if (grabbing != null) // Already grabbing
			return;
		if (touchedObject == null) // Nothing to grab
			return;

		// Grabbing a grabber is bad news.
		if (touchedObject.tag =="Gripper")
			return;

		Debug.Log( " Object : " + touchedObject.name + "  Tag : " + touchedObject.tag );

		grabbing = touchedObject;

		//Debug.logger.Log("Grabbing Object : " + grabbing.name);
		Rigidbody body = grabbing.GetComponent<Rigidbody>();

		// If this doesn't have a rigidbody, walk up the tree. 
		// It may be PART of a larger physics object.
		while (body == null)
		{
			//Debug.logger.Log("Grabbing : " + grabbing.name + " Has no body. Finding Parent. ");
			if (grabbing.transform.parent == null)
			{
				grabbing = null;
				return;
			}
			GameObject parent = grabbing.transform.parent.gameObject;
			if (parent == null)
			{
				grabbing = null;
				return;
			}
			grabbing = parent;
			body = grabbing.GetComponent<Rigidbody>();
		}

		joint = (FixedJoint)gameObject.AddComponent(typeof(FixedJoint));
		joint.connectedBody = body;
         		joint.breakForce = 10f;
		jointCreated = true;
	}
	//! changes the layer of an object, and every child of that object.
	static void SetLayerRecursively(GameObject go, int layerNumber )
	{
		if( go == null ) return;
		foreach(Transform trans in go.GetComponentsInChildren<Transform>(true))
			trans.gameObject.layer = layerNumber;
	}

	//! Stop grabbing an obhject. (Like opening a claw.) Normally called when the button is released. 
	public void release()
	{
		if( grabbing == null ) //Nothing to release
			return;

		if (joint != null)
		{
			Debug.Assert(joint != null);

			joint.connectedBody = null;
			Destroy(joint);
		}

		grabbing = null;

		if (physicsToggleStyle != PhysicsToggleStyle.none)
			hapticDevice.GetComponent<HapticPlugin>().PhysicsManipulationEnabled = false;
	}

	//! Returns true if there is a current object. 
	public bool isGrabbing()
	{
		return (grabbing != null);
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
