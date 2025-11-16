using Unity.VisualScripting;
using UnityEngine;

public class FishController : MonoBehaviour
{
    [SerializeField] RectTransform fishRt;

    float amount;
    float lerpedAmount;
    float fishY;
    float timer;
    float velocity;

    [Header("Behaviour Parameters")]
    public float[] timerIntervals = {0.4f, 1f};
    public float maxSpeed = 5f;

    public float inertia;

    public float baseErratic;
    public float erraticMultiplier;

    [SerializeField] private float FishMinPos;
    [SerializeField] private float FishMaxPos;


    void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0) NewPosition();

        lerpedAmount = Mathf.SmoothDamp(
            lerpedAmount,
            amount,
            ref velocity,
            inertia
        );

        fishY = Mathf.Lerp(FishMinPos, FishMaxPos, lerpedAmount);
        fishRt.anchoredPosition = new Vector2(fishRt.anchoredPosition.x, fishY);
    }

    void NewPosition(){
        float movement = GetNewTarget();
        amount = Mathf.Clamp01(amount + movement);

        SetTimer();
    }

    float GetNewTarget()
    {
        float offset = amount - 0.5f;
        
        float value = Random.value;
        float positive = Random.value - offset;
        if (positive <= 0.5f) value *= -1;

        float distance = value * baseErratic * erraticMultiplier;
        return distance;
    }

    void SetTimer()
    {
        timer = Random.Range(timerIntervals[0], timerIntervals[1]);
    }

}
