using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    public GameObject bullet;
    public float fireRate;
    float nextFire;
    public int maxAmmo;
    public int ammo;
    public float ammoRegenTime;
    float ammoRegenTimer;

    void Start()
    {
        
    }
    void Update()
    {

        if (Input.GetButtonDown("Fire2") && ammo > 0)
        {
            ammoRegenTimer = 0;
            if (Time.time > nextFire)
            {
                nextFire = Time.time + fireRate;
                print("shoot");
                GameObject shotBullet = Instantiate(bullet, transform.position, Quaternion.identity);
                shotBullet.transform.rotation = transform.rotation;
                ammo--;
            }
        }
        else if (ammo < maxAmmo)
        {
            ammoRegenTimer += Time.deltaTime;

            if (ammoRegenTimer >= ammoRegenTime)
            {
                ammo++;
                ammoRegenTimer = 0;
            }
        }
        else
        {
            ammoRegenTimer = 0;
        }
    }
}
