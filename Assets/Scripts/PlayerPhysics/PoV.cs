using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PoV : MonoBehaviour
{
    private float panX;
    public InputManager iManager;
    private float panY;
    public float mouseSenstivity = 700f;
    private float xRotation;
    private float yRotation;
    public GameObject CamLookAt;
    public GameObject playerBody;
    public float panLimit = 180f;
    public GameObject weaponHolder;
    WeaponSwitch weaponSwitch;


    private void Awake()
    {
        weaponSwitch = weaponHolder.GetComponent<WeaponSwitch>();   
        GameObject _inputManager = GameObject.Find("Input Manager");
        iManager = _inputManager.GetComponent<InputManager>();
    }

    void FixedUpdate()
    {
        WeaponInfo weaponInfo = weaponSwitch.currentWeapon.GetComponent<WeaponInfo>(); 

        panX = iManager.mouseX * mouseSenstivity * Time.fixedDeltaTime;
        panY = iManager.mouseY * mouseSenstivity * Time.fixedDeltaTime;

        panX += (weaponInfo.currentRecoilX);
        panY += Mathf.Abs(weaponInfo.currentRecoilY);

        xRotation -= panY;
        xRotation = Mathf.Clamp(xRotation, -panLimit, panLimit);

        transform.localRotation = Quaternion.Euler(xRotation , 0, 0);
        playerBody.transform.Rotate(Vector3.up * panX);
    }
}
