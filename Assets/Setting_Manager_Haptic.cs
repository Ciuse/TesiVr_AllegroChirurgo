using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Setting_Manager_Haptic : MonoBehaviour
{
    public Scene_Loader_Haptic sceneLoaderHaptic;
    void Start()
    {
        sceneLoaderHaptic = GameObject.Find ("SceneLoader_Haptic").GetComponent<Scene_Loader_Haptic>();
        print("Visual effect object: "+ sceneLoaderHaptic.visualObjectSetting);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
