using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public GameObject Door1;
    public GameObject Door2;
    
    
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            Door1.GetComponent<Collider>().enabled = false;
            Door2.GetComponent<Collider>().enabled = false;
            Door1.GetComponent<Renderer>().enabled = false;
            Door2.GetComponent<Renderer>().enabled = false;
        }
    }private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            Door1.GetComponent<Renderer>().enabled = true;
            Door2.GetComponent<Renderer>().enabled = true;
            Door1.GetComponent<Renderer>().enabled = true;
            Door2.GetComponent<Renderer>().enabled = true;
        }
    }
}
