using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//커스텀 클래스를 인스펙터창에 강제로 띄우게 하는 명령어
[System.Serializable]
public class Sound
{
    //사운드의 이름
    public string name;

    //사운드 파일
    public AudioClip clip;
    //사운드 플레이어
    private AudioSource source;

    public float Volume;
    public bool loop;

    //source가 private이므로 함수로 제어
    public void SetSource(AudioSource _source)
    {
        source = _source;
        source.clip = clip;
        source.volume = Volume;
        source.loop = loop;
    }

    //여러 기능을 구현한 함수들(골라 쓸 수 있음)
    public void SetVolume()
    {
        source.volume = Volume;
    }

    public void Play()
    {
        source.Play();
    }

    public void Stop()
    {
        source.Stop();
    }

    public void SetLoop()
    {
        source.loop = true;
    }

    public void SetLoopCancel()
    {
        source.loop = false;
    }
}

public class AudioManager : MonoBehaviour
{    
    //커스텀 클래스를 컴포넌트에 강제로 띄우게 하는 명령어
    [SerializeField]
    public Sound[] sounds;

    //싱글톤    
    #region Singleton
    static public AudioManager instance;
    private void Awake() {
        if(instance != null)
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

    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < sounds.Length; i++)
        {
            //실행하면 하이어라키에 추가되는 사운드파일의 이름 형식
            GameObject soundObject = new GameObject("사운드 파일 이름 : " + i + " = " + sounds[i].name);
            sounds[i].SetSource(soundObject.AddComponent<AudioSource>());
            //이 스크립트가 적용되는 AudioManager 안으로 들어감
            soundObject.transform.SetParent(this.transform);
        }
    }

    public void Play(string _name)
    {
        for(int i = 0; i < sounds.Length; i++)
        {
            if(_name == sounds[i].name)
            {
                //이름이 일치할 경우 실행시키고 for문을 나옴
                sounds[i].Play();
                return;
            }
        }
    }

    public void Stop(string _name)
    {
        for(int i = 0; i < sounds.Length; i++)
        {
            if(_name == sounds[i].name)
            {
                //이름이 일치할 경우 실행시키고 for문을 나옴
                sounds[i].Stop();
                return;
            }
        }
    }

    public void SetLoop(string _name)
    {
        for(int i = 0; i < sounds.Length; i++)
        {
            if(_name == sounds[i].name)
            {
                //이름이 일치할 경우 실행시키고 for문을 나옴
                sounds[i].SetLoop();
                return;
            }
        }
    }

    public void SetLoopCancel(string _name)
    {
        for(int i = 0; i < sounds.Length; i++)
        {
            if(_name == sounds[i].name)
            {
                //이름이 일치할 경우 실행시키고 for문을 나옴
                sounds[i].SetLoopCancel();
                return;
            }
        }
    }

    public void SetVolume(string _name, float _Volume)
    {
        for(int i = 0; i < sounds.Length; i++)
        {
            if(_name == sounds[i].name)
            {
                //이름이 일치할 경우 실행시키고 for문을 나옴
                sounds[i].Volume = _Volume;
                sounds[i].SetVolume();
                return;
            }
        }
    }
}
