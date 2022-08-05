using UnityEngine.UI;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField]GameObject settingsMenu;
    [SerializeField] Image soundsMuteImg, musicMuteImg;
    bool isOpen;
    bool muteSfx, muteMusic;

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
                SoundManager.getInstance.sfxList[i].volume = 0.2f;
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
    #endregion
}
