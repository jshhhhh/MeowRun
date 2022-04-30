using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Item : MonoBehaviour
{
    protected Player player;
    protected GameManager gameManager;
    protected SoundManager soundManager;
    public AudioClip SItemStart;

    //각 아이템에 맞는 효과
    protected abstract void itemEffect();

    //Box Collider에 Is Trigger 체크 시 사용할 수 있는 함수
    //물리적인 충돌 없이 닿았을 때의 효과만 제공
    protected virtual void OnTriggerEnter(Collider collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            itemEffect();
            Destroy(this.gameObject);
        }
    }
}
