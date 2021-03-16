using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Firebase.Database;
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


        await firebase2
            .Child("SONOILVR2").PutAsync("{\"appeared\": -70000000}");
}
    

    // Update is called once per frame
    void Update()
    {
        
    }
}
