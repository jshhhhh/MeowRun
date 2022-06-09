using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//logic flow
//1. CharacterSelection에서 선택된 플레이어의 이름을 Game Manager에서 가져옴
//2. 일치하는 자식 오브젝트의 캐릭터 프리팹 활성화(선택한 캐릭터 플레이)
public class ActiveCharacters : MonoBehaviour
{
    [SerializeField] GameObject[] characters;
    [SerializeField] GameManager gameManager;

    //Awake로 우선순위를 제일 높게 잡음
    //여기서 활성화된 Player 프리팹을 기준으로 다른 스크립트에서 FindObjectOfType을 사용하기 때문
    void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
        characters = new GameObject[this.transform.childCount];

        for (int i = 0; i < characters.Length; i++)
        {
            characters[i] = this.transform.GetChild(i).gameObject;
            if(characters[i].name == gameManager.selectedCaracterName)
            {
                characters[i].SetActive(true);
            }
        }
    }
}
