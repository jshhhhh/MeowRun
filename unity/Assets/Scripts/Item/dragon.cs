using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dragon : Item
{
    //이동 속도 증가
    void Start()
    {
        player = FindObjectOfType<Player>();
        soundManager = FindObjectOfType<SoundManager>();
    }

    protected override void itemEffect()
    {
        soundManager.RandomizeSfx(SItemStart);
        player.StopAllCoroutines();
        player.controlSpeed(3f, 6.5f);
    }
}
