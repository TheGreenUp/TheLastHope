
using UnityEngine;
using UnityEngine.SceneManagement;
public class mainMenu : MonoBehaviour
{
    public static bool gameIsPaused = false;
    public static bool isInSettingsMenu = false;

    public GameObject pauseMenuUI;
    public GameObject settingsMenu;

    public void StartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
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
}
