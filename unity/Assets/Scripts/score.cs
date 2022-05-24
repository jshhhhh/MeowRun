using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class score : MonoBehaviour
{
    //Canvas의 Score를 컨트롤함
    private TextMeshProUGUI textMesh;
    private GameManager gameManager;

    void Start()
    {
        textMesh = GetComponent<TextMeshProUGUI>();
        gameManager = FindObjectOfType<GameManager>();

        updateScore();
    }

    //Score를 갱신하는 함수
    //점수를 얻는 상황(coin 획득 등)의 스크립트에서 호출
    public void updateScore()
    {
        //ToString("D4"): 10진수 4자리로 형변환
        textMesh.text = "SCORE : " + gameManager.checkScore().ToString("D4");
    }
}
