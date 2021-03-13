using System;

using Firebase;
using Firebase.Database;
using Firebase.Extensions;
using UnityEngine;

public class FirebaseCallProva : MonoBehaviour
{
    private FirebaseApp app;
         // Start is called before the first frame update
         void Start()
         {
             
             FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task => {
                 var dependencyStatus = task.Result;
                 if (dependencyStatus == DependencyStatus.Available) {
                     // Create and hold a reference to your FirebaseApp,
                     // where app is a Firebase.FirebaseApp property of your application class.
                     app = FirebaseApp.DefaultInstance;
                     
                     DatabaseReference reference = FirebaseDatabase.DefaultInstance.RootReference;
                     
                     reference.Child("prova").SetValueAsync(1);
                     reference.Child("prova").Push().SetValueAsync(2); 
                     reference.Child("prova").Push().SetValueAsync(3); 
                     reference.Push().SetValueAsync("ciao");
                    
                     // Set a flag here to indicate whether Firebase is ready to use by your app.
                 } else {
                     Debug.LogError(System.String.Format(
                         "Could not resolve all Firebase dependencies: {0}", dependencyStatus));
                     // Firebase Unity SDK is not safe to use here.
                 }
             });

           

                
         }
         
         
     }
