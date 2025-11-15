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

    [SerializeField] Transform playerRespawnSpot;

    public List<int> fishNeeded;
    public int currentDay = 0;


    void Awake()
    {
        INSTANCE = this;
    }

    void Start(){
        StartAudios();
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

        Vector3 position = playerRespawnSpot.position;
        position.y = player.transform.position.y;

        player.transform.position = position;
        player.transform.eulerAngles = new Vector3(0, -90, 0); 
    }

    public void TransitionDay(bool isPlayMode)
    {
        player.SetActive(isPlayMode);
        environmentCamera.SetActive(!isPlayMode);
        if (isPlayMode)
        {
            StartAudios();
        }
    }

    private void StartAudios()
    {
        SoundManager.Instance.PlayMusic(ambient, .25f); // start playing the background music on start
        SoundManager.Instance.PlayOcean(ocean, .25f);
    }

    
}
