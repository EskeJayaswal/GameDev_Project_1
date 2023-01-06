using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySounds : MonoBehaviour
{
    [SerializeField]
    private AudioClip dyingSound;

    [SerializeField]
    private AudioClip[] hitSounds; // Zombie hitting player

    [SerializeField]
    private AudioClip maceHitSound;
    [SerializeField]
    private AudioClip weaponHitSound;

    private AudioSource audioSource;

    // Start is called before the first frame update
    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        
    }

    public void DieSound()
    {
        // Randomize the pitch of the dying scream, to make it sound more diverce
        float pitchVolume = Random.Range(0.75f, 1.5f);
        audioSource.pitch = pitchVolume;
        audioSource.PlayOneShot(dyingSound);
    }

    public void HitSound()
    {

        audioSource.PlayOneShot(GetRandomClip(hitSounds));
    }

    public void MaceHitSound()
    {

        audioSource.PlayOneShot(maceHitSound);
    }

    public void WeaponHitSound()
    {

        audioSource.PlayOneShot(weaponHitSound);
    }

    private AudioClip GetRandomClip(AudioClip[] audioList)
    {
        return audioList[UnityEngine.Random.Range(0, audioList.Length)];
    }


}
