using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    [SerializeField]
    private WeaponHandler[] weapons;

    private int current_weapon_index;





    void Start()
    {
        current_weapon_index = 0;
        weapons[current_weapon_index].gameObject.SetActive(true);
        
    }

 
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            TurnOnSelectedWeapon(0);

        }
        if(Input.GetKeyDown(KeyCode.Alpha2))
        {
            TurnOnSelectedWeapon(1);
        }
        if(Input.GetKeyDown(KeyCode.Alpha3))
        {
            TurnOnSelectedWeapon(2);
        }
        if(Input.GetKeyDown(KeyCode.Alpha4))
        {
            TurnOnSelectedWeapon(3);
        }
        if(Input.GetKeyDown(KeyCode.Alpha5))
        {
            TurnOnSelectedWeapon(4);
        }
        if(Input.GetKeyDown(KeyCode.Alpha6))
        {
            TurnOnSelectedWeapon(5);
        }
        
    }//update

    void TurnOnSelectedWeapon(int WeaponIndex)
    {
        if(current_weapon_index == WeaponIndex)
        return;
        weapons[current_weapon_index].gameObject.SetActive(false);

        weapons[WeaponIndex].gameObject.SetActive(true);

        current_weapon_index = WeaponIndex;
    }

    public WeaponHandler GetCurrentSelectedWeapon()
    {
        return weapons[current_weapon_index];
    }
} //class
