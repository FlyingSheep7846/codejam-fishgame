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

    [SerializeField] private CanvasGroup blackOverlay;
    [SerializeField] private TextMeshProUGUI titleText;
    [SerializeField] private TextMeshProUGUI descriptionText;

    string NEW_DAY_DESCRIPTION = "The Monster was satisfied today.";

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
        StartCoroutine("TransitionDayProcess"); 
    }

    IEnumerator TransitionDayProcess()
    {
        titleText.text = "";
        descriptionText.text = "";

        blackOverlay.DOFade(1f, 1f);
        yield return new WaitForSecondsRealtime(1f);

        yield return StartCoroutine(TypewriterProcess(titleText, $"Day {GameManager.INSTANCE.currentDay + 1}"));
        yield return StartCoroutine(TypewriterProcess(descriptionText, NEW_DAY_DESCRIPTION));

        // do all ur shit here
        yield return new WaitForSecondsRealtime(1f);
        

        blackOverlay.DOFade(0f, 1f);
        yield return new WaitForSecondsRealtime(1f);

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
