using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class GameStats : MonoBehaviour
{
    [SerializeField]
    private AudioMixer mixer;

    // Singleton
    public static GameStats instance;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        Debug.Log(PlayerPrefs.GetFloat("Volume"));
        LoadStoredVolume();
        
        
    }


    void LoadStoredVolume()
    {
        mixer.SetFloat("MasterVolume", Mathf.Log10(PlayerPrefs.GetFloat("Volume")) * 20);
    }


    
}
