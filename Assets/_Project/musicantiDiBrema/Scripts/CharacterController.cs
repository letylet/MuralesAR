using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CharacterController : MonoBehaviour
{

    [SerializeField]
    private SpriteRenderer bubbleSpeech;

    [SerializeField]
    private Animator anim;

    [SerializeField]
    private AudioManager audioManager;

    private bool isPlaying = false;


    [SerializeField]
    private MusicNotesSpawner noteSpawner;

    private MDB_MuralesController muralesManager;


    void Awake() 
    {
        this.bubbleSpeech.gameObject.SetActive(false);
        this.anim.enabled = false;
        this.noteSpawner.enabled = false;
        this.audioManager.setSource(this.gameObject.GetComponent<AudioSource>());
        
    }

    private void FixedUpdate()
    {
        if (isPlaying)
        {
            if (!audioManager.getSource().isPlaying) // se l'audio è finito, ferma anche l'animazione
            {
                stopSound();
            }
        }
    }

    public void setMuralesManager(MDB_MuralesController manager)
    {
        this.muralesManager = manager;
    }

    public MDB_MuralesController getMuralesManager()
    {
        return this.muralesManager;
    }

    private void StartAnimation()
    {
        this.anim.enabled = true;
    }

    private void StopAnimation()
    {
        this.anim.enabled = false;
    }

    public void HideSpeech()
    {
        this.bubbleSpeech.gameObject.SetActive(false);
    }

   public void ShowSpeech()
    {
        this.bubbleSpeech.gameObject.SetActive(true);
    }

    public void playSound()
    {
        if (muralesManager.getCurrentMode() == MDB_MuralesController.MODE.MUSIC)
        {
            if (!isPlaying)
            {
                StartAnimation();
                noteSpawner.enabled = true;
                audioManager.playSound();
                isPlaying = true;
            }
            else
            {
                stopSound();
            }
        }
    }

    public void stopSound()
    {
        StopAnimation();
        this.noteSpawner.enabled = false;
        this.audioManager.stopSound();
        this.isPlaying = false;
    }

    public bool IsPlaying()
    {
        return this.isPlaying;
    }


}
