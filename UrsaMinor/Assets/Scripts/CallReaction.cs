using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CallReaction : CharacterReaction
{
    private GameManager _theGameManager;
    public float CallDuration;
    public TalkBubbleTypes CallType;

    void Start()
    {
        _theGameManager = FindObjectOfType<GameManager>();
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.tag == "Ursa")
        {
            Character.Call(CallDuration, CallType);

            if(this.name == "PapaCallReaction")
            {
                _theGameManager.PapaCalls++;
                _theGameManager.PapaPointsText.rectTransform.position = Camera.main.WorldToScreenPoint(new Vector2(col.transform.position.x + 1, col.transform.position.y));
                _theGameManager.PapaPointsText.text = "+10";
                StartCoroutine("ResetText", _theGameManager.PapaPointsText);
            }
            if(this.name == "MamaCallReaction")
            {
                _theGameManager.MamaCalls++;
                _theGameManager.MamaPointsText.rectTransform.position = Camera.main.WorldToScreenPoint(new Vector2(col.transform.position.x + 1, col.transform.position.y));
                _theGameManager.MamaPointsText.text = "+10";
                StartCoroutine("ResetText", _theGameManager.MamaPointsText);
            }
        }
    }

    private IEnumerator ResetText(Text resetText)
    {
        yield return new WaitForSeconds(1);

        resetText.text = "";
    }

}