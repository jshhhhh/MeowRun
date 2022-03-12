using System.Transactions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/*
Player logic flow
1. w,s,space키로 좌우 이동 및 점프 기능
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
    private SoundManager SM;
    public AudioClip jumpSound;
    public AudioClip dieSound;
    public AudioClip damagedSound;
    public AudioClip respawnSound;
    public float speed = 3f; //public으로 유니티 에디터에서 스피드 변수 조정 가능
    public float jumpPower = 6f; //public으로 유니티 에디터에서 점프 변수 조정 가능
    private bool canJump = true;
    //데미지를 입을 수 있는 상태
    private bool canDamaged = true;
    private bool playerDied = false;
    //true가 되는 순간의 좌표를 저장하여 플레이어를 고정시킴
    private bool stopPosition = false;
    //플레이어 좌표를 고정하기 위한 임시 위치값
    private Vector3 temp;

    //플레이어의 발에 닿은 오브젝트의 태그
    public string tagOfFooting;

    // //플레이어의 속도
    // public float velocity;
    // //상태를 체크하기 위한 사실상 정지 상태의 속도
    // private float stoppedVelocity = 0.0001f;
    // //현재 위치값과 1프레임 뒤의 위치값을 비교하기 위한 변수
    // private Vector3 lastPosition;

    //태그명 const로 대체(오타 방지)
    private const string FLOOR = "Floor", ENEMIES = "Enemies";

    //플레이어의 상태
    //상태에 따라 플레이어의 행동 구분
    public enum playerState
    {
        Idle,
        Move,
        Jump,
        Damaged,
        Die
    }
    public playerState _state = playerState.Idle;

    void Start()
    {
        print("Game started"); // UnityEngine.Debug.Log => print(same but shorter)

        animator = GetComponent<Animator>();
        playerRigidbody = this.GetComponent<Rigidbody>();
        SM = FindObjectOfType<SoundManager>();

        StartCoroutine(startMoveCoroutine());

        //lastPosition = transform.position;
    }

    //게임 시작 전 딜레이를 주는 코루틴
    IEnumerator startMoveCoroutine()
    {
        yield return new WaitForSeconds(2f);
        _state = playerState.Move;
    }

    // //일정한 주기로 호출되는 함수(물리 계산 관련 함수에 사용됨)
    // void FixedUpdate()
    // {
    //     //속력을 구하는 공식
    //     //멈춰 있을 때 수치가 0으로 표시되지 않는 경우가 있음(소수점 5자리(ex: 1.192093e-05) 등으로 표시됨)
    //     //stoppedVelocity와 비교하여 움직임을 판단
    //     velocity = (((transform.position - lastPosition).magnitude) / Time.deltaTime);
    //     lastPosition = transform.position;
    // }

    void Update()
    {
        //딛고 있는 오브젝트의 태그 추출
        tagOfFooting = returnTagOfFooting();

        //상태에 따라 다른 Update문 호출
        switch (_state)
        {
            case playerState.Idle:
                UpdateIdle();
                break;
            case playerState.Move:
                UpdateMove();
                break;
            case playerState.Jump:
                UpdateJump();
                break;
            case playerState.Damaged:
                UpdateDamaged();
                break;
            case playerState.Die:
                UpdateDie();
                break;
        }

        //true가 되는 순간의 좌표를 저장하여 플레이어를 고정시킴
        if (stopPosition)
            this.transform.position = temp;
    }

    //서 있는 상태
    //Idle 애니메이션만 재생됨
    void UpdateIdle()
    {
        animator.SetInteger("animationState", 0);
    }

    //움직이는 상태
    //움직이거나 점프할 수 있음
    void UpdateMove()
    {
        animator.SetInteger("animationState", 1);

        canJump = true;

        playerMove();

        //상태 전환 조건
        //바닥을 딛고 있지 않으면 Jump 상태로(낙하 포함)
        if (tagOfFooting != FLOOR)
            _state = playerState.Jump;

        if (Input.GetKeyDown(KeyCode.Space) && canJump)
            playerJump();
    }

    //점프 또는 떨어지는 상태
    //움직일 수는 있지만 점프할 수 없음
    void UpdateJump()
    {
        animator.SetInteger("animationState", 2);

        canJump = false;

        playerMove();

        //상태 전환 조건
        //바닥을 딛고 있으면 Moving 상태로
        if (tagOfFooting == FLOOR)
            _state = playerState.Move;
    }

    //데미지를 입고 있는 상태
    void UpdateDamaged()
    {
        animator.SetInteger("animationState", 3);

        if(canDamaged)
            StartCoroutine(playerDamagedCoroutine());

        canJump = false;
        canDamaged = false;
    }

    IEnumerator playerDamagedCoroutine()
    {
        //충돌했을 때의 위치값 저장 후 플레이어를 정지시킴
        temp = transform.position;
        stopPosition = true;
        playerRigidbody.useGravity = false;

        SM.PlaySingle(damagedSound);

        //Game manager에서 체력 감소 구현 필요

        yield return new WaitForSeconds(1f);
        
        //꺼놓은 중력을 다시 켜고 정지 해제
        playerRigidbody.useGravity = true;
        stopPosition = false;

        //상태 전환 조건
        //바닥을 딛고 있으면 Move, 아니면 Jump
        if (tagOfFooting == FLOOR)
            _state = playerState.Move;
        else if (tagOfFooting != FLOOR)
            _state = playerState.Jump;

        canDamaged = true;
    }
    
    //죽는 과정의 상태
    void UpdateDie()
    {
        if (!playerDied)
            StartCoroutine(playerDieCoroutine());
    }

    //플레이어의 발에 닿은 오브젝트의 태그 추출
    private string returnTagOfFooting()
    {
        //평평한 바닥과의 최소 높이(에디터 상에서 노가다로 대략적인 수치 구함)
        //내리막에서나 오르막 발판에서의 수치는 아직 불확실
        float minHeightFromHit = 0.08f;
        string _tagOfFooting;

        //Ray를 시각적으로 표시
        Debug.DrawRay(transform.position, -transform.up * minHeightFromHit, Color.red);

        RaycastHit hit;

        //Ray를 바닥 방향(-transform.up)으로 minHeightFromHit만큼 짧게 쏘아서 닿는 오브젝트의 태그 추출
        if (Physics.Raycast(transform.position, -transform.up, out hit, minHeightFromHit))
            _tagOfFooting = hit.collider.tag;
        else
            _tagOfFooting = null;

        return _tagOfFooting;
    }


    // ================= 플레이어 이동 로직 ================= //
    // 키보드 세팅
    //TO DO : 자동으로 앞으로 가되 w, s키로 앞뒤 움직임 말고 앞으로 가는 속도 조절 필요
    void playerMove()
    {
        playerRigidbody.velocity =
            //new Vector3(Input.GetAxis("Horizontal") * speed, playerRigidbody.velocity.y, Input.GetAxis("Vertical") * speed);
            new Vector3(Input.GetAxis("Horizontal") * speed, playerRigidbody.velocity.y, speed);
    }

    // 점프(점프할 때 한 번만 호출)
    void playerJump()
    {
        SM.RandomizeSfx(jumpSound);
        playerRigidbody.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
    }
    // ================= 플레이어 이동 로직 ================= //


    // TO DO : 플레이어 이동 로직 <=> 리스폰 로직 서로 다른 스크립트로 분리하기. 
    // ================= 플레이어 리스폰 로직 ================= //
    // 장애물 충돌시 리스폰
    // 오브젝트 태그가 'Enemies'일 경우 Damaged 상태로
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag(ENEMIES))
        {
            if(canDamaged)
            {
                print("적과 충돌함");
                _state = playerState.Damaged;
            }
        }
    }

    //TO DO : 게임오버의 조건 설정, playerDieCoroutine과 게임오버 기능(씬 로드 포함)을 GameManager 스크립트로 분리
    IEnumerator playerDieCoroutine()
    {
        //충돌했을 때의 위치값 저장
        temp = transform.position;
        stopPosition = true;
        this.GetComponent<BoxCollider>().enabled = false;
        playerRigidbody.useGravity = false;

        SM.PlaySingle(dieSound);
        //플레이어가 쓰러지는 애니메이션 재생
        animator.SetInteger("animationState", 4);

        yield return new WaitForSeconds(1f);

        SM.PlaySingle(respawnSound);

        //오디오가 끝날 때까지 대기
        yield return new WaitUntil(() => !SM.efxSource.isPlaying);

        //플레이어 오브젝트가 재생성되면서 초기값인 false로 바뀌므로 변경 불필요
        //stopPosition = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // To do : 인공지능 적군 추가할 것.
    // ================= 플레이어 리스폰 로직 ================= //
}