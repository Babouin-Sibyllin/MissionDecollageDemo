using extOSC;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RandomEvents : MonoBehaviour
{
    [Header("OSC")]
    public OSCReceiver oscReceiver;

    [Header("Buttons")]
    public bool Button1 = false;
    public bool Button2 = false;
    public bool Button3 = false;
    public bool Button4 = false;
    public bool Button5 = false;
    public bool Button6 = false;
    public bool DialogueOccuring = false;

    [Header("Event State")]
    public bool EventOccuring = false;

    private int ButtonPicker = 0;
    private int EventPicker = 0;
    public int sequenceLength = 5;
    

    [Header("References")]
    public FirstPersRocket FirstPersRocket;
    public TextMeshProUGUI DialogueUI;
    public GameObject DialogueBox;

    void Start()
    {
        EventPicker = Random.Range(0, 5);

        // --- OSC Bindings ---
        oscReceiver.Bind("/key", (msg) => Button1 = msg.Values[0].IntValue == 0);
        oscReceiver.Bind("/key1", (msg) => Button2 = msg.Values[0].IntValue == 0);
        oscReceiver.Bind("/key2", (msg) => Button3 = msg.Values[0].IntValue == 0);
        oscReceiver.Bind("/key3", (msg) => Button4 = msg.Values[0].IntValue == 0);
        oscReceiver.Bind("/key4", (msg) => Button5 = msg.Values[0].IntValue == 0);
        oscReceiver.Bind("/key5", (msg) => Button6 = msg.Values[0].IntValue == 0);
    }


    void Update()
    {
        // Démarre un nouvel event si aucun en cours
        if (!EventOccuring)
        {
            StartCoroutine(EventHappening());
            EventOccuring = true;
        }

        if (DialogueOccuring) {
            DialogueBox.gameObject.SetActive(true);
        } else {
            DialogueBox.gameObject.SetActive(false);
        }
            
        
    }

    IEnumerator EventHappening()
    {
        yield return new WaitForSeconds(15f);
        Debug.Log("5 secondes passées, check event...");

        EventPicker = Random.Range(0, 5);

        if (EventPicker == 0)
        {
            DialogueOccuring = true;
            DialogueUI.text = "Réacteur principal endommagé";
            FirstPersRocket.DamagedMainReactor = true;
            FirstPersRocket.MainSlider.value = 0;
            StartCoroutine(RepairMain());
        }
        else if (EventPicker == 1)
        {
            DialogueOccuring = true;
            DialogueUI.text = "Réacteur droit endommagé";
            FirstPersRocket.DamagedRightReactor = true;
            FirstPersRocket.RightSlider.value = 0;
            StartCoroutine(RepairRight());
        }
        else if (EventPicker == 2)
        {
            DialogueOccuring = true;
            DialogueUI.text = "Réacteur gauche endommagé";
            FirstPersRocket.DamagedLeftReactor = true;
            FirstPersRocket.LeftSlider.value = 0;
            StartCoroutine(RepairLeft());
        }
        else
        {
            EventOccuring = false;
            DialogueOccuring = false;
        }

        
    }

    // --- Séquences de réparation ---
    IEnumerator RepairMain()
    {
        yield return new WaitForSeconds(2.0f);
        sequenceLength = Random.Range(3, 10);

        for (int i = 0; i < sequenceLength; i++)
        {
            ButtonPicker = Random.Range(0, 6);

            
            switch (ButtonPicker)
            {
                case 0: DialogueUI.text = "Appuyez sur le bouton rouge ! "; yield return new WaitUntil(() => Button1); break;
                case 1: DialogueUI.text = "Appuyez sur le bouton vert ! "; yield return new WaitUntil(() => Button2); break;
                case 2: DialogueUI.text = "Appuyez sur le bouton bleu ! "; yield return new WaitUntil(() => Button3); break;
                case 3: DialogueUI.text = "Appuyez sur le bouton jaune ! "; yield return new WaitUntil(() => Button4); break;
                case 4: DialogueUI.text = "Appuyez sur le bouton magenta ! "; yield return new WaitUntil(() => Button5); break;
                case 5: DialogueUI.text = "Appuyez sur le bouton cyan ! "; yield return new WaitUntil(() => Button6); break;
            }
        }

        DialogueUI.text = "Réparation du réacteur principal réussie. ";
        FirstPersRocket.DamagedMainReactor = false;
        DialogueOccuring = false;
        EventOccuring = false;
    }

    IEnumerator RepairRight()
    {
        yield return new WaitForSeconds(2.0f);
        sequenceLength = Random.Range(3, 10);

        for (int i = 0; i < sequenceLength; i++)
        {
            ButtonPicker = Random.Range(0, 6);

            
            switch (ButtonPicker)
            {
                case 0: DialogueUI.text = "Appuyez sur le bouton rouge !"; yield return new WaitUntil(() => Button1); break;
                case 1: DialogueUI.text = "Appuyez sur le bouton vert !"; yield return new WaitUntil(() => Button2); break;
                case 2: DialogueUI.text = "Appuyez sur le bouton bleu !"; yield return new WaitUntil(() => Button3); break;
                case 3: DialogueUI.text = "Appuyez sur le bouton jaune !"; yield return new WaitUntil(() => Button4); break;
                case 4: DialogueUI.text = "Appuyez sur le bouton magenta !"; yield return new WaitUntil(() => Button5); break;
                case 5: DialogueUI.text = "Appuyez sur le bouton cyan !"; yield return new WaitUntil(() => Button6); break;
            }
        }

        Debug.Log("Réparation réacteur droit réussie !");
        FirstPersRocket.DamagedRightReactor = false;
        DialogueOccuring = false;
        EventOccuring = false;
    }

    IEnumerator RepairLeft()
    {
        yield return new WaitForSeconds(2.0f);
        sequenceLength = Random.Range(3, 10);

        for (int i = 0; i < sequenceLength; i++)
        {
            ButtonPicker = Random.Range(0, 6);

            switch (ButtonPicker)
            {
                case 0: DialogueUI.text = "Appuyez sur le bouton rouge !"; yield return new WaitUntil(() => Button1); break;
                case 1: DialogueUI.text = "Appuyez sur le bouton vert !"; yield return new WaitUntil(() => Button2); break;
                case 2: DialogueUI.text = "Appuyez sur le bouton bleu !"; yield return new WaitUntil(() => Button3); break;
                case 3: DialogueUI.text = "Appuyez sur le bouton jaune !"; yield return new WaitUntil(() => Button4); break;
                case 4: DialogueUI.text = "Appuyez sur le bouton magenta !"; yield return new WaitUntil(() => Button5); break;
                case 5: DialogueUI.text = "Appuyez sur le bouton cyan !"; yield return new WaitUntil(() => Button6); break;
            }
        }

        Debug.Log("Réparation réacteur gauche réussie !");
        FirstPersRocket.DamagedLeftReactor = false;
        DialogueOccuring = false;
        EventOccuring = false;
    }
}
