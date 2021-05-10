using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

public class Vibrate_effect_haptic : MonoBehaviour
{
    public enum EFFECT_TYPE { CONSTANT, VISCOUS, SPRING, FRICTION, VIBRATE };
  
    // Public, User-Adjustable Settings
    public EFFECT_TYPE effectType; //!< Which type of effect occurs within this zone?
    [Range(0.0f,1.0f)] public double Gain;	
    [Range(0.0f,1.0f)] public double Magnitude;
    [Range(1.0f,1000.0f)] public double Frequency;
    public Vector3 Position = Vector3.zero;
    public Vector3 Direction = Vector3.up;

    // These are the user adjustable vectors, converted to world-space. 
    private Vector3 focusPointWorld = Vector3.zero;
    private Vector3 directionWorld = Vector3.up;

    public bool collided = false;
    public bool electricEdgeTouched;
    public HapticPlugin device;
    public int FXID = -1;

    public List<GameObject> sphereListPinze;
    
    public float collisionRadius;
    public LayerMask sphereCollisionMask;
    public Collider[] _colliders = new Collider[10];
    public bool vibrationEffect;
    float elapsed = 0f;
  
    void Start () 
    {
	    if (device == null)
		    device = (HapticPlugin)FindObjectOfType(typeof(HapticPlugin));
		
	    if( device /* STILL */ == null )
		    Debug.LogError("This script requires that Haptic Device be assigned.");

	    collided = false;
	    print(HapticPlugin.effects_assignEffect(device.configName));
	   
	    if (GameObject.Find("SceneLoader_Haptic") != null)
	    {
            Scene_Loader_Haptic sceneLoaderHaptic = GameObject.Find("SceneLoader_Haptic").GetComponent<Scene_Loader_Haptic>();
            vibrationEffect = sceneLoaderHaptic.vibrationSetting;
	    }

    }

    public void Update()
    {
	    elapsed += Time.deltaTime;
	    if (elapsed >= 0.1f)
	    {
		    elapsed = elapsed % 0.1f;
		    if (vibrationEffect)
		    {
			    bool oldCollided = collided;

			    collided = electricEdgeTouched;

			    if (oldCollided != collided)
			    {
				    if (collided)
				    {
					    TurnEffectOn();
					    print("on");
				    }
				    else
				    {
					    TurnEffectOff();
					    print("off");
				    }
			    }
		    }
	    }

    }


    public void TurnEffectOn()
    {
	    if (vibrationEffect)
	    {


		    if (device == null) return; //If there is no device, bail out early.

		    // If a haptic effect has not been assigned through Open Haptics, assign one now.
		    if (FXID == -1)
		    {
			    FXID = HapticPlugin.effects_assignEffect(device.configName);

			    if (FXID == -1) // Still broken?
			    {
				    return;
			    }
		    }

		    // Send the effect settings to OpenHaptics.
		    double[] pos = {0.0, 0.0, 0.0}; // Position (not used for vibration)
		    double[] dir = {0.0, 1.0, 0.0}; // Direction of vibration


		    HapticPlugin.effects_settings(
			    device.configName,
			    FXID,
			    Gain, // Gain
			    Magnitude, // Magnitude
			    Frequency, // Frequency
			    pos, // Position (not used for vibration)
			    dir); //Direction.

		    HapticPlugin.effects_type(device.configName, FXID, 4); // Vibration effect == 4

		    HapticPlugin.effects_startEffect(device.configName, FXID);
	    }
    }

    public void TurnEffectOff()
    {
	    if (vibrationEffect)
	    {
		    if (device == null) return; //If there is no device, bail out early.
		    if (FXID == -1) return; //If there is no effect, bail out early.

		    HapticPlugin.effects_stopEffect(device.configName, FXID);
	    }
    }

    
    // public void OnTriggerEnter(Collider other)
    // {
    //
	   //  print("collisione");
	   //  if(LayerMask.NameToLayer("Electric Edge").CompareTo(other.gameObject.layer)==0)
	   //  {
		  //     
		  //   if (countCollision == 0 && !collided)
		  //   {
			 //    		   
			 //    collided = true;
			 //    TurnEffectOn();
    //
			 //    //HapticPlugin.effects_startEffect(device.configName, FXID );
		  //   }
		  //   countCollision++;
	   //  
	   //  }
    // }
    //
    // private void OnTriggerExit(Collider other)
    // {
	   //  if (other.gameObject.layer == LayerMask.NameToLayer("Electric Edge"))
	   //  {
		  //   if (countCollision == 1 && collided)
		  //   {
			 //    TurnEffectOff();
			 //    collided = false;
    //
			 //    //HapticPlugin.effects_stopEffect(device.configName, FXID);
		  //   }
		  //   countCollision--;
	   //  }
	   //  
    //
    // }
    
    private bool IsPinzeCollided()
    {
           
	    foreach (GameObject sphere in sphereListPinze)
	    {
		    var sphereMovableCollisions =
			    Physics.OverlapSphereNonAlloc(sphere.transform.position, collisionRadius, _colliders,
				    sphereCollisionMask);
		    var sphereMovableCollisions2 =
			    Physics.OverlapSphereNonAlloc(sphere.transform.position, collisionRadius, _colliders,
				    sphereCollisionMask);
		    if (sphereMovableCollisions > 0 || sphereMovableCollisions2 > 0)
		    {
			    return true;
		    }
	    }
	    return false;

    }

    void OnDestroy()
	{
		//For every haptic device, send a Stop event to OpenHaptics
		
			
			if (device == null)
				return;
			HapticPlugin.effects_stopEffect(device.configName, FXID);
		
	}

    void OnDisable()
    {


	    if (device == null)
		    return;
	    HapticPlugin.effects_stopEffect(device.configName, FXID);
	    //inTheZone [ii] = false;
    }

    private void OnDrawGizmos()
    {
	    foreach (GameObject sphere in sphereListPinze)
	    {
		    Gizmos.color = Color.red;
		    Gizmos.DrawWireSphere(sphere.transform.position, collisionRadius);

	    }
	    
    }
}
