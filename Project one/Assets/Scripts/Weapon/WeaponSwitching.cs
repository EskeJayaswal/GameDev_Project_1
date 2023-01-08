using UnityEngine;

public class WeaponSwitching : MonoBehaviour
{
    //public int selectedWeapon = 0;
    
    [SerializeField]
    private Weapon[] weapons;

    private int activeWeapon;
    
    void Start()
    {
        activeWeapon = 0;
        //SelectWeapon();  
    }

    // Update is called once per frame
    void Update()
    {
        
        if(Input.GetKeyDown("1") && !weapons[0].isLocked)
        {
            SelectWeapon(0);
        }
        if(Input.GetKeyDown("2") && !weapons[1].isLocked)
        {
            SelectWeapon(1);
        }
        if(Input.GetKeyDown("3") && !weapons[2].isLocked)
        {
            SelectWeapon(2);
        }
        
    }

    void SelectWeapon(int select)
    {
        // Deactivate current weapon
        weapons[activeWeapon].gameObject.SetActive(false);
        // Activate selected weapon
        weapons[select].gameObject.SetActive(true);
        // If you change weapon during reload, you'll be unable to shoot.
        //weapons[activeWeapon].animator.SetBool("Reloading", false);


        activeWeapon = select;

    }
}
