using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public AudioManager TheAuidoManager;
    public GameObject Owlbert;
    public Transform OwlbertInstantiationPoint;
    public int OwlbertLives;

    public void PlayerStart()
    {
        Owlbert = (GameObject)Instantiate(Owlbert, OwlbertInstantiationPoint.position, Quaternion.identity);
    }
}