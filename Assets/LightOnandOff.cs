using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightOnandOff : MonoBehaviour
{
 

    public GameObject nosePatient;
    public bool visualErrorSetting;
    private Color colorDefault;
    // Start is called before the first frame update
    void Start()
    { 
        if (GameObject.Find ("SceneLoader_Haptic")!=null)
        {
            Scene_Loader_Haptic sceneLoaderHaptic = GameObject.Find ("SceneLoader_Haptic").GetComponent<Scene_Loader_Haptic>();
            visualErrorSetting = sceneLoaderHaptic.visualErrorSetting;
        }
        if (GameObject.Find("ManageJsonToSaveDB") != null)
        {
            ManageJsonAndSettingsVR manageJsonAndSettings = GameObject.Find("ManageJsonToSaveDB").GetComponent<ManageJsonAndSettingsVR>();
            visualErrorSetting= manageJsonAndSettings.visualErrorSetting;
        }
        colorDefault = nosePatient.GetComponent<MeshRenderer>().material.color;
    }

    public void OnLight()
    {
        if (visualErrorSetting)
        {
            //nosePatient.GetComponent<MeshRenderer>().material.color=Color.red;
            nosePatient.GetComponent<MeshRenderer>().material.EnableKeyword("_EMISSION");
 
        }
          }
    
    public void OffLight()
    {
        if (visualErrorSetting)
        {
            // nosePatient.GetComponent<MeshRenderer>().material.color=colorDefault;
            nosePatient.GetComponent<MeshRenderer>().material.DisableKeyword("_EMISSION");
        }
    }

}
