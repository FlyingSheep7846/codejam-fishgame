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

    private GameObject player;
    [SerializeField] private GameObject environmentCamera;

    [SerializeField] Transform playerRespawnSpot;

    public List<int> fishNeeded;
    public int currentDay = 0;

    void Awake()
    {
        INSTANCE = this;

        player = GameObject.FindWithTag("Player");
        // environmentCamera = GameObject.FindWithTag("MainCamera");
        Debug.Log($"grahhhhh {player} ermm {environmentCamera} asdasdas");
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
        if(Timer.INSTANCE.time < 0f)
        {
            if (!CameraShaker.Instance.isShaking)
            {
                CameraShaker.Instance.ShakeCamera();
            }
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
            Timer.INSTANCE.running = true;
            FishManager.INSTANCE.setFishCount(0);
            currentDay++;
            TransitionDay(true);
            sfx1Playing = false;
            sfx2Playing = false;
            CameraShaker.Instance.isShaking = false;

            // tweak environment shit here ?
        }

    
    }
