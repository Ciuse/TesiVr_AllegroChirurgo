using System.Collections;
using System.Collections.Generic;
using EventSystem2;
using UnityEngine;

public class Training_SFX : MonoBehaviour
{
    public AudioSource trainingAudioSource;
    
    public AudioClip trainingClip;

    public GameEvent endTrainingVocal;

    public bool startedVocal;
    // Start is called before the first frame update
    void Start()
    {
        
    }

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
        trainingAudioSource.PlayOneShot(trainingClip);
        startedVocal = true;
    }
}
