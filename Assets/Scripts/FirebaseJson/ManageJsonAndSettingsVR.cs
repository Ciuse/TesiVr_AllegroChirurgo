using System;
using System.Collections;
using EventSystem2;
using Firebase.Database;
using Firebase.Database.Query;
using Newtonsoft.Json;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class ManageJsonAndSettingsVR : MonoBehaviour
{
    public FirebaseClient firebase;
    private string sessionCode;
    private string complex="complexData";  
    private string simple="simpleData";
    private string userId = "-1";
    private string currentMatchId;
    public int matchId=0;
    public string handUsed;
    public bool vibrationSetting;
    public bool showHandSetting;
    public bool visualErrorSetting;
    public bool visualPinzaWarningSetting;
    public bool visualPickingUpObjectSetting;
    public bool soundObjectSetting;
    public bool detectObjectCollision;
    public bool detectPinzaCollision;

    public TextMeshProUGUI userTextId;
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
        await firebase.Child(sessionCode).Child(complex).Child("Settings").PatchAsync("{\"deviceUsed\":"+ deviceUsed+"}"); 
        await firebase.Child(sessionCode).Child(simple).PatchAsync("{\"deviceUsed\":"+ deviceUsed+"}");

    }

    public async void SceneStartedWithRightHand()
    {
        print("Scene started with right hand");
        handUsed = "\"rightHand\"";
        await firebase.Child(sessionCode).Child(complex).Child("Settings").PatchAsync("{\"hand\":"+ handUsed+"}");
        await firebase.Child(sessionCode).Child(complex).PatchAsync("{\"userId\":"+ userId.ToLower()+"}");
        await firebase.Child(sessionCode).Child(simple).PatchAsync("{\"userId\":"+ userId.ToLower()+"}");

        
    }
    
    public async void SceneStartedWithLeftHand()
    {
        print("Scene started with left hand");
        handUsed = "\"leftHand\"";
        await firebase.Child(sessionCode).Child(complex).Child("Settings").PatchAsync("{\"hand\":"+ handUsed+"}");
        await firebase.Child(sessionCode).Child(complex).PatchAsync("{\"userId\":"+ userId.ToLower()+"}");
        await firebase.Child(sessionCode).Child(simple).PatchAsync("{\"userId\":"+ userId.ToLower()+"}");
    }
    
    public void SetUserId()
    {
        userId = userTextId.text;
    }

    public async void ShowHandSetting()
    {
        showHandSetting = !showHandSetting;
        await firebase.Child(sessionCode).Child(complex).Child("Settings").PatchAsync("{\"showHand\":"+ showHandSetting.ToString().ToLower()+"}");
    }
    
    public async void VibrationFeedbackSetting()
    {
        vibrationSetting = !vibrationSetting;
        await firebase.Child(sessionCode).Child(complex).Child("Settings").PatchAsync("{\"vibration\":"+ vibrationSetting.ToString().ToLower()+"}");
    }
    
    public async void VisualErrorSetting()
    {
        visualErrorSetting = !visualErrorSetting;
        await firebase.Child(sessionCode).Child(complex).Child("Settings").PatchAsync("{\"visualError\":"+ visualErrorSetting.ToString().ToLower()+"}");
    }
    public async void VisualWarningPinzaSetting()
    {
        visualPinzaWarningSetting = !visualPinzaWarningSetting;
        await firebase.Child(sessionCode).Child(complex).Child("Settings").PatchAsync("{\"visualWarningPinza\":"+ visualPinzaWarningSetting.ToString().ToLower()+"}");
    }
    
    public async void VisualCorrectObjectSetting()
    {
        visualPickingUpObjectSetting = !visualPickingUpObjectSetting;
        await firebase.Child(sessionCode).Child(complex).Child("Settings").PatchAsync("{\"visualCorrectObject\":"+ visualPickingUpObjectSetting.ToString().ToLower()+"}");
    }
    public async void SoundObjectSetting()
    {
        soundObjectSetting = !soundObjectSetting;
        await firebase.Child(sessionCode).Child(complex).Child("Settings").PatchAsync("{\"sound\":"+ soundObjectSetting.ToString().ToLower()+"}");
    }
    
    
    public async void DetectPinzaCollisionSetting()
    {
        detectPinzaCollision = !detectPinzaCollision;
        await firebase.Child(sessionCode).Child(complex).Child("Settings").PatchAsync("{\"detectPinzaCollision\":"+ detectPinzaCollision.ToString().ToLower()+"}");
    }
    public async void DetectObjectCollisionSetting()
    {
        detectObjectCollision = !detectObjectCollision;
        await firebase.Child(sessionCode).Child(complex).Child("Settings").PatchAsync("{\"detectObjectCollision\":"+ detectObjectCollision.ToString().ToLower()+"}");
    }

    public async void SaveStartingSetting()
    {
       await firebase.Child(sessionCode).Child(complex).Child("Settings").PatchAsync("{\"hideHand\":"+showHandSetting.ToString().ToLower()+"}");
        await firebase.Child(sessionCode).Child(complex).Child("Settings").PatchAsync("{\"sound\":"+ soundObjectSetting.ToString().ToLower()+"}");
        await firebase.Child(sessionCode).Child(complex).Child("Settings").PatchAsync("{\"visualObject\":"+ visualPickingUpObjectSetting.ToString().ToLower()+"}");
        await firebase.Child(sessionCode).Child(complex).Child("Settings").PatchAsync("{\"vibration\":"+ vibrationSetting.ToString().ToLower()+"}");
        await firebase.Child(sessionCode).Child(complex).Child("Settings").PatchAsync("{\"visualPinza\":"+ visualErrorSetting.ToString().ToLower()+"}");
        await firebase.Child(sessionCode).Child(complex).Child("Settings").PatchAsync("{\"detectPinzaCollision\":"+ detectPinzaCollision.ToString().ToLower()+"}");
        await firebase.Child(sessionCode).Child(complex).Child("Settings").PatchAsync("{\"detectObjectCollision\":"+ detectObjectCollision.ToString().ToLower()+"}");
        await firebase.Child(sessionCode).Child(complex).Child("Settings").PatchAsync("{\"visualWarningPinza\":"+ visualPinzaWarningSetting.ToString().ToLower()+"}");

    }

    public void SaveJsonObject(Interactable interactable)
    {       
        JsonObject jsonObjectToSave= interactable.interactedObject.GetComponent<TrackingScript>().jsonObjectToSave;
        StartCoroutine(AsyncData(jsonObjectToSave));

        JsonObject jsonObjectSimple = jsonObjectToSave;
        jsonObjectSimple.trajectoryList = null;
        StartCoroutine(AsyncData2(jsonObjectSimple));

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
        firebase.Child(sessionCode).Child(complex).Child("listOfMatch").Child(currentMatchId).Child("listOfObject")
            .PostAsync(data);
        yield return null; 
    }

     IEnumerator AsyncData2(JsonObject jsonObjectSimple)
     {
     
         string data = JsonUtility.ToJson(jsonObjectSimple);
         yield return new WaitForSeconds(0.2f);
         StartCoroutine(AsyncSave2(data));
         yield return null; 
     }
    
     IEnumerator AsyncSave2 (String data)
     { 
         firebase.Child(sessionCode).Child(simple).Child("listOfMatch").Child(currentMatchId).Child("listOfObject")
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
        print("Nuovo match" + currentMatchId);
        }
        
    public async void SaveMatchDuration(int duration)
    {
        print("{\"duration\":"+ duration.ToString().ToLower()+"}");
        await firebase.Child(sessionCode).Child(complex).Child("listOfMatch").Child(currentMatchId).PatchAsync("{\"duration\":"+ duration.ToString().ToLower()+"}");
        await firebase.Child(sessionCode).Child(simple).Child("listOfMatch").Child(currentMatchId).PatchAsync("{\"duration\":"+ duration.ToString().ToLower()+"}");
    }

}
