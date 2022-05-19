using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public float walkspeed;
    public float runspeed;
    public float dashspeed;
    public float mouseSentisivity;
    float speed;
    float mouseHorizontal;

    RaycastHit wallJumpLeft;
    RaycastHit wallJumpRight;
    RaycastHit onFloor;

    public Vector3 rotation;
    Vector3 movement;

    public GameObject cam;

    public Rigidbody playerRB;

    public int jumps;

    public bool wallrunning;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //rotation
        mouseHorizontal = Input.GetAxis("Mouse X");
        rotation.y += mouseHorizontal * mouseSentisivity;
        transform.eulerAngles = rotation;

        //sprinting
        if (Input.GetButton("Sprint"))
        {
            speed = runspeed;
        }
        else
        {
            speed = walkspeed;
        }

        //movement
        movement.x = Input.GetAxis("Horizontal") * 3000;
        movement.z = Input.GetAxis("Vertical") * 5000;
        if (playerRB.velocity.magnitude < speed)
        {
            playerRB.AddForce(transform.forward * movement.z * Time.deltaTime);
            playerRB.AddForce(transform.right * movement.x * Time.deltaTime);
        }

        if (Input.GetButtonDown("Jump") && jumps > 0)
        {
            jumps -= 1;
            playerRB.AddForce(transform.up * 1000);
        }
    }

    private void FixedUpdate()
    {
        //gravity
        {
            playerRB.AddForce(Physics.gravity / 2);
        }

        //wallrun
        if (Physics.Raycast(transform.position, transform.right, out wallJumpRight, .5f))
        {
            if (wallJumpRight.transform.gameObject.tag == "Walljumpable")
            {
                wallrunning = true;
                if (cam.GetComponent<CamScript>().rotation.z < 45)
                {
                    cam.GetComponent<CamScript>().rotation.z += 135 * Time.deltaTime;

                }
            }
            else
            {
                wallrunning = false;
            }
        }
        else if (Physics.Raycast(transform.position, -transform.right, out wallJumpRight, .5f))
        {
            if (wallJumpRight.transform.gameObject.tag == "Walljumpable")
            {
                wallrunning = true;
                if (cam.GetComponent<CamScript>().rotation.z > -45)
                {
                    cam.GetComponent<CamScript>().rotation.z -= 135 * Time.deltaTime;

                }
            }
            else
            {
                wallrunning = false;
            }
        }
        else
        {
            wallrunning = false;
        }

        if (wallrunning == false)
        {
            playerRB.AddForce(Physics.gravity * 5f);
            if (cam.GetComponent<CamScript>().rotation.z > 1)
            {
                cam.GetComponent<CamScript>().rotation.z -= 135 * Time.deltaTime;
            }
            else if (cam.GetComponent<CamScript>().rotation.z < -1)
            {
                cam.GetComponent<CamScript>().rotation.z += 135 * Time.deltaTime;

            }
        }
        else
        {
            jumps = 2;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject)
        {
            jumps = 2;
        }
    }
}
