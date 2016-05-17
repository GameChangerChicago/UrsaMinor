using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public AudioManager TheAuidoManager;
    private GameObject _owlbert;
    static int Playthroughs;
    public int MamaCalls
    {
        get
        {
            return _mamaCalls;
        }
        set
        {
            _mamaCalls = value;

            if(_mamaCalls > 2 && _papaCalls > 2)
            {
                //Happy Parents then the game starts
            }
        }
    }
    private int _mamaCalls;
    public int PapaCalls
    {
        get
        {
            return _papaCalls;
        }
        set
        {
            _papaCalls = value;

            if (_mamaCalls > 2 && _papaCalls > 2)
            {
                //Happy Parents then the game starts
            }
        }
    }
    private int _papaCalls;
    public int OwlbertLives
    {
        get
        {
            return _owlbertLives;
        }
        set
        {
            if(value < _owlbertLives && value >= 0)
            {
                Invoke("PlayerStart", 3);
            }

            _owlbertLives = value;
        }
    }
    private int _owlbertLives = 2;

    private CameraController _cameraController;

    void Start()
    {
        _cameraController = FindObjectOfType<CameraController>();
        OwlbertController owlbert = FindObjectOfType<OwlbertController>();
        if (owlbert)
            _owlbert = owlbert.gameObject;

        if (SceneManager.GetActiveScene().name == "MainMenu")
        {
            if (Playthroughs == 0)
                TheAuidoManager.SetMainMenuTrack(AudioLoader.instance.RibitClickIt1);
            else if (Playthroughs == 1)
                TheAuidoManager.SetMainMenuTrack(AudioLoader.instance.RibitClickIt2);
            else if (Playthroughs > 1)
                TheAuidoManager.SetMainMenuTrack(AudioLoader.instance.RibitClickIt3);
        }
        else
        {
            if (Playthroughs > 0)
            {
                Invoke("PlayerStart", 0.5f);
            }

            Playthroughs++;
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            PlayerStart();
        }
    }

    public void PlayerStart()
    {
        _owlbert.GetComponent<OwlbertController>().Restart();
        _cameraController.ChangeFocus(_owlbert);
        Invoke("PlayerStartAudio", 0.5f);
    }

    private void PlayerStartAudio()
    {
        TheAuidoManager.PlayerStartTrackPlay();
    }

    public void GameOver()
    {
        TheAuidoManager.PlaySFX(AudioLoader.instance.OwlEnd);
        _cameraController.FadeOut();
        Invoke("ReturnToMenu", 1);
    }

    private void ReturnToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}