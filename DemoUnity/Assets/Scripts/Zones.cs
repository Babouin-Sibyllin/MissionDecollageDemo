using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zones : MonoBehaviour
{
    
    
    private void OnTriggerExit(Collider other)
{
    if (other.CompareTag("Player") && gameObject.CompareTag("GameZone"))
    {
        Debug.Log("Explosion");
    }

    if (other.CompareTag("Player") && gameObject.CompareTag("Earth"))
    {
        Debug.Log("Avertissement");
    }
}

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
