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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (paused == false)
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
            movement.x = Input.GetAxis("Horizontal");
            movement.z = Input.GetAxis("Vertical");
            transform.Translate(movement * speed * Time.deltaTime);
        }
    }
}
