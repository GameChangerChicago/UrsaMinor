using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public AudioSource BGMSource;
    public AudioSource[] SFXSources;

    private float _BGMTime;
    private int _SFXSourceIndex;

    void Start()
    {
        if (SceneManager.GetActiveScene().name != "MainMenu")
        {
            BGMSource.clip = AudioLoader.instance.LevelBGM;
            BGMSource.Play();
        }
    }

    public void SetMainMenuTrack(AudioClip clip)
    {
        BGMSource.clip = clip;
        BGMSource.Play();
    }

    public void PlaySFX(AudioClip clip)
    {
        SFXSources[_SFXSourceIndex].clip = clip;
        SFXSources[_SFXSourceIndex++].Play();
        if(_SFXSourceIndex == SFXSources.Length)
        {
            _SFXSourceIndex = 0;
        }
    }

    public void PlayerStartTrackPlay()
    {
        _BGMTime = BGMSource.time;
        BGMSource.clip = AudioLoader.instance.OwlStart;
        BGMSource.time = 0;
        BGMSource.Play();
        Invoke("GoBackToBGM", 2);
    }

    private void GoBackToBGM()
    {
        BGMSource.clip = AudioLoader.instance.LevelBGM;
        BGMSource.Play();
        BGMSource.time = _BGMTime;
    }
}