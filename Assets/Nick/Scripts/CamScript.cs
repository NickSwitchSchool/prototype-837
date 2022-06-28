using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamScript : MonoBehaviour
{
    public float maxXRotation;
    float mouseVertical;

    public Vector3 rotation;

    public GameObject player;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Cursor.lockState = CursorLockMode.Locked;
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.Locked;
        }

        if (player.GetComponent<PlayerScript>().isDead == false)
        {//look up and down within limits
            mouseVertical = Input.GetAxis("Mouse Y");
            if (rotation.x < maxXRotation && rotation.x > -maxXRotation)
            {
                rotation.x -= mouseVertical * player.GetComponent<PlayerScript>().mouseSentisivity;
            }
            else if (rotation.x >= maxXRotation && mouseVertical > 0)
            {
                rotation.x -= mouseVertical * player.GetComponent<PlayerScript>().mouseSentisivity;
            }
            else if (rotation.x <= -maxXRotation && mouseVertical < 0)
            {
                rotation.x -= mouseVertical * player.GetComponent<PlayerScript>().mouseSentisivity;
            }

            rotation.y = player.GetComponent<PlayerScript>().rotation.y;
            transform.eulerAngles = rotation;

            ////look up and down
            //mouseVertical = Input.GetAxis("Mouse Y");
            //rotation.x -= mouseVertical * player.GetComponent<PlayerScript>().mouseSentisivity * PlayerScript.gameSpeed;
            //rotation.y = player.GetComponent<PlayerScript>().rotation.y;
            //transform.eulerAngles = rotation;

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
