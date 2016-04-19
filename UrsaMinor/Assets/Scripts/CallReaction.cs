using UnityEngine;
using System.Collections;

public class CallReaction : CharacterReaction
{
    public float CallDuration;
    public TalkBubbleTypes CallType;    

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.tag == "Ursa")
        {
            Character.Call(CallDuration, CallType);
        }
    }
}