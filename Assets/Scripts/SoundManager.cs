using UnityEngine;

public class SoundManager : MonoBehaviour
{

    public static SoundManager Instance;
    public AudioSource music;
    public AudioSource soundEffects1;
    public AudioSource soundEffects2;
    public AudioSource soundEffects3;

    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private void Awake()
    {
        Instance = this;
    }
    
    public void PlayMusic(AudioClip clip)
    {
        music.clip = clip;
        music.loop = true;
        music.Play();
    }

    public void PlayClip(AudioClip clip)
    {
        if (clip == null)
        {
            return;
        }
        soundEffects1.PlayOneShot(clip);
    }
    public void PlayLoop(AudioSource source, AudioClip clip)
    {
        if (source == null || clip == null) { return; }
        source.clip = clip;
        source.loop = true;
        source.Play();
    }
}
