using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    Animator player;
    Rigidbody body;

    float speed;
    float walkSpeed = 3;
    float runMultiplier = 3;
    float turningSpeed = 180;

    float jumpForce = 10;

    int CHP = 100;

    // Start is called before the first frame update
    void Start()
    {
        player = GetComponentInChildren<Animator>();
        body = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        player.SetBool("IsWalking", false);
        speed = 0;

        if (Input.GetKey(KeyCode.W))
        {
            speed = walkSpeed;
            player.SetBool("IsWalking", true);
        }
        if (Input.GetKey(KeyCode.S))
        {
            speed = -walkSpeed;
            player.SetBool("IsWalking", true);
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(Vector3.up, turningSpeed * Time.deltaTime);
            player.SetBool("IsWalking", true);
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(Vector3.up, -turningSpeed * Time.deltaTime);
            player.SetBool("IsWalking", true);
        }
        if (Input.GetKey(KeyCode.LeftShift))
        {
            speed *= runMultiplier;
            player.SetBool("IsRunning", true);
        }
        else
        {
            player.SetBool("IsRunning", false);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            body.AddForce(jumpForce * Vector3.up, ForceMode.VelocityChange);
            player.ResetTrigger("Land");
            player.SetTrigger("Jump");
        }
        else if (Input.GetKeyUp(KeyCode.Space))
        {
            player.ResetTrigger("Jump");
        }

        transform.position += speed * transform.forward * Time.deltaTime;
    }

    internal void takeDamage(int damage)
    {
        CHP -= damage;
        if (CHP <= 0)
        {
            player.SetTrigger("Die");
            Destroy(gameObject, 5f);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Floor")
        {
            player.SetTrigger("Land");
        }
    }
}
