using System;
using System.Xml;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class FishingRodController : MonoBehaviour
{
    private float amount;
    private float visualAmount = 0f;
    private float barY;

    [SerializeField] private float increaseSpeed;
    [SerializeField] private float decreaseSpeed;

    [SerializeField] private float minSpeed;
    [SerializeField] private float maxSpeed;

    [SerializeField] private float resetMinSpeed;
    [SerializeField] private float resetMaxSpeed;

    [SerializeField] private RectTransform barRt;
    [SerializeField] private RectTransform fishRt;

    private float maxHeight = 500;
    [SerializeField] private float barVelocity;

    [SerializeField] private float fishProgress;

    [SerializeField] private Slider slider;

    // Update is called once per frame
    void Update()
    {
        float deltaTime = Time.deltaTime;

        if (Input.GetKey(KeyCode.Space))
        {
            barVelocity += increaseSpeed * deltaTime;
        } else
        {
            barVelocity -= decreaseSpeed * deltaTime;
        }

        barVelocity = Mathf.Clamp(barVelocity, minSpeed, maxSpeed);
        amount += barVelocity * deltaTime;
        
        visualAmount = Mathf.Clamp01(amount);

        if (amount <= -0.1f)
        {
            amount = 0f;
            barVelocity = resetMinSpeed;

        } else if (amount >= 1.1f)
        {
            amount = 1f;
            barVelocity = resetMaxSpeed;
        }


        barY = Mathf.Lerp(0, maxHeight, visualAmount);
        barRt.anchoredPosition = new Vector2(0, barY);
        CheckIfFishIn();
    }

    void CheckIfFishIn()
    {
        float fishPos = fishRt.anchoredPosition.y + 25;
        float barCap = barY + barRt.sizeDelta.y;

        if (fishPos >= barY && fishPos <= barCap)
        {
            slider.value += 0.1f * Time.deltaTime;
        } else
        {
            slider.value -= 0.1f * Time.deltaTime;
        }
    }
}
