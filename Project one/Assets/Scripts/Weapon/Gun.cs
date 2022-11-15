using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;

public class Gun : MonoBehaviour
{

    public float damage = 10f;
    public float range = 100f;

    //reload variables
    public int maxAmmo = 15;
    public int maxClips = 6;
    private int totalAmmo;
    public int currentAmmo;
    public float reloadTime = 3f;
    private bool isReloading = false;
    private bool isAllOutOfAmmo = false;

    public Camera fpsCamera;
    public ParticleSystem muzzleFlash;

    public GameObject impactEffect;

    public Animator animator;

    public TextMeshProUGUI  bulletCount;

    void start() 
    {
        currentAmmo = maxAmmo;
    }

    //Denne metode bliver aktuel når vi har en weaponholder og mulgihed for at skifte mellem våben
    void OnEnable()
    {
        currentAmmo = maxAmmo;
        totalAmmo = maxAmmo * maxClips;
        isReloading = false;
        UpdateBulletText();
        animator.SetBool("Reloading", false);
    }

    void Update()
    {
        if (isReloading)
            return;

        if (currentAmmo <= 0 && !isAllOutOfAmmo)
        {
            StartCoroutine(Reload());
            return;
        }

        if(Input.GetButtonDown("Fire1"))
        {
            if (!PauseControl.gameIsPaused && !isAllOutOfAmmo) 
                Shoot();
            else if(!PauseControl.gameIsPaused && isAllOutOfAmmo)
            {
                animator.SetTrigger("DryShoot");
            }
            
        }

        if(Input.GetKeyDown(KeyCode.R) && currentAmmo != maxAmmo && totalAmmo != 0)
        {
            StartCoroutine(Reload());
            return;
        }     
    }

    IEnumerator Reload() 
    {
        if( totalAmmo != 0) {
            isReloading = true;
            //Debug.Log("Reloading...");

            animator.SetBool("Reloading", true);

            yield return new WaitForSeconds(reloadTime - .25f);

            animator.SetBool("Reloading", false);

            yield return new WaitForSeconds(.25f);

            if (totalAmmo + currentAmmo >= maxAmmo)
            {
                totalAmmo -= maxAmmo - currentAmmo;
                currentAmmo = maxAmmo;
            } else {
                currentAmmo += totalAmmo;
                totalAmmo = 0;
            }

            isReloading = false;
            UpdateBulletText();
        }

        if (totalAmmo + currentAmmo == 0)
            isAllOutOfAmmo = true;


    }

    void Shoot()
    {
        muzzleFlash.Play();
        animator.SetTrigger("Shoot");

        currentAmmo--;
        UpdateBulletText();

        // Add to shotsfired statistic
        PlayerStats.instance.AddToStat("shot", 1);

        // Bit shift the index of the layer (11: Enemy) to get a bit mask
        // Sometimes we accidentally hit ourself and died..
        int layerMask = 1 << 11;

        RaycastHit hit;
        if (Physics.Raycast(fpsCamera.transform.position, fpsCamera.transform.forward, out hit, range, layerMask))
        {
            //Debug.Log(hit.transform.name);

            if (hit.collider.GetComponentInParent<IDamagable>() != null)
            {
                //Debug.Log("Hitting");
                hit.collider.GetComponentInParent<IDamagable>().TakePhysicalDamage(damage);

                // Add to shots hit statistic
                PlayerStats.instance.AddToStat("hit", 1);

            }

            Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
        }
    }


    private void UpdateBulletText()
    {
        bulletCount.text = string.Format("{0} | {1}", currentAmmo.ToString(), totalAmmo.ToString());
    }

}
