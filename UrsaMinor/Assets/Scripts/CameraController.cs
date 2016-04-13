using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour
{
    private Camera _mainCamera;
    public GameObject _ursa;

    void Start()
    {
        _mainCamera = Camera.main;
    }
    
    void Update()
    {
        Follow();
    }

    private void Follow()
    {
        Hashtable ht = new Hashtable();
        ht.Add("easetype", iTween.EaseType.easeInSine);
        ht.Add("x", _ursa.transform.position.x);
        iTween.MoveTo(this.gameObject, ht);
    }
}