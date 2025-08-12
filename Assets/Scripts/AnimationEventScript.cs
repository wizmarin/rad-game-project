using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class AnimationEventScript : MonoBehaviour
{
    public GameObject bulletClone;
    public Transform bulletSpawn;

    public void Shooting()
    {
        Instantiate(bulletClone, bulletSpawn.position, bulletSpawn.rotation);
    } 
}
