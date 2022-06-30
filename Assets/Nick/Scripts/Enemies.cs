using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemies : MonoBehaviour
{
    public int hp;

    private void Update()
    {
        if (hp <= 0)
        {
            Destroy(gameObject);
        }
    }
}
