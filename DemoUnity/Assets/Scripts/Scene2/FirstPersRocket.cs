using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class FirstPersRocket : MonoBehaviour
{


    public Image fadeImage;

    public float fadeDuration = 0.5f;

    public Slider RightSlider;

    public Slider MainSlider;

    public Slider LeftSlider;

    public GameObject RocketObject;

    public GameObject MarsObject;

    public float LeftReactorValue = 0;

    public float MainReactorValue = 0;

    public float RightReactorValue = 0;

    public float ReactorForce = 0;

    public float currentRotationSpeed = 0;

    public bool DamagedMainReactor = false;

    public bool DamagedRightReactor = false;

    public bool DamagedLeftReactor = false;

    public float rotationEaseSpeed = 20;

    public TextMeshProUGUI vitesseUI;

    public TextMeshProUGUI DistanceUI;

    public RandomEvents RandomEvents;





    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(FadeToTransparent());
    }

    // Update is called once per frame
    void Update()
    {

        float zValueRocket = RocketObject.transform.position.z;
        float zValueMars = MarsObject.transform.position.z;
        float distanceRocketMars = (zValueRocket - zValueMars);

        //UI
        vitesseUI.text = "Vitesse actuelle: " + ReactorForce.ToString("F1");
        DistanceUI.text = "distance restante: " + (distanceRocketMars+ 2000);


        //calcul du ease
        currentRotationSpeed = Mathf.Lerp(
        currentRotationSpeed,
        LeftReactorValue*20,
        rotationEaseSpeed * Time.deltaTime
    );

    currentRotationSpeed = Mathf.Lerp(
        currentRotationSpeed,
        -RightReactorValue*20,
        rotationEaseSpeed * Time.deltaTime
    );

        //empêche les réacteurs de fonctionner en cas de bris (events)
        if (!DamagedMainReactor)
        {
            MainReactorValue = MainSlider.value;
        } else
        {
            MainReactorValue = 0;
        }

        if (!DamagedRightReactor)
        {
            RightReactorValue = RightSlider.value;
        } else
        {
            RightReactorValue = 0;
        }

        if (!DamagedLeftReactor)
        {
            LeftReactorValue = LeftSlider.value;
        } else
        {
            LeftReactorValue = 0;
        }
        

        ReactorForce = (MainReactorValue + RightReactorValue + LeftReactorValue)*40;

        transform.Translate(Vector3.forward * ReactorForce * Time.deltaTime);
        transform.Rotate(Vector3.up * currentRotationSpeed * Time.deltaTime);
        //transform.Translate(Vector3.forward * 25 * Time.deltaTime);
    }

    IEnumerator FadeToTransparent()
    {
        yield return new WaitForSeconds(2f);
        Color startColor = fadeImage.color;
        float t = 0f;

        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            float a = Mathf.Lerp(1f, 0f, t / fadeDuration);

            fadeImage.color = new Color(
                startColor.r,
                startColor.g,
                startColor.b,
                a
            );

            yield return null;
        }

        // Ensure fully transparent at the end
        fadeImage.color = new Color(
            startColor.r,
            startColor.g,
            startColor.b,
            0f
        );

        fadeImage.gameObject.SetActive(false);
    }
}
