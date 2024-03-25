using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//@author Nick Frost (but i just copied Cameron's code)
public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    private AudioSource _bgm;
    private AudioSource[] _sfxSource;
    private int _curSFXIndex = 0;
    [SerializeField] private int _sfxSourceLength;

    private void Awake()
    {   //Singleton
        if(AudioManager.instance == null)
            AudioManager.instance = this;
        else if(AudioManager.instance != this)
            Destroy(this);
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
        _sfxSource[_curSFXIndex].Play();
        _curSFXIndex++;
        if (_curSFXIndex > _sfxSourceLength - 1)
            _curSFXIndex = 0;
    }

    //Play an audio clip. Only use this overrided method if you need to originate a sound from an object in game.
    //@param origin refers to the position of the object creating the sound.
    //@param spacialBlend determines the sound's spacial calculations.  0.0 makes the sound full 2D, 1.0 makes it full 3D.
    public void PlaySFX(AudioClip clipToPlay, Transform origin, float spacialBlend)
    {
        AudioSource temp = origin.gameObject.AddComponent<AudioSource>();
        temp.clip = clipToPlay;
        temp.spatialBlend = spacialBlend;
        temp.Play();
        StartCoroutine(DestroySourceAfterDuration(temp, clipToPlay.length));
    }

    //Destroy the audio source component once the sound is finished.  Only called when the audio is spacial. 
    private IEnumerator DestroySourceAfterDuration(AudioSource sourceToDestroy, float duration)
    {
        yield return new WaitForSeconds(duration);
        Destroy(sourceToDestroy);
    }

    // Calls the Background music Coroutine. We use a seperate method because coroutines can only be called in 
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
        while(t < fadeDuration)
        {
            t += Time.deltaTime;
            _bgm.volume = Mathf.Lerp(1,0,t/fadeDuration);
            newSource.volume = Mathf.Lerp(0,1, t / fadeDuration);
            yield return null;
        }
        Destroy(_bgm);
        _bgm = newSource;
    }
}
