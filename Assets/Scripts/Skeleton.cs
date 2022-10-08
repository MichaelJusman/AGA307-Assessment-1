using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton : MonoBehaviour
{
    [SerializeField] private float _maxHealth = 3;
    [SerializeField] private GameObject projectile;
    private float _currentHealth;
    



    void Start()
    {
        _currentHealth = _maxHealth;
    }


    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.CompareTag("Projectile"))
        {
            _currentHealth -= 1f;
        }
    }

    private void Update()
    {
        if(_currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }
}
