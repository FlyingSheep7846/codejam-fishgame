using UnityEngine;

public class SoundManager : MonoBehaviour
{

    public static SoundManager Instance;
    public AudioSource music;
    public AudioSource oneShotSfx;
    public AudioSource soundEffects1;
    public AudioSource soundEffects2;
    public AudioSource soundEffects3;

    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private void Awake()
    {
        Instance = this;
    }
    
    public void PlayMusic(AudioClip clip, float vol)
    {
        music.clip = clip;
        music.loop = true;
        music.volume = vol; 
        music.Play();
    }

    public void PlayClip(AudioClip clip, float vol)
    {
        if (clip == null)
        {
            return;
        }
        oneShotSfx.volume = vol;
        oneShotSfx.PlayOneShot(clip);
    }
    public void PlayLoop(AudioSource source, AudioClip clip, float vol)
    {
        if (source == null || clip == null) { return; }
        source.clip = clip;
        source.loop = true;
        source.volume = vol;
        source.Play();
    }

    public void StopSFX1()
    {
        soundEffects1.Stop();
    }
    public void StopSFX2()
    {
        soundEffects2.Stop();
    }
    public void StopSFX3()
    {
        soundEffects3.Stop();
    }

    public void StopAllSFX() // stops all overlaying audios other than the main bg audio
    {
        soundEffects1.Stop();
        soundEffects2.Stop();   
        soundEffects3.Stop();
    }
}
