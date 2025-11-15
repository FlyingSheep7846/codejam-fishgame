using UnityEngine;

public class FishingManager : MonoBehaviour
{
    public static FishingManager INSTANCE;
    public bool canFish;

    private FishingRodController fishingRodController;
    public AudioClip waterSplash;

    void Awake()
    {
        INSTANCE = this;
        fishingRodController = GetComponentInChildren<FishingRodController>(true);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && canFish)
        {
            fishingRodController.StartFish();
            SoundManager.Instance.PlayClip(waterSplash, 1f);
        }
    }

    public void CanFish(bool yes)
    {
        if (canFish == yes) return; // redundant call
        canFish = yes;
        UIOverlays.INSTANCE.ToggleFishIndicator(yes);

    }
}
