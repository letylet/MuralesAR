using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MDO_MuralesController : MonoBehaviour
{
    const string TEXT_COLOR_MODE = "clicca sullo schermo per far comparire la lente per cercare le scarpette che riportano Dorothy a casa";
    const string TEXT_BLACK_AND_WHITE_MODE = "Complimenti! hai riportato Dorothy nel Kansas. Per farla tornare nel regno di oz clicca sul tornado";

    [SerializeField]
    private TMPro.TextMeshProUGUI infoText;

    public enum MODE { COLOR, BLACKandWHITE };


    private MODE mode;


    [SerializeField]
    private GameObject magnifyingGlass; //  lente di ingrandimento


    [SerializeField]
    private ChangeColorFader fader;


    private void Awake()
    {

        this.mode = MODE.COLOR;
        magnifyingGlass.SetActive(false);
        infoText.text = TEXT_COLOR_MODE;

    }
    
    private void Update()
    {
        if (mode == MODE.COLOR)
        {
            if (Input.GetMouseButtonDown(0))
            {
                magnifyingGlass.SetActive(true);
            }

        }
    }

    public void ChangeStateBN()
    {
        this.mode = MODE.BLACKandWHITE;
        this.fader.setState(ChangeColorFader.STATE.ANIM_TO_VISIBLE);
        infoText.text = TEXT_BLACK_AND_WHITE_MODE;
        magnifyingGlass.SetActive(false);
    }

    public void ChangeStateColor()
    {
        this.fader.setState(ChangeColorFader.STATE.ANIM_TO_TRANSPARENT);
        this.infoText.text = TEXT_COLOR_MODE;
        this.mode = MODE.COLOR;
    }
}
