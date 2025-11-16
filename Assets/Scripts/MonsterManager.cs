using System.Collections;
using Unity.VisualScripting;
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
        Timer.INSTANCE.time = 0f;
        Timer.INSTANCE.running = false;

        SoundManager.Instance.PlayClip(door, 1f);
        DayManager.INSTANCE.NewDay();
        cutsceneController.PlayCutscene();

        StartCoroutine(STUPIDASSFIX());
    }

    IEnumerator STUPIDASSFIX()
    {
        yield return new WaitForSecondsRealtime(0.1f);
        
        SoundManager.Instance.StopMusicAndOcean();
        SoundManager.Instance.StopAllSFX();
        CameraShaker.Instance.StopShaking();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            GoToNextDay();
        }
    }
}
