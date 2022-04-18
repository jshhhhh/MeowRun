using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterSelection : MonoBehaviour
{
    
    [SerializeField] GameObject[] characters;
    [SerializeField] int currentIndex = 0;
    [SerializeField] GameObject spawnPoint;
    void Start()
    {
        disableAllCharacters();
    } 
    public void showNextCharacter()
    {
        disableAllCharacters();

        // if within range, enable characters
        if (currentIndex < characters.Length && currentIndex !=0) {
            characters[currentIndex].gameObject.SetActive(true);
            currentIndex++;
        } else {
                currentIndex = 0;
                characters[currentIndex].gameObject.SetActive(true);
                currentIndex++;
        }
    } 

    public void showPrevCharacter()
    {
        disableAllCharacters();
        // if within range, enable characters
        if (currentIndex > 0) {
            characters[currentIndex-1].gameObject.SetActive(true);
            currentIndex--;
        } else {
            currentIndex = characters.Length-1;
            characters[currentIndex].gameObject.SetActive(true);
        }
    }

    private void disableAllCharacters()
    {
        for (int i=0; i<characters.Length; i++) {
            characters[i].gameObject.SetActive(false);
        }
    }

    public void StartGame() 
    {
        int currentSceneBuildindex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(
            SceneManager.GetSceneByBuildIndex(currentIndex+1).name
        );
        print($"next scene: {SceneManager.GetSceneByBuildIndex(currentIndex+1).name}");
    }
}
