using UnityEngine;
using System.Collections;

public class Gun : MonoBehaviour
{

    public float damage = 10f;
    public float range = 100f;

    //reload variables
    public int maxAmmo = 7;
    private int currentAmmo;
    public float reloadTime = 3f;
    private bool isReloading = false;

    public Camera fpsCamera;
    public ParticleSystem muzzleFlash;

    public GameObject impactEffect;

    public Animator animator;

    void start() 
    {
        currentAmmo = maxAmmo;
    }

    //Denne metode bliver aktuel når vi har en weaponholder og mulgihed for at skifte mellem våben
    void OnEnable()
    {
        isReloading = false;
        animator.SetBool("Reloading", false);
    }

    void Update()
    {
        if (isReloading)
            return;

        if (currentAmmo <= 0)
        {
            StartCoroutine(Reload());
            return;
        }

        if(Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }        
    }

    IEnumerator Reload() 
    {
        isReloading = true;
        Debug.Log("Reloading...");

        animator.SetBool("Reloading", true);

        yield return new WaitForSeconds(reloadTime - .25f);

        animator.SetBool("Reloading", false);

        yield return new WaitForSeconds(.25f);

        currentAmmo = maxAmmo;
        isReloading = false;
    }

    void Shoot()
    {
        muzzleFlash.Play();

        currentAmmo--;

        RaycastHit hit;
        if (Physics.Raycast(fpsCamera.transform.position, fpsCamera.transform.forward, out hit, range))
        {
            Debug.Log(hit.transform.name);

            if (hit.collider.GetComponent<IDamagable>() != null)
            {
                Debug.Log("Hitting");
                hit.collider.GetComponent<IDamagable>().TakePhysicalDamage(damage);
            }

            Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
        }
    }


}
