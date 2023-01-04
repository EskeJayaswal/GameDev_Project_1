using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class MixerController : MonoBehaviour
{
    [SerializeField]
    private AudioMixer mixer;
    [SerializeField]
    private TextMeshProUGUI valueText;
    [SerializeField]
    private Slider slider;

    void Start()
    {
        slider.value = PlayerPrefs.GetFloat("Volume");
    }


    public void SetVolume(float sliderValue)
    {
        int value = (int)(sliderValue * 100);
        valueText.SetText($"{value.ToString()}");


        mixer.SetFloat("MasterVolume", Mathf.Log10(sliderValue) * 20);

        // TODO: Move this into another function, so it only saves when we hit apply.
        PlayerPrefs.SetFloat("Volume", sliderValue);
        PlayerPrefs.Save();
    }
}
