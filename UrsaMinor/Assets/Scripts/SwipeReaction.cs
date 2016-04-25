using UnityEngine;
using System.Collections;

public class SwipeReaction : MonoBehaviour
{
    public NPCController MyBear;

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.name == "Owlbert")
        {
            MyBear.EnemySpotted();   
        }
    }
}