using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public GameObject[] weaponNames;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Debug.Log(weaponNames[0]);

            
        }
        
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            Debug.Log(weaponNames[1]);

            
        }
        
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            Debug.Log(weaponNames[2]);

            
        }

       
    }
}
