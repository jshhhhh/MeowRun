using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public CharacterController playerController;
    private SoundManager soundManager;
    public AudioClip SJump, SItemEnd;
    
    public Vector3 moveDir = Vector3.zero;
    //플레이어의 속도
    [field:SerializeField] public float velocity{get; private set;}
    public float zeroVelocity{get;} = 0.01f;

    private const float originalSpeed = 3.5f;
    private float maxSpeed = 10f, minspeed = 1f;
    public float currentSpeed;

    private const float originalJumpPower = 6f;
    private float maxJumpPower = 10f, minJumpPower = 0f;
    private float addJumpPower = 0;
    public float currentJumpPower;    
    public bool canMoreJump { get; private set; } = false;

    private float gravity = 9.81f;
    const string HORIZONTAL = "Horizontal", VERTICAL = "Vertical";
    //회전값을 계산하기 위한 변수
    private float horizontal, vertical;

    //현재 위치값과 1프레임 뒤의 위치값을 비교하기 위한 변수
    private Vector3 lastPosition;

    Jump jump = new Jump(1,2,3,4);

    void Start()
    {
        playerController = GetComponent<CharacterController>();
        soundManager = FindObjectOfType<SoundManager>();

        lastPosition = transform.position;

        currentSpeed = originalSpeed;
        currentJumpPower = originalJumpPower;
    }

    void FixedUpdate()
    {
        velocity = (((transform.position - lastPosition).magnitude) / Time.deltaTime);
        lastPosition = transform.position;

        playerMove();
    }

    void playerMove()
    {
        //바닥에 붙어 있을 때
        if (playerController.isGrounded)
        {
            moveDir = new Vector3(Input.GetAxis(HORIZONTAL), 0, Input.GetAxis(VERTICAL));
            moveDir *= currentSpeed;

            if (Input.GetKeyDown(KeyCode.Space))
                playerJump();
        }
        //공중일 때
        else
        {
            moveDir.x = Input.GetAxis(HORIZONTAL) * currentSpeed;
            moveDir.z = Input.GetAxis(VERTICAL) * currentSpeed;
        }

        moveDir.y -= gravity * Time.deltaTime;

        playerController.Move(moveDir * Time.deltaTime);

        playerTurn();
    }

    void playerJump()
    {
        soundManager.RandomizeSfx(SJump);
        moveDir.y = currentJumpPower;
    }

    //witchHat을 먹으면 witchHat 스크립트에서 호출
    public void moreJump(float _addJumpPower = 0f)
    {
        canMoreJump = true;
        addJumpPower = _addJumpPower;
    }

    public void playerMoreJump()
    {
        soundManager.RandomizeSfx(SJump);
        moveDir.y = currentJumpPower + addJumpPower;
        canMoreJump = false;
    }

    //방향키에 따라 플레이어가 회전하는 함수
    public void playerTurn()
    {
        float ROTATION_SPEED = 10f;

        horizontal = Input.GetAxis(HORIZONTAL);
        vertical = Input.GetAxis(VERTICAL);

        Vector3 direction = new Vector3(horizontal, 0, vertical);

        if (!(horizontal == 0 && vertical == 0))
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(direction), Time.deltaTime * ROTATION_SPEED);
    }

    //이동 속도, 점프력 조절
    //속도 관련 아이템을 먹으면 그 아이템 스크립트에서 호출됨
    //아이템에서 코루틴을 바로 호출하면 아이템 오브젝트가 파괴되면서 코루틴이 멈추기 때문에 Player에서 코루틴 호출
    public void controlSpeed(float _time, float _addSpeed = 0f, float _addJumpPower = 0f)
    {
        StartCoroutine(controlSpeedCoroutine(_time, _addSpeed, _addJumpPower));
    }

    private IEnumerator controlSpeedCoroutine(float _time, float _addSpeed = 0f, float _addJumpPower = 0f)
    {
        currentSpeed += _addSpeed;
        currentJumpPower += _addJumpPower;

        yield return new WaitForSeconds(_time);

        currentSpeed -= _addSpeed;
        currentJumpPower -= _addJumpPower;
        soundManager.RandomizeSfx(SItemEnd);
    }
}
