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

    [Header("Son Explosion")]
    public AudioSource explosionSound;

    [Header("Ambience Terre")]
    public AudioSource ambienceTerre;

    public bool PlayerIsDead = false;

    void Start()
    {
        if (Explosion != null)
            Explosion.Stop();

        if (ambienceTerre != null)
            ambienceTerre.Stop();
    }

    // =========================
    //        TRIGGERS
    // =========================
    void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        // ENTER EARTH → start ambience
        if (CompareTag("Earth"))
        {
            if (ambienceTerre != null && !ambienceTerre.isPlaying)
                ambienceTerre.Play();

            Debug.Log("Ambience Terre ON");
        }


    }

    void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        // EXIT EARTH → stop ambience
        if (CompareTag("Earth"))
        {
            if (ambienceTerre != null && ambienceTerre.isPlaying)
                ambienceTerre.Stop();

            Debug.Log("Ambience Terre OFF");
        }
                // ENTER FORBIDDEN ZONE → explosion
        if (CompareTag("GameZone"))
        {
            TriggerExplosion();
        }
    }

    // =========================
    //      EXPLOSION
    // =========================
    public void TriggerExplosion()
    {
        if (PlayerIsDead) return;

        PlayerIsDead = true;

        if (Fusee != null)
            Fusee.GetComponent<Renderer>().enabled = false;

        if (Explosion != null)
            Explosion.Play();

        if (explosionSound != null)
            explosionSound.Play();

        MainSlider.value = 0;
        RightSlider.value = 0;
        LeftSlider.value = 0;
    }
}
