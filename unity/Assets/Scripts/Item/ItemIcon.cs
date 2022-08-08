using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

/*
logic flow
1. Canvas 안 Item Icon의 자식 오브젝트(아이콘)를 불러옴
2. 아이템을 획득할 때마다 각각 아이템의 스크립트에서 updateItemIcon 함수 호출됨
3. updateItemIcon에서 획득 아이템의 이름, 지속시간을 가져옴
4. 아이콘의 이름과 획득한 아이템의 이름을 비교하여 지속시간만큼 아이콘 활성화
4-1. Icon_witchHat은 지속시간이 없기 때문에 추가 점프 여부에 따라 아이콘 활성화
5. itemNameArray 배열에 획득한 아이템 이름을 저장 후 지속시간이 지나면 삭제
6. updateItemIcon가 호출됐을 때 이미 같은 아이템의 아이콘이 활성화되어 있다면
    (itemNameArray에 아이템 이름이 남아 있디면) 기존의 updateItemIcon 비활성화 후 아이콘 지속시간 새로 갱신
*/
public class ItemIcon : MonoBehaviour
{
    [SerializeField] private GameObject[] iconArray;
    private PlayerMove playerMove;
    //같은 아이콘의 코루틴이 중복 실행될 경우 이전에 실행된 코루틴을 멈추기 위해 배열 선언
    private Coroutine[] coroutineArray;
    //지속 중인 아이템 이름 저장
    private string[] itemNameArray;
    private int currentIndex, maxIndex, previousIndex;

    public const string ICON_WITCHHAT = "Icon_witchHat", ICON_DRAGON = "Icon_dragon", ICON_SKULL = "Icon_skull";

    void Start()
    {
        playerMove = FindObjectOfType<PlayerMove>();

        iconArray = new GameObject[transform.childCount];
        coroutineArray = new Coroutine[transform.childCount];
        itemNameArray = new string[transform.childCount];

        maxIndex = transform.childCount - 1;
        currentIndex = 0;
        previousIndex = maxIndex;

        for (int i = 0; i < iconArray.Length; i++)
        {
            iconArray[i] = this.transform.GetChild(i).gameObject;
            iconArray[i].SetActive(false);

            itemNameArray[i] = "";
        }
    }

    //아이템을 획득할 때마다 각 아이템의 스크립트에서 호출됨
    //파괴되지 않는 Item Icon 스크립트에서 코루틴 실행
    public void updateItemIcon(string _itemName, float _itemDuration)
    {
        coroutineArray[currentIndex] = StartCoroutine(updateItemIconCoroutine(_itemName, _itemDuration));
    }

    private IEnumerator updateItemIconCoroutine(string _itemName, float _itemDuration)
    {
        foreach (GameObject iconArray in iconArray)
        {
            if (iconArray.name == _itemName)
            {
                //Icon_witchHat일 경우 moreJump 여부에 따라 아이콘 지속됨
                if (iconArray.name == ICON_WITCHHAT)
                {
                    iconArray.SetActive(true);
                    yield return new WaitUntil(() => !playerMove.canMoreJump);
                    iconArray.SetActive(false);
                }
                //그 외 지속시간이 존재하는 아이템의 경우
                else
                {
                    StartCoroutine(removeItemNameArray(_itemDuration));

                    for(int i = 0; i < itemNameArray.Length; i++)
                    {
                        //지속 중인 아이템을 중복해서 먹었을 경우
                        if (iconArray.name == itemNameArray[i])
                        {
                            StopCoroutine(coroutineArray[i]);
                            Debug.Log("이전 아이콘 코루틴 정지됨");
                        }
                    }

                    itemNameArray[currentIndex] = _itemName;
                    updateIndex();

                    iconArray.SetActive(true);
                    yield return new WaitForSeconds(_itemDuration);
                    iconArray.SetActive(false);
                }
            }
        }
    }

    //지속시간 중인 아이템 이름 저장
    private IEnumerator removeItemNameArray(float _itemDuration)
    {
        int index = currentIndex;
        yield return new WaitForSeconds(_itemDuration);
        itemNameArray[index] = "";
    }

    //인덱스를 1씩 늘림
    private void updateIndex()
    {
        currentIndex = (currentIndex < maxIndex) ? currentIndex + 1 : 0;
        previousIndex = (currentIndex == 0) ? maxIndex : currentIndex - 1;
    }
}
