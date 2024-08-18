using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class ChangeColorFader : MonoBehaviour
{
    

    public enum STATE { VISIBLE, ANIM_TO_TRANSPARENT, TRANSPARENT, ANIM_TO_VISIBLE }


    [SerializeField] private STATE state = STATE.TRANSPARENT;

    [SerializeField] private CanvasGroup canvasGroup;

    [SerializeField] private float fadeDuration = 0.5f;

    private float timer;

    [SerializeField]
    private SpriteRenderer tornado;

    private void Awake()
    {
        tornado.gameObject.SetActive(false);
    }

    public void setState(STATE newState)
    {


        switch (newState)
        {
            case STATE.ANIM_TO_TRANSPARENT:
                StartFadeToTransparent();
                break;

            case STATE.TRANSPARENT:
                this.canvasGroup.alpha = 0;
                this.canvasGroup.blocksRaycasts = true;
                tornado.gameObject.SetActive(false);
                break;

            case STATE.ANIM_TO_VISIBLE:
                StartFadeToVisible();

                break;

            case STATE.VISIBLE:
                this.canvasGroup.alpha = 1;
                this.canvasGroup.blocksRaycasts = true;
                this.canvasGroup.interactable = true;
                tornado.gameObject.SetActive(true);
                break;
        }

        this.state = newState;
    }


    public void StartFadeToTransparent()
    {
        this.canvasGroup.alpha = 1;
        this.canvasGroup.blocksRaycasts = true;
        this.timer = 0;
        this.state = STATE.ANIM_TO_TRANSPARENT;
        
    }

    public void StartFadeToVisible()
    {
        this.canvasGroup.alpha = 0;
        this.canvasGroup.blocksRaycasts = true;
        this.timer = 0;
        this.state = STATE.ANIM_TO_VISIBLE;
    }

    private void Update()
    {
        switch (this.state)
        {
            case STATE.ANIM_TO_TRANSPARENT:
                this.timer += Time.deltaTime;

                if (this.timer < this.fadeDuration)
                {
                    this.canvasGroup.alpha = 1 - this.timer / this.fadeDuration;
                }
                else
                {
                    setState(STATE.TRANSPARENT);
                }
                break;

            case STATE.TRANSPARENT:
                break;

            case STATE.ANIM_TO_VISIBLE:
                this.timer += Time.deltaTime;

                if (this.timer < this.fadeDuration)
                {
                    this.canvasGroup.alpha = this.timer / this.fadeDuration;
                }
                else
                {
                    setState(STATE.VISIBLE);

                }
                break;

            case STATE.VISIBLE:
                break;

        }


    }


}
