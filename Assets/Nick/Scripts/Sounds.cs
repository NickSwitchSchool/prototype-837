using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sounds : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<AudioSource>().pitch = PlayerScript.gameSpeed;
        if (GetComponent<AudioSource>().time >= GetComponent<AudioSource>().clip.length)
        {
            Destroy(gameObject);
        }
    }
}
