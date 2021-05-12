using System;
using System.Collections;
using EventSystem2;
using Firebase.Database;
using Firebase.Database.Query;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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
    public String sessionCode;
    private string complex="complexData";  
    private string simple="simpleData";
    public string userId = "-1";
    public String device;
    public int matchId=0;   
    public String currentMatchId;
    public Text text;
    
    public bool showRightHand;
    public bool showLeftHand;

    public Canvas optionCanvas;
    public Canvas selectHandCanvas;


    public void Update()
    {
        if (!Keyboard.current.rKey.wasPressedThisFrame)
        {
            return;
        }
        if (SceneManager.GetActiveScene().name == "Allegro_Chirurgo_Haptic_VR")
        {
            LoadGameHapticAndVR();
        }
    }

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
        await firebase.Child(sessionCode).Child(complex).Child("Settings").PatchAsync("{\"deviceUsed\":"+ deviceUsed+"}");
        await firebase.Child(sessionCode).Child(simple).PatchAsync("{\"deviceUsed\":"+ deviceUsed+"}");
    }
    
    public async void SaveStartingSetting()
    {
        await firebase.Child(sessionCode).Child(complex).Child("Settings").PatchAsync("{\"sound\":"+ soundObjectSetting.ToString().ToLower()+"}");
        await firebase.Child(sessionCode).Child(complex).Child("Settings").PatchAsync("{\"visualObject\":"+ visualCorrectObjectSetting.ToString().ToLower()+"}");
        await firebase.Child(sessionCode).Child(complex).Child("Settings").PatchAsync("{\"visualPinza\":"+ visualErrorSetting.ToString().ToLower()+"}");
        await firebase.Child(sessionCode).Child(complex).Child("Settings").PatchAsync("{\"vibration\":"+ vibrationSetting.ToString().ToLower()+"}");
        await firebase.Child(sessionCode).Child(complex).Child("Settings").PatchAsync("{\"showHand\":"+ showHandSetting.ToString().ToLower()+"}");

    }
    
    public void LoadSceneOnlyHaptic()
    {
        SceneManager.LoadScene(4);
   
    }

    public async void CheckUserIdAndStart()
    {
        userId = text.text;
        if(!userId.Equals("-1")&&!userId.Equals(""))
        {
            await firebase.Child(sessionCode).Child(complex).PatchAsync("{\"userId\":"+ userId.ToLower()+"}");
            await firebase.Child(sessionCode).Child(simple).PatchAsync("{\"userId\":"+ userId.ToLower()+"}");
            SceneManager.LoadScene("Allegro_Chirurgo_Training_Haptic_VR");
        }

    }
    
    
    public void LoadSceneHapticAndVR()
    {
        if (!showHandSetting)
        {
            CheckUserIdAndStart();
        }
        else
        {
            optionCanvas.enabled = false;
            selectHandCanvas.enabled = true;
        }
    }

    public void LoadSceneHapticAndVRRightHandShow()
    {
        showRightHand = true;
        SetJsonRightHand();
        CheckUserIdAndStart();
    
    }
    public void LoadSceneHapticAndVRLeftHandShow()
    {
        showLeftHand = true;
        SetJsonLeftHand();
        CheckUserIdAndStart();

    }

    public void LoadGameHapticAndVR()
    {
        CreateNewMatchId();
        SceneManager.LoadScene("Allegro_Chirurgo_Haptic_VR");
    }
    

    public async void Vibration()
    {
        vibrationSetting = !vibrationSetting;
        await firebase.Child(sessionCode).Child(complex).Child("Settings").PatchAsync("{\"vibration\":"+ vibrationSetting.ToString().ToLower()+"}");
    }
    
    
    public async void VisualCorrectObject()
    {
        visualCorrectObjectSetting = !visualCorrectObjectSetting;
        await firebase.Child(sessionCode).Child(complex).Child("Settings").PatchAsync("{\"visualCorrectObject\":"+ visualCorrectObjectSetting.ToString().ToLower()+"}");
    }
    
    public async void VisualError()
    {
        visualErrorSetting = !visualErrorSetting;
        await firebase.Child(sessionCode).Child(complex).Child("Settings").PatchAsync("{\"visualError\":"+ visualErrorSetting.ToString().ToLower()+"}");
    }
    public async void SoundObject()
    {
        soundObjectSetting = !soundObjectSetting;
        await firebase.Child(sessionCode).Child(complex).Child("Settings").PatchAsync("{\"sound\":"+ soundObjectSetting.ToString().ToLower()+"}");
    }
    
    public async void ShowHand()
    {
        showHandSetting = !showHandSetting;
        await firebase.Child(sessionCode).Child(complex).Child("Settings").PatchAsync("{\"showHand\":"+ showHandSetting.ToString().ToLower()+"}");
    }
    public async void SetJsonRightHand()
    {
        string handUsed = "\"rightHand\"";
        await firebase.Child(sessionCode).Child(complex).Child("Settings").PatchAsync("{\"hand\":"+ handUsed+"}");

        
    }
    
    public async void SetJsonLeftHand()
    {
        string handUsed = "\"leftHand\"";
        await firebase.Child(sessionCode).Child(complex).Child("Settings").PatchAsync("{\"hand\":"+ handUsed+"}");
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
        
        JsonObject jsonObjectSimple = jsonObjectToSave;
        jsonObjectSimple.trajectoryList = null;
        StartCoroutine(AsyncData2(jsonObjectSimple));
    }
    IEnumerator AsyncData(JsonObject jsonObjectToSave)
    {
        string data = JsonUtility.ToJson(jsonObjectToSave);
        print(data);
        yield return new WaitForSeconds(0.1f);
        StartCoroutine(AsyncSave(data));
        yield return null; 
    }
    
    IEnumerator AsyncSave (String data)
    {
        print("entrato");
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
    
    public void CreateNewMatchId()
    {
        matchId = matchId + 1;
        currentMatchId = "match"+matchId;
    }
        
    public async void SaveMatchDuration(int duration)
    {
        print("{\"duration\":"+ duration.ToString().ToLower()+"}");
        await firebase.Child(sessionCode).Child(complex).Child("listOfMatch").Child(currentMatchId).PatchAsync("{\"duration\":"+ duration.ToString().ToLower()+"}");
        await firebase.Child(sessionCode).Child(simple).Child("listOfMatch").Child(currentMatchId).PatchAsync("{\"duration\":"+ duration.ToString().ToLower()+"}");

    }


}
