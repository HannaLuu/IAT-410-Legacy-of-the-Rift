using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CurrHeroUI : MonoBehaviour
{
    public RectTransform LokirBars;
    public RectTransform HalvarBars;
    public RectTransform UrsaBars;

    public PlayerSwitching psScript;

    protected Vector2 currHeroSlotPos;
    protected Vector2 secondHeroSlotPos;
    protected Vector2 thirdHeroSlotPos;

    protected Vector2 currHeroSlotScale;
    protected Vector2 secondHeroSlotScale;
    protected Vector2 thirdHeroSlotScale;

    // Start is called before the first frame update
    void Start()
    {
        currHeroSlotPos = LokirBars.anchoredPosition;
        secondHeroSlotPos = HalvarBars.anchoredPosition;
        thirdHeroSlotPos = UrsaBars.anchoredPosition;

        currHeroSlotScale = LokirBars.transform.localScale;
        secondHeroSlotScale = HalvarBars.transform.localScale;
        thirdHeroSlotScale = UrsaBars.transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        if (psScript.isSwitch == true && psScript.pastHero == PlayerSwitching.Hero.Lokir && psScript.currHero == PlayerSwitching.Hero.Halvar)
        {
            SwitchLokirToHalvar();
        }
        if (psScript.isSwitch == true && psScript.pastHero == PlayerSwitching.Hero.Halvar && psScript.currHero == PlayerSwitching.Hero.Lokir)
        {
            SwitchHalvarToLokir();
        }

        if (psScript.isSwitch == true && psScript.pastHero == PlayerSwitching.Hero.Halvar && psScript.currHero == PlayerSwitching.Hero.Ursa)
        {
            SwitchHalvarToUrsa();
        }
        if (psScript.isSwitch == true && psScript.pastHero == PlayerSwitching.Hero.Ursa && psScript.currHero == PlayerSwitching.Hero.Halvar)
        {
            SwitchUrsaToHalvar();
        }

        if (psScript.isSwitch == true && psScript.pastHero == PlayerSwitching.Hero.Lokir && psScript.currHero == PlayerSwitching.Hero.Ursa)
        {
            SwitchLokirToUrsa();
        }
        if (psScript.isSwitch == true && psScript.pastHero == PlayerSwitching.Hero.Ursa && psScript.currHero == PlayerSwitching.Hero.Lokir)
        {
            SwitchUrsaToLokir();
        }
    }

    private void SwitchLokirToHalvar()
    {
        LokirBars.transform.LeanMoveLocal(secondHeroSlotPos, 1).setEaseOutQuart();
        LokirBars.transform.LeanScale(secondHeroSlotScale, 1).setEaseOutQuart();
        HalvarBars.transform.LeanMoveLocal(currHeroSlotPos, 1).setEaseOutQuart();
        HalvarBars.transform.LeanScale(currHeroSlotScale, 1).setEaseOutQuart();
    }
    private void SwitchHalvarToLokir()
    {
        LokirBars.transform.LeanMoveLocal(currHeroSlotPos, 1).setEaseOutQuart();
        LokirBars.transform.LeanScale(currHeroSlotScale, 1).setEaseOutQuart();
        HalvarBars.transform.LeanMoveLocal(secondHeroSlotPos, 1).setEaseOutQuart();
        HalvarBars.transform.LeanScale(secondHeroSlotScale, 1).setEaseOutQuart();
    }

    private void SwitchHalvarToUrsa()
    {
        UrsaBars.transform.LeanMoveLocal(currHeroSlotPos, 1).setEaseOutQuart();
        UrsaBars.transform.LeanScale(currHeroSlotScale, 1).setEaseOutQuart();
        HalvarBars.transform.LeanMoveLocal(thirdHeroSlotPos, 1).setEaseOutQuart();
        HalvarBars.transform.LeanScale(thirdHeroSlotScale, 1).setEaseOutQuart();
    }
    private void SwitchUrsaToHalvar()
    {
        UrsaBars.transform.LeanMoveLocal(thirdHeroSlotPos, 1).setEaseOutQuart();
        UrsaBars.transform.LeanScale(thirdHeroSlotScale, 1).setEaseOutQuart();
        HalvarBars.transform.LeanMoveLocal(currHeroSlotPos, 1).setEaseOutQuart();
        HalvarBars.transform.LeanScale(currHeroSlotScale, 1).setEaseOutQuart();
    }

    private void SwitchLokirToUrsa()
    {
        LokirBars.transform.LeanMoveLocal(secondHeroSlotPos, 1).setEaseOutQuart();
        LokirBars.transform.LeanScale(secondHeroSlotScale, 1).setEaseOutQuart();
        UrsaBars.transform.LeanMoveLocal(currHeroSlotPos, 1).setEaseOutQuart();
        UrsaBars.transform.LeanScale(currHeroSlotScale, 1).setEaseOutQuart();
        HalvarBars.transform.LeanMoveLocal(thirdHeroSlotPos, 1).setEaseOutQuart();
        HalvarBars.transform.LeanScale(thirdHeroSlotScale, 1).setEaseOutQuart();
    }
    private void SwitchUrsaToLokir()
    {
        LokirBars.transform.LeanMoveLocal(currHeroSlotPos, 1).setEaseOutQuart();
        LokirBars.transform.LeanScale(currHeroSlotScale, 1).setEaseOutQuart();
        UrsaBars.transform.LeanMoveLocal(thirdHeroSlotPos, 1).setEaseOutQuart();
        UrsaBars.transform.LeanScale(thirdHeroSlotScale, 1).setEaseOutQuart();
        HalvarBars.transform.LeanMoveLocal(secondHeroSlotPos, 1).setEaseOutQuart();
        HalvarBars.transform.LeanScale(secondHeroSlotScale, 1).setEaseOutQuart();
    }
}
