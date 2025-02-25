using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public Slider volumeSlider; 
    public Text volumeText; 
    void Start()
    {
        volumeSlider.value = AudioListener.volume;
        
        volumeSlider.onValueChanged.AddListener(ChangeVolume);
        
        UpdateVolumeText(AudioListener.volume);
    }

   public void ChangeVolume(float newVolume)
    {
        AudioListener.volume = newVolume;
        UpdateVolumeText(newVolume);
    }

    void UpdateVolumeText(float newVolume)
    {
        if (volumeText != null)
        {
            volumeText.text = "Volumen: " + Mathf.Round(newVolume * 100) + "%";
        }
    }

}
