using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;

public class Mace : Weapon
{
    
    void Update()
    {
        if(Input.GetButtonDown("Fire1"))
        {
            if (!PauseControl.gameIsPaused) 
                Strike();
        }
    }

    void Start()
    {
        UpdateBulletText();
    }

    void OnEnable()
    {
        UpdateBulletText();
        animator.SetBool("Reloading", false);
    }

    void Strike()
    {
        animator.SetTrigger("Strike");
        UpdateBulletText();

        int layerMask = 1 << 11;

        RaycastHit hit;
        if (Physics.Raycast(fpsCamera.transform.position, fpsCamera.transform.forward, out hit, range, layerMask))
        {
            if (hit.collider.GetComponentInParent<IDamagable>() != null)
            {
                hit.collider.GetComponentInParent<IDamagable>().TakePhysicalDamage(damage);
            }

            Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
        }
    }

    private void UpdateBulletText()
    {
        bulletCount.text = "";
    }
}
