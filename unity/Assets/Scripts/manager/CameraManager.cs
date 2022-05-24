using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    //카메라가 따라갈 대상
    public GameObject target;
    //카메라의 속도
    public float moveSpeed = 5f;
    //대상의 현재 위치값
    private Vector3 targetPosition;

    //Start 함수보다 더 먼저 실행되는 함수
    // #region Singleton
    // static public CameraManager instance;
    // private void Awake() {
    //     if(instance != null)
    //     {
    //         Destroy(this.gameObject);
    //     }
    //     else
    //     {
    //     //이 오브젝트를 다른 씬을 불러올 때마다 파괴시키지 말라는 명령어
    //         DontDestroyOnLoad(this.gameObject);
    //         instance = this;
    //     }
    // }
    // #endregion

    // Update is called once per frame
    void Update()
    {
        //대상이 존재한다면
        if(target.gameObject != null)
        {
            //대상의 현재 위치값(카메라를 x축으로 조금 더 옮김)
            targetPosition.Set(target.transform.position.x, target.transform.position.y + 4f, target.transform.position.z - 4f);

            //카메라를 움직임(Lerp: A값과 B값까지 t의 속도로 움직임)
            //1초에 moveSpeed만큼 이동
            //Time.deltaTime: 1초에 60프레임이 실행된다면, 60분의 1값을 지님
            this.transform.position = Vector3.Lerp(this.transform.position, targetPosition, moveSpeed * Time.deltaTime);
        }
    }
}
