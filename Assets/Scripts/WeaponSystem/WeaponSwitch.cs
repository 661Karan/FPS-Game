using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwitch : MonoBehaviour
{
    public int index;
    public WeaponInfo[] Weapons;

    [HideInInspector]public GameObject currentWeapon;
    void Start()
    {
        index = 0;
        equipWeapon(index);
    }


    void Update()
    {
        if(Input.GetAxis("Mouse ScrollWheel") > 0f)
        {
            index++;
            if (index > Weapons.Length - 1)
            {
                index = 0; 
            }
            equipWeapon(index);
        }
        else if(Input.GetAxis("Mouse ScrollWheel") < 0f)
        {
            index--;
            if (index < 0)
            {
                index = Weapons.Length - 1;
            }
            equipWeapon(index);
        }
    }

    public void equipWeapon( int index)
    {
        for (int i = 0; i < Weapons.Length; i++)
        {
            if (i == index)
            {
                Weapons[i].gameObject.SetActive(true);
                currentWeapon = Weapons[i].gameObject;
            }
            else
            {
                Weapons[i].gameObject.SetActive(false);
            }
        }
    }
}
