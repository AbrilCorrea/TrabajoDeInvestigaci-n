using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerRegistration : MonoBehaviour
{
    [SerializeField] private TMP_InputField nameInput;
    [SerializeField] private string nextSceneName = "Game";

    public void SaveName()
    {
        string playerName = nameInput.text.Trim();

        if (string.IsNullOrEmpty(playerName))
            return;

        PlayerPrefs.SetString("PlayerAlias", playerName);
        PlayerPrefs.Save();

        SceneManager.LoadScene(nextSceneName);
    }
}