using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WeaponType
{
    Bullet,
    Cannon,
    Pelt,
    Laser
}
public class FiringPoint : MonoBehaviour
{

    public GameObject[] weaponNames;

    public GameObject projectilePrefab;         //The projectile
    public float projectileSpeed = 1000;        //The speed of projectile
    public Transform firingPoint;               //The point the projectile spawn
    public LineRenderer laser;
    public GameObject hitSparks;
    public WeaponType weaponType;

    [Header("Bullet")]
    public float bulletDamage = 10;
    public float bulletSpeed = 10000;

    [Header("Cannon")]
    public float cannonDamage = 100;
    public float cannonSpeed = 1000;

    [Header("Pelt")]
    public float peltDamage = 30;
    public float peltSpeed = 5000;


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Debug.Log(weaponNames[0]);
            weaponType = WeaponType.Bullet;
        }
        
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            Debug.Log(weaponNames[1]);
            weaponType = WeaponType.Cannon;
        }
        
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            Debug.Log(weaponNames[2]);
            weaponType = WeaponType.Pelt;
        }

        if (Input.GetButtonDown("Fire2"))
        {
            FireRaycast();
        }
        
        if (Input.GetButtonDown("Fire1"))
        {
            switch (weaponType)
            {
                case WeaponType.Cannon:
                    FireCannon();
                    break;
                case WeaponType.Pelt:
                    FirePelt();
                    break;
            }
        }
        if (Input.GetButton("Fire1"))
        {
            switch (weaponType)
            {
                case WeaponType.Bullet:
                    FireBullet();
                    break;
            }
        }

    }

    void FireBullet()
    {
        GameObject projectileInstance = Instantiate(weaponNames[0], firingPoint.position, firingPoint.rotation);

        projectileInstance.GetComponent<Rigidbody>().AddForce(firingPoint.forward * bulletSpeed);

        Destroy(projectileInstance, 2);
    }

    void FireCannon()
    {
        GameObject projectileInstance = Instantiate(weaponNames[1], firingPoint.position, firingPoint.rotation);

        projectileInstance.GetComponent<Rigidbody>().AddForce(firingPoint.forward * cannonSpeed);

        Destroy(projectileInstance, 5);
    }

    void FirePelt()
    {
        GameObject projectileInstance = Instantiate(weaponNames[2], firingPoint.position, firingPoint.rotation);

        projectileInstance.GetComponent<Rigidbody>().AddForce(firingPoint.forward * peltSpeed);

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
