using UnityEngine;
using System.Collections;

public class TalkBubble : MonoBehaviour
{
    public float Duration = -1;
    private SpriteRenderer _mySpriteRenderer;
    private bool _startFade;

    void Start()
    {
        _mySpriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if(!_startFade && Duration != -1)
            Invoke("IntiateFade", Duration);

        if (_startFade)
            FadeOut();
    }

    private void IntiateFade()
    {
        _startFade = true;
    }

    private void FadeOut()
    {
        if (_mySpriteRenderer.color.a > 0)
        {
            _mySpriteRenderer.color = new Color(1, 1, 1, _mySpriteRenderer.color.a - (1 / (0.25f / Time.deltaTime)));
        }
        else
            Destroy(this.gameObject);
    }
}