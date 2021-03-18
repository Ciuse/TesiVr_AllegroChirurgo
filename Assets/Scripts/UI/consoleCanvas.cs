using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class consoleCanvas : MonoBehaviour
{
    public  GameObject canvas;

    private TextMeshProUGUI textArea;
    public InputActionReference triggerPressing;
    [SerializeField]
    private int maxLines = 15;
    // Start is called before the first frame update
    void Start()
    {
        textArea = canvas.GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {     
        ClearLines();
        textArea.text = triggerPressing.action.ReadValue<float>().ToString();
    }

    public void AddErrore()
    {
        textArea.text = textArea.text + "\n OGGETTO CADUTO";
    }
    
    private void ClearLines()
    {
        if (textArea.text.Split('\n').Length >= maxLines)
        {
            textArea.text = string.Empty;
        }
    }
}
