using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NumPadButton : MonoBehaviour
{
    public int number;
    public TextMeshProUGUI text;
    
    public void DigitNumber()
    {
        text.text = text.text + number.ToString();
    }
    public void Delete()
    {
        text.text = "";
    }
    
}
