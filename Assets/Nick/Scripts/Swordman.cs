using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Swordman : MonoBehaviour
{
    NavMeshAgent agent;

    public Transform player;

    public float range;
    public float attackRange;
    public float attackFreq;
    float distance;
    float attackDelay;

    public bool triggered;

    public RaycastHit hit;

    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

        distance = Vector3.Distance(player.position, transform.position);
        if (distance <= range && distance > attackRange)
        {
            agent.destination = player.position + -transform.forward * 1;
            animator.SetInteger("Action", 1);
            triggered = true;
        }
        
        if (distance <= attackRange + 1)
        {
            gameObject.transform.LookAt(player);
            attackDelay += Time.deltaTime * PlayerScript.gameSpeed;

            if (attackDelay >= attackFreq + Random.Range(0f, 2f))
            {
                if (animator.GetInteger("Action") != 2 && animator.GetInteger("Action") != 3)
                {
                    animator.SetInteger("Action", 2);
                }
                else
                {
                    animator.SetInteger("Action", 3);
                }

                if (Physics.Raycast(transform.position, transform.forward, out hit, attackRange))
                {
                    if (hit.transform.tag == "Player")
                    {
                        hit.transform.gameObject.GetComponent<PlayerScript>().hp -= 1;
                    }
                }
                attackDelay = 0;
            }
        }

        if (distance > range)
        {
            animator.SetInteger("Action", 0);
            agent.destination = transform.position;
            triggered = false;
        }
        agent.speed = PlayerScript.gameSpeed * 3.5f;
    }
}
