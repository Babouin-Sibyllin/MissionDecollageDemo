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

    [Header("Event State")]
    public bool EventOccuring = false;

    private int ButtonPicker = 0;
    private int EventPicker = 0;
    public int sequenceLength = 5;

    [Header("References")]
    public FirstPersRocket FirstPersRocket;
    public TextMeshProUGUI DialogueUI;

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
    }

    IEnumerator EventHappening()
    {
        yield return new WaitForSeconds(5f);
        Debug.Log("5 secondes passées, check event...");

        EventPicker = Random.Range(0, 5);

        if (EventPicker == 0)
        {
            DialogueUI.text = "Réacteur principal endommagé";
            FirstPersRocket.DamagedMainReactor = true;
            FirstPersRocket.MainSlider.value = 0;
            StartCoroutine(RepairMain());
        }
        else if (EventPicker == 1)
        {
            Debug.Log("Réacteur droit endommagé !");
            FirstPersRocket.DamagedRightReactor = true;
            FirstPersRocket.RightSlider.value = 0;
            StartCoroutine(RepairRight());
        }
        else if (EventPicker == 2)
        {
            Debug.Log("Réacteur gauche endommagé !");
            FirstPersRocket.DamagedLeftReactor = true;
            FirstPersRocket.LeftSlider.value = 0;
            StartCoroutine(RepairLeft());
        }
        else
        {
            EventOccuring = false;
        }

        
    }

    // --- Séquences de réparation ---
    IEnumerator RepairMain()
    {
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
        EventOccuring = false;
    }

    IEnumerator RepairRight()
    {
        sequenceLength = Random.Range(3, 10);

        for (int i = 0; i < sequenceLength; i++)
        {
            ButtonPicker = Random.Range(0, 6);
            switch (ButtonPicker)
            {
                case 0: Debug.Log("Séquence: appuyez sur le bouton 1 (Rouge)"); yield return new WaitUntil(() => Button1); break;
                case 1: Debug.Log("Séquence: appuyez sur le bouton 2 (Vert)"); yield return new WaitUntil(() => Button2); break;
                case 2: Debug.Log("Séquence: appuyez sur le bouton 3 (Bleu)"); yield return new WaitUntil(() => Button3); break;
                case 3: Debug.Log("Séquence: appuyez sur le bouton 4 (Jaune)"); yield return new WaitUntil(() => Button4); break;
                case 4: Debug.Log("Séquence: appuyez sur le bouton 5 (Magenta)"); yield return new WaitUntil(() => Button5); break;
                case 5: Debug.Log("Séquence: appuyez sur le bouton 6 (Cyan)"); yield return new WaitUntil(() => Button6); break;
            }
        }

        Debug.Log("Réparation réacteur droit réussie !");
        FirstPersRocket.DamagedRightReactor = false;
        EventOccuring = false;
    }

    IEnumerator RepairLeft()
    {
        sequenceLength = Random.Range(3, 10);

        for (int i = 0; i < sequenceLength; i++)
        {
            ButtonPicker = Random.Range(0, 6);
            switch (ButtonPicker)
            {
                case 0: Debug.Log("Séquence: appuyez sur le bouton 1 (Rouge)"); yield return new WaitUntil(() => Button1); break;
                case 1: Debug.Log("Séquence: appuyez sur le bouton 2 (Vert)"); yield return new WaitUntil(() => Button2); break;
                case 2: Debug.Log("Séquence: appuyez sur le bouton 3 (Bleu)"); yield return new WaitUntil(() => Button3); break;
                case 3: Debug.Log("Séquence: appuyez sur le bouton 4 (Jaune)"); yield return new WaitUntil(() => Button4); break;
                case 4: Debug.Log("Séquence: appuyez sur le bouton 5 (Magenta)"); yield return new WaitUntil(() => Button5); break;
                case 5: Debug.Log("Séquence: appuyez sur le bouton 6 (Cyan)"); yield return new WaitUntil(() => Button6); break;
            }
        }

        Debug.Log("Réparation réacteur gauche réussie !");
        FirstPersRocket.DamagedLeftReactor = false;
        EventOccuring = false;
    }
}
