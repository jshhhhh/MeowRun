using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

// logic flow
// 1. 모든 캐릭터 disable
// 2. 현재 인덱스 캐릭터 enable
// 3. 특정 위치에 캐릭터 display
// 4. 실시간으로 바뀌는 spawnPoint 회전값에 맞추어 캐릭터 회전
public class CharacterSelection : MonoBehaviour
{
    [SerializeField] GameObject spawnPoint; // character generation point
    [SerializeField] float rotationY;
    [SerializeField] WaitForSeconds changeCycle = new WaitForSeconds(3f);
    [SerializeField] TextMeshProUGUI label; // character name label
    [SerializeField] string characterName;
    [SerializeField] GameManager gameManager;
    // [SerializeField] GameObject button;
    // [SerializeField] Button[] buttons;
    [SerializeField] public GameObject[] characters {get; private set;}
    [SerializeField] public int currentIndex {get; private set;} = 0;
    [SerializeField] public AsyncOperation nextScene;

    // =============== Init setup =============== //
    void Start()
    {
        loadObjects();

        for (int i = 0; i < characters.Length; i++)
        {
            switchCharactersComponents(i, false);
        }

        spawnPoint.transform.eulerAngles = new Vector3(0, rotationY, 0);
        StartCoroutine(changeRotateY());

        disableAllCharacters();
        characters[0].gameObject.SetActive(true); // set first character as default
        setCharacterName();
        spawnCharacterOnPoint(currentIndex);
    }

    private void loadObjects()
    {
        gameManager = FindObjectOfType<GameManager>();
        spawnPoint = GameObject.Find("spawnPoint");
        label = GameObject.Find("label").GetComponent<TextMeshProUGUI>();

        characters = new GameObject[transform.childCount];
        for (int i = 0; i < characters.Length; i++)
        {
            characters[i] = this.transform.GetChild(i).gameObject;
        }

        // button = GameObject.Find("Canvas/buttons");
        // buttons = new Button[button.transform.childCount];
        // for (int i = 0; i < buttons.Length; i++)
        // {
        //     buttons[i] = button.transform.GetChild(i).GetComponent<Button>();
        // }
    }
    // =============== Init setup =============== //

    // =============== Characters sliders =============== //
    public void showNextCharacter()
    {
        disableAllCharacters();

        // if within range, enable characters
        if (currentIndex + 1 < characters.Length)
        {
            characters[currentIndex + 1].gameObject.SetActive(true);
            spawnCharacterOnPoint(currentIndex + 1);
            currentIndex++;
        }
        else
        {
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
        if (currentIndex > 0)
        {
            characters[currentIndex - 1].gameObject.SetActive(true);
            spawnCharacterOnPoint(currentIndex - 1);
            currentIndex--;
        }
        else
        {
            currentIndex = characters.Length - 1;
            characters[currentIndex].gameObject.SetActive(true);
            spawnCharacterOnPoint(currentIndex);
        }
        setCharacterName();
    }

    private void disableAllCharacters()
    {
        for (int i = 0; i < characters.Length; i++)
        {
            characters[i].gameObject.SetActive(false);
        }
    }

    public void switchCharactersComponents(int _index, bool _bool)
    {
        characters[_index].gameObject.GetComponent<Player>().enabled = _bool;
        characters[_index].gameObject.GetComponent<CharacterController>().enabled = _bool;
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
        while (true)
        {
            yield return changeCycle;
            rotationY *= -1f;
            spawnPoint.transform.eulerAngles = new Vector3(0, rotationY, 0);
        }
    }

    // =============== Characters sliders =============== //

    // =============== Button logics =============== //

    public void StartGameButton()
    {
        gameManager.StartGame();
    }

    // =============== Button logics =============== //

    void Update()
    {
        rotateCharacters();
    }
}
