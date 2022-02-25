using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiateBalls : MonoBehaviour
{
    public GameObject target; // 3D object sphere
    public GameObject ballCreator; // 공 생성 위치
    public int height = 10; // 공 생성 이후 y축 위치 변수
    public int counter = 0; // 공 생성 제한 변수
     float invokeTime = 2; // 초기 함수 호출 텀
     float repeatTime = 0.5f; // 호출 텀 재설정 
    void Awake()
    {
        print("spawnBall executed");
        InvokeRepeating("spawnBalls", invokeTime, repeatTime);
    }

    void spawnBalls() 
    {
        if (counter < 10) // 공 생성 제한 조건
        {
            // 특정 위치에서 장애물 공 생성
            Instantiate(
            target, 
            new Vector3(
                // FIX : x, z 좌표 위치 수정
                ballCreator.transform.position.x, 
                ballCreator.transform.position.y + height, 
                ballCreator.transform.position.z 
            ), 
            Quaternion.identity);
            counter++; // 카운터 증가
        }
    }

    void setAxis()
    {
        // 랜덤 x,z 좌표 생성 로직 추가 
    }

}
