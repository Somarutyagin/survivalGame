//using Unity.Mathematics;
using System;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private Slider sliderSound;
    [SerializeField] private Slider sliderMusic;
    [SerializeField] private AudioSource mainMenuMusic;

    [SerializeField] private AudioSource gameMusic1;
    [SerializeField] private AudioSource gameMusic2;
    [SerializeField] private AudioSource gameMusic3;
    [SerializeField] private AudioSource gameMusic4;

    private AudioSource ActiveGameMusic;

    public AudioSource hitSound;
    public AudioSource deathSound;
    public AudioSource bonusCollectSound;
    public AudioSource btnClickSound;

    private string keySound = "sound";
    private float _valueSound;
    public float sound
    {
        get
        {
            Init();
            return _valueSound;
        }
        set
        {
            PlayerPrefs.SetFloat(keySound, value);
            _valueSound = value;
        }
    }
    private string keyMusic = "music";
    private float _valueMusic;
    public float music
    {
        get
        {
            Init();
            return _valueMusic;
        }
        set
        {
            PlayerPrefs.SetFloat(keyMusic, value);
            _valueMusic = value;
        }
    }
    private static AudioManager _instance;
    public static AudioManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new GameObject("AudioManager").AddComponent<AudioManager>();
            }
            return _instance;
        }
    }
    private void Init()
    {
        sound = PlayerPrefs.GetFloat(keySound);
        music = PlayerPrefs.GetFloat(keyMusic);
    }

    public void soundValueChange()
    {
        audioMixer.SetFloat(keySound, Mathf.Lerp(-80.0f, 0, sliderSound.value));
    }
    public void musicValueChange()
    {
        audioMixer.SetFloat(keyMusic, Mathf.Lerp(-80.0f, 0, sliderMusic.value));
    }
    public void mainMenuMusicPlay()
    {
        ActiveGameMusic.Pause();

        mainMenuMusic.Play();
    }
    public void gameMusicPlay()
    {
        mainMenuMusic.Pause();

        int randomMusic = UnityEngine.Random.Range(0, 4);

        switch (randomMusic)
        {
            case 0:
                gameMusic1.Play();
                ActiveGameMusic = gameMusic1;
                break;
            case 1:
                gameMusic2.Play();
                ActiveGameMusic = gameMusic2;
                break;
            case 2:
                gameMusic3.Play();
                ActiveGameMusic = gameMusic3;
                break;
            case 3:
                gameMusic4.Play();
                ActiveGameMusic = gameMusic4;
                break;
        }
    }
}
