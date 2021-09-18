using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class OptionsMenu : MonoBehaviour
{
    public AudioMixer audioMixer;

    public void SetTextSpeed(float TextSpeed) {
        DialogueManager.instance.textSpeedMultiplier = TextSpeed; //Sets text speed to a certain multiplier
      }

    public void SetVolume (float volume)
    {
        audioMixer.SetFloat("Volume", volume); //access audomixer and corresponds slider to volume value
    }

    public void SetGraphicQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex); //Changes graphics quality with drropdown menu connects to project quality settings
    }

    public void SetFullscreen (bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen; //Uses a toggle to make fullscreen or not
    }
}
