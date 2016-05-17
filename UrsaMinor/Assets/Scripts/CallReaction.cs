using UnityEngine;
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
            }
            if(this.name == "MamaCallReaction")
            {
                _theGameManager.MamaCalls++;
            }
        }
    }
}