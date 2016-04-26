using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public AudioManager TheAuidoManager;
    public GameObject Owlbert;
    public int OwlbertLives;

    private CameraController _cameraController;

    void Start()
    {
        _cameraController = FindObjectOfType<CameraController>();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Q))
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
}