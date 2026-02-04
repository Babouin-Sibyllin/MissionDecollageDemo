using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;




public class RocketScript : MonoBehaviour
{

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

    public LayerMask groundLayer;

    [SerializeField] float torqueForce = 0.2f;

    [SerializeField] float torqueDamp = 0.98f;

    public Gravity GravityScript;


    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Physics.gravity = new Vector3(0f, -9.81f, 0f);
    }

    public static float Proportion(float value, float inputMin, float inputMax, float outputMin, float outputMax)
{
    return Mathf.Clamp(((value - inputMin) / (inputMax - inputMin) * (outputMax - outputMin) + outputMin), outputMin, outputMax);
}



    void FixedUpdate()
    {

        bool w = Input.GetKey(KeyCode.W);
        bool a = Input.GetKey(KeyCode.A);
        bool d = Input.GetKey(KeyCode.D);
        bool e = Input.GetKey(KeyCode.E);
        bool q = Input.GetKey(KeyCode.Q);



        float MainValue = Proportion(MainSlider.value, 0f, 1f, 0f, 9f);
        float RightValue = Proportion(RightSlider.value, 0f, 1f, 0f, 0.2f);
        float LeftValue = Proportion(LeftSlider.value, 0f, 1f, 0f, 0.2f);
        float PivotValue = Proportion(PivotSlider.value, 0f, 1f, -0.6f, 0.6f);

        Quaternion invertedRotation = Quaternion.Inverse(rb.transform.localRotation); // declare first


        Quaternion targetRotation;

    //if (!Gravity.MarsReached)
    //{
        targetRotation = invertedRotation;
    //}
    //else
    //{
        //Vector3 euler = invertedRotation.eulerAngles;
        //euler.z = 90f; // or euler.y = 90f
        //targetRotation = Quaternion.Euler(euler);
    //}

    
    Camera.transform.localRotation = Quaternion.RotateTowards(
        Camera.transform.localRotation,
        targetRotation,
        rotationSpeed * Time.deltaTime
    );
        

        //avance vers le haut
        
        rb.AddForce(transform.forward * MainValue, ForceMode.Acceleration);
        // se déplace à gauche
        rb.AddTorque(Vector3.forward * -LeftValue, ForceMode.Acceleration);
        rb.AddForce(transform.forward * LeftValue*20, ForceMode.Acceleration);
        
        
        // se déplace à droite
        rb.AddTorque(Vector3.forward * RightValue, ForceMode.Acceleration);
        rb.AddForce(transform.forward * RightValue*20, ForceMode.Acceleration);
        // se déplace sur les côtés
        

        if (MainValue > 0f)
            MainFire.Play();
        else
            MainFire.Stop();

        if (RightValue > 0)
            RightFire.Play();
        else
            RightFire.Stop();

        if (LeftValue > 0) {
            LeftFire.Play();
            }  
        else {
            LeftFire.Stop();
        }
            

        if (PivotValue > 0.2f) 
        {
            RightPivot.Play();
            rb.AddTorque(Vector3.forward * PivotValue, ForceMode.Acceleration);
        } 
        else {
            RightPivot.Stop();
            rb.AddTorque(Vector3.forward * 0, ForceMode.Acceleration);
            }
            
            

        if (PivotValue < -0.2f) {
            LeftPivot.Play();
            rb.AddTorque(Vector3.forward * PivotValue, ForceMode.Acceleration);
        }
        else {
            rb.AddTorque(Vector3.forward * 0, ForceMode.Acceleration);
            LeftPivot.Stop();
            }
            

        if (w)
        {
            rb.AddForce(transform.forward * MainReactorForce, ForceMode.Acceleration);
            rb.AddForce(transform.forward * BaseReactorForce, ForceMode.Acceleration);
        }

        if (a)
        {
            rb.AddTorque(Vector3.forward * torqueForce, ForceMode.Acceleration);
        }


        if (d)
        {
            rb.AddTorque(Vector3.forward * -torqueForce, ForceMode.Acceleration);
        }

        if (e) {
            rb.AddTorque(Vector3.forward * -torqueForce*3, ForceMode.Acceleration);
        }

        if (q) {
            rb.AddTorque(Vector3.forward * torqueForce*3, ForceMode.Acceleration);
        }

        if (!d && !a && !w && !e && !q)
        {
        }

        
        rb.angularVelocity *= torqueDamp;

        //if (w)
            //MainFire.Play();
        //else
            //MainFire.Stop();

        //if (a)
            //RightFire.Play();
        //else
            //RightFire.Stop();

        //if (d)
            //LeftFire.Play();
        //else
            //LeftFire.Stop();

        //if (q) 
            //RightPivot.Play();
        //else 
            //RightPivot.Stop();
            

        //if (e) 
            //LeftPivot.Play();
        //else 
            //LeftPivot.Stop();
            


    }


    void Update()
    {
        
    }
}
