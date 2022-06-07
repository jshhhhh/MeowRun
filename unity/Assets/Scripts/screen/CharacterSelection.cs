using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

// logic flow
// 1. 모든 캐릭터 disable
// 2. 현재 인덱스 캐릭터 enable
// 3. 특정 위치에 캐릭터 display
public class CharacterSelection : MonoBehaviour
{
    [SerializeField] GameObject[] characters;
    [SerializeField] GameObject spawnPoint; // character generation point
    //[SerializeField] Transform rotationPoint;
    [SerializeField] float rotationY;
    [SerializeField] WaitForSeconds changeCycle = new WaitForSeconds(3f);
    [SerializeField] TextMeshProUGUI label; // character name label
    [SerializeField] int currentIndex = 0;
    [SerializeField] string characterName;
    //[SerializeField] bool stopCharacterRotate = false;

    // =============== Init setup =============== //
    void Start()
    {
        loadObject();

        spawnPoint.transform.eulerAngles = new Vector3(0, rotationY, 0);
        StartCoroutine(changeRotateY());

        switchAllCharactersComponents(false);
        disableAllCharacters();
        characters[0].gameObject.SetActive(true); // set first character as default
        setCharacterName();
        spawnCharacterOnPoint(currentIndex);
    }

    private void loadObject()
    {
        spawnPoint = GameObject.Find("spawnPoint");
        label = GameObject.Find("label").GetComponent<TextMeshProUGUI>();
        characters = new GameObject[transform.childCount];
        for (int i = 0; i < characters.Length; i++)
        {
            characters[i] = this.transform.GetChild(i).gameObject;
        }
    }
    // =============== Init setup =============== //

    // =============== Characters sliders =============== //
    public void showNextCharacter()
    {
        disableAllCharacters();

        // if within range, enable characters
        if (currentIndex+1 < characters.Length) {
            characters[currentIndex+1].gameObject.SetActive(true);
            spawnCharacterOnPoint(currentIndex+1);
            currentIndex++;
        } else {
            currentIndex = 0;
            characters[currentIndex].gameObject.SetActive(true);
            spawnCharacterOnPoint(currentIndex);
        }
        setCharacterName();
    } 

    public void showPrevCharacter()
    {
        disableAllCharacters();
        // if within range, enable characters
        if (currentIndex > 0) {
            characters[currentIndex-1].gameObject.SetActive(true);
            spawnCharacterOnPoint(currentIndex-1);
            currentIndex--;
        } else {
            currentIndex = characters.Length-1;
            characters[currentIndex].gameObject.SetActive(true);
            spawnCharacterOnPoint(currentIndex);
        }
        setCharacterName();
    }

    private void disableAllCharacters()
    {
        for (int i=0; i<characters.Length; i++) {
            characters[i].gameObject.SetActive(false);
        }
    }

    private void switchAllCharactersComponents(bool _bool)
    {
        foreach(GameObject characters in characters)
        {
            characters.gameObject.GetComponent<Player>().enabled = _bool;
            characters.gameObject.GetComponent<Rigidbody>().isKinematic = _bool;
            characters.gameObject.GetComponent<Rigidbody>().detectCollisions = _bool;
        }
    }

    private string getCharacterName()
    {
        characterName = characters[currentIndex].gameObject.name;
        return characterName;
    }

    private void setCharacterName()
    {
        label.SetText(getCharacterName(), true);
    }

    private void spawnCharacterOnPoint(int activeIndex)
    {
        characters[activeIndex].gameObject.transform.position = new Vector3(
                spawnPoint.transform.position.x, 
                spawnPoint.transform.position.y,
                spawnPoint.transform.position.z);
    }

    private void rotateCharacters() 
    {
        this.transform.rotation = Quaternion.Lerp(this.transform.rotation, spawnPoint.transform.rotation, Time.deltaTime);
    }

    IEnumerator changeRotateY()
    {
        while(true)
        {
            yield return changeCycle;
            rotationY *= -1f;
            spawnPoint.transform.eulerAngles = new Vector3(0, rotationY, 0);
        }
    }

    // =============== Characters sliders =============== //

    // =============== Button logics =============== //
    // TO DO: add start game on click
    public void StartGame() 
    {
        switchAllCharactersComponents(true);
        int currentSceneBuildindex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(
            SceneManager.GetSceneByBuildIndex(currentIndex+1).name
        );
        print($"next scene: {SceneManager.GetSceneByBuildIndex(currentIndex+1).name}");
    }
    // =============== Button logics =============== //

    void Update()
    {
        rotateCharacters();
    }
}
