using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public GameObject Door1;
    
    
    
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            Door1.SetActive(false);
            
        }
    }private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            Door1.SetActive(true);
        }
    }
}
