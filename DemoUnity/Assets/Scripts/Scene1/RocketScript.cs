using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using extOSC;
 
public class RocketScript : MonoBehaviour
{
    public OSCReceiver oscReceiver;
    public OSCReceiver oscReceiverKey;
 
    public GameObject Rocket;
    public GameObject Camera;
    public GameObject panelUI;
 
    public ParticleSystem LeftFire;
    public ParticleSystem MainFire;
    public ParticleSystem RightFire;
    public ParticleSystem LeftPivot;
    public ParticleSystem RightPivot;
 
    public Slider LeftSlider;
    public Slider RightSlider;
    public Slider MainSlider;
    public Slider PivotSlider;
 
    public float rotationSpeed = 90f;
 
    private Rigidbody rb;
 
    [SerializeField] float torqueDamp = 0.98f;
 
    // --- SEUILS ---
    [Header("Thresholds")]
    public float mainIgnitionThreshold = 0.05f;
    public float sideIgnitionThreshold = 0.03f;
    public float pivotDeadZone = 0.2f;
 
    // --- GAME STATE ---
    bool controlsEnabled = false;
 
    // 🔑 état précédent du key (pull-up → 1 au repos)
    int lastKeyState = 1;
 
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Physics.gravity = new Vector3(0f, -9.81f, 0f);
 
        // --- OSC FADERS ---
        oscReceiver.Bind("/faderGauche", OnFaderGauche);
        oscReceiver.Bind("/faderCentre", OnFaderCentre);
        oscReceiver.Bind("/faderDroit", OnFaderDroit);
 
        // --- OSC KEYS ---
        oscReceiverKey.Bind("/key", OnAnyKeyPressed);
        oscReceiverKey.Bind("/key1", OnAnyKeyPressed);
        oscReceiverKey.Bind("/key2", OnAnyKeyPressed);
        oscReceiverKey.Bind("/key3", OnAnyKeyPressed);
        oscReceiverKey.Bind("/key4", OnAnyKeyPressed);
        oscReceiverKey.Bind("/key5", OnAnyKeyPressed);
        oscReceiverKey.Bind("/key6", OnAnyKeyPressed);
 
        // UI visible au départ
        panelUI.SetActive(true);
    }
 
    // =========================
    //        KEYS (0 / 1)
    // =========================
    void OnAnyKeyPressed(OSCMessage message)
    {
        int currentState = 1;
 
        // sécurité INT / FLOAT
        if (message.Values[0].Type == OSCValueType.Int)
            currentState = message.Values[0].IntValue;
        else if (message.Values[0].Type == OSCValueType.Float)
            currentState = (int)message.Values[0].FloatValue;
 
        // 🔽 FRONT DESCENDANT : 1 → 0
        if (lastKeyState == 1 && currentState == 0 && !controlsEnabled)
        {
            controlsEnabled = true;
            panelUI.SetActive(false);
            Debug.Log("KEY PRESS (1→0) → CONTROLS ENABLED");
        }
 
        lastKeyState = currentState;
    }
 
    // =========================
    //        FADERS
    // =========================
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
        if (!controlsEnabled) return;
 
        float mainInput = MainSlider.value;
        float leftInput = LeftSlider.value;
        float rightInput = RightSlider.value;
        float pivotInput = PivotSlider.value;
 
        float mainForce = Mathf.Lerp(0f, 9f, mainInput);
        float leftForce = Mathf.Lerp(0f, 0.2f, leftInput);
        float rightForce = Mathf.Lerp(0f, 0.2f, rightInput);
        float pivotForce = Mathf.Lerp(-0.6f, 0.6f, pivotInput);
 
        // --- CAMERA STABILISÉE ---
        Quaternion invertedRotation = Quaternion.Inverse(rb.transform.localRotation);
        Camera.transform.localRotation = Quaternion.RotateTowards(
            Camera.transform.localRotation,
            invertedRotation,
            rotationSpeed * Time.deltaTime
        );
 
        // MAIN
        if (mainInput > mainIgnitionThreshold)
        {
            rb.AddForce(transform.forward * mainForce, ForceMode.Acceleration);
            SetFire(MainFire, true);
        }
        else SetFire(MainFire, false);
 
        // LEFT
        if (leftInput > sideIgnitionThreshold)
        {
            rb.AddForce(transform.forward * leftForce * 20f, ForceMode.Acceleration);
            rb.AddTorque(Vector3.forward * -leftForce, ForceMode.Acceleration);
            SetFire(LeftFire, true);
        }
        else SetFire(LeftFire, false);
 
        // RIGHT
        if (rightInput > sideIgnitionThreshold)
        {
            rb.AddForce(transform.forward * rightForce * 20f, ForceMode.Acceleration);
            rb.AddTorque(Vector3.forward * rightForce, ForceMode.Acceleration);
            SetFire(RightFire, true);
        }
        else SetFire(RightFire, false);
 
        // PIVOT
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
 
    void SetFire(ParticleSystem ps, bool active)
    {
        if (active && !ps.isPlaying) ps.Play();
        if (!active && ps.isPlaying) ps.Stop();
    }
}