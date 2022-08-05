using UnityEngine;

public class SoundManager : MonoBehaviour
{

    public AudioSource[] musicList;
    public AudioSource[] sfxList;

    private static SoundManager _instance;
    public static SoundManager getInstance => _instance;

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        else if (_instance != this)
        {
            Destroy(this.gameObject);
        }

        PlayGameplayMusic(true);
    }

    public void PlayGameplayMusic(bool isPlay)
    {
        if(isPlay)
        {
            musicList[0].Play();
        }
        else
        {
            musicList[0].Stop();
        }
    }

    public void PlayTapSfx()
    {
        sfxList[0].Play();
    }
    public void PlayUpgradeSfx()
    {
        sfxList[1].Play();
    }
    public void PlaySpawnHeroSfx()
    {
        sfxList[2].Play();
    }
    public void PlaySpawnMonsterSfx()
    {
        sfxList[3].Play();
    }
    public void PlayDespawnMonsterSfx()
    {
        sfxList[4].Play();
    }
    public void PlayUnitsMeetSfx()
    {
        sfxList[5].Play();
    }
}
