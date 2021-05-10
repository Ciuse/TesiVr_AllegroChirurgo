using System.Collections;
using System.Collections.Generic;
using EventSystem2;
using UnityEngine;

public class ObjectSFX : MonoBehaviour
{
    public AudioSource[] objectAudioSources;
    
    public AudioClip[] tapClip;
    
    public AudioClip[] wrongPickClip;

    public AudioClip[] correctPickClip;

    public float elapsed = 0f;

    public bool soundTouchEffectPlayed = false;
    public bool wait = false;

    public void Update()
    {
        elapsed += Time.deltaTime;
        if (soundTouchEffectPlayed)
        {
            if (elapsed >= 1f)
            {
                soundTouchEffectPlayed = false;
            }
        }

    }

    public void ObjectTouchBox(Interactable interactable)
    {
        if (!soundTouchEffectPlayed)
        {
            var rand = Random.Range(0, tapClip.Length);
            var randPitch = Random.Range(0.9f, 1.1f);


            objectAudioSources[interactable.id].pitch = randPitch;
            objectAudioSources[interactable.id].Stop();
            objectAudioSources[interactable.id].PlayOneShot(tapClip[rand]);
            soundTouchEffectPlayed = true;
        }
        

    }

    public void WrongPick(Interactable interactable)
    {
        var rand = Random.Range(0, wrongPickClip.Length);
        var randPitch = Random.Range(0.9f, 1.1f);
        
        
        objectAudioSources[interactable.id].pitch = randPitch;
        objectAudioSources[interactable.id].Stop();
        objectAudioSources[interactable.id].PlayOneShot(wrongPickClip[rand]);    
    }
    
    public void CorrectPick(Interactable interactable)
    {
        var rand = Random.Range(0, correctPickClip.Length);
        var randPitch = Random.Range(0.9f, 1.1f);
        
        
        objectAudioSources[interactable.id].pitch = randPitch;
        objectAudioSources[interactable.id].Stop();
        objectAudioSources[interactable.id].PlayOneShot(correctPickClip[rand]);    
    }
}
