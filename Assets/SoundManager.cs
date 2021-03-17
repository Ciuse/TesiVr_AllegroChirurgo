using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SoundManager
{
   
   public static void PlaySound(AudioClip pinzaTouchedElectricEdgeSound)
   {
      GameObject soundGameObject = new GameObject("Sound");
      AudioSource audioSource = soundGameObject.AddComponent<AudioSource>();
      audioSource.PlayOneShot(pinzaTouchedElectricEdgeSound);
   }
   
}
