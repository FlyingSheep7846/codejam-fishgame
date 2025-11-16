using TMPro;
using UnityEditor.Rendering;
using UnityEngine;
using DG.Tweening;
using System.Collections;
using System;
using System.Collections.Generic;

public class UIOverlays : MonoBehaviour
{
    [SerializeField] GameObject fishIndicator;
    [SerializeField] GameObject endDayIndicator;


    [SerializeField] TextMeshProUGUI fishAmount;

    [SerializeField] CanvasGroup hideWhileFishing;
    [SerializeField] CanvasGroup hideInCutscene;

    [SerializeField] private CanvasGroup blackOverlay;
    [SerializeField] private TextMeshProUGUI titleText;

    private Dictionary<int, string> intNumbers = new Dictionary<int, string>
    {
        {0, "TWO"},
        {1, "THREE"},
        {2, "FOUR"},
        {3, "FIVE"},
    };

    public static UIOverlays INSTANCE;

    void Awake()
    {
        INSTANCE = this;
    }

    public void ToggleFishIndicator(bool visible)
    {
        fishIndicator.SetActive(visible);
    }

    public void ToggleEndDayIndicator(bool visible)
    {
        endDayIndicator.SetActive(visible);
    }


    public void SetFish(int amount)
    {
         fishAmount.text = amount.ToString();
    }

    public void ToggleFishingView(bool view)
    {
        hideWhileFishing.DOFade(view ? 1f : 0f, 0.5f);
    }

    public void TransitionDay()
    {
        DayManager.INSTANCE.TransitionDay(false);
        StartCoroutine("TransitionDayProcess"); 
    }

    public void CutToBlack()
    {
        blackOverlay.alpha = 1f;
    }

    public void FadeBlack(float duration = 0.8f)
    {
        blackOverlay.DOFade(1f, duration);   
    }

    IEnumerator TransitionDayProcess()
    {
        hideInCutscene.alpha = 0f;
        Timer.INSTANCE.TimerReset();

        yield return new WaitForSecondsRealtime(1f);

        yield return StartCoroutine(TypewriterProcess(titleText, $"DAY {intNumbers[DayManager.INSTANCE.currentDay]}"));
        
        yield return new WaitForSecondsRealtime(2f);
        blackOverlay.DOFade(1f, 1f).OnComplete(
            () => {
                titleText.text = "";
                hideInCutscene.alpha = 1f;
                blackOverlay.DOFade(0f,1f);

                UpgradeManager.INSTANCE.OpenUpgrades();
            }
        );

    }

    IEnumerator TypewriterProcess(TextMeshProUGUI tmp, string text)
    {
        tmp.text = "";
    
        for (int i = 0; i < text.Length; i++)
        {
            tmp.text += text[i];
            yield return new WaitForSeconds(0.1f);
        }
    }
}
