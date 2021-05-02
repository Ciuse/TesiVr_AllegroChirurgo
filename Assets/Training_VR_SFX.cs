using System.Collections;
using System.Collections.Generic;
using EventSystem2;
using UnityEngine;

public class Training_VR_SFX : MonoBehaviour
{
    public AudioSource trainingAudioSource;
    
    public AudioClip trainingClip1;
    public AudioClip trainingClip2;
    public AudioClip trainingClip3;

    public GameEvent endTrainingVocal;

    public bool startedVocals;
    public bool startedVocal1;
    public bool startedVocal2;
    public bool startedVocal3;

    public StartingSimulationVr startingSimulationVr;
   

    // Update is called once per frame
    void Update()
    {
        if (!trainingAudioSource.isPlaying && startedVocals && startedVocal1 && startingSimulationVr.valueGrip>0.5f)
        {
           //endTrainingVocal.Raise();
           startedVocal1 = false;
           StartVocal2TrainingVR();
        }
        if (!trainingAudioSource.isPlaying && startedVocals && startedVocal2 && startingSimulationVr.valueTrigger>0.5f)
        {
            //endTrainingVocal.Raise();
            startedVocal2 = false;
            StartVocal3TrainingVR();
        }
        if (!trainingAudioSource.isPlaying && startedVocals && startedVocal3)
        {
            endTrainingVocal.Raise();
            startedVocal3 = false;
            startedVocals = false;

        }
    }

    public void StartVocal1TrainingVR()
    {
        trainingAudioSource.PlayOneShot(trainingClip1);
        startedVocal1 = true;
        startedVocals = true;
    }
    
    public void StartVocal2TrainingVR()
    {
        trainingAudioSource.PlayOneShot(trainingClip2);
        startedVocal2 = true;
        
    }
    public void StartVocal3TrainingVR()
    {
        trainingAudioSource.PlayOneShot(trainingClip3);
        startedVocal3 = true;
        
    }
}
