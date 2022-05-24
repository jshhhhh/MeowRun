using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class healingCan : Item
{
    //회복 효과
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        soundManager = FindObjectOfType<SoundManager>();
    }

    protected override void itemEffect()
    {
        soundManager.RandomizeSfx(SItemStart);
        gameManager.increaseLife();
    }

    //currentLife가 TotalLife보다 적을 경우에만 아이템이 먹어지도록 OnTriggerEnter를 override
    protected override void OnTriggerEnter(Collider collision)
    {
        if (gameManager.checkLife() < gameManager.TotalLife)
        {
            base.OnTriggerEnter(collision);
        }
    }
}
