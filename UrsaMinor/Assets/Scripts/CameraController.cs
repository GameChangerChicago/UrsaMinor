using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour
{
    private Camera _mainCamera;

    void Start()
    {
        _mainCamera = Camera.main;
    }
    
    void Update()
    {
        
    }

    private void Follow(Transform subject)
    {
        iTween.MoveTo(new GameObject(), new Vector3(0, 0, 0), 2);
    }
}