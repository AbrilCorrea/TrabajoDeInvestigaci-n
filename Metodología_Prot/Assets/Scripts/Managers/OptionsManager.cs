using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
public class OptionsManager : MonoBehaviour
{
    public AudioMixer audioMixer;
    public Slider volumeSlider;
    
    void Start()
    {
        if (volumeSlider != null)
        {
            float currentVolume;
            if (audioMixer.GetFloat("MasterVolume", out currentVolume))
            {
                volumeSlider.value = Mathf.Pow(10f, currentVolume / 20f);
            }

            volumeSlider.onValueChanged.AddListener(SetVolume);
        }
    }

    public void SetVolume(float value)
    {
        if (value <= 0.0001f)
        {
            audioMixer.SetFloat("MasterVolume", -80f); 
        }
        else
        {
            float volumeDb = Mathf.Log10(value) * 20f;
            audioMixer.SetFloat("MasterVolume", volumeDb);
        }
    }
}