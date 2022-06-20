using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamScript : MonoBehaviour
{
    float mouseVertical;

    public Vector3 rotation;

    public GameObject player;

    // Update is called once per frame
    void Update()
    {
        if (player.GetComponent<PlayerScript>().isDead == false)
        {
            //look up and down
            mouseVertical = Input.GetAxis("Mouse Y");
            rotation.x -= mouseVertical * player.GetComponent<PlayerScript>().mouseSentisivity * PlayerScript.gameSpeed;
            rotation.y = player.GetComponent<PlayerScript>().rotation.y;
            transform.eulerAngles = rotation;

            if (player.GetComponent<PlayerScript>().isDashing == true && GetComponent<Camera>().fieldOfView < 120)
            {
                GetComponent<Camera>().fieldOfView += 180 * Time.deltaTime * PlayerScript.gameSpeed;
            }
            else if (Input.GetAxis("Vertical") >= .5f && GetComponent<Camera>().fieldOfView > 75)
            {
                GetComponent<Camera>().fieldOfView -= 60 * Time.deltaTime * PlayerScript.gameSpeed;
            }
            else if (Input.GetAxis("Vertical") >= .5f && GetComponent<Camera>().fieldOfView < 75)
            {
                GetComponent<Camera>().fieldOfView += 60 * Time.deltaTime * PlayerScript.gameSpeed;
            }
            else if (Input.GetAxis("Vertical") < .5f && GetComponent<Camera>().fieldOfView > 60)
            {
                GetComponent<Camera>().fieldOfView -= 120 * Time.deltaTime * PlayerScript.gameSpeed;
            }
        }
    }
}
