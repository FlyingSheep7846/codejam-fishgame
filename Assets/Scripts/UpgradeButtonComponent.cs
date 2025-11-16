using TMPro;

using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UpgradeButtonComponent : MonoBehaviour
{
    [SerializeField] private bool right;
    [SerializeField] private int upgradeId;

    [Header("UI References")]
    [SerializeField] private Image image;
    [SerializeField] private TextMeshProUGUI titleText;
    [SerializeField] private TextMeshProUGUI descriptionText;

    private Button button;

    private Color disabledColor;

    private UpgradeManager upgradeParent;

    void Awake()
    {
        upgradeParent = transform.root.GetComponentInChildren<UpgradeManager>();
        button = GetComponent<Button>();

        ColorUtility.TryParseHtmlString("#5A5265", out disabledColor);
    }

    public void PurchaseUpgrade()
    {
        upgradeParent.SelectUpgrade(upgradeId);
        button.interactable = false;

        image.color = disabledColor;
        titleText.color = disabledColor;
        descriptionText.color = disabledColor;
    }
}
