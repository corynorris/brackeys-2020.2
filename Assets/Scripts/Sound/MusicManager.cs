using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{

    private static MusicManager _instance = null;
    [SerializeField]
    private AudioClip menuMuisc;
    [SerializeField]
    private AudioClip ambienceMuisc;

    private AudioClip newMusic;

    [SerializeField]
    private float volume;

    [SerializeField]
    private float ambienceVolume;

    private float currentVolume;
    private AudioSource musicSource;
    private AudioSource ambienceSource;

    private bool fadingOut;
    private bool fadingIn;
    // Start is called before the first frame update
    void Awake()
    {

        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            _instance = this;
        }

        DontDestroyOnLoad(this.gameObject);

        if (!PlayerPrefs.HasKey("EffectsVolume"))
        {
            PlayerPrefs.SetFloat("EffectsVolume", 1f);
        }
        if (!PlayerPrefs.HasKey("MusicVolume"))
        {
            PlayerPrefs.SetFloat("MusicVolume", 1f);
        }



    }

    void Start()
    {
        musicSource = Utils.AddAudioNoFalloff(gameObject, menuMuisc, true, false, volume, 1);

        musicSource.Play();

        ambienceSource = Utils.AddAudioNoFalloff(gameObject, ambienceMuisc, true, false, ambienceVolume, 1);
        currentVolume = ambienceSource.volume;
        ambienceSource.Play();
    }

    public static MusicManager getInstance()
    {
        return _instance;
    }

    public void updateMusic(AudioClip music)
    {
        if (ambienceSource.clip != music)
        {
            newMusic = music;
            fadingOut = true;

        }
    }

    public void updateSound()
    {
        float globalVolume = PlayerPrefs.GetFloat("MusicVolume");
        musicSource.volume = volume * globalVolume;
        currentVolume = musicSource.volume;
    }

    public void changeSoundPercent(float factor)
    {
        float globalVolume = PlayerPrefs.GetFloat("MusicVolume");
        musicSource.volume = volume * globalVolume * factor;
        currentVolume = musicSource.volume;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (fadingOut)
        {
            musicSource.volume -= 0.01f;
            if (musicSource.volume <= 0)
            {
                fadingIn = true;
                fadingOut = false;
                musicSource.clip = newMusic;
                musicSource.Play();
            }
        }
        else if (fadingIn)
        {
            musicSource.volume += 0.01f;
            if (musicSource.volume >= currentVolume)
            {
                musicSource.volume = currentVolume;
                fadingIn = false;
            }
        }
    }
}
