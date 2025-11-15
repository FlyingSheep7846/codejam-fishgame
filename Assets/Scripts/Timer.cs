using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField] public float timeLimit = 120f;
    public bool dayOver = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
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
