using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class coin : Item
{
    //점수 올라감
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        soundManager = FindObjectOfType<SoundManager>();
        score = FindObjectOfType<score>();
    }

    protected override void itemEffect()
    {
        soundManager.RandomizeSfx(SItemStart);

        gameManager.addScore(1);
        score.updateScore();
    }
}