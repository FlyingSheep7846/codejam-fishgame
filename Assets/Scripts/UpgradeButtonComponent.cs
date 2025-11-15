using UnityEditor.Rendering.Universal;
using UnityEngine;
using UnityEngine.EventSystems;

public class UpgradeButtonComponent : MonoBehaviour
{
    [SerializeField] private bool right;
    [SerializeField] private int upgradeId;

    private UpgradeManager upgradeParent;

    void Awake()
    {
        upgradeParent = transform.root.GetComponentInChildren<UpgradeManager>();
    }

    public void PurchaseUpgrade()
    {
        upgradeParent.SelectUpgrade(upgradeId);
    }
}
