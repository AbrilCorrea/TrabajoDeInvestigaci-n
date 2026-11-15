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

    [SerializeField] private Slider tripofobiaSlider;
    [SerializeField] private Slider claustrofobiaSlider;
    [SerializeField] private Slider aracnofobiaSlider;

    [SerializeField] private GameObject CanvasName;
    [SerializeField] private GameObject CanvasQuestions;

    public void SaveName()
    {
        string playerName = nameInput.text.Trim();

        if (string.IsNullOrEmpty(playerName))
            return;

        PlayerPrefs.SetString("PlayerAlias", playerName);
        PlayerPrefs.Save();

        CanvasName.SetActive(false);
        CanvasQuestions.SetActive(true);

    }
    
    public void SaveSesibility()
    {
        PlayerPrefs.SetInt("Tripofobia", (int)tripofobiaSlider.value);
        PlayerPrefs.SetInt("Claustrofobia", (int)claustrofobiaSlider.value);
        PlayerPrefs.SetInt("Aracnofobia", (int)aracnofobiaSlider.value);

        PlayerPrefs.Save();

        SceneManager.LoadScene(nextSceneName);

    }
}
