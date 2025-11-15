using System.Collections.Generic;
using UnityEngine;

public class DayManager : MonoBehaviour
{
    public static DayManager INSTANCE;
    public AudioClip ambient;
    public AudioClip heartbeat1;
    public AudioClip heartbeat2;
    public AudioClip breathing;
    public AudioClip ocean;
    private bool sfx2Playing = false;
    private bool sfx1Playing = false;   

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
        if (Timer.INSTANCE.time < 60f && sfx1Playing == false)
        {
            sfx1Playing = true;
            SoundManager.Instance.PlayLoop(SoundManager.Instance.soundEffects4, heartbeat2, 1f);
        }

        if (Timer.INSTANCE.time < 30f && sfx2Playing == false)
        {
            
            SoundManager.Instance.StopSFX4();

            sfx2Playing = true;
            SoundManager.Instance.PlayLoop(SoundManager.Instance.soundEffects1, heartbeat1, 1f);
            SoundManager.Instance.PlayLoop(SoundManager.Instance.soundEffects2, breathing, 1f);
        }
        if(Timer.INSTANCE.time == 0f)
        {
            sfx1Playing = false;
            sfx2Playing= false;
        }
    }

    public void NewDay()
        {
            //FishManager.INSTANCE.setFishCount(0);
        

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

        public void BeginDay()
        {
            Timer.INSTANCE.time = 120f;
            FishManager.INSTANCE.setFishCount(0);
            currentDay++;
            TransitionDay(true);

            // tweak environment shit here ?
        }

    
    }
