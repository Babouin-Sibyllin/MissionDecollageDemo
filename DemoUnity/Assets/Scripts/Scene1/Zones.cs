using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Zones : MonoBehaviour
{


    public GameObject Fusee;

    public ParticleSystem Explosion;

    public Slider RightSlider;

    public Slider LeftSlider;

    public Slider MainSlider;

    public ParticleSystem Feu1;

    public ParticleSystem Feu2;

    public ParticleSystem Feu3;

    public bool PlayerIsDead = false;

     public void TriggerExplosion()
    {
        Fusee.gameObject.GetComponent<Renderer>().enabled = false;
        Explosion.Play();
        PlayerIsDead = true;

        if (PlayerIsDead) {
        MainSlider.value = 0;
        RightSlider.value = 0;
        LeftSlider.value = 0;
        }
    }
    
    
    public void OnTriggerExit(Collider other)
{
    if (other.CompareTag("Player") && gameObject.CompareTag("GameZone"))
    {
     TriggerExplosion();   
    }

    if (other.CompareTag("Player") && gameObject.CompareTag("Earth"))
    {
        Debug.Log("Avertissement");
    }
}

    void Start()
    {
        Explosion.Stop();
    }


    void Update()
    {
        
    }
}
