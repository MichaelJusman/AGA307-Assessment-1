using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WeaponType
{
    Bullet,
    Cannon,
    Laser
}
public class FiringPoint : MonoBehaviour
{
    
   
    
    
    public GameObject projectilePrefab;         //The projectile
    public float projectileSpeed = 1000;        //The speed of projectile
    public Transform firingPoint;               //The point the projectile spawn
    public LineRenderer laser;
    public GameObject hitSparks;
    public WeaponType weaponType;
   

   //void Setup()
   // {
   //     switch(weaponType)
   //     {
   //         case WeaponType.Cannon:
   //             if (Input.GetButtonDown("Fire1"))
   //             {
   //                 FireCannon();

   //             }
   //     }
   // }


    private void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            FireCannon();

        }

        if (Input.GetButtonDown("Fire2"))
        {
            FireRaycast();
        }
    }

    void FireCannon()
    {
        GameObject projectileInstance = Instantiate(projectilePrefab, firingPoint.position, firingPoint.rotation);

        projectileInstance.GetComponent<Rigidbody>().AddForce(firingPoint.forward * projectileSpeed);

        Destroy(projectileInstance, 5);
    }

    void FireRaycast()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;

        if(Physics.Raycast(ray,out hit, Mathf.Infinity))
        {
            laser.SetPosition(0, transform.position);
            laser.SetPosition(1, hit.point);
            GameObject party = Instantiate(hitSparks, hit.point, hit.transform.rotation);

                Destroy(party, 2);
        }
    }
}
