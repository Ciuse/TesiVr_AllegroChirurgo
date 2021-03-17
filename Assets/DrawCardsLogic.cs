using System.Collections;
using System.Collections.Generic;
using EventSystem;
using UnityEngine;
using UnityEngine.UI;

public class DrawCardsLogic : MonoBehaviour
{
    
    public List<Sprite> imagesCards = new List<Sprite>();
    public List<Sprite> imagesCardsSuccess = new List<Sprite>();
    public List<GameEvent> cardsEvents;
    public ObjectEvent cardHasBeenDrawedEvent;
    public Image imageCard;
    public Sprite wellDoneImage;
    private int numChoosen;
    private Sprite cardPickedPending;
    public GameEvent resetCardsEvent;
   

    // Start is called before the first frame update
    void Start()
    {
        
    }
    

    public void drawCard()
    {
        resetCardsEvent.Raise();
        StartCoroutine(waitBeforeDraw());
       
    }

    IEnumerator waitBeforeDraw()
    {
        yield return new WaitForSeconds(0.5f);
        numChoosen = Random.Range(0, imagesCards.Count);
        imageCard.sprite = imagesCards[numChoosen];
        cardsEvents[numChoosen].Raise();
        cardPickedPending = imagesCards[numChoosen];
        Interactable interactable = new Interactable {id = numChoosen};
        cardHasBeenDrawedEvent.Raise(interactable);
   
    }

    public void cardPickedWithSuccess()
    {
        imagesCards.RemoveAt(numChoosen);
        cardsEvents.RemoveAt(numChoosen);
        imagesCardsSuccess.Add(cardPickedPending);
        imageCard.sprite = wellDoneImage;

    }
    
}
