using TMPro;
using UnityEditor.Rendering;
using UnityEngine;
using DG.Tweening;
using System.Collections;
using System;

public class UIOverlays : MonoBehaviour
{
    [SerializeField] GameObject fishIndicator;
    [SerializeField] GameObject endDayIndicator;


    [SerializeField] TextMeshProUGUI fishAmount;

    [SerializeField] CanvasGroup hideWhileFishing;
    [SerializeField] CanvasGroup hideInCutscene;

    [SerializeField] private CanvasGroup blackOverlay;
    [SerializeField] private TextMeshProUGUI titleText;

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

    IEnumerator TransitionDayProcess()
    {
        hideInCutscene.alpha = 0f;
        Timer.INSTANCE.StartCoroutine("TimerReset");

        yield return new WaitForSecondsRealtime(1f);

        yield return StartCoroutine(TypewriterProcess(titleText, $"Day {DayManager.INSTANCE.currentDay}"));
        
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
