using UnityEngine;
using System.Collections;

public class TalkBubble : MonoBehaviour
{
    public float Duration;
    private SpriteRenderer _mySpriteRenderer;
    private bool _startFade;

    void Start()
    {
        _mySpriteRenderer = GetComponent<SpriteRenderer>();
        Invoke("IntiateFade", Duration);
    }

    void Update()
    {
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
            _mySpriteRenderer.color = new Color(1, 1, 1, _mySpriteRenderer.color.a - (1 / (1 / Time.deltaTime)));
        }
        else
            Destroy(this.gameObject);
    }
}