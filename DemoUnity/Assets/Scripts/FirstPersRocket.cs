using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class FirstPersRocket : MonoBehaviour
{


    public Image fadeImage;

    private float targetFadeAlpha = 1f;

    public float fadeDuration = 0.5f;

    public Slider mySlider;

    public float LeftReactorValue = 0;

    public float MainReactorValue = 0;

    public float RightReactorValue = 0;



    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(FadeToTransparent());
    }

    // Update is called once per frame
    void Update()
    {
        LeftReactorValue = mySlider.value;
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
    }
}
