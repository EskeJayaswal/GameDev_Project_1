using UnityEngine;

public class Gun : MonoBehaviour
{
    public float damage = 10f;
    public float range = 100f;

    public Camera fpsCamera;
    public ParticleSystem muzzleFlash;

    void Update()
    {
        if(Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }        
    }

    void Shoot()
    {
        muzzleFlash.Play();

        RaycastHit hit;
        if (Physics.Raycast(fpsCamera.transform.position, fpsCamera.transform.forward, out hit, range))
        {
            Debug.Log(hit.transform.name);

            if (hit.collider.GetComponent<IDamagable>() != null)
            {
                Debug.Log("Hitting");
                hit.collider.GetComponent<IDamagable>().TakePhysicalDamage(damage);
            }
        }
    }
}
