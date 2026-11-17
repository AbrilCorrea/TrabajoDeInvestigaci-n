using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagerEnd : MonoBehaviour
{
    public CanvasGroup endPanel;
    public FirstPersonController playerController;

    private int roomsCompleted = 0;
    private bool gameFinished = false;

    void Start()
    {
        endPanel.alpha = 0;
        endPanel.interactable = false;
        endPanel.blocksRaycasts = false;
    }

    public void CompleteRoom()
    {
        if (gameFinished) return;

        roomsCompleted++;

        if (roomsCompleted >= 4)
        {
            FinishGame();
        }
    }

    void FinishGame()
    {
        gameFinished = true;

        endPanel.alpha = 1;
        endPanel.interactable = true;
        endPanel.blocksRaycasts = true;

        if (playerController != null)
            playerController.canMove = false;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        Time.timeScale = 0f;
    }

    public void ExitGame()
    {
        Time.timeScale = 1f;

        Application.Quit();

        #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }
}