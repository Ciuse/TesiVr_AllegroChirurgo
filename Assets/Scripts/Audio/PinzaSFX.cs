using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinzaSFX : MonoBehaviour
{
    public AudioSource PinzaAudioSource;
    
    public AudioClip[] vibrationClips;

    private void Start()
    {
    
    }

    public void VibrationTouchElectricEdge()
    {
        var rand = Random.Range(0, vibrationClips.Length);
        var randPitch = Random.Range(0.95f, 1.1f);
        PinzaAudioSource.pitch = randPitch;
        PinzaAudioSource.Stop();
        PinzaAudioSource.volume = 0.9f;
        PinzaAudioSource.loop = true;
        PinzaAudioSource.clip=vibrationClips[rand];    
        PinzaAudioSource.Play();
    }

    public void VibrationStopTouchElectricEdge()
    {
        PinzaAudioSource.Stop();
    }
}
