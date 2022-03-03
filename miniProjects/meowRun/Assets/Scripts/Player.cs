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
    public float speed = 3f; //public으로 유니티 에디터에서 스피드 변수 조정 가능
    public float jumpPower = 6f; //public으로 유니티 에디터에서 점프 변수 조정 가능
    public bool canJump = true;
    //키보드 조작 가능 여부
    private bool canMove = true;
    //플레이어 좌표값 고정 여부
    private bool stopPosition = false;
    //플레이어 좌표를 고정하기 위한 임시 위치값
    private Vector3 temp;

    public float heightFromHit;
    public string tagOfHit;
    public float minHeight = 100f;

    //상태로써 활용할 방법 생각 중
    public enum playerState
    {
        Idle,
        Move,
        Die
    }
    playerState _state = playerState.Idle;

    void Start()
    {
        print("Game started"); // UnityEngine.Debug.Log => print(same but shorter)

        animator = GetComponent<Animator>();
        playerRigidbody = this.GetComponent<Rigidbody>(); // 게임 시작 시 캐릭터 선택
    }

    void Update()
    {
        //Ray를 시각적으로 표시
        Debug.DrawRay(transform.position, -transform.up * 0.5f, Color.red);

        RaycastHit hit;

        //Ray와 닿는 오브젝트의 태그 추출
        if (Physics.Raycast(transform.position, -transform.up, out hit, 0.1f))
            tagOfHit = hit.collider.tag;
        else
            tagOfHit = null;

        //태그가 Floor(바닥)이면
        if (tagOfHit == "Floor")
        {
            //플레이어와 물체의 거리인 heightFromHit 계산
            heightFromHit = Vector3.Distance(transform.position, hit.point);
            if (minHeight >= heightFromHit)
                minHeight = heightFromHit;
        }
        else
        {
            animator.SetBool("playerJump", true);
            heightFromHit = 0f;
        }

        //캐릭터 회전값 고정(뒤집어지지 않게) -> 인스펙터창에서 고정했으므로 지금은 필요X
        //transform.eulerAngles = new Vector3(transform.rotation.x, 90.0f, transform.rotation.z);

        if (canMove)
        {
            if (Input.GetKeyDown(KeyCode.Space) && canJump)
            {
                animator.SetBool("playerJump", true);
                canJump = false;
                Jump();
            }

            //FIX : 게임 방식 변경에 따라 카메라 각도에 맞게 움직임 변경
            movePlayer();
        }

        if (stopPosition)
            this.transform.position = temp;

        //상태로써 활용할 방법 생각 중
        switch (_state)
        {
            case playerState.Idle:
                //UpdateIdle();
                break;
            case playerState.Move:
                //UpdateMoving();
                break;
            case playerState.Die:
                //UpdateDie();
                break;
        }
    }


    // ================= 플레이어 이동 로직 ================= //
    // 키보드 세팅
    //TO DO : 자동으로 앞으로 가되 w, s키로 앞뒤 움직임 말고 앞으로 가는 속도 조절 필요
    void movePlayer()
    {
        string INPUT_HORIZONTAL = "Horizontal";
        string INPUT_VERTICAL = "Vertical";
        float horizontalInput = Input.GetAxis(INPUT_HORIZONTAL);
        float verticalInput = Input.GetAxis(INPUT_VERTICAL);
        playerRigidbody.velocity = new Vector3(horizontalInput * speed, playerRigidbody.velocity.y, verticalInput * speed);
    }

    // 점프 기능
    void Jump()
    {
        playerRigidbody.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
        animator.SetBool("playerJump", true);
    }
    // ================= 플레이어 이동 로직 ================= //


    // TO DO : 플레이어 이동 로직 <=> 리스폰 로직 서로 다른 스크립트로 분리하기. 
    // ================= 플레이어 리스폰 로직 ================= //
    // 장애물 충돌시 리스폰
    //FIX : 충돌이 유지된다면 매 프레임마다 호출되는 OnCollisionStay 변경
    void OnCollisionStay(Collision collision)
    {
        //플레이어가 바닥을 딛고 서있을 경우(옆으로의 충돌 배제)
        if (collision.gameObject.CompareTag("Floor") && tagOfHit == "Floor")
        {
            animator.SetBool("playerJump", false);
            canJump = true;
        }
        //바닥과 닿았지만 착지하지 않았을 때
        else if(collision.gameObject.CompareTag("Floor") && tagOfHit == null)
        {
            animator.SetBool("playerJump", true);
            canJump = false;
        }

        //print("on collision executed");
        if (collision.gameObject.CompareTag("Enemies"))
        {
            //충돌했을 때의 위치값 저장
            temp = transform.position;
            StartCoroutine(playerDieCoroutine());
        }

        // 오브젝트 태그가 'Enemies'일 경우 scene 리로드
    }

    //TO DO : 게임오버의 조건 설정, playerDieCoroutine과 게임오버 기능(씬 로드 포함)을 GameManager 스크립트로 분리
    IEnumerator playerDieCoroutine()
    {
        stopPosition = true;
        canMove = false;
        this.GetComponent<BoxCollider>().enabled = false;
        playerRigidbody.useGravity = false;

        //플레이어가 쓰러지는 애니메이션 재생
        animator.SetBool("playerDie", true);

        //1초 대기
        yield return new WaitForSeconds(1f);

        //플레이어 오브젝트가 재생성되면서 초기값인 false로 바뀌므로 변경 불필요
        //stopPosition = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // To do : 인공지능 적군 추가할 것.
    // ================= 플레이어 리스폰 로직 ================= //
}
