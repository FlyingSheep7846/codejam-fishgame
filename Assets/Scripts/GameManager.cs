using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager INSTANCE;
    public AudioClip ambient;
    public AudioClip heartbeat1;
    public AudioClip breathing;
    private bool sfxPlaying = false;

    public List<int> fishNeeded;
    public int currentDay = 0;

    void Awake()
    {
        INSTANCE = this;
        SoundManager.Instance.PlayMusic(ambient, .25f); // start playing the background music on start
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

    
}
