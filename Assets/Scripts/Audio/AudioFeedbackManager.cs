using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioFeedbackManager : MonoBehaviour
{
    public GameObject pinzaSFX;
    public GameObject objectSFX;
    public GameObject UISFX;

    public void ActivateSoundEffect()
    {
        pinzaSFX.SetActive(!pinzaSFX.activeSelf);
        objectSFX.SetActive(!objectSFX.activeSelf);
    }
}
