using System.Collections;
using System.Collections.Generic;
using EventSystem;
using UnityEngine;
using UnityEngine.UI;

public class StartingGameLogic : MonoBehaviour
{
    
    public List<Sprite> imagesCards = new List<Sprite>();
    private List<Sprite> imagesCardsSuccess = new List<Sprite>();
    public List<GameEvent> cardsEvents = new List<GameEvent>();
    public Image imageCard;
    private int numChoosen;
    // Start is called before the first frame update
    void Start()
    {
        drawCard();
    }
    

    public void drawObject1ToTest()
    {
        numChoosen = 0;
        imageCard.sprite = imagesCards[numChoosen];
        cardsEvents[numChoosen].Raise();   
    }

    public void drawCard()
    {
        numChoosen = Random.Range(0, imagesCards.Count);
        imageCard.sprite = imagesCards[numChoosen];
        cardsEvents[numChoosen].Raise();
    }
}
