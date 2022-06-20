using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{ 
    // Update is called once per frame
    void Update()
    {
        transform.position += transform.forward * 55 * Time.deltaTime * PlayerScript.gameSpeed;
    }
}
