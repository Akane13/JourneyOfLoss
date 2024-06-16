using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioSettings : MonoBehaviour
{
    public Slider bgmSlider;
    public Toggle muteToggle;

    private void Start()
    {
        // Initialize the slider and toggle based on current settings
        float initialVolume = AudioManager.Instance.bgmSource.volume;
        bool initialMute = AudioListener.pause;

        bgmSlider.value = initialVolume;
        muteToggle.isOn = initialMute;

        // Add listeners to handle changes
        bgmSlider.onValueChanged.AddListener(SetBGMVolume);
        muteToggle.onValueChanged.AddListener(MuteAll);

        Debug.Log($"Initializing BGM slider with volume: {initialVolume}, mute: {initialMute}");
    }

    private void SetBGMVolume(float volume)
    {
        Debug.Log($"BGM Volume set to: {volume}");
        AudioManager.Instance.SetBGMVolume(volume);
    }

    private void MuteAll(bool isMuted)
    {
        Debug.Log($"Mute all: {isMuted}");
        AudioManager.Instance.MuteAll(isMuted);
    }
}
