using System.Collections;
using System.Collections.Generic;
using EventSystem2;
using UnityEngine;

public class TRaining_SFX_Haptic : MonoBehaviour
{
    
    public AudioSource trainingAudioSource;
    
    public AudioClip trainingClip1;
    public GameEvent endTrainingVocal;

    public bool startedVocal;
    
    

    // Update is called once per frame
    void Update()
    {
        if (!trainingAudioSource.isPlaying && startedVocal)
        {
            endTrainingVocal.Raise();
            startedVocal = false;
          

        }
    }
    
    public void StartVocalTrainingHaptic()
    {
        trainingAudioSource.PlayOneShot(trainingClip1);
        startedVocal = true;
        
    }
}
