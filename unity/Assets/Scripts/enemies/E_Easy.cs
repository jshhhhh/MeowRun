using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

// Enemy type : Easy
public class E_Easy : MonoBehaviour, IEnemyBehavior
{
    private Player player; 
    private float distance; // Player ~ enemy 추적 거리
    private string easyType;
    internal static IEnemyBehavior.enemyState current; // enemy 상태
    private IEnemyBehavior.playerDistanceState isDetected; // enemy의 player 탐지  
    public NavMeshAgent _agent; // enemy 인공지능 인스턴스
    [SerializeField] Transform[] AgentRoutes;
    public int routeIndex = 0;
    public float detectLimit = 5f; // enemy 감지 거리 한계, 에디터에서 설정 가능하도록 세팅
    
    // ============== Object initialization and update ============== // 
    void Awake()
    {
        InitSetup();
    }
    void Update()
    {
        updateState();
        updateBehavior();
    }
    void InitSetup() 
    {
        // 플레이어 & NavMesh 초기화
        player = FindObjectOfType<Player>(); 
        _agent = this.GetComponent<NavMeshAgent>(); 

        // Enemy 초기화 : awake시 상태는 idle, not detectable
        if (player != null && _agent != null) // 오브젝트 null check
        {
            current = IEnemyBehavior.enemyState.Idle; 
            isDetected = IEnemyBehavior.playerDistanceState.TooFar;
            easyType = IEnemyBehavior.enemyType.Easy.ToString();
            _agent.autoBraking = false; // NavMeshAgent 연속적인 움직임 설정
        } 
    }
    // ============== Object initialization and update ============== // 

    // ============== Enemy state and behavior ============== // 
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
    public void updateBehavior()
    {
        switch(current) 
        {
            case IEnemyBehavior.enemyState.Idle : 
                Idle();
                break;
            case IEnemyBehavior.enemyState.Track : 
                Track();
                break;
            case IEnemyBehavior.enemyState.Fire :
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
    public void calculateDistance() { 
        // 플레이어가 일정 거리 이상 좁혀지면 추적 시작
        distance = Vector3.Distance(player.transform.position, this.transform.position);
        if (distance < detectLimit) {
            isDetected = IEnemyBehavior.playerDistanceState.Within;
        }

        //  플레이어가 탐지 거리 바깥이면 패트롤 시작
        if (distance > detectLimit) isDetected = IEnemyBehavior.playerDistanceState.TooFar;
    }
    // ============== Enemy state and behavior ============== // 


    // ============== IEnemyBehavior implementation ============== // 
    public void Idle() 
    {
        Patrol();
    }

    public void Track() 
    {
        _agent.destination = player.transform.position;
    }

    public void Fire() 
    {
        // do nothing
    }

    public void Die() 
    {
        Destroy(this.gameObject);
    }

    public void Patrol()
    {
        // AgentRoutes: empty 게임 오브젝트로 맵 내 이동 포인트 지정
        _agent.destination = AgentRoutes[routeIndex].transform.position;

        // 이동 포인트 남은 거리 0.5f 미만일시 다음 이동 포인트로 이동
        if (!_agent.pathPending && _agent.remainingDistance < 0.5f)
        {
            routeIndex = (routeIndex+1)%AgentRoutes.Length;
        }
    }

    public IEnemyBehavior.enemyState GetEnemyState()
    {
        return current;
    } 
    // ============== IEnemyBehavior implementation ============== // 
}
