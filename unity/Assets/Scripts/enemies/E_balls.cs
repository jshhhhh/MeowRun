using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Object type enemy는 IEnemyBehavior를 상속하지 않음.
public class E_balls : MonoBehaviour
{
    // ==================== 변수 세팅 ==================== //
    private string objectType;
    public GameObject target; // 3D object sphere
    public GameObject ballCreator; // 공 생성 위치
    public int height = 10; // 공 생성 이후 y축 위치 변수
    public int counter = 0; // 공 생성 제한 변수
    public float invokeTime = 2; // 초기 함수 호출 텀
    public float repeatTime = 0.5f; // 호출 텀 재설정 
    // 난수 생성 범위
    public float coverage = 2; 

    // ==================== 변수 세팅 ==================== //


    // ============== Object initialization and update ============== // 
    void Awake()
    {
        print("spawnBall executed");
        InitSetup();
        InvokeRepeating("spawnBalls", invokeTime, repeatTime);
    }
    void InitSetup() 
    {
        objectType = IEnemyBehavior.enemyType.Object.ToString();
    }
    // ============== Object initialization and update ============== // 

    void spawnBalls() 
    {
        if (counter < 10) // 공 생성 제한 조건
        {
            // 특정 위치에서 장애물 공 생성
            Instantiate(
            target, 
            new Vector3(
                // coverage : 값이 동일하지 않게 세팅할 것
                ballCreator.transform.position.x + setRandomAxis(-coverage, coverage),
                ballCreator.transform.position.y + height, 
                ballCreator.transform.position.z + setRandomAxis(-coverage, coverage)
            ), 
            Quaternion.identity);
            counter++; // 카운터 증가
        }
    }

    float setRandomAxis(float _min, float _max)
    {
        // 랜덤 x,z 좌표 생성 로직 추가 
        float axis = Random.Range(_min, _max); 
        return axis;
    }

}
