using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.PostProcessing;


public class PauseMenu : MonoBehaviour
{   
    public static bool gameIsPaused = false;
    public static bool isInSettingsMenu = false;

    public GameObject pauseMenuUI;
    public GameObject settingsMenu;
    public GameObject missionText;
    public GameObject subtitleText;

    public GameObject player;
    private Camera playerCamera;
    public GameObject playersCrosshair;

    private PostProcessVolume volume;
    private DepthOfField depthOfField;

    private void Start()
    {
        playerCamera = player.GetComponentInChildren<Camera>();
        volume = playerCamera.GetComponent<PostProcessVolume>();
        volume.profile.TryGetSettings(out depthOfField);    
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {

            if (gameIsPaused)
            {
                if (isInSettingsMenu)//if we are in settingsMenu we shoud not quit the menu
                {
                    ReturnToPause();
                }
                else
                {
                    Resume();
                }
            }
            else
            {
                Pause();
            }
        }
    }
    void Resume()
    {
        Cursor.lockState = CursorLockMode.Locked;//Gives the cursor to player
        Cursor.visible = false;//Gives the cursor to player
        pauseMenuUI.SetActive(false);
        player.GetComponent<FirstPersonContoller>().enabled = true;
        AudioListener.pause = false;
        playersCrosshair.SetActive(true);
        Time.timeScale = 1f;
        gameIsPaused = false;
        BlurOffOn(false);
        missionText.SetActive(true);
        subtitleText.SetActive(true);
    }
    private void Pause()
    {
        missionText.SetActive(false);
        subtitleText.SetActive(false);
        if (isInSettingsMenu) settingsMenu.SetActive(false);
        Cursor.lockState = CursorLockMode.None;//Gives the cursor to player
        Cursor.visible = true;//Gives the cursor to player
        pauseMenuUI.SetActive(true);//set pause menu to active
        player.GetComponent<FirstPersonContoller>().enabled = false;//off player camera rotation
        AudioListener.pause = true;//pause audio
        playersCrosshair.SetActive(false);//off crosshair
        Time.timeScale = 0f;
        gameIsPaused = true;
        BlurOffOn(true);
    }
    private void ReturnToPause()//if we swtich back from setting to pause menu
    {

        if (isInSettingsMenu) settingsMenu.SetActive(false);
        Cursor.lockState = CursorLockMode.None;//Gives the cursor to player
        Cursor.visible = true;//Gives the cursor to player
        pauseMenuUI.SetActive(true);//set pause menu to active
        player.GetComponent<FirstPersonContoller>().enabled = false;//off player camera rotation
        AudioListener.pause = true;//pause audio
        playersCrosshair.SetActive(false);//off crosshair
        Time.timeScale = 0f;
        gameIsPaused = true;
        isInSettingsMenu = false;
    }
    public void ResumeGame()
    {
        Resume();
    }
    public void EnterSettings()
    {
        isInSettingsMenu = true;
        pauseMenuUI.SetActive(false);
        settingsMenu.SetActive(true);
    }
    public void ExitGame()
    {
        Application.Quit();
    }
    public void BlurOffOn(bool on)
    {
        if (on) depthOfField.active = true;
        else depthOfField.active = false;
    }

}
