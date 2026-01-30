using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class FirstPersRocket : MonoBehaviour
{


    public Image fadeImage;

    public float fadeDuration = 0.5f;

    public Slider RightSlider;

    public Slider MainSlider;

    public Slider LeftSlider;

    public float LeftReactorValue = 0;

    public float MainReactorValue = 0;

    public float RightReactorValue = 0;

    public float ReactorForce = 0;

    public bool DamagedMainReactor = false;

    public RandomEvents RandomEvents;





    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(FadeToTransparent());
    }

    // Update is called once per frame
    void Update()
    {
        if (!DamagedMainReactor)
        {
            MainReactorValue = MainSlider.value;
        } else
        {
            MainReactorValue = 0;
        }


        LeftReactorValue = LeftSlider.value;
        RightReactorValue = RightSlider.value;
        

        ReactorForce = (MainReactorValue + RightReactorValue + LeftReactorValue)*40;

        transform.Translate(Vector3.forward * ReactorForce * Time.deltaTime);
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
