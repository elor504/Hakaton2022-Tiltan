using UnityEngine.UI;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField]GameObject settingsMenu;
    [SerializeField] Image soundsMuteImg, musicMuteImg;
    bool isOpen;
    bool muteSfx, muteMusic;

    public void OpenSettingsMenu()
    {
        if(isOpen == false)
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
        if (muteSfx == false)
        {
            soundsMuteImg.enabled = true;
            muteSfx = true;
            for (int i = 0; i < SoundManager.getInstance.musicList.Length; i++)
            {
                SoundManager.getInstance.sfxList[i].volume = 0;
            }
        }
        else
        {
            soundsMuteImg.enabled = false;
            muteSfx = false;
            for (int i = 0; i < SoundManager.getInstance.musicList.Length; i++)
            {
                SoundManager.getInstance.sfxList[i].volume = 0.3f;
            }
        }
    }
    public void ChangeMusic()
    {
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
}
