using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;


public class GameManager : MonoBehaviour
{
    [SerializeField] private Player player;
    [SerializeField] private LifeManager lifeManager;
    [SerializeField] private GameObject selectedCaracter;
    [SerializeField] private CharacterSelection characterSelection;
    //캐릭터 선택 후 로딩될 씬
    [SerializeField] private string playScene = "Development";
    public int currentLife {get; private set;}
    public int score {get; private set;}
    public string selectedCaracterName;
    public int TotalLife = 3;

    //싱글톤
    #region Singleton
    static public GameManager instance;
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

    void Start()
    {
        characterSelection = FindObjectOfType<CharacterSelection>();
    }

    //체력 감소
    public void decreaseLife(int _life = 1)
    {
        currentLife -= _life;

        if (currentLife <= 0)
            currentLife = 0;

        lifeManager.updateHeartImage();

        if (isDead()) player._state = Player.playerState.Die;
    }

    //체력이 0인지 확인
    public bool isDead()
    {
        return (currentLife <= 0) ? true : false;
    }

    //체력 증가
    public void increaseLife(int _life = 1)
    {
        currentLife += _life;

        if (currentLife > TotalLife)
            currentLife = TotalLife;

        lifeManager.updateHeartImage();
    }

    public void addScore(int _score)
    {
        score += _score;
    }

    //========================씬 제어===============================
    //CharacterSelection.cs의 StartGame 함수를 여기로 옮김
    //싱글톤으로 설정하여 씬을 옮기더라도 파괴되지 않고 제어함

    //선택된 캐릭터의 이름을 저장 후 게임 스타트
    public void StartGame()
    {
        selectedCaracterName = characterSelection.characters[characterSelection.currentIndex].name;

        StartCoroutine(resetPlaySceneCoroutine());
    }

    //플레이어가 죽으면 Player 스크립트에서 호출됨
    //씬을 새로 로드하고 스크립트 재탐색
    public void resetPlayScene()
    {
        StartCoroutine(resetPlaySceneCoroutine());
    }

    IEnumerator resetPlaySceneCoroutine()
    {
        SceneManager.LoadScene(playScene);

        yield return null;

        player = FindObjectOfType<Player>();
        lifeManager = FindObjectOfType<LifeManager>();

        currentLife = TotalLife;
    }
}
