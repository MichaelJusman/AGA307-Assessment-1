using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Target"))
        {
            collision.collider.GetComponent<Renderer>().material.color = Color.red;

            Destroy(collision.collider.gameObject, 1f);

            Destroy(gameObject);
        }
        
        
        
       
   

    }




}
