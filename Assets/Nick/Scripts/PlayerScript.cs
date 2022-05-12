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

    RaycastHit wallJump;
    RaycastHit onFloor;

    public Vector3 rotation;
    Vector3 movement;

    public bool paused;

    public Rigidbody player;

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
        movement.x = Input.GetAxis("Horizontal") * 3;
        movement.z = Input.GetAxis("Vertical") * 5;
        if (player.velocity.magnitude < speed)
        {
            player.AddForce(transform.forward * movement.z);
            player.AddForce(transform.right * movement.x);
        } 
    }
}
