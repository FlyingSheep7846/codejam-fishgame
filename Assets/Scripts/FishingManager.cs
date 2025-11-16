using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FishingManager : MonoBehaviour
{
    public static FishingManager INSTANCE;
    public bool canFish;
    private PlayerController playerController;

    private float rotation;

    private FishingRodController fishingRodController;
    public AudioClip waterSplash;
    public AudioClip monsterRoar;
    public AudioClip monsterKill;


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
        if (Timer.INSTANCE.time < 0f)
        {
            int random = Random.Range(0, 20);
            float scale = Timer.INSTANCE.time + random;
            if (scale < 0)
            {
                SoundManager.Instance.StopAllSFX();
                SoundManager.Instance.StopMusicAndOcean();
                Death();
            }

        }
        fishingRodController.StartFish();
        playerController.ToggleFreeLook(false, rotation);
        SoundManager.Instance.PlayClip(waterSplash, 1f);

    }

    public async void Death()
    {
        UIOverlays.INSTANCE.CutToBlack();
        SoundManager.Instance.PlayClip(monsterKill, 1f);
        await Task.Delay(3000);
        SceneManager.LoadScene("Death");
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
