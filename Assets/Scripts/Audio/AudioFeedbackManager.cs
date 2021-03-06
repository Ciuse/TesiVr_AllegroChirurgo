﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioFeedbackManager : MonoBehaviour
{
    public GameObject pinzaSFX;
    public GameObject objectSFX;
    public GameObject UISFX;

    public void Start()
    {
        if (GameObject.Find ("SceneLoader_Haptic")!=null)
        {
            Scene_Loader_Haptic sceneLoaderHaptic = GameObject.Find ("SceneLoader_Haptic").GetComponent<Scene_Loader_Haptic>();
            pinzaSFX.SetActive(sceneLoaderHaptic.soundObjectSetting);
            objectSFX.SetActive(sceneLoaderHaptic.soundObjectSetting);
           
        }
        if (GameObject.Find("ManageJsonToSaveDB") != null)
        {
            ManageJsonAndSettingsVR manageJsonAndSettings = GameObject.Find("ManageJsonToSaveDB").GetComponent<ManageJsonAndSettingsVR>();
            pinzaSFX.SetActive(manageJsonAndSettings.soundObjectSetting);
            objectSFX.SetActive(manageJsonAndSettings.soundObjectSetting);
        }
        
    }

    
}
