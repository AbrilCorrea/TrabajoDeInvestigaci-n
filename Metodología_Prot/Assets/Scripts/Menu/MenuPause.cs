using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;
public class MenuPause : MonoBehaviour
{
    public GameObject menuPause;
    public FirstPersonController playerController;

    public List<VideoPlayer> videosToPause;
    public List<AudioSource> audiosToPause;

    private bool isPaused = false;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!isPaused)
                PauseGame();
            else
                Resume();
        }
    }

    public void PauseGame()
    {
        isPaused = true;

        menuPause.SetActive(true);
        Time.timeScale = 0f;

        foreach (var vp in videosToPause)
        {
            if (vp != null)
                vp.Pause();
        }

        foreach (var audio in audiosToPause)
        {
            if (audio != null)
                audio.Pause();
        }

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

      

    }

    public void Resume()
    {
        isPaused = false;

        menuPause.SetActive(false);
        Time.timeScale = 1f;

        foreach (var vp in videosToPause)
        {
            if (vp != null)
                vp.Play();
        }

        foreach (var audio in audiosToPause)
        {
            if (audio != null)
                audio.UnPause();
        }

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

    }

    public void BackToMenuFromPlay()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }

    public void Exit()
    {
        Application.Quit();
    }
}