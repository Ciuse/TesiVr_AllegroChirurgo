using System;
using System.Collections;
using EventSystem2;
using Firebase.Database;
using Firebase.Database.Query;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scene_Loader_Haptic : MonoBehaviour
{
    public bool vibrationSetting;
    public bool soundObjectSetting;
    public bool visualCorrectObjectSetting;
    public bool visualErrorSetting;
    public bool showHandSetting;
    public bool detectObjectCollision;
    public bool detectPinzaCollision;
    public FirebaseClient firebase;
    private String sessionCode;
    public String device;
    public int matchId=0;   
    private String currentMatchId;
    
    public bool showRightHand;
    public bool showLeftHand;

    public Canvas optionCanvas;
    public Canvas selectHandCanvas;
    public void Start()
    {
        DontDestroyOnLoad(this);
        selectHandCanvas.enabled = false;
        firebase = new FirebaseClient("https://allegrochirurgovr-default-rtdb.europe-west1.firebasedatabase.app/");
        sessionCode = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss");
        SaveStartingSetting();
        GameStartedWithHaptic();
    }

    public async void GameStartedWithHaptic()
    {
        String deviceUsed = "\""+device+"\"";
        await firebase.Child(sessionCode).Child("Settings").PatchAsync("{\"deviceUsed\":"+ deviceUsed+"}");
    }
    
    public async void SaveStartingSetting()
    {
        await firebase.Child(sessionCode).Child("Settings").PatchAsync("{\"sound\":"+ soundObjectSetting.ToString().ToLower()+"}");
        await firebase.Child(sessionCode).Child("Settings").PatchAsync("{\"visualObject\":"+ visualCorrectObjectSetting.ToString().ToLower()+"}");
        await firebase.Child(sessionCode).Child("Settings").PatchAsync("{\"visualPinza\":"+ visualErrorSetting.ToString().ToLower()+"}");
        await firebase.Child(sessionCode).Child("Settings").PatchAsync("{\"vibration\":"+ vibrationSetting.ToString().ToLower()+"}");
        await firebase.Child(sessionCode).Child("Settings").PatchAsync("{\"showHand\":"+ showHandSetting.ToString().ToLower()+"}");

    }
    
    public void LoadSceneOnlyHaptic()
    {
        SceneManager.LoadScene(4);
   
    }
    
    
    
    public void LoadSceneHapticAndVR()
    {
        if (!showHandSetting)
        {
            SceneManager.LoadScene("Allegro_Chirurgo_Training_Haptic_VR");
        }
        else
        {
            //TODO aprire un altro pannello e chiedere quale mano mostrare
            optionCanvas.enabled = false;
            selectHandCanvas.enabled = true;
        }
    }

    public void LoadSceneHapticAndVRRightHandShow()
    {
        showRightHand = true;
        SetJsonRightHand();
        SceneManager.LoadScene("Allegro_Chirurgo_Training_Haptic_VR");
    
    }
    public void LoadSceneHapticAndVRLeftHandShow()
    {
        showLeftHand = true;
        SetJsonLeftHand();
        SceneManager.LoadScene("Allegro_Chirurgo_Training_Haptic_VR");

    }

    public async void Vibration()
    {
        vibrationSetting = !vibrationSetting;
        await firebase.Child(sessionCode).Child("Settings").PatchAsync("{\"vibration\":"+ vibrationSetting.ToString().ToLower()+"}");
    }
    
    
    public async void VisualCorrectObject()
    {
        visualCorrectObjectSetting = !visualCorrectObjectSetting;
        await firebase.Child(sessionCode).Child("Settings").PatchAsync("{\"visualObject\":"+ visualCorrectObjectSetting.ToString().ToLower()+"}");
    }
    
    public async void VisualError()
    {
        visualErrorSetting = !visualErrorSetting;
        await firebase.Child(sessionCode).Child("Settings").PatchAsync("{\"visualPinza\":"+ visualErrorSetting.ToString().ToLower()+"}");
    }
    public async void SoundObject()
    {
        soundObjectSetting = !soundObjectSetting;
        await firebase.Child(sessionCode).Child("Settings").PatchAsync("{\"sound\":"+ soundObjectSetting.ToString().ToLower()+"}");
    }
    
    public async void ShowHand()
    {
        showHandSetting = !showHandSetting;
        await firebase.Child(sessionCode).Child("Settings").PatchAsync("{\"showHand\":"+ showHandSetting.ToString().ToLower()+"}");
    }
    public async void SetJsonRightHand()
    {
        string handUsed = "\"rightHand\"";
        await firebase.Child(sessionCode).Child("Settings").PatchAsync("{\"hand\":"+ handUsed+"}");

        
    }
    
    public async void SetJsonLeftHand()
    {
        string handUsed = "\"leftHand\"";
        await firebase.Child(sessionCode).Child("Settings").PatchAsync("{\"hand\":"+ handUsed+"}");
    }
    
    public void DetectObjectCollision()
    {
        detectObjectCollision = !detectObjectCollision;
    }
    
    public void DetectPinzaCollision()
    {
        detectPinzaCollision = !detectPinzaCollision;
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
