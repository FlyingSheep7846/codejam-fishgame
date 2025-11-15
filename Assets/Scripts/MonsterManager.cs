using UnityEngine;

public class MonsterManager : MonoBehaviour
{
    public void GoToNextDay()
    {
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            GoToNextDay();
            UIOverlays.INSTANCE.TransitionDay();
        }
    }
}
