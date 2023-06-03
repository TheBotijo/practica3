using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    //new input system
    bool GameIsPaused = false;
    public bool OptionsMenuOpen = false;
    public GameObject pauseMenuUI;
    public GameObject UIGeneral;
    public GameObject pauseBotones;
    public GameObject pauseOpciones;



    void Update()
    {

        if (Input.GetKeyDown("escape"))
        { 
            if (OptionsMenuOpen)
            {
                Cross();
            }
            else if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }
public void Cross()
    {
        OptionsMenuOpen = false;
        pauseBotones.SetActive(true);
        pauseOpciones.SetActive(false);

    }
public void Resume()
    {
        pauseMenuUI.SetActive(false);
        UIGeneral.SetActive(true);
        Time.timeScale = 1f;
        GameIsPaused = false;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Pause()
    {
        pauseMenuUI.SetActive(true);
        UIGeneral.SetActive(false);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    public void Options()
    {
        OptionsMenuOpen = true;
        pauseOpciones.SetActive(true);
        pauseBotones.SetActive(false);
    }
    public void BackToMenu()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
        Time.timeScale = 1f;
    }
}