using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMeowRun : MonoBehaviour
{
    // should be public so that it can be called in editor. 
    public void StartGame()
    {
        print("start button clicked");
        // SceneManager.LoadScene : Loads the Scene by its name or index in Build Settings.
        SceneManager.LoadScene(1); // Scenes are zero-index. 0 => start screen, 1 => first level
    }
}
