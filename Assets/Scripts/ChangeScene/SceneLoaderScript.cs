using System.Collections;
using System.Collections.Generic;
using EventSystem2;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class SceneLoaderScript : MonoBehaviour
{
    
    
    private void Update()
    {
        if (!Keyboard.current.rKey.wasPressedThisFrame)
        {
            return;
        }

        if (SceneManager.GetActiveScene().name == "Allegro_Chirurgo_RightHandGrab")
        {
            SceneManager.LoadScene("Allegro_Chirurgo_RightHandGrab");
        }
        if (SceneManager.GetActiveScene().name == "Allegro_Chirurgo_LeftHandGrab")
        {
            SceneManager.LoadScene("Allegro_Chirurgo_LeftHandGrab");
        }
    }
    
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
    }
    
    public void LoadSceneLeftHandGrab()
    {

        SceneManager.LoadScene("Allegro_Chirurgo_LeftHandGrab");
    }

    
}
