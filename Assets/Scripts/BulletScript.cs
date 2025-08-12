using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    private float bulletSpeed = 50;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.position += bulletSpeed * Time.deltaTime * transform.right;

    }

    private void OnCollisionEnter(Collision collision)
    {
        EnemyScript health = collision.gameObject.GetComponent<EnemyScript>();
        if (health != null)
        {
            health.takeDamage(10);
        }

        Destroy(this.gameObject, 3f);
    }
}
