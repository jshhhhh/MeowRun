using System.Transactions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    //애니메이터 컴포넌트의 레퍼런스 가져와 저장
    private Animator animator;
    private Rigidbody playerRigidbody;
    private AudioManager AM;
    private SoundManager SM;
    public AudioClip jumpSound;
    public AudioClip dieSound;
    public AudioClip respawnSound;
    public float speed = 3f; //public으로 유니티 에디터에서 스피드 변수 조정 가능
    public float jumpPower = 6f; //public으로 유니티 에디터에서 점프 변수 조정 가능
    public bool canJump = true;
    //플레이어 좌표값 고정 여부
    private bool stopPosition = false;
    //플레이어 좌표를 고정하기 위한 임시 위치값
    private Vector3 temp;
    //플레이어의 발에 닿은 오브젝트의 태그
    public string tagOfFooting;

    //플레이어의 속도
    public float velocity;
    //상태를 체크하기 위한 사실상 정지 상태의 속도
    private float stoppedVelocity = 0.0001f;
    //현재 위치값과 1프레임 뒤의 위치값을 비교하기 위한 변수
    private Vector3 lastPosition;

    //태그명 const로 대체(오타 방지)
    private const string FLOOR = "Floor", ENEMIES = "Enemies";

    //플레이어의 상태
    public enum playerState
    {
        Idle,
        Move,
        //Die는 게임오버 조건에 따라 수정할 예정
        //일단은 기존 방식인 코루틴으로
        Die
    }
    public playerState _state = playerState.Idle;

    void Start()
    {
        print("Game started"); // UnityEngine.Debug.Log => print(same but shorter)
        print($"current player location : {this.transform.position}");
        animator = GetComponent<Animator>();
        playerRigidbody = this.GetComponent<Rigidbody>(); // 게임 시작 시 캐릭터 선택
        AM = FindObjectOfType<AudioManager>();
        SM = FindObjectOfType<SoundManager>();

        lastPosition = transform.position;
    }

    //일정한 주기로 호출되는 함수(물리 계산 관련 함수에 사용됨)
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
        //딛고 있는 오브젝트의 태그 추출
        tagOfFooting = returnTagOfFooting();

        if (stopPosition)
            this.transform.position = temp;

        //상태에 따라 다른 Update문 호출
        switch (_state)
        {
            case playerState.Idle:
                UpdateIdle();
                break;
            case playerState.Move:
                UpdateMove();
                break;
            case playerState.Die:
                //UpdateDie();
                break;
        }
    }

    //유휴 상태
    void UpdateIdle()
    {
        //디폴트 애니메이션 재생(다른 애니메이션 끔)
        animator.SetBool("playerWalk", false);
        animator.SetBool("playerJump", false);

        playerMove();

        if (Input.GetKeyDown(KeyCode.Space) && canJump)
            playerJump();

        //속도가 있으면 Move 상태
        if (velocity >= stoppedVelocity)
            _state = playerState.Move;
    }

    //움직이는 상태
    void UpdateMove()
    {
        //점프와 걷기 애니메이션 컨트롤
        if(canJump)
        {
            animator.SetBool("playerWalk", true);
            animator.SetBool("playerJump", false);
        }
        else
        {
            animator.SetBool("playerWalk", false);
            animator.SetBool("playerJump", true);
        }

        playerMove();

        //바닥을 딛고 있으면(또는 착지하면)
        if (tagOfFooting == FLOOR)
        {
            //점프 가능
            canJump = true;
            //속도가 0이면 Idle 상태
            if (velocity < stoppedVelocity)
                _state = playerState.Idle;
        }
        //바닥을 딛고 있지 않으면 점프 불가능
        else
            canJump = false;

        if (Input.GetKeyDown(KeyCode.Space) && canJump)
        {
            playerJump();
        }
    }

    void UpdateDie()
    {

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
            new Vector3(Input.GetAxis("Horizontal") * speed, playerRigidbody.velocity.y, Input.GetAxis("Vertical") * speed);
    }

    // 점프(점프할 때 한 번만 호출)
    void playerJump()
    {
        //AM.Play("jumpSound");
        SM.RandomizeSfx(jumpSound);
        playerRigidbody.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
    }
    // ================= 플레이어 이동 로직 ================= //


    // TO DO : 플레이어 이동 로직 <=> 리스폰 로직 서로 다른 스크립트로 분리하기. 
    // ================= 플레이어 리스폰 로직 ================= //
    // 장애물 충돌시 리스폰
    // 오브젝트 태그가 'Enemies'일 경우 scene 리로드
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag(ENEMIES))
        {
            print("적과 충돌함");
            StartCoroutine(playerDieCoroutine());
        }
    }

    //TO DO : 게임오버의 조건 설정, playerDieCoroutine과 게임오버 기능(씬 로드 포함)을 GameManager 스크립트로 분리
    IEnumerator playerDieCoroutine()
    {
        //충돌했을 때의 위치값 저장
        temp = transform.position;
        //AM.Play("dieSound");
        SM.PlaySingle(dieSound);
        stopPosition = true;
        this.GetComponent<BoxCollider>().enabled = false;
        playerRigidbody.useGravity = false;
        //플레이어가 쓰러지는 애니메이션 재생
        animator.SetBool("playerDie", true);

        //1초 대기(애니메이션이 끝날 때까지)
        yield return new WaitForSeconds(1f);

        //AM.Play("respawnSound");
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