using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField] public float time = 60f;
    public bool dayOver = false;

    public static Timer INSTANCE;

    void Awake()
    {
        INSTANCE = this;
        this.enabled = true;
    }

    public void DecreaseTime(float increment = 5f)
    {
        time -= increment;
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

}
