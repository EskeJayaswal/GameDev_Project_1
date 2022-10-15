using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunSounds : MonoBehaviour
{
    /* https://pixabay.com/sound-effects/search/reload/ */


    [SerializeField]
    private AudioClip shoot;
    [SerializeField]
    private AudioClip reload;

    private AudioSource audioSource;

    // Start is called before the first frame update
    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        
    }

    void WeaponShoot()
    {
        audioSource.PlayOneShot(shoot);
    }
    void WeaponReload()
    {
        audioSource.PlayOneShot(reload);
    }
}
