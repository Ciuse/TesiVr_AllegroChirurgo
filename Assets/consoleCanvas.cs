using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class consoleCanvas : MonoBehaviour
{
    public  GameObject canvas;

    private TextMeshProUGUI textArea;
    // Start is called before the first frame update
    void Start()
    {
        textArea = canvas.GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddErrore()
    {
        textArea.text = textArea.text + "\n OGGETTO CADUTO";
    }
    
    
}
