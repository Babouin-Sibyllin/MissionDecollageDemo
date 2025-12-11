using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Gravity : MonoBehaviour
{

    public float gravityStrength = 35f;

    public Material SpaceSkybox;

    public Material EarthSkybox;

    public Material MarsSkybox;

    public static bool MarsReached = false;


    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && CompareTag("Space"))
        {
            RenderSettings.skybox = SpaceSkybox;
            DynamicGI.UpdateEnvironment();
            Debug.Log("espace");
            gravityStrength = 0f;
        }

        if (other.CompareTag("Player") && CompareTag("Earth"))
        {
            RenderSettings.skybox = EarthSkybox;
            DynamicGI.UpdateEnvironment();
            Debug.Log("Terre");
            gravityStrength = 9.81f;
        }

        if (other.CompareTag("Player") && CompareTag("Mars"))
        {
            RenderSettings.skybox = MarsSkybox;
            DynamicGI.UpdateEnvironment();
            Debug.Log("Mars");
            gravityStrength = -9.81f;
            MarsReached = true;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        Rigidbody rb = other.attachedRigidbody;

        if (rb != null)
        {
            
            rb.AddForce(Vector3.down * gravityStrength, ForceMode.Acceleration);
        }
    }

    void Start()
    {
        
    }


    void Update()
    {
        
    }
}
