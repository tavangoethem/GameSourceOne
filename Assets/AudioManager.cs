using System;
using System.Collections;
using UnityEngine;

//@author Nick Frost (but i just copied Cameron's code)
public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    private AudioSource _bgm;
    private AudioSource[] _sfxSource;
    [SerializeField] private AudioSource WaterSound;
    private int _curSFXIndex = 0;
    [SerializeField] private int _sfxSourceLength;

    private const string BGMPREFSNAME = "BGMVolume";
    private const string SFXPREFSNAME = "SFXVolume";

    private void Awake()
    {   //Singleton
        if (AudioManager.instance == null)
            AudioManager.instance = this;
        else if (AudioManager.instance != this)
            Destroy(this);
        DontDestroyOnLoad(this);
    }

    private void Start()
    {
        _bgm = gameObject.AddComponent<AudioSource>();
        _sfxSource = new AudioSource[_sfxSourceLength];
        for (int i = 0; i < _sfxSourceLength; i++)
        {
            _sfxSource[i] = gameObject.AddComponent<AudioSource>();
            _sfxSource[i].spatialBlend = 0;
        }
    }

    // Play an audio clip. 
    public void PlaySFX(AudioClip clipToPlay)
    {
        _sfxSource[_curSFXIndex].clip = clipToPlay;
        _sfxSource[_curSFXIndex].volume = PlayerPrefs.GetFloat(SFXPREFSNAME, .5f);
        _sfxSource[_curSFXIndex].Play();
        _curSFXIndex++;
        if (_curSFXIndex > _sfxSourceLength - 1)
            _curSFXIndex = 0;
    }

    public void Update()
    {
        if (_sfxSource[_curSFXIndex].volume != PlayerPrefs.GetFloat(SFXPREFSNAME))
        {
            if (WaterSound != null)
            {
                WaterSound.volume = PlayerPrefs.GetFloat(SFXPREFSNAME);
            }
            for (int i = 0; i < _sfxSource.Length; i++)
            {
                _sfxSource[i].volume = PlayerPrefs.GetFloat(SFXPREFSNAME);
            }
        }
        if (_bgm.volume != PlayerPrefs.GetFloat(BGMPREFSNAME))
        {
            _bgm.volume = PlayerPrefs.GetFloat(BGMPREFSNAME);
        }
    }

    //Play an audio clip. Only use this override method if you need to originate a sound from an object in game.
    //@param origin refers to the position of the object creating the sound.
    //@param spacialBlend determines the sound's spacial calculations.  0.0 makes the sound full 2D, 1.0 makes it full 3D.
    public void PlaySFX(AudioClip clipToPlay, Transform origin, float spacialBlend)
    {
        AudioSource temp = origin.gameObject.AddComponent<AudioSource>();
        temp.clip = clipToPlay;
        temp.spatialBlend = spacialBlend;
        temp.volume = PlayerPrefs.GetFloat(SFXPREFSNAME, .5f);
        temp.Play();
        StartCoroutine(DestroySourceAfterDuration(temp, clipToPlay.length));
    }
    public void PlaySFX(AudioClip clipToPlay, Vector3 origin, float spacialBlend)
    {
        GameObject tempGO = new GameObject("TempAudio");

        tempGO.transform.position = origin;
        AudioSource temp = tempGO.AddComponent<AudioSource>();

        temp.clip = clipToPlay;
        temp.volume = PlayerPrefs.GetFloat(SFXPREFSNAME, .5f);
        temp.spatialBlend = spacialBlend;
        temp.Play();
        StartCoroutine(DestroySourceAfterDuration(temp, clipToPlay.length, true));
    }
    public void PlaySFX(AudioClip clipToPlay, Vector3 origin, float spacialBlend, bool isLoop, int MinDistanceHeard)
    {
        _sfxSource[_curSFXIndex].clip = clipToPlay;
        _sfxSource[_curSFXIndex].volume = PlayerPrefs.GetFloat(SFXPREFSNAME, .5f);
        _sfxSource[_curSFXIndex].Play();
        _sfxSource[_curSFXIndex].minDistance = MinDistanceHeard;
        _sfxSource[_curSFXIndex].loop = isLoop;
        _sfxSource[_curSFXIndex].spatialBlend = spacialBlend;
        _curSFXIndex++;
    }

    //Destroy the audio source component once the sound is finished.  Only called when the audio is spacial. 
    private IEnumerator DestroySourceAfterDuration(AudioSource sourceToDestroy, float duration, bool destoryObject = false)
    {
        yield return new WaitForSeconds(duration);
        if (destoryObject == false)
            Destroy(sourceToDestroy);
        else
            Destroy(sourceToDestroy.gameObject);
    }

    // Calls the Background music Coroutine. We use a separate method because coroutines can only be called in 
    // The script where they are declared. 
    public void PlayBGM(AudioClip musicToPlay, float fadeDuration)
    {
        StartCoroutine(PlayBGMCo(musicToPlay, fadeDuration));
    }

    // Plays the background music. 
    private IEnumerator PlayBGMCo(AudioClip musicToPlay, float fadeDuration)
    {
        float t = 0;
        AudioSource newSource = gameObject.AddComponent<AudioSource>();
        newSource.clip = musicToPlay;
        newSource.Play();
        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            if(_bgm != null)
                _bgm.volume = Mathf.Lerp(PlayerPrefs.GetFloat(BGMPREFSNAME, .5f), 0, t / fadeDuration);
            newSource.volume = Mathf.Lerp(0, PlayerPrefs.GetFloat(BGMPREFSNAME, .5f), t / fadeDuration);
            yield return null;
        }
        if(_bgm != null)
            Destroy(_bgm);
        _bgm = newSource;
    }
}
