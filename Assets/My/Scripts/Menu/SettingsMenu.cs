using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Audio;
public class SettingsMenu : MonoBehaviour
{
    public GameObject MainMenu;
    public GameObject settingsMenu;

    public Toggle fullscreenToggle;//if fullscreen of or on

    public List<ResItem> resolutions = new List<ResItem>();
    public List<QualityItem> qualities = new List<QualityItem>();
    private int selectedRes;
    private int selectedQuality;

    public TMP_Text resolutionLable;//1920x1080, or smth else, text
    public TMP_Text qualityLable;//1920x1080, or smth else, text

    public AudioMixer audioMixer;
    public TMP_Text masterLabel, musicLabel, SFXlable;
    public Slider masterSlider, musicSlider, SFXSlider;
     void Start()
    {
        fullscreenToggle.isOn = Screen.fullScreen;
        bool foundRes = false;
        for (int i = 0; i < resolutions.Count; i++)
        {
            if(Screen.width == resolutions[i].horizontal && Screen.height == resolutions[i].vertical)
            {
                foundRes = true;
                selectedRes = i;
                
            }
            ResLableRefresh();
        }
        if (!foundRes)
        {
            ResItem newRes = new ResItem();
            newRes.horizontal = Screen.width;
            newRes.vertical = Screen.height;
            resolutions.Add(newRes);
            selectedRes = resolutions.Count-1;
            ResLableRefresh();
        }
        selectedQuality = QualitySettings.GetQualityLevel();
        QualityLableRefresh();
    }


    #region Resolution
    public void ResLeft()
    {
        selectedRes++;
        if (selectedRes > resolutions.Count - 1)
        {
            selectedRes = 0;//if we press > on the highest value we go to the smallest res
        }
        ResLableRefresh();
    }
    public void ResRight()
    {
        selectedRes--;
        if (selectedRes < 0)
        {
            selectedRes = resolutions.Count - 1;//if we press < on the smallest value we go to the highest res
        }
        ResLableRefresh();
    }
    public void ResLableRefresh()
    {
        resolutionLable.text = resolutions[selectedRes].horizontal.ToString() + " x " + resolutions[selectedRes].vertical.ToString();
    }

    [System.Serializable]
    public class ResItem
    {
        public int horizontal, vertical;
    }
    #endregion

    #region Quality
    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }

    public void QualityLeft()
    {
        selectedQuality--;
        if (selectedQuality < 0)
        {
            selectedQuality = qualities.Count - 1;//if we press < on the smallest value we go to the highest res
        }
        QualityLableRefresh();
    }
    public void QualityRight()
    {
        selectedQuality++;
        if (selectedQuality > qualities.Count - 1)
        {
            selectedQuality = 0;//if we press > on the highest value we go to the smallest res
        }
        QualityLableRefresh();
    }
    public void QualityLableRefresh()
    {
        if (selectedQuality == 0) qualityLable.text = "Оч. низк";
        if (selectedQuality == 1) qualityLable.text = "Низкое";
        if (selectedQuality == 2) qualityLable.text = "Среднее";
        if (selectedQuality == 3) qualityLable.text = "Высокое";
        if (selectedQuality == 4) qualityLable.text = "Оч. выс";
        if (selectedQuality == 5) qualityLable.text = "Ультра";
    }

    [System.Serializable]
    public class QualityItem
    {
        public int qualityLevel;
    }
    #endregion

    #region Volume
    public void setMasterVol()
    {
        masterLabel.text = Mathf.RoundToInt(masterSlider.value + 80).ToString();
        audioMixer.SetFloat("MasterVol", masterSlider.value);
    }
    public void setMusicVol()
    {
        musicLabel.text = Mathf.RoundToInt(musicSlider.value + 80).ToString();
        audioMixer.SetFloat("MusicVol", musicSlider.value);
    } 
    public void setSFXVol()
    {
        SFXlable.text = Mathf.RoundToInt(SFXSlider.value + 80).ToString();
        audioMixer.SetFloat("SFXVol",SFXSlider.value);
    }
    #endregion
    public void ApplyGraphics()
    {
        Screen.SetResolution(resolutions[selectedRes].horizontal, resolutions[selectedRes].vertical, fullscreenToggle.isOn);//gets res + check fullscreen
        QualitySettings.SetQualityLevel(selectedQuality);

    }
    public void ExitToMainMenu()
    {

        PauseMenu.isInSettingsMenu = false;
        settingsMenu.SetActive(false);
        MainMenu.SetActive(true);
    }

}


