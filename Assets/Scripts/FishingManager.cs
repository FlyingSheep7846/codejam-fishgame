using UnityEngine;

public class FishingManager : MonoBehaviour
{
    public static FishingManager INSTANCE;
    public bool canFish;

    private FishingRodController fishingRodController;

    void Awake()
    {
        INSTANCE = this;
        fishingRodController = GetComponentInChildren<FishingRodController>(true);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            fishingRodController.StartFish();
        }
    }

    public void CanFish(bool yes)
    {
        if (canFish == yes) return; // redundant call
        canFish = yes;
        UIOverlays.INSTANCE.ToggleFishIndicator(yes);

    }
}
