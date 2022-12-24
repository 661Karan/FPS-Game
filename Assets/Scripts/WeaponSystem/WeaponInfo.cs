using System.Collections;
//using System.Collections.Generic;
//using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

public abstract class WeaponInfo : MonoBehaviour
{
    [Header("Gun Settings")]
    public string weaponName = "NA";
    public float fireRate = 1f;
    public float damage = 50f;
    public float impactForce = 1f;
    public float range = 100f;
    public float reloadTime = 3f;
    public float maxAmmo = 10f;
    public GameObject camRecoil;

    public GameObject cam;

    [HideInInspector] public bool reloading = false;
    [HideInInspector] public bool shooting = false;

    [HideInInspector] public Target target;
    [HideInInspector] public LayerMask mask;

    [HideInInspector] public float shootTimeDiff = 0f;
    [HideInInspector] public float TimePressed = 0f;

    // Recoil
    [HideInInspector] public float currentRecoilX = 0f;
    [HideInInspector] public float currentRecoilY = 0f;

    [HideInInspector] public float currentAmmo;


    //public Vector3 targetRotation;
    //public Vector3 currentRotation;
    //public float returnSpeed;
    //public float snappiness;
    [Header("Recoil Settings")]
    public float xRecoil;
    public float yRecoil;
    public float zRecoil;
    public float aimSmoothing;
    public Vector3  normalPos;

    void Start()
    {
        currentAmmo = maxAmmo;
    }
    public abstract void fire();
    public abstract void shoot();
    public abstract IEnumerator reload();
    public abstract void recoil();
}
