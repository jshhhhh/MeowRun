using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationManager : MonoBehaviour
{
    // 사용법: 특정 스크립트에서 Animator 변수 A 생성
    // AnimationManager.instance = A 로 초기화
    // 특정 스크립트에서 AnimationManager.Setter 호출 후 사용
    public Animator instance;

    public void Setter<T>(string _name, T _condition)
    {
        if (_condition.GetType() == typeof(bool))
        {
            instance.SetBool(_name, bool.Parse(_condition.ToString()));
        }
        if (_condition.GetType() == typeof(int))
        {
            instance.SetInteger(_name, (int.Parse(_condition.ToString())));
        }
        if (_condition.GetType() == typeof(float))
        {
            instance.SetFloat(_name, (float.Parse(_condition.ToString())));
        }
        if (_condition == null)
        {
            instance.SetTrigger(_name);
        }
    }
}
