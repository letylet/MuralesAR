using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;


public class MDB_MuralesController : MonoBehaviour
{

    const int N_CHARACTERS = 4; // numero dei personaggi nel murales

    const string BUTTONTEXT_STORY = "CLICCA PER FAR SUONARE I PERSONAGGI"; // testo del bottone per cambiare modalità da Story a Music
    const string BUTTONTEXT_MUSIC = "CLICCA PER LEGGERE LE STORIE"; // testo del bottone per cambiare modalità da Music a Story

    public enum MODE // modalità di interazione con il murales
    {
        STORY,
        MUSIC
    };

    [SerializeField]
    private MODE currentMode;

    CharacterController[] characters = new CharacterController[N_CHARACTERS];

    [SerializeField]
    private Camera ARcamera = new Camera();

    [SerializeField]
    TMPro.TextMeshProUGUI changeModeButtonText;


    private void Awake()
    {
        this.characters = GetComponentsInChildren<CharacterController>();

        foreach (CharacterController character in this.characters)
        {
            character.setMuralesManager(this);
        }

        setCurrentMode(MODE.STORY);
    }


    private void Update()
    {
        if (this.currentMode == MODE.STORY)
        {
            if (Physics.Raycast(this.ARcamera.transform.position, this.ARcamera.transform.forward, out RaycastHit hit))
            {
                GameObject gameObject = hit.collider.gameObject;

                if (gameObject.CompareTag("HasSpeechBubble"))
                {
                    CharacterController control = gameObject.GetComponentInChildren<CharacterController>();

                    if(control.getMuralesManager().gameObject == this.gameObject)
                    {
                        ShowSpeech(control);
                    }

                }
                else
                {
                    HideAll();
                }
            }
        }
    }


    public MODE getCurrentMode()
    {
        return currentMode;
    }


    private void setCurrentMode(MODE mode)
    {
        if(mode == MODE.STORY)
        {

            setButtonText(BUTTONTEXT_STORY);
            currentMode = MODE.STORY;
        }

        else if(mode == MODE.MUSIC)
        {
            HideAll(); // nasconde tutte le vignette
            setButtonText(BUTTONTEXT_MUSIC);
            currentMode = MODE.MUSIC;
        }
    }
   
    // funzione assegnata al pulsante per cambiare modalità (da STORY = vignette che compaiono a MUSIC = personaggi che suonano)
    public void ChangeMode()
    {
        if (currentMode == MODE.STORY)
        {
            setCurrentMode(MODE.MUSIC);
        }
        else if (currentMode == MODE.MUSIC)
        {
            setCurrentMode(MODE.STORY);
        }
    }

    //mostra la vignetta del personaggio verso il quale è rivolta la camera e nasconde quelle degli altri
    private void ShowSpeech(CharacterController c)
    {
        foreach (CharacterController character in characters)
        {
            if (character == c)
            {
                character.ShowSpeech();
            }
            else
            {
                character.HideSpeech();
            }

        }
    }

    // nasconde le vignette di tutti i personaggi
    private void HideAll()
    {
        foreach (CharacterController character in characters)
        {
            character.HideSpeech();
        }
    }

    //cambia il testo del pulsante per cambiare modalità
    public void setButtonText(string newText)
    {
        changeModeButtonText.text = newText;
    }
}
