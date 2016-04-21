using UnityEngine;
using System.Collections;

public class SceneFadeInOut : MonoBehaviour
{


	public float fadeSpeed = 1.5f;
	public string nextLevel;
	private bool sceneStarting = true;
	//private GUITexture guiTexture = gameObject.GetComponent<GUITexture> ();

	void Awake ()
	{

		GetComponent<GUITexture> ().pixelInset = new Rect (0f, 0f, Screen.width, Screen.height);
	}

	
	// Update is called once per frame
	void Update ()
	{
	
		if (sceneStarting) {

			StartScene ();
		}
	}

	void FadeToClear ()
	{

		GetComponent<GUITexture> ().color = Color.Lerp (GetComponent<GUITexture> ().color, Color.black, fadeSpeed * Time.deltaTime);

	}

	void FadeToBlack ()
	{

		GetComponent<GUITexture> ().color = Color.Lerp (GetComponent<GUITexture> ().color, Color.black, fadeSpeed * Time.deltaTime);
	}

	void StartScene ()
	{

		FadeToClear ();

		if (GetComponent<GUITexture> ().color.a <= 0.05f) {
			GetComponent<GUITexture> ().color = Color.clear;
			GetComponent<GUITexture> ().enabled = false;


			sceneStarting = false;

		}
	}

	public void EndScene ()
	{
		GetComponent<GUITexture> ().enabled = true;

		FadeToBlack ();

		if (GetComponent<GUITexture> ().color.a >= 0.95) {

			Application.LoadLevel (0);

		}
	}
}

