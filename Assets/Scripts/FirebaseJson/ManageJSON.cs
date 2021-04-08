using System;
using EventSystem2;
using Firebase.Database;
using Firebase.Database.Query;
using Newtonsoft.Json;
using UnityEngine;

public class ManageJSON : MonoBehaviour
{
    public FirebaseClient firebase;
    public String sessionCode;
    public String handUsed;
    public bool vibrationSetting;
    public bool hideHandSetting;
    public bool visualPinzaSetting;
    public bool visualObjectSetting;
    public bool soundObjectSetting;
    public bool detectObjectCollision;
    public bool detectPinzaCollision;

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        firebase = new FirebaseClient("https://allegrochirurgovr-default-rtdb.europe-west1.firebasedatabase.app/");
        sessionCode = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss");
        SaveStartingSetting();
    }
    

    public async void SceneStartedWithRightHand()
    {
        print("Scene started with right hand");
        handUsed = "\"rightHand\"";
        await firebase.Child(sessionCode).Child("SETTINGS").PatchAsync("{\"hand\":"+ handUsed+"}");

        
    }
    
    public async void SceneStartedWithLeftHand()
    {
        print("Scene started with left hand");
        handUsed = "\"leftHand\"";
        await firebase.Child(sessionCode).Child("SETTINGS").PatchAsync("{\"hand\":"+ handUsed+"}");
    }
    

    
    public async void HideHandSetting()
    {
        hideHandSetting = !hideHandSetting;
        await firebase.Child(sessionCode).Child("SETTINGS").PatchAsync("{\"hideHand\":"+ hideHandSetting.ToString().ToLower()+"}");
    }
    
    public async void VibrationFeedbackSetting()
    {
        vibrationSetting = !vibrationSetting;
        await firebase.Child(sessionCode).Child("SETTINGS").PatchAsync("{\"vibration\":"+ vibrationSetting.ToString().ToLower()+"}");
    }
    
    public async void VisualPinzaSetting()
    {
        visualPinzaSetting = !visualPinzaSetting;
        await firebase.Child(sessionCode).Child("SETTINGS").PatchAsync("{\"visualPinza\":"+ visualPinzaSetting.ToString().ToLower()+"}");
    }
    
    public async void VisualObjectSetting()
    {
        visualObjectSetting = !visualObjectSetting;
        await firebase.Child(sessionCode).Child("SETTINGS").PatchAsync("{\"visualObject\":"+ visualObjectSetting.ToString().ToLower()+"}");
    }
    public async void SoundObjectSetting()
    {
        soundObjectSetting = !soundObjectSetting;
        await firebase.Child(sessionCode).Child("SETTINGS").PatchAsync("{\"sound\":"+ soundObjectSetting.ToString().ToLower()+"}");
    }
    
    
    public async void DetectPinzaCollisionSetting()
    {
        detectPinzaCollision = !detectPinzaCollision;
        await firebase.Child(sessionCode).Child("SETTINGS").PatchAsync("{\"detectPinzaCollision\":"+ detectPinzaCollision.ToString().ToLower()+"}");
    }
    public async void DetectObjectCollisionSetting()
    {
        detectObjectCollision = !detectObjectCollision;
        await firebase.Child(sessionCode).Child("SETTINGS").PatchAsync("{\"detectObjectCollision\":"+ detectObjectCollision.ToString().ToLower()+"}");
    }

    public async void SaveStartingSetting()
    {
       await firebase.Child(sessionCode).Child("SETTINGS").PatchAsync("{\"hideHand\":"+hideHandSetting.ToString().ToLower()+"}");
        await firebase.Child(sessionCode).Child("SETTINGS").PatchAsync("{\"sound\":"+ soundObjectSetting.ToString().ToLower()+"}");
        await firebase.Child(sessionCode).Child("SETTINGS").PatchAsync("{\"visualObject\":"+ visualObjectSetting.ToString().ToLower()+"}");
        await firebase.Child(sessionCode).Child("SETTINGS").PatchAsync("{\"vibration\":"+ vibrationSetting.ToString().ToLower()+"}");
        await firebase.Child(sessionCode).Child("SETTINGS").PatchAsync("{\"visualPinza\":"+ visualPinzaSetting.ToString().ToLower()+"}");
        await firebase.Child(sessionCode).Child("SETTINGS").PatchAsync("{\"detectPinzaCollision\":"+ detectPinzaCollision.ToString().ToLower()+"}");
        await firebase.Child(sessionCode).Child("SETTINGS").PatchAsync("{\"detectObjectCollision\":"+ detectObjectCollision.ToString().ToLower()+"}");

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


    public void CreateNewSessionId()
    {
        sessionCode = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss");
    }
    




}
