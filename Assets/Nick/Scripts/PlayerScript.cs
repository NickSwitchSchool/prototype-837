using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    static public float gameSpeed;
    public float mouseSentisivity;
    public float speed;
    public float dashTime;
    public float slowmoSpeed;
    float mouseHorizontal;
    float inAir;

    RaycastHit wallJumpLeft;
    RaycastHit wallJumpRight;
    RaycastHit onFloor;

    public Vector3 rotation;
    Vector3 movement;

    public GameObject cam;
    public GameObject footStep;

    public Rigidbody playerRB;

    public int hp;
    int jumps;

    public bool isDead;
    public bool isDashing;
    bool wallrunning;
    bool soundsStarted;

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
            rotation.y += mouseHorizontal * mouseSentisivity * gameSpeed;
            transform.eulerAngles = rotation;
            if (dashTime < .3f)
            {
                dashTime += Time.deltaTime / 20;
            }

            if (Input.GetButtonDown("Jump") && jumps > 0 && !Input.GetButton("SlowMotion"))
            {
                jumps -= 1;
                playerRB.AddForce(transform.up * 1000);
            }
            else if (Input.GetButtonDown("Jump") && jumps > 0 && Input.GetButton("SlowMotion"))
            {
                jumps -= 1;
                playerRB.AddForce(transform.up * 500);
            }

            if (Input.GetButtonDown("Dash") && dashTime >= 0.3f)
            {
                isDashing = true;
            }

            if (isDashing == true)
            {
                dashTime -= Time.deltaTime * gameSpeed;
                playerRB.AddForce(transform.forward * 300 * gameSpeed);
                if (dashTime < 0)
                {
                    isDashing = false;
                }
            }

            if (Input.GetButton("SlowMotion"))
            {
                gameSpeed = slowmoSpeed;
            }
            else
            {
                gameSpeed = 1;
            }
        }

        //movesound
        if (playerRB.velocity.y == 0 && movement.z == 1 && soundsStarted == false)
        {
            soundsStarted = true;
            StartCoroutine(FootSteps());
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
                        cam.GetComponent<CamScript>().rotation.z += 135 * Time.deltaTime * gameSpeed;

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
                        cam.GetComponent<CamScript>().rotation.z -= 135 * Time.deltaTime * gameSpeed;

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
                playerRB.AddForce(Physics.gravity * 5f * gameSpeed);
                if (cam.GetComponent<CamScript>().rotation.z > 2)
                {
                    cam.GetComponent<CamScript>().rotation.z -= 135 * Time.deltaTime * gameSpeed;
                }
                else if (cam.GetComponent<CamScript>().rotation.z < -2)
                {
                    cam.GetComponent<CamScript>().rotation.z += 135 * Time.deltaTime * gameSpeed;

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
        Vector3 moveVector = transform.TransformDirection(movement) * speed * Time.deltaTime * gameSpeed;
        playerRB.velocity = new Vector3(moveVector.x, playerRB.velocity.y, moveVector.z);
    }

    IEnumerator FootSteps()
    {
        Instantiate(footStep, transform.position, Quaternion.identity);
        yield return new WaitForSeconds(0.2f);
        if (movement.z < 1 || playerRB.velocity.y <= 1 || playerRB.velocity.y >= 1)
        {
            soundsStarted = false;
        }
        else
        {
            StartCoroutine(FootSteps());
        }
    }
}
