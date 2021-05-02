using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartingSceneVR_UILogic : MonoBehaviour
{
    public Canvas settingCanvas;

    public Canvas selectHandCanvas;
    
    // Start is called before the first frame update
    void Start()
    {
        settingCanvas.enabled = true;
        selectHandCanvas.enabled = false;
    }


    public void ClickOkOnOption()
    {
        settingCanvas.enabled = false;
        selectHandCanvas.enabled = true;
    }
}
