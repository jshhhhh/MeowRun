using System.Transactions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/*
Player logic flow
1. a,d,space키로 좌우 이동 및 점프 기능
2. 자동으로 일정한 속도로 앞으로 진행
3. 장애물이나 enemy에 닿으면 체력 감소
4. 체력이 모두 감소하면 게임 오버
5. 플레이어가 점프해서 누를 경우 enemy 죽음
*/
public class Player : MonoBehaviour
{
    //애니메이터 컴포넌트의 레퍼런스 가져와 저장
    private Animator animator;
    private Rigidbody playerRigidbody;
    private GameManager gameManager;
    private PlayerMove playerMove;
    private CharacterController playerController;
    private SoundManager soundManager;
    public AudioClip SDie, SDamaged, SRespawn;
    //데미지를 입을 수 있는 상태
    private bool canDamaged = true;
    private bool playerDied = false;
    //true가 되는 순간의 좌표를 저장하여 플레이어를 고정시킴
    private bool stopPosition = false;
    //플레이어 좌표를 고정하기 위한 임시 위치값
    private Vector3 tempPosition;
    private Quaternion tempRotation;

    //태그명 const로 대체(오타 방지)
    private const string FLOOR = "Floor", ENEMIES = "Enemies", GAME_OVER = "GameOver", ANIMATION_STATE = "animationState";

    //플레이어의 상태
    //상태에 따라 플레이어의 행동 구분
    private enum playerState
    {
        IDLE,
        MOVE,
        JUMP,
        DAMAGED,
        DIE
    }
    private playerState _state = playerState.IDLE;

    //적 타입 선언
    public enum enemyType
    {
        Easy, // (쥐, 개구리)
        Intermediate, // (뱀, 거미)
        Difficult, // (벌, 외계인<2종>)
        Object // (공)
    }

    // 타입 활용
    public string easyEnemyType = enemyType.Easy.ToString();  // "Easy"

    void Start()
    {
        print("Game started"); // UnityEngine.Debug.Log => print(same but shorter)

        animator = GetComponent<Animator>();
        playerMove = GetComponent<PlayerMove>();
        playerController = GetComponent<CharacterController>();

        if (animator == null)
            animator = this.transform.GetChild(0).GetComponent<Animator>();

        soundManager = FindObjectOfType<SoundManager>();
        gameManager = FindObjectOfType<GameManager>();

        tempPosition = transform.position;
        tempRotation = transform.rotation;

        playerForcedStop(true);
        StartCoroutine(delayCoroutine());
    }

    //게임 시작 전 딜레이를 주는 코루틴
    IEnumerator delayCoroutine()
    {
        yield return new WaitForSeconds(2f);
        playerForcedStop(false);
    }

    void Update()
    {
        //상태에 따라 다른 Update문 호출
        switch (_state)
        {
            case playerState.IDLE:
                UpdateIDLE();
                break;
            case playerState.MOVE:
                UpdateMove();
                break;
            case playerState.JUMP:
                UpdateJump();
                break;
            case playerState.DAMAGED:
                UpdateDamaged();
                break;
            case playerState.DIE:
                UpdateDie();
                break;
        }
    }

    private void checkState()
    {
        if(playerController.isGrounded)
            _state = playerMove.velocity > playerMove.zeroVelocity ? playerState.MOVE : playerState.IDLE;
        else    _state = playerState.JUMP;
    }

    //서 있는 상태
    //IDLE 애니메이션만 재생됨
    void UpdateIDLE()
    {
        /*
        애니메이션 파라미터 제어
        0: IDLE, 1: Walk, 2: JUMP, 3: DAMAGED, 4: DIE
        Animator의 파라미터 참고
        */
        AnimationSetter(ANIMATION_STATE, 0);

        checkState();
    }

    //움직이는 상태
    //움직이거나 점프할 수 있음
    void UpdateMove()
    {
        AnimationSetter(ANIMATION_STATE, 1);

        checkState();
    }

