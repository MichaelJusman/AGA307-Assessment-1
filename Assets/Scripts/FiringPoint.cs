using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FiringPoint : MonoBehaviour
{

    public GameObject projectilePrefab;         //The projectile
    public float projectileSpeed = 1000;        //The speed of projectile
    public Transform firingPoint;               //The point the projectile spawn

    private void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            GameObject projectileInstance = Instantiate(projectilePrefab, firingPoint.position, firingPoint.rotation);

            projectileInstance.GetComponent<Rigidbody>().AddForce(firingPoint.forward * projectileSpeed);

            Destroy(projectileInstance, 5);

        }
    }



}
