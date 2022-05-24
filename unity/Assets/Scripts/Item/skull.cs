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
        itemIcon = FindObjectOfType<ItemIcon>();

        itemDuration = 5f;
        addSpeed = -2f;
        addJumpPower = -2f;
    }

    protected override void itemEffect()
    {
        soundManager.RandomizeSfx(SItemStart);
        gameManager.decreaseLife();
        player.controlSpeed(itemDuration, addSpeed, addJumpPower);
    }

    protected override void OnTriggerEnter(Collider collision)
    {
        base.OnTriggerEnter(collision);

        if(collision.gameObject.CompareTag("Player"))
            itemIcon.updateItemIcon(itemName, itemDuration);
    }
}
