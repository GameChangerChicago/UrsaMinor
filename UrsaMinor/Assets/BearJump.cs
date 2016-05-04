using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BearJump : MonoBehaviour
{

	private bool clicked = false;
	private Image image;
    private Animator _myAnimator;


	// Use this for initialization
	void Start ()
	{
        _myAnimator = this.GetComponent<Animator>();
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

    public void Animate()
    {
        _myAnimator.SetBool("MenuJump", true);
        Invoke("PostAnimate", 0.5f);
    }

    private void PostAnimate()
    {
        _myAnimator.SetBool("MenuJump", false);
    }


	private void ReturnColor ()
	{
			

		image.color = Color.Lerp (GetComponent<Image> ().color, Color.white, 1f * Time.deltaTime);
		//image.color.a = Mathf.Lerp (image.color.a, 49f, 1f * Time.deltaTime);
	}
}

