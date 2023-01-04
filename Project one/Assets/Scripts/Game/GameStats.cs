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
        // Makes sure the volume is set to whatever level we defined last time.
        LoadStoredVolume();
        
        
    }


    void LoadStoredVolume()
    {
        mixer.SetFloat("MasterVolume", Mathf.Log10(PlayerPrefs.GetFloat("Volume")) * 20);
    }


    
}
