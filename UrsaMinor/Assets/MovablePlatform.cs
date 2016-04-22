using UnityEngine;
using System.Collections;

public class MovablePlatform : MonoBehaviour
{


	public float speed;
	private Vector3 platformVector;


	// Use this for initialization
	void Start ()
	{

		platformVector = GetComponent<Transform> ().position;
	
	}
	
	// Update is called once per frame
	void Update ()
	{
	

	}

	void MovePlatform ()
	{


	}
}
