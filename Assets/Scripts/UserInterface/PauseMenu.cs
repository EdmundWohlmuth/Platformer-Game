using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool isPaused = false;
    public GameObject pauseMenuUi;

    PlayerController action;

    private void Awake()
    {
        action = new PlayerController();
    }

    private void OnEnable()
    {
        action.Enable();
    }

    private void OnDisable()
    {
        action.Disable();
    }

    private void Start()
    {
        action.Pause.PauseGame.performed += _ => isGamePaused();
    }

    private void isGamePaused()
    {
        if (isPaused)
        {
            Resume();
        }
        else Pause();
    }

    void Resume()
        {
            pauseMenuUi.SetActive(false);
            Time.timeScale = 1f;
            isPaused = false;
        }

        void Pause()
        {
            pauseMenuUi.SetActive(true);
            Time.timeScale = 0f;
            isPaused = true;
        }

    //--------------Buttons---------------------------

    public void MainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}

