using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamScript : MonoBehaviour
{
    float mouseVertical;

    Vector3 rotation;

    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //look up and down
        mouseVertical = Input.GetAxis("Mouse Y");
        rotation.x -= mouseVertical * player.GetComponent<PlayerScript>().mouseSentisivity;
        rotation.y = player.GetComponent<PlayerScript>().rotation.y;
        transform.eulerAngles = rotation;
    }
}
