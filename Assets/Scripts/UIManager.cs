using UnityEngine.UI;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField]GameObject settingsMenu;
    [SerializeField] Image soundsMuteImg, musicMuteImg, vibrImg;
    [SerializeField] CanvasScaler canvasScaler;
    bool isOpen;
    bool muteSfx, muteMusic, switchOffVibr;
    public bool canVibrate = true;


    private static UIManager _instance;
    public static UIManager getInstance => _instance;
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
    }


    private void Start()
    {
        if(Screen.currentResolution.width == 2160 || Screen.currentResolution.width == 2400 || Screen.currentResolution.width == 2220 || Screen.currentResolution.width == 2960)
        {
            canvasScaler.matchWidthOrHeight = 0;
        }
        else if(Screen.currentResolution.width == 1920 || Screen.currentResolution.width == 1280 || Screen.currentResolution.width == 2560)
        {
            canvasScaler.matchWidthOrHeight = 1;
        }
        else
        {
            canvasScaler.matchWidthOrHeight = 0.5f;
        }
    }

    #region Upgrades
    public void CallHero()
    {
        SoundManager.getInstance.PlayUpgradeSfx();
    }
    public void PowerHereoes()
    {
        SoundManager.getInstance.PlayUpgradeSfx();
    }
    public void SpeedHeroes()
    {
        SoundManager.getInstance.PlayUpgradeSfx();
    }
    #endregion


    #region Settings
    public void OpenSettingsMenu()
    {
        SoundManager.getInstance.PlayTapSfx();

        if (isOpen == false)
        {
            settingsMenu.SetActive(true);
            isOpen = true;
        }
        else
        {
            settingsMenu.SetActive(false);
            isOpen = false; 
        }
    }
    public void ChangeSounds()
    {
        SoundManager.getInstance.PlayTapSfx();
        if (muteSfx == false)
        {
            soundsMuteImg.enabled = true;
            muteSfx = true;
            for (int i = 0; i < SoundManager.getInstance.sfxList.Length; i++)
            {
                SoundManager.getInstance.sfxList[i].volume = 0;
            }
        }
        else
        {
            soundsMuteImg.enabled = false;
            muteSfx = false;
            for (int i = 0; i < SoundManager.getInstance.sfxList.Length; i++)
            {
                SoundManager.getInstance.sfxList[i].volume = 0.1f;
            }
        }
    }
    public void ChangeMusic()
    {
        SoundManager.getInstance.PlayTapSfx();
        if (muteMusic == false)
        {
            musicMuteImg.enabled = true;
            muteMusic = true;
            SoundManager.getInstance.musicList[0].volume = 0;
        }
        else
        {
            musicMuteImg.enabled = false;
            muteMusic = false;
            SoundManager.getInstance.musicList[0].volume = 0.1f;
        }
    }
    public void ChangeVibrationMode()
    {
        SoundManager.getInstance.PlayTapSfx();
        if (switchOffVibr == false)
        {
            vibrImg.enabled = true;
            switchOffVibr = true;
            canVibrate = false;
        }
        else
        {
            vibrImg.enabled = false;
            switchOffVibr = false;
            canVibrate = true;
        }
    }
    #endregion
}
