using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CharacterSelection : MonoBehaviour
{
    
    [SerializeField] GameObject[] characters;
    [SerializeField] int currentIndex = 0;
    [SerializeField] Sprite unclicked, clicked; // 2D graphic button images
    [SerializeField] Image _image; // play button image object

    private bool isClicked = false;

    // =============== Characters sliders =============== //
    void Start()
    {
        disableAllCharacters();
        characters[0].gameObject.SetActive(true);
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
    // =============== Characters sliders =============== //

    // =============== Button logics =============== //

    // TO DO: add start game on click
    public void StartGame() 
    {
        int currentSceneBuildindex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(
            SceneManager.GetSceneByBuildIndex(currentIndex+1).name
        );
        print($"next scene: {SceneManager.GetSceneByBuildIndex(currentIndex+1).name}");
    
    }

    public void OnMouseDown()
    {
        print("button down");
        _image.sprite = clicked;
        isClicked = true;
    }

    // FIX: on mouse up not wokring
    public void OnMouseUp()
    {
        _image.sprite = unclicked;
    }
    public void ResetMouseDown()
    {
        print("button up");
        _image.sprite = unclicked;
    }
    // =============== Button logics =============== //

}
