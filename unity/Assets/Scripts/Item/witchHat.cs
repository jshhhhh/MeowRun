using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class witchHat : Item
{
    //추가 점프 효과, 추가 점프의 점프력 상승
    void Start()
    {
        player = FindObjectOfType<Player>();
        soundManager = FindObjectOfType<SoundManager>();
    }

    protected override void itemEffect()
    {
        soundManager.RandomizeSfx(SItemStart);
        player.moreJumping(2f);
    }
}
