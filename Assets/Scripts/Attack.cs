using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] Transform firePoint;
    [SerializeField] float fireRate = 0.5f;
    [SerializeField] float currentFireRate = 0f;
    [SerializeField] int ammoCount = 5;
    [SerializeField] int maxAmmoCount = 10;
    [SerializeField] bool isPlayer = false;
    public int GetBullet
    {
        get{return ammoCount;}
        set{ammoCount = value;
            if(ammoCount > maxAmmoCount)
                ammoCount = maxAmmoCount;
            }
    }
    public int GetMaxBulletAmount{get{return maxAmmoCount;}}

    public float GetCurrentFireRate{get{return currentFireRate;} set{currentFireRate = value;}}
    bool canFire = true;

    void Start()
    {
        
    }

    void Update()
    {
        CheckCanFire();

        if(isPlayer)
        {
            if(Input.GetMouseButtonDown(0) && canFire && ammoCount > 0)
            {
                Fire();
            }
        }
        
    }

    public void Fire()
    {
        canFire = false;
        SpawnBullet();
        DecreaseAmmo();
    }


    void SpawnBullet()
    {
        float bulletRot = 0;
        float diff = 180 - transform.eulerAngles.y;

        if(diff >= 0)
            bulletRot = -180;
        else if(diff < 0)
            bulletRot = 0;
        GameObject bullet = Instantiate(bulletPrefab,firePoint.position,Quaternion.Euler(0,0,bulletRot));
        bullet.GetComponent<BulletMovement>().owner = this.gameObject;
    }


    void CheckCanFire()
    {
        if(!canFire)
        {
            currentFireRate += Time.deltaTime;
            if(currentFireRate >= fireRate)
            {
                currentFireRate = 0f;
                canFire = true;
            }
        } 
    }

    void DecreaseAmmo()
    {
        ammoCount--;
    }
}
