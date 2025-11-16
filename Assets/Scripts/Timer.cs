using System.Collections;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField] public float time = 60f;
    public float timeInADay = 60f;

    public bool running = true;
    public bool dayOver = false;

    public static Timer INSTANCE;

    void Awake()
    {
        INSTANCE = this;
        this.enabled = true;
    }

    public void DecreaseTime(float increment = 5f)
    {
        if (running) time -= increment;
    }

    // Update is called once per frame
    void Update()
    {
        time -= Time.deltaTime;
        if (time < 0)
        {
            dayOver = true;
        }
    } 

    public float GetTimeRemaining()
    {
        return time;
    }

    public void TimerReset()
    {
        StartCoroutine(TimerResetProcess(3f));
    }

    public IEnumerator TimerResetProcess(float duration)
    {
        float elapsed = 0f;
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;

            float t = elapsed / duration;
            time = Mathf.Lerp(0, timeInADay, t);
            yield return null;
        }
    }
}
