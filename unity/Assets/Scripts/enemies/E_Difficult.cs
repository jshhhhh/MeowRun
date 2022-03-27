using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

// Enemy type : Difficult
public class E_Difficult : MonoBehaviour, IEnemyBehavior
{
    private Player player; 
    private float distance; // Player ~ enemy 사이 거리
    private string difficultType;
    
    public IEnemyBehavior.enemyState current; // enemy 상태
    public IEnemyBehavior.playerDistanceState isDetected; // enemy의 player 탐지  
    public NavMeshAgent _agent; // enemy 인공지능 인스턴스
    [SerializeField] Transform[] AgentRoutes;
    public int routeIndex = 0;
    
    public float detectLimit = 5f; // enemy 감지 거리 한계, 에디터에서 설정 가능하도록 세팅

    // ============== Object initialization and update ============== // 
    void Awake()
    {
        current = IEnemyBehavior.enemyState.Idle; // awake시 상태는 idle
        player = FindObjectOfType<Player>();
        _agent = this.GetComponent<NavMeshAgent>();
        difficultType = IEnemyBehavior.enemyType.Easy.ToString();
        _agent.autoBraking = false; // for continuous movement between points
        isDetected = IEnemyBehavior.playerDistanceState.TooFar;
        print($"player is at : {player.transform.position}");
    }

    void Update()
    {
        updateState();
        updateBehavior();
    }
    // ============== Object initialization and update ============== // 

    // ============== Enemy state and behavior ============== // 
    public void calculateDistance() { 
        // 플레이어가 일정 거리 이상 좁혀지면 추적 시작
        distance = Vector3.Distance(player.transform.position, this.transform.position);
        if (distance < detectLimit) {
            isDetected = IEnemyBehavior.playerDistanceState.Within;
        }
    }
    public void updateBehavior()
    {
        switch(current) 
        {
            case IEnemyBehavior.enemyState.Idle : 
                Idle();
                break;
            case IEnemyBehavior.enemyState.Track : 
                // TO DO : enemy에 작은 신호등 붙이기(track state 표시용)
                // this.gameObject.add
                Track();
                break;
            case IEnemyBehavior.enemyState.Fire : // Enemy type 중 rat은 fire하지 않음
                Fire();
                break;
            case IEnemyBehavior.enemyState.Die : 
                Die();
                break;
            default : 
                Idle();
                break;
        }
    }

    public void updateState()
    {
        calculateDistance(); // isDetected 변수 상태 change
        switch (isDetected) {
            case IEnemyBehavior.playerDistanceState.TooFar :
                current = IEnemyBehavior.enemyState.Idle;
                break;
            case IEnemyBehavior.playerDistanceState.Within : 
                current = IEnemyBehavior.enemyState.Track;
                break;
            default : 
                current = IEnemyBehavior.enemyState.Idle;
                break;
        }
    }
    // ============== Enemy state and behavior ============== // 


    // ============== IEnemyBehavior implementation ============== // 
    public void Idle() 
    {
        // TO DO : 플레이어가 탐지 거리 바깥이면 주변 패트롤
        print("Enemy being idle");
        Patrol();
    }

    public void Track() 
    {
        print("Enemy detected a player, starting tracking");
        _agent.destination = player.transform.position;
    }

    public void Fire() 
    {
        print("Difficult Enemy firing projectiles faster");
    }

    public void Die() 
    {
        // TO DO : 플레이어가 밟고 지나가면 죽음
        print("Enemy died by a player");
        Destroy(this.gameObject);
    }

    public void Patrol()
    {
        _agent.destination = AgentRoutes[routeIndex].transform.position;
        if (!_agent.pathPending && _agent.remainingDistance < 0.5f)
        {
            routeIndex = (routeIndex+1)%AgentRoutes.Length;
        }
    }
    // ============== IEnemyBehavior implementation ============== // 
}
