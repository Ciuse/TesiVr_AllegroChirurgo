using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Firebase.Database;
using Firebase.Database.Query;
using UnityEngine;

public class FireBaseProvaREST : MonoBehaviour
{
    // Start is called before the first frame update
    async void Start()
    {
//        var firebase = new FirebaseClient("https://allegrochirurgovr.firebaseio.com");
//
//        await firebase
//            .Child("prova").PostAsync("a");
//        await firebase
//            .Child("prova").PutAsync("a");

        var firebase2 = new FirebaseClient("https://allegrochirurgovr-default-rtdb.europe-west1.firebasedatabase.app/");


        /*await firebase2
            .Child("SONOILVR2").PostAsync("{\"appeared\": -70000000}");*/

        await firebase2.Child("User entered " + System.DateTime.Now.ToString("yyyy-MM-dd")).Child("ERRORS").PostAsync("{\"Num\": 0}");
    }
    

    // Update is called once per frame
    void Update()
    {
        
    }
}
