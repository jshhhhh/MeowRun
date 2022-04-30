using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private Player player;
    private LifeManager lifeManager;

    [SerializeField] private int currentLife;
    public int TotalLife = 3;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player>();
        lifeManager = FindObjectOfType<LifeManager>();

        currentLife = TotalLife;
    }

    //체력 감소
    public void decreaseLife(int _life = 1)
    {
        currentLife -= _life;
        
        if(currentLife <= 0)
            currentLife = 0;

        lifeManager.updateHeartImage();

        if(isDead())    player._state = Player.playerState.Die;
    }

    //체력이 0인지 확인
    public bool isDead()
    {
        if(currentLife <= 0)    return true;
        else                    return false;
    }

    //체력 증가
    public void increaseLife(int _life = 1)
    {
        currentLife += _life; 

        if(currentLife > TotalLife)
            currentLife = TotalLife;
        
        lifeManager.updateHeartImage();
    }

    //private인 currentLife를 외부에서 체크하는 함수
    public int checkLife()
    {
        return currentLife;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
