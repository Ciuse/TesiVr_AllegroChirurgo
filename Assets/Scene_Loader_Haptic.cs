using System;
using EventSystem2;
using Firebase.Database;
using Firebase.Database.Query;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scene_Loader_Haptic : MonoBehaviour
{
    public bool vibrationSetting;
    public bool visualObjectSetting;
    public bool soundObjectSetting;
    public bool detectObjectCollision;
    public bool detectPinzaCollision;
    public FirebaseClient firebase;
    private String sessionCode;
    public String device;
    
    public void Start()
    {
        DontDestroyOnLoad(this);
        firebase = new FirebaseClient("https://allegrochirurgovr-default-rtdb.europe-west1.firebasedatabase.app/");
        sessionCode = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss");
        SaveStartingSetting();
        GameStartedWithHaptic();
    }

    public async void GameStartedWithHaptic()
    {
        String deviceUsed = "\""+device+"\"";
        await firebase.Child(sessionCode).Child("SETTINGS").PatchAsync("{\"deviceUsed\":"+ deviceUsed+"}");
    }
    
    public async void SaveStartingSetting()
    {
        //await firebase.Child(sessionCode).Child("SETTINGS").PatchAsync("{\"hideHand\":"+hideHandSetting.ToString().ToLower()+"}");
        await firebase.Child(sessionCode).Child("SETTINGS").PatchAsync("{\"sound\":"+ soundObjectSetting.ToString().ToLower()+"}");
        await firebase.Child(sessionCode).Child("SETTINGS").PatchAsync("{\"visualObject\":"+ visualObjectSetting.ToString().ToLower()+"}");
        await firebase.Child(sessionCode).Child("SETTINGS").PatchAsync("{\"vibration\":"+ vibrationSetting.ToString().ToLower()+"}");
       
    }
    
    public void LoadSceneOnlyHaptic()
    {
        SceneManager.LoadScene(4);
   
    }
    
    
    
    public void LoadSceneHapticAndVR()
    {
        SceneManager.LoadScene("Allegro_Chirurgo_Training_Haptic_VR");

    }

    public async void Vibration()
    {
        vibrationSetting = !vibrationSetting;
        await firebase.Child(sessionCode).Child("SETTINGS").PatchAsync("{\"vibration\":"+ vibrationSetting.ToString().ToLower()+"}");
    }
    
    
    public async void VisualObject()
    {
        visualObjectSetting = !visualObjectSetting;
        await firebase.Child(sessionCode).Child("SETTINGS").PatchAsync("{\"visualObject\":"+ visualObjectSetting.ToString().ToLower()+"}");
    }
    
    public async void SoundObject()
    {
        soundObjectSetting = !soundObjectSetting;
        await firebase.Child(sessionCode).Child("SETTINGS").PatchAsync("{\"sound\":"+ soundObjectSetting.ToString().ToLower()+"}");
    }
    
    public void DetectObjectCollision()
    {
        detectObjectCollision = !detectObjectCollision;
    }
    
    public void DetectPinzaCollision()
    {
        detectPinzaCollision = !detectPinzaCollision;
    }
    
    public async void SaveJsonObject(Interactable interactable)
    {
        JsonObject jsonObjectToSave= interactable.interactedObject.GetComponent<TrackingScript>().jsonObjectToSave;
        await firebase.Child(sessionCode).Child("ListOfObject")
            .PostAsync(JsonConvert.SerializeObject(jsonObjectToSave,Formatting.None,
                new JsonSerializerSettings()
                { 
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                }));
//            .ContinueWith(interactable.interactedObject.GetComponent<TrackingScript>().jsonObjectToSave=new JsonObject());
//        

    }

}
