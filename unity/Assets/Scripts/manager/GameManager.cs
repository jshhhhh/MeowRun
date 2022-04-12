using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private Player player;

    [SerializeField] private int health;
    public int maxHealth = 3;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player>();

        health = maxHealth;
    }

    //체력 감소
    public void decreaseHealth(int _health = 1)
    {
        health -= _health;
        
        if(health <= 0)
            health = 0;
    }

    //private인 health를 외부에서 체크하기 위한 함수
    public bool checkPlayerDied()
    {
        if(health <= 0)
            return true;
        else
            return false;
    }

    //체력 증가
    public void increaseHealth(int _health = 1)
    {
        health += _health;

        if(health > maxHealth)
            health = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
