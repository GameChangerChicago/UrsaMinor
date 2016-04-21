using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BearJump : MonoBehaviour
{

	private bool clicked = false;
	private Image image;


	// Use this for initialization
	void Start ()
	{
		image = GetComponent<Image> ();
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (!clicked)
			ReturnColor ();
			
	
	}

	public void ChangeColor ()
	{

		clicked = true;

		if (clicked) {
			
		
			image.color = Color.cyan;
			//float alpha = 255;
			//image.color.a = Color.clear;
		
			clicked = false;
			ReturnColor ();
		}




	}


	private void ReturnColor ()
	{
			

		image.color = Color.Lerp (GetComponent<Image> ().color, Color.white, 1f * Time.deltaTime);
		//image.color.a = Mathf.Lerp (image.color.a, 49f, 1f * Time.deltaTime);
	}
}

