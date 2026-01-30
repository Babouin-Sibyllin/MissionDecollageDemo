using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RandomEvents : MonoBehaviour  
{

    public bool EventOccuring1 = false;

    public bool EventOccuring2 = false;

    public bool EventFixStep1 = false;
    public bool EventFixStep2 = false;

    public bool EventFixStep3 = false;

    public bool EventFixStep4 = false;

    public bool EventFixStep5 = false;
    

    int EventPicker = 0;

    private KeyCode randomKey;

    public int sequenceLength = 5; // How many keys in the sequence
    private List<KeyCode> keySequence = new List<KeyCode>();

    public FirstPersRocket FirstPersRocket;

    // Start is called before the first frame update
    void Start()
    {
        EventPicker = Random.Range(0, 5);
    }

    // Update is called once per frame
    void Update()
    {

        //Debug.Log("MainSlider: " + FirstPersRocket.MainReactorValue);

        if (!EventOccuring1)
        {
            StartCoroutine(EventHappening());
            EventOccuring1 = true;
        }
        
    }

    IEnumerator EventHappening()
    {
        yield return new WaitForSeconds(10f); // 10 seconds
        Debug.Log("10s ont passées");
        if (EventPicker == 0)
        {
            Debug.Log("Réacteur principal endommagé !");
            FirstPersRocket.DamagedMainReactor = true;
            StartCoroutine(RepairMain());
            // ici on illuminerait le LED du premier bouton à appuyer
        } else if (EventPicker == 1)
        {
            Debug.Log("Réacteur droit endommagé !");
            FirstPersRocket.RightSlider.value = 0;
        } else if (EventPicker == 2)
        {
            Debug.Log("Réacteur gauche endommagé !");
            FirstPersRocket.LeftSlider.value = 0;
        } else if (EventPicker == 3)
        {
            Debug.Log("3");
        } else if (EventPicker == 4)
        {
            Debug.Log("4");
        }

        EventPicker = Random.Range(0, 5); // 5 est exclus donc le chiffre est --> 0 à 4
        EventOccuring1 = false;
    }

    IEnumerator RepairMain()
    {
        // Génère des lettres à appuyer aléatoires
        //KeySequence = la string de lettre à appuyer.
        keySequence.Clear();
        for (int i = 0; i < sequenceLength; i++)
        {
            //lettre de A à Z
            KeyCode randomKey = (KeyCode)Random.Range((int)KeyCode.A, (int)KeyCode.Z + 1);
            keySequence.Add(randomKey);
        }

        // Montre quelles touches à appuyer
        Debug.Log("Sequence: " + string.Join(", ", keySequence));

        // Attent que le joueur ait appuyé les touches en ordre
        for (int i = 0; i < keySequence.Count; i++)
        {
            bool keyPressed = false;
            while (!keyPressed)
            {
                if (Input.GetKeyDown(keySequence[i]))
                {
                    Debug.Log(keySequence[i] + " pressed correctly!");
                    keyPressed = true;

                    // on allume la LED du bouton ici Ahmoud
                }
                yield return null;
            }
        }

        FirstPersRocket.DamagedMainReactor = false;
        Debug.Log("Sequence completed!");
    }
}
