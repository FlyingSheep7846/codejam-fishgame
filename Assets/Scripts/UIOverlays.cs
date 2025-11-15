using TMPro;
using UnityEditor.Rendering;
using UnityEngine;
using DG.Tweening;

public class UIOverlays : MonoBehaviour
{
    [SerializeField] GameObject fishIndicator;
    [SerializeField] TextMeshProUGUI fishAmount;

    [SerializeField] CanvasGroup hideWhileFishing;

    public static UIOverlays INSTANCE;

    void Awake()
    {
        INSTANCE = this;
    }

    public void ToggleFishIndicator(bool visible)
    {
        fishIndicator.SetActive(visible);
    }

    public void SetFish(int amount)
    {
         fishAmount.text = amount.ToString();
    }

    public void ToggleFishingView(bool view)
    {
        hideWhileFishing.DOFade(view ? 1f : 0f, 0.5f);
    }
}
