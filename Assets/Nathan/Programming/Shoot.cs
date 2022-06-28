using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    public GameObject bullet;
    public GameObject bulletSound;
    public float fireRate;
    float nextFire;
    public int maxAmmo;
    public int ammo;
    public float ammoRegenTime;
    float ammoRegenTimer;
    public Animator arms;

    void Start()
    {
        
    }
    void Update()
    {

        if (Input.GetButtonDown("Fire2") && ammo > 0)
        {
            arms.SetInteger("Action", 1);
            StartCoroutine(StopAnimation());
            ammoRegenTimer = 0;
            if (Time.time > nextFire)
            {
                nextFire = Time.time + fireRate;
                print("shoot");
                Instantiate(bulletSound, transform.position, Quaternion.identity);
                GameObject shotBullet = Instantiate(bullet, transform.position, Quaternion.identity);
                shotBullet.transform.rotation = transform.rotation;
                shotBullet.GetComponent<BulletScript>().playerShot = true;
                ammo--;
            }
        }
        else if (ammo < maxAmmo)
        {
            ammoRegenTimer += Time.deltaTime * PlayerScript.gameSpeed;

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

    IEnumerator StopAnimation()
    {
        yield return new WaitForSeconds(0.2f);
        arms.SetInteger("Action", 0);
    }
}
