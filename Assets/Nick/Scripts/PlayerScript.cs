using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{
    static public float gameSpeed;
    public float mouseSentisivity;
    public float speed;
    public float dashTime;
    public float slowmoSpeed;
    float mouseHorizontal;

    RaycastHit wallJump;
    RaycastHit hit;

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

    public Animator arms;

    public Image hpbar;

    public Sprite hp3;
    public Sprite hp2;
    public Sprite hp1;

    public Text ammo;

    // Update is called once per frame
    void Update()
    {
        Debug.Log(arms.GetInteger("Action"));

        //show hp and ammo
        ammo.text = $"{GetComponent<Shoot>().ammo} / {GetComponent<Shoot>().maxAmmo}";
        if (hp >= 3)
        {
            hpbar.sprite = hp3;
        }
        else if (hp == 2)
        {
            hpbar.sprite = hp2;
        }
        else if (hp == 1)
        {
            hpbar.sprite = hp1;
        }
        else
        {
            Destroy(hpbar);
            playerRB.constraints = RigidbodyConstraints.None;
        }

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


            if (Input.GetButtonDown("Fire1") && Physics.Raycast(transform.position, transform.forward, out hit, 2))
            {
                arms.SetInteger("Action", Random.Range(3, 5));
                StartCoroutine(AnimationFix());
                if (hit.transform.tag == "Enemy")
                {
                    hit.transform.gameObject.GetComponent<Enemies>().hp -= 1;
                }
            }
            else if (Input.GetButtonDown("Fire1"))
            {
                arms.SetInteger("Action", Random.Range(3, 5));
                StartCoroutine(AnimationFix());
            }
        }

        //movesound
        if (playerRB.velocity.y == 0 && movement.z >= .7 && soundsStarted == false)
        {
            arms.SetInteger("Action", 2);
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
            if (Physics.Raycast(transform.position, transform.right, out wallJump, .5f))
            {
                if (wallJump.transform.gameObject.tag == "Walljumpable")
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
            else if (Physics.Raycast(transform.position, -transform.right, out wallJump, .5f))
            {
                if (wallJump.transform.gameObject.tag == "Walljumpable")
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
        yield return new WaitForSeconds(0.2f / PlayerScript.gameSpeed);
        if (movement.z < .7f || playerRB.velocity.y <= 1 || playerRB.velocity.y >= 1)
        {
            arms.SetInteger("Action", 0);
            soundsStarted = false;
        }
        else
        {
            StartCoroutine(FootSteps());
        }
    }

    IEnumerator AnimationFix()
    {
        yield return new WaitForSeconds(0.01f);
        arms.SetInteger("Action", 0);
    }
}
