using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public GameObject CanvasOptions;
    public GameObject CanvasMenu;
    public void Play()
    {
        SceneManager.LoadScene(1);
    }

    public void Options()
    {
        CanvasOptions.gameObject.SetActive(true);
        CanvasMenu.SetActive(false);
    }
    public void BackToMenu()
    {
        CanvasOptions.gameObject.SetActive(false);
        CanvasMenu.SetActive(true);
    }

   
    public void Exit()
    {
        Application.Quit();
    }
}