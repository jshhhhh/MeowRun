using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifeManager : MonoBehaviour
{
    private GameManager gameManager;

    public RawImage[] heartSlot;
    //텍스쳐(이미지)
    public Texture heart, heart_empty;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        heartSlot = this.GetComponentsInChildren<RawImage>();

        //리소스 폴더에서 텍스쳐 불러옴
        heart = Resources.Load("healthBar/heart", typeof(Texture)) as Texture;
        heart_empty = Resources.Load("healthBar/heart_empty", typeof(Texture)) as Texture;
    }

    //life 텍스쳐 업데이트 함수
    //life가 변경될 때마다 GameManager에서 호출됨
    public void updateHeartImage()
    {
        for(int i = 0; i < gameManager.checkLife(); i++)
            heartSlot[i].texture = heart;

        for(int i = gameManager.TotalLife - 1; i > gameManager.checkLife() - 1; i--)
            heartSlot[i].texture = heart_empty;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
