using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Weapon : MonoBehaviour
{
    public bool isLocked;

    public float damage;
    public float range;
    public GameObject impactEffect;

    public Camera fpsCamera;

    public Animator animator;
    public TextMeshProUGUI bulletCount;

    public void UnlockWeapon()
    {
        isLocked = false;
    }

}
