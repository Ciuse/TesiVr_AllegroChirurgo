using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardsPickedWithSuccess : MonoBehaviour
{
    
    public Image imageCard;
    // Start is called before the first frame update
    void Start()
    {
        imageCard.enabled = false;
    }
    

    public void activateImageCard()
    {
        imageCard.enabled = true;
    }
}
