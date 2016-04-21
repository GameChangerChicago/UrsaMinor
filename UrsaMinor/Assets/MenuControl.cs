using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class MenuControl : MonoBehaviour
{


	private string[] menuOptions = new string[2];
	public Button newGame, credits;
	public Sprite newGameHighlight, creditsHighlight, newGameDefault, creditsDefault;
	public AudioClip buttonSelect, buttonHighlight;
	private AudioSource audioSource;
	private bool menuSelected;
	private int selectedIndex = 0;

	// Use this for initialization
	void Start ()
	{
		newGame.GetComponent<Image> ().sprite = newGameHighlight;
		audioSource = GetComponent<AudioSource> ();

		audioSource.clip = buttonHighlight;
		selectedIndex = 0;
		menuSelected = false;
	
		menuOptions [0] = "New Game";
		menuOptions [1] = "Credits";
	}
	
	// Update is called once per frame
	void Update ()
	{
	
		if (Input.GetKeyDown (KeyCode.DownArrow)) {

			newGame.GetComponent<Image> ().sprite = newGameDefault;
			credits.GetComponent<Image> ().sprite = creditsHighlight;
			selectedIndex = 1;
			audioSource.Play ();
		}

		if ((Input.GetKeyDown (KeyCode.UpArrow))) {
			newGame.GetComponent<Image> ().sprite = newGameHighlight;
			credits.GetComponent<Image> ().sprite = creditsDefault;

			selectedIndex = 0;
			audioSource.Play ();
		}

		if (Input.GetKeyDown (KeyCode.Return)) {

			if (selectedIndex == 0) {

				audioSource.clip = buttonSelect;
				audioSource.Play ();

				menuSelected = true;

			
			}

			if (selectedIndex == 1) {

				audioSource.clip = buttonSelect;
				audioSource.Play ();

			
				PlayCredits ();

			}
		}

		if (menuSelected && selectedIndex == 0 && !audioSource.isPlaying) {

			Application.LoadLevel (1);

		}
	}

	void PlayCredits ()
	{
		audioSource.clip = buttonHighlight;
		//	audioSource.Play ();
	}




}
