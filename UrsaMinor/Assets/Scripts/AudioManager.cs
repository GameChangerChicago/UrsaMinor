using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public AudioSource BGMSource;
    public AudioSource[] SFXSources;

    private int _SFXSourceIndex;

    void Start()
    {
        if (SceneManager.GetActiveScene().name == "Main")
        {
            BGMSource.clip = AudioLoader.instance.LevelBGM;
            BGMSource.Play();
        }
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
}