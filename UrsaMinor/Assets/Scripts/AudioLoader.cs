using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AudioLoader : MonoBehaviour
{
    public static AudioLoader instance;

    public AudioClip LevelBGM,
                     OwlbertHit,
                     OwlEnd,
                     OwlStart,
                     BearAngry1,
                     BearAngry2,
                     BearAngry3,
                     AdultBearHappy1,
                     AdultBearHappy2,
                     AdultBearHappy3,
                     AdultBearHappy4,
                     SmallBearHappy1,
                     SmallBearHappy2,
                     SmallBearHappy3,
                     SmallBearHappy4,
                     BearSwipe1,
                     BearSwipe2,
                     BearSwipe3,
                     RibitClickIt1,
                     RibitClickIt2,
                     RibitClickIt3,
                     ButtonSelect,
                     ButtonClick;

    void Awake()
    {
        AudioLoader.instance = this;
    }
}