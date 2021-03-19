using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_SFX : MonoBehaviour
{
    
    public AudioSource UIAudioSource;
    
    public AudioClip menuClickClip;
    
    public void MenuClickAudio()
    {
        var randPitch = Random.Range(0.9f, 1.1f);
        UIAudioSource.pitch = randPitch;
        UIAudioSource.PlayOneShot(menuClickClip);    
    }
}
