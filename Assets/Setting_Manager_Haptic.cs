using System.Collections;
using System.Collections.Generic;
using EventSystem2;
using UnityEngine;

public class Setting_Manager_Haptic : MonoBehaviour
{
    private Scene_Loader_Haptic sceneLoaderHaptic;
    public GameEvent activateVisualEffectObject;
    
    void Start()
    {
        if (GameObject.Find ("SceneLoader_Haptic")!=null)
        {
            sceneLoaderHaptic = GameObject.Find ("SceneLoader_Haptic").GetComponent<Scene_Loader_Haptic>();
           
        }

        
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
