using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public bool playerShot;

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.forward * 55 * Time.deltaTime * PlayerScript.gameSpeed;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Player" && playerShot == false)
        {
            Debug.Log("Nub");
            other.gameObject.GetComponent<PlayerScript>().hp -= 1;
        }
        else if (other.gameObject.tag == "Enemy" && playerShot == true)
        {
            Debug.Log("PewPew");
            other.gameObject.GetComponent<Enemies>().hp -= 1;
        }
    }
}
