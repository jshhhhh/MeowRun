using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E_rat : MonoBehaviour, IEnemyBehavior
{
    public GameObject rat;
    private Player player; 
    
    public IEnemyBehavior.enemyState current; 

    void Awake()
    {
        player = FindObjectOfType<Player>();
        print($"player is at : {player.transform.position}");
        current = IEnemyBehavior.enemyState.Idle;
    }

    // Update is called once per frame
    void Update()
    {
        switch(current) 
        {
            case IEnemyBehavior.enemyState.Idle : 
                Idle();
                break;
            case IEnemyBehavior.enemyState.Track : 
                Track();
                return;
            case IEnemyBehavior.enemyState.Fire : 
                Fire();
                return;
            case IEnemyBehavior.enemyState.Die : 
                Die();
                return;
            default : 
                Idle();
                return;
        }
    }

    public void Idle() 
    {
        // 플레이어가 탐지 거리 바깥이면 주변 패트롤
        print("Enemy being idle");
    }
    public void Fire() 
    {
        print("Enemy found a player, starting to shoot");
    }
    public void Track() 
    {
        // 플레이어가 일정 거리 이상 좁혀지면 추적 시작
        print("Enemy detected a player, starting tracking");
    }
    public void Die() 
    {
        // 플레이어가 밟고 지나가면 죽음
        print("Enemy died by a player");
        Destroy(this.gameObject);
    }
}
