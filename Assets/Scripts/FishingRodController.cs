using System;
using System.Xml;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

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

    [SerializeField] private CanvasGroup cg;

    public AudioClip reeling;
    public AudioClip fishGot;
    public AudioClip fishLost;
    private bool sfxPlaying = false;

    public void StartFish()
    {
        slider.value = 0.3f;
        this.enabled = true;
        barVelocity = 0f;
        amount = 0f;
        visualAmount = 0f;
        barY = 0f;

        UIOverlays.INSTANCE.ToggleFishingView(false);
        cg.DOFade(1f, 0.5f).OnComplete(
            () => this.enabled = true
        );


    }

    // Update is called once per frame
    void Update()
    {
        float deltaTime = Time.deltaTime;

        if (Input.GetKey(KeyCode.Space))
        {
            barVelocity += increaseSpeed * deltaTime;
            if (sfxPlaying == false)
            {
                sfxPlaying = true;
                SoundManager.Instance.PlayLoop(SoundManager.Instance.soundEffects3, reeling, 1);
            }
            
        } else
        {
            barVelocity -= decreaseSpeed * deltaTime;
            SoundManager.Instance.StopSFX3();
            sfxPlaying = false;
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
            slider.value += 0.3f * Time.deltaTime;
        } else
        {
            slider.value -= 0.1f * Time.deltaTime;
        }

        if (slider.value >= 1f)
        {
            FishComplete();
        } else if (slider.value <= 0f)
        {
            FishFailed();
        }
    }

    void FishComplete()
    {
        this.enabled = false;
        Debug.Log("Fish Succeeded");
        FishManager.INSTANCE.AddFish();
        cg.DOFade(0f, 0.5f).OnComplete(
            () => UIOverlays.INSTANCE.ToggleFishingView(true)
        );
        SoundManager.Instance.PlayClip(fishGot, 1f);
        SoundManager.Instance.StopSFX3();
        sfxPlaying = false;
    }

    void FishFailed()
    {
        this.enabled = false;
        Debug.Log("Fish Failed");
        Timer.INSTANCE.DecreaseTime();
        cg.DOFade(0f, 0.5f).OnComplete(
            () => UIOverlays.INSTANCE.ToggleFishingView(true)
        );
        SoundManager.Instance.PlayClip(fishLost, 1f);
        SoundManager.Instance.StopSFX3();
        sfxPlaying= false;
    }
}
