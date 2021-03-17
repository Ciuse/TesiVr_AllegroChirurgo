using System.Collections;
using System.Collections.Generic;
using EventSystem;
using UnityEngine;
using UnityEngine.UI;

public class DrawCardsLogic : MonoBehaviour
{
    
    public List<Sprite> imagesCards = new List<Sprite>();
    public List<Sprite> imagesCardsSuccess = new List<Sprite>();
    public ObjectEvent cardHasBeenDrawedEvent;
    public Image imageCard;
    public Sprite wellDoneImage;
    private int numChoosen;
    private Sprite cardPickedPending;
    public GameEvent resetCardsEvent;
    public List<int> numbers;
   

    // Start is called before the first frame update
    void Start()
    {
        for (int i=0; i<imagesCards.Count; i++)
        {
            numbers.Add(i);
        }
        
    }
    

    public void drawCard()
    {
        resetCardsEvent.Raise();
        StartCoroutine(waitBeforeDraw());
       
    }

    IEnumerator waitBeforeDraw()
    {
        yield return new WaitForSeconds(0.5f);
        numChoosen = numbers[Random.Range(0, numbers.Count-1)];
        imageCard.sprite = imagesCards[numChoosen];
        cardPickedPending = imagesCards[numChoosen];
        Interactable interactable = new Interactable {id = numChoosen};
        cardHasBeenDrawedEvent.Raise(interactable);
   
    }

    public void cardPickedWithSuccess()
    {
        imagesCards.RemoveAt(numChoosen);
        numbers.RemoveAt(numChoosen);
        imagesCardsSuccess.Add(cardPickedPending);
        imageCard.sprite = wellDoneImage;

    }
    
}
