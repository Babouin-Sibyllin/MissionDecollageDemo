using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class Gravity : MonoBehaviour
{

    public float gravityStrength = 35f;

    public Material SpaceSkybox;

    public Material EarthSkybox;

    public Material MarsSkybox;

    public Camera cam;

    public float targetFOV = 10f;
    
    public float zoomSmoothness = 20f;

    public static bool MarsReached = false;

    private bool SpaceReached = false;

    private float targetFadeAlpha = 0f;

    public float fadeSpeed = 0.5f;

    public Image fadeImage;

    private bool isLoadingScene = false;



    void OnTriggerEnter(Collider other)
{
    if (!other.CompareTag("Player")) return;

    // Check what this trigger actually is
    if (gameObject.CompareTag("Space"))
    {
        RenderSettings.skybox = SpaceSkybox;
        DynamicGI.UpdateEnvironment();
        Debug.Log("Espace");
        gravityStrength = 0f;
        targetFOV = 5f;
        SpaceReached = true;
        if (!isLoadingScene)
            StartCoroutine(FadeAndLoadScene());
    }

    if (gameObject.CompareTag("Earth"))
    {
        RenderSettings.skybox = EarthSkybox;
        DynamicGI.UpdateEnvironment();
        Debug.Log("Terre");
        gravityStrength = 9.81f;
    }

    if (gameObject.CompareTag("Mars"))
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
        if (cam == null)
        cam = Camera.main;
    }

    IEnumerator FadeAndLoadScene()
{
    isLoadingScene = true;

    fadeImage.gameObject.SetActive(true);
    targetFadeAlpha = 1f;

    // Wait until fully black
    while (fadeImage.color.a < 0.99f)
    {
        yield return null;
    }

    SceneManager.LoadScene("Space");
}


    void Update()
    {
    if (!SpaceReached) return;

    cam.fieldOfView = Mathf.MoveTowards(
        cam.fieldOfView,
        targetFOV,
        zoomSmoothness * Time.deltaTime
    );

    if (fadeImage != null)
    {
        Color c = fadeImage.color;
        c.a = Mathf.MoveTowards(c.a, targetFadeAlpha, fadeSpeed * Time.deltaTime);
        fadeImage.color = c;
    }
}
}
