using UnityEngine;

public class FishManager : MonoBehaviour
{
    private float fishCount = 0f;
    [SerializeField] private float fishNeeded;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public float getFishCount()
    {
        return fishCount;
    }
    public void setFishCount(float add)
    {
        fishCount += add;
    }

    public float getFishNeeded()
    {
        return fishNeeded;
    }

    public void setFishNeeded(float fish)
    {
        fishNeeded = fish;
    }
}
