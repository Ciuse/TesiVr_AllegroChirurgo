using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiReloadGame : MonoBehaviour
{

    public Canvas gameUI;
    public Canvas reloadUI;

     void Start()
    {
        gameUI.enabled = true;
        reloadUI.enabled = false;
    }

    public void ReloadGame()
    {
        gameUI.enabled = false;
        reloadUI.enabled = true;
    }
}
