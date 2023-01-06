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





        /*
        if(!gun.isLocked)
        {

        int previousSelectedWeapon = selectedWeapon;
        if (Input.GetAxis("Mouse ScrollWheel") > 0f)
        {
            if (selectedWeapon >= transform.childCount -1)
                selectedWeapon = 0;
            else
                selectedWeapon++;
        }

         if (Input.GetAxis("Mouse ScrollWheel") < 0f)
        {
            if (selectedWeapon <= 0)
                selectedWeapon = transform.childCount - 1;
            else
                selectedWeapon--;
        }

        if (previousSelectedWeapon != selectedWeapon)
        {
            SelectWeapon();
        }
        */
        
    }

    void SelectWeapon(int select)
    {
        // Deactivate current weapon
        weapons[activeWeapon].gameObject.SetActive(false);
        // Activate selected weapon
        weapons[select].gameObject.SetActive(true);

        activeWeapon = select;

        
        /*
        int i = 0;
        foreach (Transform weapon in transform)
        {
            if (i == selectedWeapon)
                weapon.gameObject.SetActive(true);
            else
                weapon.gameObject.SetActive(false);
            i++;
        }
        */
    }
}
