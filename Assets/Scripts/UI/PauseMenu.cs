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
    public Animator animator;



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
        //pauseMenuUI.SetActive(false);
        UIGeneral.SetActive(true);
        animator.SetBool("IsPaused", false);
        Time.timeScale = 1f;
        GameIsPaused = false;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Pause()
    {
        bool timerReached = false;
        float timer = 0;
        //pauseMenuUI.SetActive(true);
        animator.SetBool("IsPaused", true);
        Invoke(nameof(pausetime), 0.5f);
        GameIsPaused = true;
    }
    void pausetime()
    {
        UIGeneral.SetActive(false);
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
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