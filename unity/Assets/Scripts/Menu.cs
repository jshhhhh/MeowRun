using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//ESC 눌렀을 때 나타나는 MenuCanvas를 제어하는 스크립트
public class Menu : MonoBehaviour
{
    #region Singleton
    static public Menu instance;
    private void Awake()
    {
        if (instance != null)
        {
            Destroy(this.gameObject);
        }
        else
        {
            //이 오브젝트를 다른 씬을 불러올 때마다 파괴시키지 말라는 명령어
            DontDestroyOnLoad(this.gameObject);
            instance = this;
        }
    }
    #endregion

    [SerializeField] GameManager gameManager;
    [SerializeField] GameObject menu;
    [SerializeField] bool activated = false;

    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            OnOffMenuButton();
        }
    }

    public void OnOffMenuButton()
    {
        activated = !activated;
        menu.SetActive(activated);

        if (activated)
        {
            //일시정지
            Time.timeScale = 0;
        }
        else
        {
            //일시정지 해제
            Time.timeScale = 1;
        }
    }

    public void CharacterSelectButton()
    {
        OnOffMenuButton();
        StartCoroutine(gameManager.resetCharacterSelectSceneCoroutine());
    }

    public void ExitButton()
    {
        //종료 기능(에디터에선 게임 종료 안 됨)
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit(); // 어플리케이션 종료
#endif
    }
}
