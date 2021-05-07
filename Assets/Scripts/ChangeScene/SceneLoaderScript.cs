using System.Collections;
using System.Collections.Generic;
using EventSystem2;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoaderScript : MonoBehaviour
{

    public GameEvent createNewSessionId;
    public GameEvent createNewMatchId;

    public void LoadSceneRightHandTraining()
    {
        SceneManager.LoadScene("Allegro_Chirurgo_Training_RightHandGrab");
    }
    
    public void LoadSceneLeftHandTraining()
    {
       
        SceneManager.LoadScene("Allegro_Chirurgo_Training_LeftHandGrab");
        
    }
    
    public void LoadSceneRightHandGrab()
    {
        SceneManager.LoadScene("Allegro_Chirurgo_RightHandGrab");
        createNewMatchId.Raise();
    }
    
    public void LoadSceneLeftHandGrab()
    {
        SceneManager.LoadScene("Allegro_Chirurgo_LeftHandGrab");
        createNewMatchId.Raise();
    }

    
}
