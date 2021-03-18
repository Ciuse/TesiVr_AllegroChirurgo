using System.Collections;
using System.Collections.Generic;
using EventSystem;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoaderScript : MonoBehaviour
{

    

    public void LoadSceneRightHandGrab()
    {
        SceneManager.LoadScene(1);
    }
    
    public void LoadSceneLeftHandGrab()
    {
       
        SceneManager.LoadScene(2);
        
    }
    
    public void LoadStartScene()
    {
        SceneManager.LoadScene(0);
        
    }
    
}
