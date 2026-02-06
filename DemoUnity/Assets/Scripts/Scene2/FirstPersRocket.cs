using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using extOSC;

public class FirstPersRocket : MonoBehaviour
{
    // ======================
    //         OSC
    // ======================
    [Header("OSC")]
    public OSCReceiver oscReceiver;

    // ======================
    //         UI
    // ======================
    public Image fadeImage;
    public float fadeDuration = 0.5f;

    public Slider RightSlider;
    public Slider MainSlider;
    public Slider LeftSlider;

    // ======================
    //        OBJECTS
    // ======================
    public GameObject RocketObject;
    public GameObject MarsObject;

    // ======================
    //        VALUES
    // ======================
    public float LeftReactorValue = 0;
    public float MainReactorValue = 0;
    public float RightReactorValue = 0;

    public float ReactorForce = 0;
    public float currentRotationSpeed = 0;

    // ======================
    //        DAMAGE
    // ======================
    public bool DamagedMainReactor = false;
    public bool DamagedRightReactor = false;
    public bool DamagedLeftReactor = false;

    public float rotationEaseSpeed = 20;

    // ======================
    //         UI TEXT
    // ======================
    public TextMeshProUGUI vitesseUI;
    public TextMeshProUGUI DistanceUI;

    public RandomEvents RandomEvents;

    // ======================
    //         AUDIO
    // ======================
    [Header("Audio")]
    public AudioSource spaceAmbience;

    [Header("Propulseur Audio")]
    public AudioSource propulseurStart; // ignition one-shot
    public AudioSource propulseurLoop;  // looping thrust sound

    // --- thresholds ---
    public float mainIgnitionThreshold = 0.05f;
    public float sideIgnitionThreshold = 0.03f;

    // --- reactor states ---
    bool mainActive;
    bool leftActive;
    bool rightActive;

    bool isPropulseurActive = false;

    // ======================
    //        START
    // ======================
    void Start()
    {
        StartCoroutine(FadeToTransparent());

        oscReceiver.Bind("/faderGauche", OnFaderGauche);
        oscReceiver.Bind("/faderCentre", OnFaderCentre);
        oscReceiver.Bind("/faderDroit", OnFaderDroit);

        // --- SPACE AMBIENCE ---
        if (spaceAmbience != null && !spaceAmbience.isPlaying)
        {
            spaceAmbience.loop = true;
            spaceAmbience.Play();
        }
    }

    // ======================
    //      OSC CALLBACKS
    // ======================
    void OnFaderGauche(OSCMessage message)
    {
        LeftSlider.value = Mathf.Clamp01(message.Values[0].IntValue / 4095f);
    }

    void OnFaderCentre(OSCMessage message)
    {
        MainSlider.value = Mathf.Clamp01(message.Values[0].IntValue / 4095f);
    }

    void OnFaderDroit(OSCMessage message)
    {
        RightSlider.value = Mathf.Clamp01(message.Values[0].IntValue / 4095f);
    }

    // ======================
    //        UPDATE
    // ======================
    void Update()
    {
        float zValueRocket = RocketObject.transform.position.z;
        float zValueMars = MarsObject.transform.position.z;
        float distanceRocketMars = zValueRocket - zValueMars;

        // --- UI ---
        vitesseUI.text = "Vitesse actuelle: " + ReactorForce.ToString("F1");
        DistanceUI.text = "distance restante: " + (distanceRocketMars + 2000);

        // --- DAMAGE LOGIC ---
        MainReactorValue = DamagedMainReactor ? 0 : MainSlider.value;
        RightReactorValue = DamagedRightReactor ? 0 : RightSlider.value;
        LeftReactorValue = DamagedLeftReactor ? 0 : LeftSlider.value;

        // --- ROTATION ---
        currentRotationSpeed = Mathf.Lerp(
            currentRotationSpeed,
            LeftReactorValue * 20f - RightReactorValue * 20f,
            rotationEaseSpeed * Time.deltaTime
        );

        // --- FORCE ---
        ReactorForce = (MainReactorValue + RightReactorValue + LeftReactorValue) * 40f;

        // --- MOVE ---
        transform.Translate(Vector3.forward * ReactorForce * Time.deltaTime, Space.Self);
        transform.Rotate(Vector3.up * currentRotationSpeed * Time.deltaTime, Space.Self);

        // ======================
        //   REACTOR STATES
        // ======================
        mainActive  = MainReactorValue  > mainIgnitionThreshold;
        leftActive  = LeftReactorValue  > sideIgnitionThreshold;
        rightActive = RightReactorValue > sideIgnitionThreshold;

        // ======================
        //   PROPULSEUR AUDIO
        // ======================
        bool allReactorsActive = mainActive && leftActive && rightActive;

        if (allReactorsActive)
        {
            if (!isPropulseurActive)
            {
                if (propulseurStart != null)
                    propulseurStart.Play();

                if (propulseurLoop != null && !propulseurLoop.isPlaying)
                    propulseurLoop.Play();

                isPropulseurActive = true;
            }
        }
        else
        {
            if (isPropulseurActive)
            {
                if (propulseurLoop != null)
                    propulseurLoop.Stop();

                isPropulseurActive = false;
            }
        }
    }

    // ======================
    //        FADE
    // ======================
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

        fadeImage.color = new Color(
            startColor.r,
            startColor.g,
            startColor.b,
            0f
        );

        fadeImage.gameObject.SetActive(false);
    }
}
