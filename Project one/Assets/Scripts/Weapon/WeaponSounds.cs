using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSounds : MonoBehaviour
{
    /* https://pixabay.com/sound-effects/search/reload/ */


    [SerializeField]
    private AudioClip shoot;
    [SerializeField]
    private AudioClip reload;
    [SerializeField]
    private AudioClip dryfire;
    [SerializeField]
    private AudioClip maceSwing;

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

    void WeaponDryFire()
    {
        audioSource.PlayOneShot(dryfire);
    }

    void WeaponMaceSwing()
    {
        audioSource.PlayOneShot(maceSwing);
    }
}
