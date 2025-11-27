using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class RocketScript : MonoBehaviour
{

    public GameObject Rocket;

    public GameObject Camera;

    public ParticleSystem LeftFire;

    public ParticleSystem MainFire;

    public ParticleSystem RightFire;

    public float MainReactorForce = 15f;

     public float BaseReactorForce = 20f;

    public float SideReactorForce = 20f;

    private Rigidbody rb;

    public LayerMask groundLayer;

    [SerializeField] float torqueForce = 2f;
    
    [SerializeField] float torqueDamp = 0.98f;


    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Physics.gravity = new Vector3(0f, -9.81f, 0f);
        
    }

    

    void FixedUpdate()
{

    bool w = Input.GetKey(KeyCode.W);
    bool a = Input.GetKey(KeyCode.A);
    bool d = Input.GetKey(KeyCode.D);


    if (w)
    {
        rb.AddForce(transform.forward * MainReactorForce, ForceMode.Acceleration);
        rb.AddForce(transform.forward * BaseReactorForce, ForceMode.Acceleration);

    }

    if (a)
    {
        rb.AddTorque(Vector3.forward * torqueForce, ForceMode.Acceleration);
        Camera.transform.localRotation = Quaternion.Inverse(rb.transform.localRotation);
    }
    
    
     if (d)
    {
        rb.AddTorque(Vector3.forward * -torqueForce, ForceMode.Acceleration);
        Camera.transform.localRotation = Quaternion.Inverse(rb.transform.localRotation);
    }

    // --- PASSIVE ROTATION DECAY ---
    rb.angularVelocity *= torqueDamp;

    if (w)
        MainFire.Play();
    else
        MainFire.Stop();

    if (a)
        RightFire.Play();
    else
        RightFire.Stop();

    if (d)
        LeftFire.Play();
    else
        LeftFire.Stop();

    
}


    void Update()
    {
        
    }
}
