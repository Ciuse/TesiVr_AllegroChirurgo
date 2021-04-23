using System;
using System.Collections;
using System.Collections.Generic;
using EventSystem2;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scene_Loader_Haptic : MonoBehaviour
{
    public bool vibrationSetting;
    public bool hideHandSetting;
    public bool visualPinzaSetting;
    public bool visualObjectSetting;
    public bool soundObjectSetting;
    public bool detectObjectCollision;
    public bool detectPinzaCollision;
   
    public void Start()
    {
       
        DontDestroyOnLoad(this);
    }


    public void LoadSceneOnlyHaptic()
    {
        SceneManager.LoadScene(4);
   
    }
    
    
    
    public void LoadStartScene()
    {
        SceneManager.LoadScene(3);

    }

    public void Vibration()
    {
        vibrationSetting = !vibrationSetting;
    }
    
    public void HideHand()
    {
        hideHandSetting = !hideHandSetting;
    }
    
    public void VisualPinza()
    {
        visualPinzaSetting = !visualPinzaSetting;
    }
    
    public void VisualObject()
    {
        visualObjectSetting = !visualObjectSetting;
    }
    
    public void SoundObject()
    {
        soundObjectSetting = !soundObjectSetting;
    }
    
    public void DetectObjectCollision()
    {
        detectObjectCollision = !detectObjectCollision;
    }
    
    public void DetectPinzaCollision()
    {
        detectPinzaCollision = !detectPinzaCollision;
    }
}
