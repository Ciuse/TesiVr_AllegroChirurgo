using System;
using System.Collections;
using System.Collections.Generic;
using Firebase.Database;
using Firebase.Database.Query;
using UnityEngine;

public class ManageJSON : MonoBehaviour
{
    public FirebaseClient firebase2;

    public String handUsed;
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
        print("scena iniziata con la mano destra");
        handUsed = "rightHand";
        await firebase2.Child("User entered " + System.DateTime.Now.ToString("yyyy-MM-dd") ).Child("SETTINGS").PatchAsync("{\"hand\": \"right\"}");
        await firebase2.Child("User entered " + System.DateTime.Now.ToString("yyyy-MM-dd") ).Child("SETTINGS").PatchAsync("{\"mode\": 1}");
        await firebase2.Child("User entered " + System.DateTime.Now.ToString("yyyy-MM-dd")).Child("ERRORS").PatchAsync("{\"errorHand\": 2}");
        
    }
    
    public async void SceneStartedWithLeftHand()
    {
        print("scena iniziata con la mano sinistra");
        handUsed = "leftHand";
        await firebase2.Child("User entered " + System.DateTime.Now.ToString("yyyy-MM-dd") ).Child("SETTINGS").PostAsync("{\"hand\": \"left\"}");
    }
}
