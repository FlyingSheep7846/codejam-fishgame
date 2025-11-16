using UnityEngine;

public class MonsterManager : MonoBehaviour
{
    private CutsceneController cutsceneController;
    public AudioClip door;

    void Awake()
    {
        cutsceneController = Object.FindAnyObjectByType<CutsceneController>();
        this.enabled = false;
    }

    public void GoToNextDay()
    {
        DayManager.INSTANCE.NewDay();
        cutsceneController.PlayCutscene();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            GoToNextDay();
            SoundManager.Instance.PlayClip(door, 1f);
            SoundManager.Instance.StopMusicAndOcean();
            SoundManager.Instance.StopAllSFX();
            CameraShaker.Instance.StopShaking();
            
        }
    }
}
