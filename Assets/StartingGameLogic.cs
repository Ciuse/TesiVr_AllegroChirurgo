using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartingGameLogic : MonoBehaviour
{
    
    public List<Sprite> imagesCards = new List<Sprite>();
    private List<Sprite> imagesCardsSuccess = new List<Sprite>();
    public Image imageCard;
    private int numChoosen;
    // Start is called before the first frame update
    void Start()
    {
        //drawCard();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void drawCard()
    {
        numChoosen = Random.Range(0, imagesCards.Count);
        imageCard.sprite = imagesCards[numChoosen];
    }
}
