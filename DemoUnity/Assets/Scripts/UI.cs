using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoneChecker : MonoBehaviour
{
    public Transform player;           
    private BoxCollider box;           

    public float PlayerMapPositionX;
    public float PlayerMapPositionY; 
    public RectTransform mapArea;      
    public RectTransform dot;          

    void Start()
    {
        box = GetComponent<BoxCollider>();
    }

    void Update()
    {
        
        Vector3 localPos = transform.InverseTransformPoint(player.position);

        
        float xPercent = Mathf.InverseLerp(-box.size.x / 2, box.size.x / 2, localPos.x);
        float yPercent = Mathf.InverseLerp(-box.size.y / 2, box.size.y / 2, localPos.y);

        
        float xPos = Mathf.Lerp(-mapArea.rect.width / 2, mapArea.rect.width / 2, xPercent);
        float yPos = Mathf.Lerp(-mapArea.rect.width / 2, mapArea.rect.width / 2, yPercent);

        
        PlayerMapPositionX = xPercent;
        PlayerMapPositionX = yPercent; 

        
        dot.anchoredPosition = new Vector2(xPos, -yPos);
    }
}
