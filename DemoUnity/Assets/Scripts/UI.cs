using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[System.Serializable]
public class MapTarget
{
    public Transform worldObject;      // Object in the world
    public RectTransform dot;          // Dot on the UI map
}


public class UI : MonoBehaviour
{
    public Transform player;           
    private BoxCollider box;           

    public float PlayerMapPositionX;
    public float PlayerMapPositionY; 
    public RectTransform mapArea;      
    public RectTransform playerDot;
    public List<MapTarget> targets = new List<MapTarget>();     

    void Start()
    {
        box = GetComponent<BoxCollider>();
    }

    void Update()
    {
        UpdateDot(player, playerDot);

        
        foreach (var t in targets)
        {
            if (t.worldObject != null && t.dot != null)
                UpdateDot(t.worldObject, t.dot);
        }
    }

    void UpdateDot(Transform worldObj, RectTransform dot)
    {
        Vector3 localPos = transform.InverseTransformPoint(worldObj.position);

        float xPercent = Mathf.InverseLerp(-box.size.x / 2, box.size.x / 2, localPos.x);
        float yPercent = Mathf.InverseLerp(-box.size.y / 2, box.size.y / 2, localPos.y);

        float xPos = Mathf.Lerp(-mapArea.rect.width / 2, mapArea.rect.width / 2, xPercent);
        float yPos = Mathf.Lerp(-mapArea.rect.height / 2, mapArea.rect.height / 2, yPercent);

        dot.anchoredPosition = new Vector2(xPos, -yPos);
    }
}
