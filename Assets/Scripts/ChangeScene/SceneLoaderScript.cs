using System.Collections;
using System.Collections.Generic;
using EventSystem2;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoaderScript : MonoBehaviour
{

    public GameEvent createNewSessionId;

    public void LoadSceneRightHandGrab()
    {
        SceneManager.LoadScene("Allegro_Chirurgo_Training_RightHandGrab");
    }
    
    public void LoadSceneLeftHandGrab()
    {
       
        SceneManager.LoadScene("Allegro_Chirurgo_Training_LeftHandGrab");
        
    }
    
    public void LoadStartScene()
    {
        SceneManager.LoadScene(0);
        createNewSessionId.Raise();
        
    }
    
}
