using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundManager : MonoBehaviour
{
    public AudioSource efxSource;
    public AudioSource musicSource;

    //사운드 이펙트 피치에 약간의 랜덤한 변화
    //1f: 원음 피치, .95f: 5% 낮음, 1.05f: 5% 높음
    public float lowPitchRange = .9f;
    public float highPitchRange = 1.1f;

    //싱글톤    
    // #region Singleton
    // static public SoundManager instance;
    // private void Awake() {
    //     if(instance != null)
    //     {
    //         Destroy(this.gameObject);
    //     }
    //     else
    //     {
    //     //이 오브젝트를 다른 씬을 불러올 때마다 파괴시키지 말라는 명령어
    //         DontDestroyOnLoad(this.gameObject);
    //         instance = this;
    //     }
    // }
    // #endregion

    //하나의 오디오 클립을 재생하는 함수
    public void PlaySingle(AudioClip clip)
    {
        efxSource.clip = clip;
        efxSource.pitch = 1f;
        efxSource.Play();
    }

    //params: 콤마로 구분된 같은 타입의 여러 입력들을 한 번에 받게 해줌
    //클립 랜덤 선택, 랜덤 피치 후 재생
    public void RandomizeSfx(params AudioClip[] clips)
    {
        //0~clips.Length(clip 배열의 길이) 사이의 랜덤한 수 생성
        int randomIndex = Random.Range(0, clips.Length);
        float randomPitch = Random.Range(lowPitchRange, highPitchRange);

        //랜덤 피치
        efxSource.pitch = randomPitch;
        //랜덤하게 선택한 클립 대체
        efxSource.clip = clips[randomIndex];
        efxSource.Play();
    }
}
