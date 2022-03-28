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

    private SoundManager SM;
    public AudioClip SJump;
    public AudioClip SDie;
    public AudioClip SDamaged;
    public AudioClip SRespawn;

    public float speed = 3.5f; //public으로 유니티 에디터에서 스피드 변수 조정 가능
    public float jumpPower = 6f; //public으로 유니티 에디터에서 점프 변수 조정 가능
    public bool canJump = true;
    //데미지를 입을 수 있는 상태
    private bool canDamaged = true;
    private bool playerDied = false;
    //true가 되는 순간의 좌표를 저장하여 플레이어를 고정시킴
    public bool stopPosition = false;
    //플레이어 좌표를 고정하기 위한 임시 위치값
    private Vector3 tempPosition;
    private Quaternion tempRotation;

    private RaycastHit hit;
    //ray의 길이
    private float lengthOfRay = 0.1f;
    //BoxCast의 크기
    private Vector3 localScale = new Vector3(0.35f, 0.01f, 0.35f);
    //ray가 물체에 닿았는지의 여부
    private bool isHit;
    //플레이어의 발에 닿은 오브젝트의 태그
    public string tagOfFooting;

    //플레이어의 속도
    public float velocity;
    //상태를 체크하기 위한 사실상 정지 상태의 속도
    private float zeroVelocity = 0.01f;
    //현재 위치값과 1프레임 뒤의 위치값을 비교하기 위한 변수
    private Vector3 lastPosition;

    //회전값을 계산하기 위한 변수
    public float horizontal = 0f;
    public float vertical = 0f;
    //플레이어의 최종 회전값을 담을 변수
    public float direction = 0f;

    //태그명 const로 대체(오타 방지)
    private const string FLOOR = "Floor", ENEMIES = "Enemies", GAME_OVER = "GameOver", ANIMATION_STATE = "animationState";

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
        playerRigidbody = this.GetComponent<Rigidbody>();
        SM = FindObjectOfType<SoundManager>();

        tempPosition = transform.position;
        tempRotation = transform.rotation;
        lastPosition = transform.position;

        playerForcedStop(true);
        StartCoroutine(startMoveCoroutine());
    }

    //게임 시작 전 딜레이를 주는 코루틴
    IEnumerator startMoveCoroutine()
    {
        yield return new WaitForSeconds(2f);
        playerForcedStop(false);
    }

    //일정한 주기로 호출되는 함수(물리 계산 관련 함수에 사용됨)
    //속력에 따라 Idle 상태 변환
    void FixedUpdate()
    {
        //속력을 구하는 공식
        //멈춰 있을 때 수치가 0으로 표시되지 않는 경우가 있음(소수점 5자리(ex: 1.192093e-05) 등으로 표시됨)
        //stoppedVelocity와 비교하여 움직임을 판단
        velocity = (((transform.position - lastPosition).magnitude) / Time.deltaTime);
        lastPosition = transform.position;
    }

    void Update()
    {
        returnTag();

        preventFlip();

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
        {
            this.transform.position = tempPosition;
            this.transform.rotation = tempRotation;
        }
    }

    //서 있는 상태
    //Idle 애니메이션만 재생됨
    void UpdateIdle()
    {
        /*
        애니메이션 파라미터 제어
        0: Idle, 1: Walk, 2: Jump, 3: Damaged, 4: Die
        Animator의 파라미터 참고
        */
        AnimationSetter(ANIMATION_STATE, 0);

        playerMove();

        if (velocity > zeroVelocity)
            _state = playerState.Move;

        if (Input.GetKeyDown(KeyCode.Space) && canJump)
            playerJump();
    }

    //움직이는 상태
    //움직이거나 점프할 수 있음
    void UpdateMove()
    {
        AnimationSetter(ANIMATION_STATE, 1);

        canJump = true;

        playerMove();

        //상태 전환 조건
        //바닥을 딛고 있지 않으면 Jump 상태로(낙하 포함)
        if (tagOfFooting != FLOOR)
            _state = playerState.Jump;
        //속력이 없으면 Idle 상태로
        else if (velocity <= zeroVelocity)
            _state = playerState.Idle;

        if (Input.GetKeyDown(KeyCode.Space) && canJump)
            playerJump();
    }

    //점프 또는 떨어지는 상태
    //움직일 수는 있지만 점프할 수 없음
    void UpdateJump()
    {
        AnimationSetter(ANIMATION_STATE, 2);

        canJump = false;

        playerMove();

        //공중에서 플레이어 수평으로 보정
        //transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0), Time.deltaTime);

        //상태 전환 조건
        //바닥을 딛고 있으면 Moving 상태로
        if (tagOfFooting == FLOOR)
        {
            if (velocity >= zeroVelocity)
                _state = playerState.Move;
            else
                _state = playerState.Idle;
        }
    }

    //데미지를 입고 있는 상태
    void UpdateDamaged()
    {
        AnimationSetter(ANIMATION_STATE, 3);
        if (canDamaged)
            StartCoroutine(playerDamagedCoroutine());

        canJump = false;
        canDamaged = false;
    }

    IEnumerator playerDamagedCoroutine()
    {
        SM.PlaySingle(SDamaged);
        playerForcedStop(true);

        //Game manager에서 체력 감소 구현 필요
        //Damaged()

        yield return new WaitForSeconds(1f);

        playerForcedStop(false);

        //상태 전환 조건
        //바닥을 딛고 있으면 Move, 아니면 Jump
        if (tagOfFooting == FLOOR)
        {
            if (velocity >= zeroVelocity)
                _state = playerState.Move;
            else
                _state = playerState.Idle;
        }
        else if (tagOfFooting != FLOOR)
            _state = playerState.Jump;
        
        yield return new WaitForSeconds(0.5f);

        canDamaged = true;
    }

    //죽는 상태
    void UpdateDie()
    {
        if (!playerDied)
            StartCoroutine(playerDieCoroutine());

        playerDied = true;
    }

    //애니메이션 파라미터 타입에 따른 제네릭 함수
    //향후 애니메이션이 많아질 경우 여러 파라미터를 함수 하나로 제어 가능
    private void AnimationSetter<T>(string _name, T _condition)
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

    //ray에 닿은 오브젝트의 태그 가져옴
    private void returnTag()
    {
        isHit = Physics.BoxCast(transform.position, localScale, -transform.up, out hit, transform.rotation, lengthOfRay);

        if (isHit)
            tagOfFooting = hit.collider.tag;
        else
            tagOfFooting = null;
    }

    //Gizmos.DrawWireCube로 시각적으로 ray 표시
    //Update에 넣을 필요 없이 바로 동작함
    private void OnDrawGizmos()
    {
        //Ray를 시각적으로 표시
        Gizmos.color = Color.blue;

        //Ray를 바닥 방향(-transform.up)으로 minHeightFromHit만큼 짧게 쏘아서 닿는 오브젝트의 태그 추출
        if (isHit)
        {
            Gizmos.DrawRay(transform.position, -transform.up * hit.distance);
            Gizmos.DrawWireCube(transform.position + -transform.up * hit.distance, localScale);
            tagOfFooting = hit.collider.tag;
        }
        else
        {
            Debug.DrawRay(transform.position, -transform.up * lengthOfRay, Color.red);
            tagOfFooting = null;
        }
    }

    // ================= 플레이어 이동 로직 ================= //
    // 키보드 세팅
    void playerMove()
    {
        playerTurn();

        // Fix : 3d 지형 맵에서는 플레이어 자유도가 높은 게 좋아서 상/하/좌/우 + 카메라 시점
        playerRigidbody.velocity =
            new Vector3(Input.GetAxis("Horizontal") * speed, playerRigidbody.velocity.y, Input.GetAxis("Vertical") * speed);
    }

    // 점프(점프할 때 한 번만 호출)
    void playerJump()
    {
        SM.RandomizeSfx(SJump);
        playerRigidbody.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
    }

    //방향키에 따라 플레이어가 회전하는 함수
    private void playerTurn()
    {
        direction = 0f;

        //각 방향키에 따라 회전각 설정
        if (Input.GetAxisRaw("Horizontal") == 1) horizontal = 90f;
        else if (Input.GetAxisRaw("Horizontal") == -1) horizontal = -90f;
        else horizontal = 0f;
        if (Input.GetAxisRaw("Vertical") == 1) vertical = Mathf.Epsilon;
        else if (Input.GetAxisRaw("Vertical") == -1) vertical = -180f;
        else vertical = 0f;

        //앞뒤와 좌우키가 동시에 눌렸을 경우 회전값 연산
        if (Input.GetAxisRaw("Horizontal") != 0 && Input.GetAxisRaw("Vertical") != 0)
        {
            if (Input.GetAxisRaw("Horizontal") == 1)
                direction = Mathf.Abs((horizontal - vertical) / 2);
            else
                direction = (horizontal + vertical) / 2;
        }
        else
            direction = horizontal + vertical;

        //계산한 회전값을 Lerp 함수로 플레이어에 적용
        if(direction != 0)
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(transform.rotation.x, direction, transform.rotation.z), Time.deltaTime * 50f);

        tempRotation = transform.rotation;
    }

    private void preventFlip()
    {
        // if(transform.rotation.eulerAngles.x > 40f || transform.rotation.eulerAngles.x < -40)
        //     transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, transform.rotation.y, transform.rotation.z), Time.deltaTime * 10f);

        // if(transform.rotation.eulerAngles.z > 40f || transform.rotation.eulerAngles.z < -40)
        //     transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(transform.rotation.x, transform.rotation.y, 0), Time.deltaTime * 10f);

        // transform.rotation
        //     = Quaternion.Euler(Mathf.Clamp(transform.rotation.eulerAngles.x, 0, -0),
        //         transform.rotation.eulerAngles.y,
        //         Mathf.Clamp(transform.rotation.eulerAngles.z, 0, -0));
    }

    //강제로 멈추는 함수
    private void playerForcedStop(bool stop)
    {
        if (stop)
        {
            //충돌했을 때의 위치값 저장
            tempPosition = transform.position;
            tempRotation = transform.rotation;
            stopPosition = true;
            //this.GetComponent<BoxCollider>().enabled = false;
            playerRigidbody.useGravity = false;
        }
        else
        {
            stopPosition = false;
            playerRigidbody.useGravity = true;
        }
    }
    // ================= 플레이어 이동 로직 ================= //


    // TO DO : 플레이어 이동 로직 <=> 리스폰 로직 서로 다른 스크립트로 분리하기. 
    // ================= 플레이어 리스폰 로직 ================= //
    // 오브젝트 태그가 'Enemies'일 경우 Damaged 상태로, 즉사할 경우 Die 상태로
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag(GAME_OVER))
        {
            print("플레이어 사망");
            _state = playerState.Die;
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag(ENEMIES))
        {
            //데미지를 입을 수 있는 상태라면
            if (canDamaged)
            {
                print("적과 충돌함");
                _state = playerState.Damaged;
            }
        }
    }

    //TO DO : 게임오버의 조건 설정, playerDieCoroutine과 게임오버 기능(씬 로드 포함)을 GameManager 스크립트로 분리 필요
    IEnumerator playerDieCoroutine()
    {
        playerForcedStop(true);

        SM.PlaySingle(SDie);
        AnimationSetter(ANIMATION_STATE, 4);

        yield return new WaitForSeconds(1f);

        SM.PlaySingle(SRespawn);

        //오디오가 끝날 때까지 대기
        yield return new WaitUntil(() => !SM.efxSource.isPlaying);

        //플레이어 오브젝트가 재생성되면서 초기값인 false로 바뀌므로 변경 불필요
        //playerForcedStop(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // To do : 인공지능 적군 추가할 것.
    // ================= 플레이어 리스폰 로직 ================= //
}