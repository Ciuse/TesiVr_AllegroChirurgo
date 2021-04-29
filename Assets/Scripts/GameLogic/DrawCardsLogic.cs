using System;
using System.Collections;
using System.Collections.Generic;
using EventSystem2;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class DrawCardsLogic : MonoBehaviour
{
    
    public List<Sprite> imagesCards = new List<Sprite>();
    public List<Sprite> imagesCardsSuccess = new List<Sprite>();
    public ObjectEvent cardHasBeenDrawedEvent;
    public Image imageCard;
    public Sprite wellDoneImage;
    private int numChoosen=-1;
    private Sprite cardPickedPending;
    public GameEvent resetCardsEvent;
    public List<int> numbers;
    public int currentCard=0;
    public Button startGameButton;
    public bool startGame = false;
    public TextMeshProUGUI startGameText;
    public bool startTraining = false;


    // Start is called before the first frame update
    void Start()
    {
        for (int i=0; i<imagesCards.Count; i++)
        {
            numbers.Add(i);
        }
        
    }

    private void Update()
    {
        if (Keyboard.current.qKey.wasPressedThisFrame && !startGame)
        {
            StartGameHaptic();
            startGame = true;
            startGameText.enabled = false;
        }
        if (startTraining && !startGame)
        {
            StartGameHaptic();
            startGame = true;
            startGameText.enabled = false;
            
        }

    }

    public void StartTrainingAfterVocal()
    {
        startTraining = true;
        
    }
    
    public void StartGameHaptic()
    {
        resetCardsEvent.Raise();
        StartCoroutine(WaitSequentialDraw());
    }
    
    

    public void StartFirstDraw()
    {
        resetCardsEvent.Raise();
        StartCoroutine(WaitSequentialDraw());
        startGameButton.interactable = false;

    }

    public void DrawCard()
    {
        resetCardsEvent.Raise();
        StartCoroutine(WaitRandomDraw());
       
    }

    IEnumerator WaitRandomDraw()
    {
        yield return new WaitForSeconds(0.5f);
        numChoosen = numbers[Random.Range(0, numbers.Count)];
        imageCard.sprite = imagesCards[numChoosen];
        cardPickedPending = imagesCards[numChoosen];
        Interactable interactable = new Interactable {id = numChoosen};
        cardHasBeenDrawedEvent.Raise(interactable);
   
    }

    IEnumerator WaitSequentialDraw()
    {
        if (currentCard<imagesCards.Count)
        {
            yield return new WaitForSeconds(0.5f);
            imageCard.sprite = imagesCards[currentCard];
            cardPickedPending = imagesCards[currentCard];
            Interactable interactable = new Interactable {id = currentCard};
            cardHasBeenDrawedEvent.Raise(interactable);
            currentCard++;
        }
        else
        {
            print("vinto");
            if (startTraining)
            {
                SceneManager.LoadScene("Allegro_Chirurgo_Haptic_VR");
            }
        }
        
    }

    IEnumerator Wait2Sec()
    {
        yield return new WaitForSeconds(2f);
        StartCoroutine(WaitSequentialDraw());
    }
    
    public void CardPickedWithSuccess()
    {
        
        if (numChoosen != -1)
        {
            imagesCards.RemoveAt(numChoosen);
            numbers.RemoveAt(numbers.Count-1); //se si rimuove anche l immagine la lista delle immagini ora sarà grande 1 in meno, quindi il numero piu grande da pescare va rimosso
        }
        
        resetCardsEvent.Raise();

        imagesCardsSuccess.Add(cardPickedPending);
        imageCard.sprite = wellDoneImage;
        StartCoroutine(Wait2Sec());
    }
    
}