    //점프 또는 떨어지는 상태
    //움직일 수는 있지만 점프할 수 없음
    void UpdateJump()
    {
        AnimationSetter(ANIMATION_STATE, 2);

        if (playerMove.canMoreJump)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                playerMove.playerMoreJump();
                animator.Play("PlayerJump", -1, 0f);
            }
        }

        checkState();
    }

    //데미지를 입고 있는 상태
    void UpdateDamaged()
    {
        if (canDamaged)
        {
            canDamaged = false;
            gameManager.decreaseLife();

            //체력이 남아 있다면 데미지 애니메이션 출력
            if(isDead())    _state = playerState.DIE;
            else            StartCoroutine(playerDamagedCoroutine());
        }
    }

    IEnumerator playerDamagedCoroutine()
    {
        AnimationSetter(ANIMATION_STATE, 3);
        soundManager.PlaySingle(SDamaged);
        playerForcedStop(true);

        yield return new WaitForSeconds(1f);

        playerForcedStop(false);

        //상태 전환 조건
        //바닥을 딛고 있으면 MOVE, 아니면 JUMP
        checkState();

        yield return new WaitForSeconds(0.5f);

        canDamaged = true;
    }

    //죽는 상태
    void UpdateDie()
    {
        if (!playerDied) StartCoroutine(playerDieCoroutine());

        playerDied = true;
    }

    //======================================================================

    //애니메이션 파라미터 타입에 따른 제네릭 함수
    //향후 애니메이션이 많아질 경우 여러 파라미터를 함수 하나로 제어 가능
    public void AnimationSetter<T>(string _name, T _condition)
    {
        // 조건 T에 따라 animator.SetBool 또는 animator.SetInteger 실행
        if (_condition.GetType() == typeof(bool))
        {
            // T가 boolean일 경우 => string 변환 => bool 변환
            //print("애니메이션 조건 : bool");
            animator.SetBool(_name, bool.Parse(_condition.ToString()));
        }
        if (_condition.GetType() == typeof(int))
        {
            // T가 int일 경우 => string 변환 => int 변환
            //print("애니메이션 조건 : int");
            animator.SetInteger(_name, (int.Parse(_condition.ToString())));
        }
    }

    //강제로 멈추는 함수
    private void playerForcedStop(bool stop)
    {
        if (stop)   playerMove.enabled = false;
        else        playerMove.enabled = true;
    }

    // ================= 플레이어 리스폰 로직 ================= //
    //오브젝트 태그가 'GameOver'일 경우 즉사(health를 다 깎고 DIE 상태로)
    private void OnControllerColliderHit(ControllerColliderHit _hit)
    {
        if (_hit.gameObject.CompareTag(GAME_OVER))
        {
            gameManager.decreaseLife(gameManager.TotalLife);
        }
        else if (_hit.gameObject.CompareTag(ENEMIES))
        {
            //데미지를 입을 수 있는 상태라면
            if (canDamaged)
            {
                print("적과 충돌함");
                _state = playerState.DAMAGED;
            }
        }
    }

    //체력이 0인지 확인
    public bool isDead()
    {
        return (gameManager.currentLife <= 0) ? true : false;        
    }

    IEnumerator playerDieCoroutine()
    {
        playerForcedStop(true);

        soundManager.PlaySingle(SDie);
        AnimationSetter(ANIMATION_STATE, 4);

        yield return new WaitForSeconds(1f);

        soundManager.PlaySingle(SRespawn);

        //오디오가 끝날 때까지 대기
        yield return new WaitUntil(() => !soundManager.efxSource.isPlaying);
        //현재 실행 중인 애니메이션이 끝날 때까지 대기
        //yield return new WaitUntil(() => animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f);

        gameManager.resetPlayScene();
    }

    // To do : 인공지능 적군 추가할 것.
    // ================= 플레이어 리스폰 로직 ================= //
}