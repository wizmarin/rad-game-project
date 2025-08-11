using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameObject victim;
    public Transform target;
    Animator enemy;
    Rigidbody rb;

    float speed = 4.5f;
    float distance = 15;

    // Start is called before the first frame update
    void Start()
    {
        enemy = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, target.position) <= distance && Vector3.Distance(transform.position, target.position) >= 4)
        {
            Vector3 targetDirection = transform.position - target.position;
            transform.rotation = Quaternion.LookRotation(-targetDirection, Vector3.up);
            transform.position = Vector3.MoveTowards(transform.position, victim.transform.position, speed * Time.deltaTime);
            enemy.SetBool("IsWalking", true);
        }
        else
        {
            enemy.SetBool("IsWalking", false);
        }

        if (Vector3.Distance(transform.position, target.position) <= 4)
        {
            rb.AddForce(Vector3.forward, ForceMode.Impulse);
            enemy.SetTrigger("Attack");
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        enemy.ResetTrigger("Attack");
    }
}