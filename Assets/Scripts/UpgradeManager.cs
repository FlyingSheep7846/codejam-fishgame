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

    void Awake()
    {
        INSTANCE = this;

        upgrades = GetComponentsInChildren<UpgradeButtonComponent>()
            .Select(a => a.GetComponent<RectTransform>())
            .Where(b => b != null)
            .ToArray();
    }

    public void OpenUpgrades()
    {
        Cursor.lockState = CursorLockMode.None;

        backgroundCg.alpha = 1f;
        Debug.Log(upgrades.Length);
        foreach (RectTransform rt in upgrades)
        {
            rt.GetComponent<CanvasGroup>().alpha = 0f;
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
            rt.GetComponent<CanvasGroup>().DOFade(0f, 0.4f);
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
            case 0:
                Debug.Log("0");
                break;

            case 1:
                Debug.Log("1");
                break;

            case 2:
                Debug.Log("2");
                break;

            case 3:
                Debug.Log("3");
                break;

            case 4:
                Debug.Log("4");
                break;

            case 5:
                Debug.Log("");
                break;

            default:
                break;
        }

        StartCoroutine("CloseUpgrades");

        // start the day
        DayManager.INSTANCE.BeginDay();

    }
}
