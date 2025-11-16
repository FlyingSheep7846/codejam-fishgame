using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
    public static UpgradeManager INSTANCE;

    [SerializeField] private CanvasGroup backgroundCg;
    [SerializeField] private CanvasGroup textCg;

    [SerializeField] private RectTransform upgradeParent;
    [SerializeField] private RectTransform[] upgrades; 


    private FishingRodController fishingRodController;
    private QTEController qteController;
    private FishController fishController;

    void Awake()
    {
        INSTANCE = this;

        fishingRodController = FindAnyObjectByType<FishingRodController>();

        upgrades = GetComponentsInChildren<UpgradeButtonComponent>()
            .Select(a => a.GetComponent<RectTransform>())
            .Where(b => b != null)
            .ToArray();

        fishingRodController = Object.FindAnyObjectByType<FishingRodController>();
        qteController = Object.FindAnyObjectByType<QTEController>();
        fishController = Object.FindAnyObjectByType<FishController>();
    }

    public void OpenUpgrades()
    {
        Cursor.lockState = CursorLockMode.None;

        backgroundCg.alpha = 1f;
        Debug.Log(upgrades.Length);
        foreach (RectTransform rt in upgrades)
        {
            var cg = rt.GetComponent<CanvasGroup>();
            cg.alpha = 0f;
            cg.interactable = true;
            rt.DOAnchorPosY(-50f, 0f).SetRelative(true);
        }

        StartCoroutine("OpenUpgradesProcess");
    }

    IEnumerator OpenUpgradesProcess()
    {
        textCg.DOFade(1f, 0.8f);

        foreach (RectTransform rt in upgrades)
        {
            rt.DOAnchorPosY(50f, 0.4f).SetRelative(true);
            rt.GetComponent<CanvasGroup>().DOFade(1f, 0.4f).SetRelative(true);
            yield return new WaitForSecondsRealtime(0.3f);
        }

        yield return new WaitForSecondsRealtime(0.2f);

        
    }

    IEnumerator CloseUpgrades()
    {
        textCg.DOFade(0f, 0.5f);

        foreach (RectTransform rt in upgrades)
        {
            var cg = rt.GetComponent<CanvasGroup>();
            cg.DOFade(0f, 0.4f);
            cg.interactable = false;
            yield return new WaitForSecondsRealtime(0.2f);
        }

        yield return new WaitForSecondsRealtime(0.2f);
        backgroundCg.DOFade(0f, 0.5f);

        Cursor.lockState = CursorLockMode.Locked;
    }

    public void SelectUpgrade(int upgrade)
    {
        switch (upgrade)
        {
            // larger bar
            case 0:
                fishingRodController.IncreaseBarSize(100);
                break;

            // smaller bar, faster catch
            case 1:
                Debug.Log("1");
                fishingRodController.IncreaseBarSize(-75);
                break;

            // qtes longer
            case 2:
                qteController.IncreaseQTETime(1.5f);
                break;

            // qtes assist but deal more damage on fail
            case 3:
                qteController.QTESpecialEffects();
                break;

            // qtes are less impactful
            case 4:
                qteController.IncrementQTEEffect(0.7f);
                break;

            // challenging fishing but chance to double reward
            case 5:
                fishingRodController.CanDouble(true);
                // make more challenging
                break;

            default:
                break;
        }

        StartCoroutine("CloseUpgrades");

        // start the day
        DayManager.INSTANCE.BeginDay();

    }
}
