using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.Rendering;

public class EnemyScript : MonoBehaviour
{
    private GameObject player; // victim
    private Transform target; // victim's transform
    private Animator enemy;
    
    Rigidbody enemyRB;
    Rigidbody targetRB;

    float speed = 4.5f;
    float strength = 15;
    float distance = 15;

    int EHP = 50;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            target = player.GetComponent<Transform>();
            targetRB = player.GetComponent<Rigidbody>();
        }

        enemy = GetComponentInChildren<Animator>();
        enemyRB = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, target.position) <= distance) //Vector3.Distance(transform.position, target.position) >= 4
        {
            Vector3 targetDirection = transform.position - target.position;
            transform.rotation = Quaternion.LookRotation(-targetDirection, Vector3.up);
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
            enemy.SetBool("IsWalking", true);
        }
        else
        {
            enemy.SetBool("IsWalking", false);
        }

        if (Vector3.Distance(transform.position, target.position) <= 4)
        {
            enemy.SetTrigger("Attack");
        }

    }

    internal void takeDamage(int damage)
    {
        EHP -= damage;
        if (EHP <= 0)
        {
            enemy.SetTrigger("Die");
            Destroy(gameObject, 5f);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            enemy.ResetTrigger("Attack");
            enemyRB.AddRelativeForce(transform.forward * strength, ForceMode.VelocityChange);

            PlayerScript health = collision.gameObject.GetComponent<PlayerScript>();
            if (health != null) 
            {
                health.takeDamage(10);
            }
        }
    }
}