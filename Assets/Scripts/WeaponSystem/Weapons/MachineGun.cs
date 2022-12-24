//using JetBrains.Annotations;
using System.Collections;
//using System.Collections.Generic;
//using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;

public class MachineGun : WeaponInfo
{
  
    void FixedUpdate()
    {
        recoil();
    }
    public override void fire()
    {
        if (currentAmmo > 0 && !reloading)
        {
            shooting = true;
            shoot();
        }
        else if (currentAmmo <= 0 || Input.GetKeyDown(KeyCode.R))
        {
            if (!reloading && currentAmmo < maxAmmo)
            {
                reloading = true;
                StartCoroutine("reload");
            }
        }
    }
    public override void shoot()
    {
        RaycastHit hit;
        //WeaponInfo weapon = this.gameObject.GetComponent<WeaponInfo>();
        if(Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, 1000f, mask) && Time.time > shootTimeDiff)
        {
            target = hit.transform.GetComponent<Target>();
            shootTimeDiff = Time.time + 1/fireRate;

            if(hit.rigidbody != null)
            {
                hit.rigidbody.AddForce(-hit.normal * impactForce, ForceMode.Impulse);
            }

            if(target != null)
            {
                target.takeDamage(damage);
            }
        }
        currentAmmo--;
        transform.localPosition = Vector3.Lerp(transform.localPosition, normalPos, aimSmoothing * Time.deltaTime);
    }
    public override IEnumerator reload()
    {
        Debug.Log("Reloadig....");
        yield return new WaitForSeconds(reloadTime);
        currentAmmo = maxAmmo;
        reloading = false;
        Debug.Log("Reloaded");
    }

    public override void recoil()
    {
        if(shooting)
        {
            transform.localPosition -= Vector3.forward * zRecoil;
            currentRecoilX = Random.Range(-xRecoil,xRecoil);
            currentRecoilY = (Random.value - .5f / 2) * (TimePressed >= 4f ? yRecoil / 4 : yRecoil);
        }
        else
        {
            currentRecoilX = 0f;
            currentRecoilY = 0f;
        }
    }
}
