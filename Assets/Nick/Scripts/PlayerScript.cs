using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public float hp;
    public float mouseSentisivity;
    public float speed;
    public float dashTime;
    float mouseHorizontal;
    float inAir;

    RaycastHit wallJumpLeft;
    RaycastHit wallJumpRight;
    RaycastHit onFloor;

    public Vector3 rotation;
    Vector3 movement;

    public GameObject cam;

    public Rigidbody playerRB;

    int jumps;

    public bool isDead;
    bool wallrunning;
    public bool isDashing;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //die
        if (hp <= 0)
        {
            isDead = true;
        }

        if (isDead == false)
        {
            //rotation
            mouseHorizontal = Input.GetAxis("Mouse X");
            rotation.y += mouseHorizontal * mouseSentisivity;
            transform.eulerAngles = rotation;
            if (dashTime < .2f)
            {
                dashTime += Time.deltaTime / 40;
            }

            if (Input.GetButtonDown("Jump") && jumps > 0)
            {
                jumps -= 1;
                playerRB.AddForce(transform.up * 1000);
            }

            if (Input.GetButtonDown("Sprint") && dashTime >= .2f)
            {
                isDashing = true;
            }

            if (isDashing == true)
            {
                dashTime -= Time.deltaTime;
                playerRB.AddForce(transform.forward * 300);
                if (dashTime < 0)
                {
                    isDashing = false;
                }
            }
        }
    }

    private void FixedUpdate()
    {
        if (isDead == false)
        {
            //movement
            movement.x = Input.GetAxis("Horizontal");
            movement.z = Input.GetAxis("Vertical");
            MovePlayer();

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
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject)
        {
            inAir = 0;
            jumps = 2;
        }
    }
    void MovePlayer()
    {
        Vector3 moveVector = transform.TransformDirection(movement) * speed * Time.deltaTime;
        playerRB.velocity = new Vector3(moveVector.x, playerRB.velocity.y, moveVector.z);
    }
}
