using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndButton : MonoBehaviour
{
    public void EndGame()
    {
        UnityEngine.Debug.Log("Game ended");
        // this will never file in Unity play mode, only works when deployed.
        Application.Quit();
    }
}
