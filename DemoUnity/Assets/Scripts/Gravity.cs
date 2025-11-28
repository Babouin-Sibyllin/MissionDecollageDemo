using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Gravity : MonoBehaviour
{

    public float gravityStrength = 9.81f;

    public Material SpaceSkybox;

    public Material EarthSkybox;

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
    }

    private void OnTriggerStay(Collider other)
    {
        Rigidbody rb = other.attachedRigidbody;

        if (rb != null)
        {
            // Apply custom gravity (downward)
            rb.AddForce(Vector3.down * gravityStrength, ForceMode.Acceleration);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
