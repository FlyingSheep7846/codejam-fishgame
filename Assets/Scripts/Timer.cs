using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField] public float timeLimit = 120f;
    public bool dayOver = false;

    public static Timer INSTANCE;

    void Awake()
    {
        INSTANCE = this;
        this.enabled = false;
    }

    public void DecreaseTime(float increment = 5f)
    {
        timeLimit -= increment;
    }

    // Update is called once per frame
    void Update()
    {
        timeLimit -= Time.deltaTime;
        if (timeLimit < 0)
        {
            dayOver = true;
        }
    } 
}
