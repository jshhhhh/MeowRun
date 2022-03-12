using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SetNickname : MonoBehaviour
{
    public Button runButton;
    // FIX : can't drag and drop input field
    public TMP_InputField nicknameInput;    

    void Start()
    {

        // 게임 시작시 버튼 이벤트 리스닝 시작
        runButton.onClick.AddListener(handleClick);
    }

    // 버튼 클릭 리스너
    public void handleClick() 
    {
        print($"button clicked : {nicknameInput.text}");
    }

}
