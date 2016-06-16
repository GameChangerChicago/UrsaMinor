using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public AudioManager TheAuidoManager;
    private GameObject _owlbert;
    private NPCController[] parents = new NPCController[2];
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

            if(_mamaCalls == 2 && _papaCalls == 2)
            {
                parents[0].Call(0.5f, TalkBubbleTypes.HAPPY);
                parents[1].Call(0.5f, TalkBubbleTypes.HAPPY);
                Invoke("PlayerStart", 1.0f);
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

            if (_mamaCalls == 2 && _papaCalls == 2)
            {
                parents[0].Call(0.5f, TalkBubbleTypes.HAPPY);
                parents[1].Call(0.5f, TalkBubbleTypes.HAPPY);
                Invoke("PlayerStart", 1.0f);
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
    public Text MamaPointsText,
                PapaPointsText,
                OwlbertPointsText;

    void Start()
    {
        _cameraController = FindObjectOfType<CameraController>();
        OwlbertController owlbert = FindObjectOfType<OwlbertController>();
        if (owlbert)
            _owlbert = owlbert.gameObject;

        NPCController[] npcControllers = FindObjectsOfType<NPCController>();

        for (int i = 0; i < npcControllers.Length; i++)
        {
            if(npcControllers[i].IsParent)
            {
                if (!parents[0])
                    parents[0] = npcControllers[i];
                else
                    parents[1] = npcControllers[i]; 
            }
        }

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
        if (Input.GetKey(KeyCode.Q))
        {
            //PlayerStart();
        }

        if (SceneManager.GetActiveScene().name == "Level1")
            TextManager();
    }

    private void TextManager()
    {
        if (MamaPointsText.text != "")
        {
            MamaPointsText.transform.position = new Vector2(MamaPointsText.transform.position.x, MamaPointsText.transform.position.y + 100 * Time.deltaTime);
        }
        if (PapaPointsText.text != "")
        {
            PapaPointsText.transform.position = new Vector2(PapaPointsText.transform.position.x, PapaPointsText.transform.position.y + 100 * Time.deltaTime);
        }
        if (OwlbertPointsText.text != "")
        {
            OwlbertPointsText.transform.position = new Vector2(OwlbertPointsText.transform.position.x, OwlbertPointsText.transform.position.y + 100 * Time.deltaTime);
        }
    }

    public void PlayerStart()
    {
        _owlbert.GetComponent<OwlbertController>().Restart();
        _cameraController.ChangeFocus(_owlbert);
        Invoke("PlayerStartAudio", 0.5f);
        Invoke("SwipeInstructions", 5);
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

    private void SwipeInstructions()
    {
        parents[0].Call(0.5f, TalkBubbleTypes.SWIPE);
        parents[1].Call(0.5f, TalkBubbleTypes.SWIPE);
    }
}