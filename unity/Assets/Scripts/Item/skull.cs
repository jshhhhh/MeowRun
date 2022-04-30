using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class skull : Item
{
    //이동 속도, 점프력 감소, 데미지
    void Start()
    {
        player = FindObjectOfType<Player>();
        gameManager = FindObjectOfType<GameManager>();
        soundManager = FindObjectOfType<SoundManager>();
    }

    protected override void itemEffect()
    {
        soundManager.RandomizeSfx(SItemStart);
        player.StopAllCoroutines();
        gameManager.decreaseLife();
        player.controlSpeed(3f, -2f, -3f);
    }
}
