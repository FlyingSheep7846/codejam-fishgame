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
    private bool canDouble = false;

    [SerializeField] private float increaseSpeed;
    [SerializeField] private float decreaseSpeed;

    [SerializeField] private float minSpeed;
    [SerializeField] private float maxSpeed;

    [SerializeField] private float resetMinSpeed;
    [SerializeField] private float resetMaxSpeed;

    [SerializeField] private RectTransform barRt;
    [SerializeField] private RectTransform fishRt;

    [SerializeField] float minPosY;
    [SerializeField] float maxPosY;
    
    [SerializeField] private float barVelocity;

    [SerializeField] private float fishProgress;

    [SerializeField] private Slider slider;
    [SerializeField] private CanvasGroup cg;

    [SerializeField] private float progressIncrease;
    [SerializeField] private float progressDecrease;

    [Header("Testing")]
    [SerializeField] private bool isTesting;

    [SerializeField] private QTEController qteController; //AAAAAAAAAAAAAAAAAAAAAAAAA

    private PlayerController playerController;

    void Awake()
    {
        if (isTesting)
        {
            progressIncrease = 0f;
            progressDecrease = 0f;
        } 
        else
        {
            this.enabled = false;
            cg.alpha = 0f;
        }     
    }

    public PlayerController m_playerController
    {
        get
        {
            if (playerController == null)
                playerController = GameObject.FindWithTag("Player").GetComponent<PlayerController>();   // expensive call

            return playerController;
        }
    }

    [Header("Audio")]
    public AudioClip reeling;
    public AudioClip fishGot;
    public AudioClip fishLost;
    private bool sfxPlaying = false;

    public void StartFish()
    {
        slider.value = 0.7f;
        this.enabled = true;
        barVelocity = 0f;
        amount = 0f;
        visualAmount = 0f;
        barY = 0f;

        UIOverlays.INSTANCE.ToggleFishingView(false);
        cg.DOFade(1f, 0.5f).OnComplete(
            () => this.enabled = true
        );

        qteController.InitializeQTE();
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


        barY = Mathf.Lerp(minPosY, maxPosY, visualAmount);
        barRt.anchoredPosition = new Vector2(barRt.anchoredPosition.x, barY);
        CheckIfFishIn();
    }

    public void DecreaseProgressBar(float increment)
    {
        slider.value += increment;
    }

    void CheckIfFishIn()
    {
        float fishPos = fishRt.anchoredPosition.y;
        float barCap = barY + barRt.sizeDelta.y;

        // Debug.Log($"{fishPos} {barY} {barCap}");

        if (fishPos >= barY && fishPos <= barCap)
        {
            slider.value -= progressIncrease * Time.deltaTime;
        } else
        {
            slider.value += progressDecrease * Time.deltaTime;
        }

        if (slider.value <= 0f)
        {
            FishComplete();
        } else if (slider.value >= 1f)
        {
            FishFailed();
        }
    }

    void FishComplete()
    {
        this.enabled = false;
        Debug.Log("Fish Succeeded");

        if (canDouble && UnityEngine.Random.value >= 0.5f)
            FishManager.INSTANCE.AddFish(2);
        else
            FishManager.INSTANCE.AddFish(1);

        cg.DOFade(0f, 0.5f).OnComplete(
            () => UIOverlays.INSTANCE.ToggleFishingView(true)
        );

        qteController.enabled = false;
        m_playerController.ToggleFreeLook(true, 0);

        SoundManager.Instance.PlayClip(fishGot, .25f);
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

        qteController.enabled = false;
        m_playerController.ToggleFreeLook(true, 0);

        SoundManager.Instance.PlayClip(fishLost, 1f);
        SoundManager.Instance.StopSFX3();
        sfxPlaying= false;
    }


    // upgrades
    public void IncreaseBarSize(int increment)
    {
        Vector2 barSize = barRt.sizeDelta;
        barSize.y = barSize.y + increment;
        barRt.sizeDelta = barSize;
        maxPosY -= increment;
    }

    public void CanDouble(bool canDouble)
    {
        this.canDouble = canDouble;
    }

}
