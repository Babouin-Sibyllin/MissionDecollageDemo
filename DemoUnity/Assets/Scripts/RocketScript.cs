using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using extOSC;

public class RocketScript : MonoBehaviour
{
    public OSCReceiver oscReceiver;
    public OSCTransmitter oscTransmiter;

    public GameObject Rocket;
    public GameObject Camera;

    public ParticleSystem LeftFire;
    public ParticleSystem MainFire;
    public ParticleSystem RightFire;
    public ParticleSystem LeftPivot;
    public ParticleSystem RightPivot;

    public Slider LeftSlider;
    public Slider RightSlider;
    public Slider MainSlider;
    public Slider PivotSlider;

    public float MainReactorForce = 4f;
    public float BaseReactorForce = 10f;
    public float SideReactorForce = 10f;
    public float rotationSpeed = 90f;

    private Rigidbody rb;

    [SerializeField] float torqueForce = 0.2f;
    [SerializeField] float torqueDamp = 0.98f;

    public Gravity GravityScript;

    // --- SEUILS ---
    [Header("Thresholds")]
    public float mainIgnitionThreshold = 0.05f;
    public float sideIgnitionThreshold = 0.03f;
    public float pivotDeadZone = 0.2f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Physics.gravity = new Vector3(0f, -9.81f, 0f);

        // --- OSC BINDINGS ---
        oscReceiver.Bind("/faderGauche", OnFaderGauche);
        oscReceiver.Bind("/faderCentre", OnFaderCentre);
        oscReceiver.Bind("/faderDroit", OnFaderDroit);
    }

    public static float Proportion(float value, float inputMin, float inputMax, float outputMin, float outputMax)
    {
        return Mathf.Clamp(
            ((value - inputMin) / (inputMax - inputMin)) * (outputMax - outputMin) + outputMin,
            outputMin,
            outputMax
        );
    }

    public void OnFaderGauche(OSCMessage message)
    {
        LeftSlider.value = Mathf.Clamp01(message.Values[0].IntValue / 4095f);
    }

    public void OnFaderCentre(OSCMessage message)
    {
        MainSlider.value = Mathf.Clamp01(message.Values[0].IntValue / 4095f);
    }

    public void OnFaderDroit(OSCMessage message)
    {
        RightSlider.value = Mathf.Clamp01(message.Values[0].IntValue / 4095f);
    }

    void FixedUpdate()
    {
        // --- INPUTS BRUTS ---
        float mainInput = MainSlider.value;   // 0..1
        float leftInput = LeftSlider.value;
        float rightInput = RightSlider.value;
        float pivotInput = PivotSlider.value;

        // --- VALEURS CONVERTIES ---
        float mainForce = Proportion(mainInput, 0f, 1f, 0f, 9f);
        float leftForce = Proportion(leftInput, 0f, 1f, 0f, 0.2f);
        float rightForce = Proportion(rightInput, 0f, 1f, 0f, 0.2f);
        float pivotForce = Proportion(pivotInput, 0f, 1f, -0.6f, 0.6f);

        // --- CAMERA STABILISÉE ---
        Quaternion invertedRotation = Quaternion.Inverse(rb.transform.localRotation);
        Camera.transform.localRotation = Quaternion.RotateTowards(
            Camera.transform.localRotation,
            invertedRotation,
            rotationSpeed * Time.deltaTime
        );

        // =========================
        //      MAIN REACTOR
        // =========================
        if (mainInput > mainIgnitionThreshold)
        {
            rb.AddForce(transform.forward * mainForce, ForceMode.Acceleration);
            SetFire(MainFire, true);
        }
        else
        {
            SetFire(MainFire, false);
        }

        // =========================
        //      LEFT REACTOR
        // =========================
        if (leftInput > sideIgnitionThreshold)
        {
            rb.AddForce(transform.forward * leftForce * 20f, ForceMode.Acceleration);
            rb.AddTorque(Vector3.forward * -leftForce, ForceMode.Acceleration);
            SetFire(LeftFire, true);
        }
        else
        {
            SetFire(LeftFire, false);
        }

        // =========================
        //      RIGHT REACTOR
        // =========================
        if (rightInput > sideIgnitionThreshold)
        {
            rb.AddForce(transform.forward * rightForce * 20f, ForceMode.Acceleration);
            rb.AddTorque(Vector3.forward * rightForce, ForceMode.Acceleration);
            SetFire(RightFire, true);
        }
        else
        {
            SetFire(RightFire, false);
        }

        // =========================
        //          PIVOT
        // =========================
        if (pivotForce > pivotDeadZone)
        {
            rb.AddTorque(Vector3.forward * pivotForce, ForceMode.Acceleration);
            SetFire(RightPivot, true);
            SetFire(LeftPivot, false);
        }
        else if (pivotForce < -pivotDeadZone)
        {
            rb.AddTorque(Vector3.forward * pivotForce, ForceMode.Acceleration);
            SetFire(LeftPivot, true);
            SetFire(RightPivot, false);
        }
        else
        {
            SetFire(LeftPivot, false);
            SetFire(RightPivot, false);
        }

        rb.angularVelocity *= torqueDamp;
    }

    // --- SAFE PARTICLE CONTROL ---
    void SetFire(ParticleSystem ps, bool active)
    {
        if (active && !ps.isPlaying) ps.Play();
        if (!active && ps.isPlaying) ps.Stop();
    }
}
