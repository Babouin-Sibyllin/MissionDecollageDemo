using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RandomEvents : MonoBehaviour  
{

    public bool EventOccuring = false;

    public bool EventIsOccuring = false;

    public bool Button1 = false;

    public bool Button2 = false;

    public bool Button3 = false;

    public bool Button4 = false;

    public bool Button5 = false;

    public bool Button6 = false;

    int ButtonPicker = 0;
    

    int EventPicker = 0;

    private KeyCode randomKey;

    public int sequenceLength = 5; // How many keys in the sequence
    //private List<KeyCode> keySequence = new List<KeyCode>();

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

        if (!EventOccuring)
        {
            StartCoroutine(EventHappening());
            EventOccuring = true;
        }


        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Button1 = true;
        } else
        {
            Button1 = false;
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            Button2 = true;
        } else
        {
            Button2 = false;
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            Button3 = true;
        } else
        {
            Button3 = false;
        }

        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            Button4 = true;
        } else
        {
            Button4 = false;
        }

        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            Button5 = true;
        } else
        {
            Button5 = false;
        }

        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            Button6 = true;
        } else
        {
            Button6 = false;
        }
        
    }

    IEnumerator EventHappening()
    {
        yield return new WaitForSeconds(5f); // 10 secondes
        Debug.Log("10s ont passées");
        if (EventPicker == 0)
        {
            Debug.Log("Réacteur principal endommagé !");
            FirstPersRocket.DamagedMainReactor = true;
            FirstPersRocket.MainSlider.value = 0;
            StartCoroutine(RepairMain());
            // ici on illuminerait le LED du premier bouton à appuyer
        } else if (EventPicker == 1)
        {
            Debug.Log("Réacteur droit endommagé !");
            FirstPersRocket.DamagedRightReactor = true;
            FirstPersRocket.RightSlider.value = 0;
            StartCoroutine(RepairRight());
        } else if (EventPicker == 2)
        {
            Debug.Log("Réacteur gauche endommagé !");
            FirstPersRocket.DamagedLeftReactor = true;
            FirstPersRocket.LeftSlider.value = 0;
            StartCoroutine(RepairLeft());
        } else if (EventPicker == 3)
        {
            Debug.Log("3");
            EventOccuring = false;
        } else if (EventPicker == 4)
        {
            Debug.Log("4");
            EventOccuring = false;
        }

        EventPicker = Random.Range(0, 5); // 5 est exclus donc le chiffre est --> 0 à 4
    }

        //séquence de réparation du réacteur principal
    IEnumerator RepairMain()
    {
        sequenceLength = Random.Range(3, 10);


            for (int i = 0; i < sequenceLength; i++)
        {
            ButtonPicker = Random.Range(0, 6);

            if (ButtonPicker == 0)
            {
                Debug.Log("press 1");
                yield return new WaitUntil(() => Button1 == true);
            } else if (ButtonPicker == 1)
            {
                Debug.Log("press 2");
                yield return new WaitUntil(() => Button2 == true);
            } else if (ButtonPicker == 2)
            {
                Debug.Log("press 3");
                yield return new WaitUntil(() => Button3 == true);
            } else if (ButtonPicker == 3)
            {
                Debug.Log("press 4");
                yield return new WaitUntil(() => Button4 == true);
            } else if (ButtonPicker == 4)
            {
                Debug.Log("press 5");
                yield return new WaitUntil(() => Button5 == true);
            } else if (ButtonPicker == 5)
            {
                Debug.Log("press 6");
                yield return new WaitUntil(() => Button6 == true);
            }
        }

        Debug.Log("réparation réussie");
        FirstPersRocket.DamagedMainReactor = false;
        EventOccuring = false;
        yield break;
    }


        //séquence de réparation du réacteur gauche
    IEnumerator RepairRight()
    {
        sequenceLength = Random.Range(3, 10);


            for (int i = 0; i < sequenceLength; i++)
        {
            ButtonPicker = Random.Range(0, 6);

            if (ButtonPicker == 0)
            {
                Debug.Log("press 1");
                yield return new WaitUntil(() => Button1 == true);
            } else if (ButtonPicker == 1)
            {
                Debug.Log("press 2");
                yield return new WaitUntil(() => Button2 == true);
            } else if (ButtonPicker == 2)
            {
                Debug.Log("press 3");
                yield return new WaitUntil(() => Button3 == true);
            } else if (ButtonPicker == 3)
            {
                Debug.Log("press 4");
                yield return new WaitUntil(() => Button4 == true);
            } else if (ButtonPicker == 4)
            {
                Debug.Log("press 5");
                yield return new WaitUntil(() => Button5 == true);
            } else if (ButtonPicker == 5)
            {
                Debug.Log("press 6");
                yield return new WaitUntil(() => Button6 == true);
            }
        }

        Debug.Log("réparation réussie");
        FirstPersRocket.DamagedRightReactor = false;
        EventOccuring = false;
        yield break;
    }

    IEnumerator RepairLeft()
    {
        sequenceLength = Random.Range(3, 10);


            for (int i = 0; i < sequenceLength; i++)
        {
            ButtonPicker = Random.Range(0, 6);

            if (ButtonPicker == 0)
            {
                Debug.Log("press 1");
                yield return new WaitUntil(() => Button1 == true);
            } else if (ButtonPicker == 1)
            {
                Debug.Log("press 2");
                yield return new WaitUntil(() => Button2 == true);
            } else if (ButtonPicker == 2)
            {
                Debug.Log("press 3");
                yield return new WaitUntil(() => Button3 == true);
            } else if (ButtonPicker == 3)
            {
                Debug.Log("press 4");
                yield return new WaitUntil(() => Button4 == true);
            } else if (ButtonPicker == 4)
            {
                Debug.Log("press 5");
                yield return new WaitUntil(() => Button5 == true);
            } else if (ButtonPicker == 5)
            {
                Debug.Log("press 6");
                yield return new WaitUntil(() => Button6 == true);
            }
        }

        Debug.Log("réparation réussie");
        FirstPersRocket.DamagedLeftReactor = false;
        EventOccuring = false;
        yield break;
    }
}
