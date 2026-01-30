using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{


    public Zones zone;

    void OnTriggerEnter(Collider other)
{

    if (other.CompareTag("Player") && gameObject.CompareTag("Obstacles"))
    {
        Debug.Log("explosion");

        if (zone != null)
        {
            zone.TriggerExplosion();
        }
        else
        {
            Debug.LogWarning("Zones reference not assigned in Inspector!");
        }
    }
}
}

