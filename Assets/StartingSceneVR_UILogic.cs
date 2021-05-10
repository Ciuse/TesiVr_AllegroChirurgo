using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StartingSceneVR_UILogic : MonoBehaviour
{
    public Canvas settingCanvas;

    public Canvas selectHandCanvas;
    
    public Canvas numPad;

    public TextMeshProUGUI userId;

    // Start is called before the first frame update
    void Start()
    {
        settingCanvas.enabled = true;
        selectHandCanvas.enabled = false;
        numPad.enabled = false;
    }


    public void ClickOkOnOption()
    {
        settingCanvas.enabled = false;
        numPad.enabled = true;
        selectHandCanvas.enabled = false;
    }
    
    public void ClickOkOnNumPad()
    {
        if (!userId.text.Equals(""))
        {
            settingCanvas.enabled = false;
            numPad.enabled = false;
            selectHandCanvas.enabled = true;
        }

    }
}
