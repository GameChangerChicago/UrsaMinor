using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public AudioManager TheAuidoManager;
    public GameObject Owlbert;
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
        Owlbert.GetComponent<OwlbertController>().Restart();
        _cameraController.ChangeFocus(Owlbert);
        Invoke("PlayerStartAudio", 0.5f);
    }

    private void PlayerStartAudio()
    {
        TheAuidoManager.PlayerStartTrackPlay();
    }

    public void GameOver()
    {
        Debug.Log("sup");
        TheAuidoManager.PlaySFX(AudioLoader.instance.OwlEnd);
        _cameraController.FadeOut();
        Invoke("ReturnToMenu", 1);
    }

    private void ReturnToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}