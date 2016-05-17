using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class NextLevel : MonoBehaviour
{
    public void NewLevel (string name)
	{
        SceneManager.LoadScene(name);
	}
}
