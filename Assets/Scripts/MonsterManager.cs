using UnityEngine;

public class MonsterManager : MonoBehaviour
{
    private CutsceneController cutsceneController;

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
            
        }
    }
}
