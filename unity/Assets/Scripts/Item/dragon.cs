using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dragon : Item
{
    //이동 속도 증가
    void Start()
    {
        playerMove = FindObjectOfType<PlayerMove>();
        soundManager = FindObjectOfType<SoundManager>();
        itemIcon = FindObjectOfType<ItemIcon>();

        itemDuration = 5f;
        addSpeed = 3f;
    }

    protected override void itemEffect()
    {
        soundManager.RandomizeSfx(SItemStart);
        playerMove.controlSpeed(itemDuration, addSpeed);
    }

    protected override void OnTriggerEnter(Collider collision)
    {
        base.OnTriggerEnter(collision);

        if(collision.gameObject.CompareTag("Player"))
            itemIcon.updateItemIcon(itemName, itemDuration);
    }
}
