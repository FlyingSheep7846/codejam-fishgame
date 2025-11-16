using UnityEngine;

public class FishManager : MonoBehaviour
{
    private int fishCount = 0;
    [SerializeField] private int fishNeeded;

    public static FishManager INSTANCE;

    void Awake()
    {
        INSTANCE = this;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public int getFishCount()
    {
        return fishCount;
    }
    public void setFishCount(int amount)
    {
        fishCount = amount;
        UIOverlays.INSTANCE.SetFish(fishCount);
    }

    public void AddFish(int fish)
    {
        setFishCount(fishCount + fish);
    }

    public float getFishNeeded()
    {
        return fishNeeded;
    }

    public void setFishNeeded(int fish)
    {
        fishNeeded = fish;
    }
}
