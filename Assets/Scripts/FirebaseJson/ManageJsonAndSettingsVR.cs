using System;
using System.Collections;
using EventSystem2;
using Firebase.Database;
using Firebase.Database.Query;
using Newtonsoft.Json;
using UnityEngine;

public class ManageJsonAndSettingsVR : MonoBehaviour
{
    public FirebaseClient firebase;
    private String sessionCode;    
    private String currentMatchId;
    public int matchId=0;
    public String handUsed;
    public bool vibrationSetting;
    public bool hideHandSetting;
    public bool visualPinzaSetting;
    public bool visualObjectSetting;
    public bool soundObjectSetting;
    public bool detectObjectCollision;
    public bool detectPinzaCollision;

    // Start is called before the first frame update
    private static ManageJsonAndSettingsVR singleton;
     
    void Awake()
    {
        if(!singleton)
        {
            singleton = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    void Start()
    {
        firebase = new FirebaseClient("https://allegrochirurgovr-default-rtdb.europe-west1.firebasedatabase.app/");
        sessionCode = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss");
        SaveStartingSetting();
        GameStartedWithVR();

    }

    public async void GameStartedWithVR()
    {
        String deviceUsed = "\"Oculus_VR\"";
        await firebase.Child(sessionCode).Child("Settings").PatchAsync("{\"deviceUsed\":"+ deviceUsed+"}");
    }

    public async void SceneStartedWithRightHand()
    {
        print("Scene started with right hand");
        handUsed = "\"rightHand\"";
        await firebase.Child(sessionCode).Child("Settings").PatchAsync("{\"hand\":"+ handUsed+"}");

        
    }
    
    public async void SceneStartedWithLeftHand()
    {
        print("Scene started with left hand");
        handUsed = "\"leftHand\"";
        await firebase.Child(sessionCode).Child("Settings").PatchAsync("{\"hand\":"+ handUsed+"}");
    }
    

    
    public async void HideHandSetting()
    {
        hideHandSetting = !hideHandSetting;
        await firebase.Child(sessionCode).Child("Settings").PatchAsync("{\"hideHand\":"+ hideHandSetting.ToString().ToLower()+"}");
    }
    
    public async void VibrationFeedbackSetting()
    {
        vibrationSetting = !vibrationSetting;
        await firebase.Child(sessionCode).Child("Settings").PatchAsync("{\"vibration\":"+ vibrationSetting.ToString().ToLower()+"}");
    }
    
    public async void VisualPinzaSetting()
    {
        visualPinzaSetting = !visualPinzaSetting;
        await firebase.Child(sessionCode).Child("Settings").PatchAsync("{\"visualPinza\":"+ visualPinzaSetting.ToString().ToLower()+"}");
    }
    
    public async void VisualObjectSetting()
    {
        visualObjectSetting = !visualObjectSetting;
        await firebase.Child(sessionCode).Child("Settings").PatchAsync("{\"visualObject\":"+ visualObjectSetting.ToString().ToLower()+"}");
    }
    public async void SoundObjectSetting()
    {
        soundObjectSetting = !soundObjectSetting;
        await firebase.Child(sessionCode).Child("Settings").PatchAsync("{\"sound\":"+ soundObjectSetting.ToString().ToLower()+"}");
    }
    
    
    public async void DetectPinzaCollisionSetting()
    {
        detectPinzaCollision = !detectPinzaCollision;
        await firebase.Child(sessionCode).Child("Settings").PatchAsync("{\"detectPinzaCollision\":"+ detectPinzaCollision.ToString().ToLower()+"}");
    }
    public async void DetectObjectCollisionSetting()
    {
        detectObjectCollision = !detectObjectCollision;
        await firebase.Child(sessionCode).Child("Settings").PatchAsync("{\"detectObjectCollision\":"+ detectObjectCollision.ToString().ToLower()+"}");
    }

    public async void SaveStartingSetting()
    {
       await firebase.Child(sessionCode).Child("Settings").PatchAsync("{\"hideHand\":"+hideHandSetting.ToString().ToLower()+"}");
        await firebase.Child(sessionCode).Child("Settings").PatchAsync("{\"sound\":"+ soundObjectSetting.ToString().ToLower()+"}");
        await firebase.Child(sessionCode).Child("Settings").PatchAsync("{\"visualObject\":"+ visualObjectSetting.ToString().ToLower()+"}");
        await firebase.Child(sessionCode).Child("Settings").PatchAsync("{\"vibration\":"+ vibrationSetting.ToString().ToLower()+"}");
        await firebase.Child(sessionCode).Child("Settings").PatchAsync("{\"visualPinza\":"+ visualPinzaSetting.ToString().ToLower()+"}");
        await firebase.Child(sessionCode).Child("Settings").PatchAsync("{\"detectPinzaCollision\":"+ detectPinzaCollision.ToString().ToLower()+"}");
        await firebase.Child(sessionCode).Child("Settings").PatchAsync("{\"detectObjectCollision\":"+ detectObjectCollision.ToString().ToLower()+"}");

    }

    public void SaveJsonObject(Interactable interactable)
    {       
        JsonObject jsonObjectToSave= interactable.interactedObject.GetComponent<TrackingScript>().jsonObjectToSave;
        
            
         StartCoroutine(AsyncData(jsonObjectToSave));
//            .ContinueWith(interactable.interactedObject.GetComponent<TrackingScript>().jsonObjectToSave=new JsonObject());
//        
    }
    IEnumerator AsyncData(JsonObject jsonObjectToSave)
    {
//        string data= JsonConvert.SerializeObject(jsonObjectToSave, Formatting.None,
//            new JsonSerializerSettings()
//            {
//                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
//            });      
        string data = JsonUtility.ToJson(jsonObjectToSave);
        yield return new WaitForSeconds(0.1f);
        StartCoroutine(AsyncSave(data));
        yield return null; 
    }
    
     IEnumerator AsyncSave (String data)
    { 
        firebase.Child(sessionCode).Child("listOfMatch").Child(currentMatchId).Child("listOfObject")
            .PostAsync(data);
        yield return null; 
    }

    public void CreateNewSessionId()
    {
        sessionCode = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss");
    }
    

    public void CreateNewMatchId()
        {
            matchId = matchId + 1;
            currentMatchId = "match"+matchId;
        }
        
    public async void SaveMatchDuration(int duration)
    {
        print("{\"duration\":"+ duration.ToString().ToLower()+"}");
        await firebase.Child(sessionCode).Child("listOfMatch").Child(currentMatchId).PatchAsync("{\"duration\":"+ duration.ToString().ToLower()+"}");
    }

}
