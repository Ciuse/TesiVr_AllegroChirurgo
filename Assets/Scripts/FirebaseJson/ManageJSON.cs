using System;
using System.Collections;
using System.Collections.Generic;
using EventSystem;
using Firebase.Database;
using Firebase.Database.Query;
using UnityEngine;

public class ManageJSON : MonoBehaviour
{
    public FirebaseClient firebase2;
    public String handUsed;
    public int cardHasBeenDrawed;
    
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        firebase2 = new FirebaseClient("https://allegrochirurgovr-default-rtdb.europe-west1.firebasedatabase.app/");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public async void SceneStartedWithRightHand()
    {
        print("Scene started with right hand");
        handUsed = "rightHand";
        /*await firebase2.Child("User entered " + System.DateTime.Now.ToString("yyyy-MM-dd") ).Child("SETTINGS").PatchAsync("{\"hand\": \"right\"}");
        await firebase2.Child("User entered " + System.DateTime.Now.ToString("yyyy-MM-dd") ).Child("SETTINGS").PatchAsync("{\"mode\": 1}");
        await firebase2.Child("User entered " + System.DateTime.Now.ToString("yyyy-MM-dd")).Child("ERRORS").PatchAsync("{\"errorHand\": 2}");*/
        
    }
    
    public async void SceneStartedWithLeftHand()
    {
        print("Scene started with left hand");
        handUsed = "leftHand";
        /*await firebase2.Child("User entered " + System.DateTime.Now.ToString("yyyy-MM-dd") ).Child("SETTINGS").PostAsync("{\"hand\": \"left\"}");*/
    }
    
    public void CardHasBeenDrawedEvent(Interactable interactable)
    {
        cardHasBeenDrawed = interactable.id;
        print("The card that has been drawed is : "+ interactable.id);
    }
}
