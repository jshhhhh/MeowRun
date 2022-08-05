using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class witchHat : Item
{
    //추가 점프 효과, 추가 점프의 점프력 상승
    void Start()
    {
        playerMove = FindObjectOfType<PlayerMove>();
        soundManager = FindObjectOfType<SoundManager>();
        itemIcon = FindObjectOfType<ItemIcon>();

        addJumpPower = 2f;
    }

    protected override void itemEffect()
    {
        soundManager.RandomizeSfx(SItemStart);
        playerMove.moreJump(addJumpPower);
    }

    protected override void OnTriggerEnter(Collider collision)
    {
        base.OnTriggerEnter(collision);

        if(collision.gameObject.CompareTag("Player"))
            itemIcon.updateItemIcon(itemName, itemDuration);
    }
}
