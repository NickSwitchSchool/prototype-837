using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gunman : MonoBehaviour
{
    public GameObject bullet;

    public Transform player;
    public Transform bulletLocation;

    public float range;
    public float fireFreq;
    float distance;
    float fireDelay;

    Vector3 direction;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        distance = Vector3.Distance(player.position, transform.position);
        if (distance <= range)
        {
            gameObject.transform.LookAt(player);
            fireDelay += Time.deltaTime * PlayerScript.gameSpeed;

            if (fireDelay >= fireFreq)
            {
                fireDelay = 0;
                GameObject firedBullet = Instantiate(bullet, bulletLocation.position, Quaternion.identity);
                firedBullet.GetComponent<Transform>().LookAt(player);
            }
        }
    }
}
