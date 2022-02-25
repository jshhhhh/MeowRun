using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //애니메이터 컴포넌트의 레퍼런스 가져와 저장
    private Animator animator;

    private Transform _transform;
    public bool _isJumping;
    //오브젝트의 초기 높이
    private float _posY;
    //중력가속도
    private float _gravity;
    //점프력
    private float _jumpPower;
    //점프 이후 경과시간
    private float _jumpTime;

    private float xMove, zMove;
    private float speed = 3f;

    void Start()
    {
        UnityEngine.Debug.Log("Game started");

        animator = GetComponent<Animator>();

        _transform = transform;
        _isJumping = false;
        _posY = transform.position.y;
        _gravity = 9.8f;
        _jumpPower = 5.0f;
        _jumpTime = 0.0f;
    }

    // 키보드 컨트롤 삽입

    void Update()
    {
        //캐릭터 회전값 고정(뒤집어지지 않게)
        transform.eulerAngles = new Vector3(transform.rotation.x, 90.0f, transform.rotation.z);


        xMove = 0;
        zMove = 0;

        if (Input.GetKeyDown(KeyCode.Space) && !_isJumping)
        {
            animator.SetBool("playerJump", true);
            _isJumping = true;
            _posY = _transform.position.y;
        }
 
        if (_isJumping)
        {
            Jump();
        }

        //키보드 A, D 기능(좌, 우 이동)
        if(Input.GetKey(KeyCode.A))
        {
            zMove = -speed *Time.deltaTime;
        }
        else if(Input.GetKey(KeyCode.D))
        {
            zMove = speed *Time.deltaTime;
        }
        this.transform.Translate(new Vector3(0, 0, zMove));
    }

    void Jump()
    {
        //y=-a*x+b에서 (a: 중력가속도, b: 초기 점프속도)
        //적분하여 y = (-a/2)*x*x + (b*x) 공식을 얻는다.(x: 점프시간, y: 오브젝트의 높이)
        //변화된 높이 height를 기존 높이 _posY에 더한다.
        float height = (_jumpTime * _jumpTime * (-_gravity) / 2) + (_jumpTime * _jumpPower);
        _transform.position = new Vector3(_transform.position.x, _posY + height, _transform.position.z);
        //점프시간을 증가시킨다.
        _jumpTime += Time.deltaTime;
 
        //처음의 높이 보다 더 내려 갔을때 => 점프전 상태로 복귀한다.
        if (height < 0.0f)
        {
            animator.SetBool("playerJump", false);
            _isJumping = false;
            _jumpTime = 0.0f;
            _transform.position = new Vector3(_transform.position.x, _posY, _transform.position.z);
        }
    }
}
