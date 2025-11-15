using UnityEngine;

public class FishingManager : MonoBehaviour
{
    public static FishingManager INSTANCE;
    public bool canFish;
    private PlayerController playerController;

    private float rotation;

    private FishingRodController fishingRodController;
    public AudioClip waterSplash;

    void Awake()
    {
        INSTANCE = this;
        fishingRodController = GetComponentInChildren<FishingRodController>(true);
        playerController = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && canFish)
        {
            StartFish();
        }
    }

    public void StartFish()
    {
        fishingRodController.StartFish();
        playerController.ToggleFreeLook(false, rotation);
        SoundManager.Instance.PlayClip(waterSplash, 1f);
    }

    public void CanFish(bool yes, float rotation)
    {
        if (canFish == yes) return; // redundant call
        canFish = yes;
        this.rotation = rotation;
        Debug.Log($"Setting rotation to {rotation} and can fish {canFish}");
        UIOverlays.INSTANCE.ToggleFishIndicator(yes);

    }
}
