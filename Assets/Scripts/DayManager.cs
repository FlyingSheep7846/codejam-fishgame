using System.Collections.Generic;
using UnityEngine;

public class DayManager : MonoBehaviour
{
    public static DayManager INSTANCE;
    public AudioClip ambient;
    public AudioClip heartbeat1;
    public AudioClip breathing;
    public AudioClip ocean;
    private bool sfxPlaying = false;

    [SerializeField] GameObject player;
    [SerializeField] GameObject environmentCamera;

    public List<int> fishNeeded;
    public int currentDay = 0;

    void Awake()
    {
        INSTANCE = this;
    }

    void Start()
    {
        SoundManager.Instance.PlayMusic(ambient, 0f); // start playing the background music on start
        SoundManager.Instance.PlayOcean(ocean, 0f); 
    }

    private void Update()
    {
        if(Timer.INSTANCE.time < 30f && sfxPlaying == false)
        {
            sfxPlaying = true;
            SoundManager.Instance.PlayLoop(SoundManager.Instance.soundEffects1, heartbeat1, 1f);
            SoundManager.Instance.PlayLoop(SoundManager.Instance.soundEffects2, breathing, 1f);
        }
    }

    public void NewDay()
    {
        FishManager.INSTANCE.setFishCount(0);
        currentDay++;

        // move player back to middle
    }

    public void TransitionDay(bool isPlayMode)
    {
        player.SetActive(isPlayMode);
        environmentCamera.SetActive(!isPlayMode);
    }

    
}
