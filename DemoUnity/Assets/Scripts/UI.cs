using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[System.Serializable]
public class MapTarget
{
    public Transform worldObject;      
    public RectTransform dot;          
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


    public RectTransform dotPrefab; 

public void RegisterObstacle(Transform obstacle)
{
    
    RectTransform newDot = Instantiate(dotPrefab, mapArea);

    
    MapTarget mt = new MapTarget();
    mt.worldObject = obstacle;
    mt.dot = newDot;

   
    targets.Add(mt);
}

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

    
    Renderer rend = worldObj.GetComponentInChildren<Renderer>();
if (rend != null)
{
    Vector3 objSize = rend.bounds.size;

    
    float xFraction = objSize.x / box.size.x;
    float yFraction = objSize.z / box.size.z; 

    
    float dotWidth = xFraction * mapArea.rect.width;
    float dotHeight = yFraction * mapArea.rect.height;

    
    float minSize = 25f;
    float maxSize = 100f;
    dotWidth = Mathf.Clamp(dotWidth, minSize, maxSize);
    dotHeight = Mathf.Clamp(dotHeight, minSize, maxSize);

    dot.sizeDelta = new Vector2(dotWidth, dotHeight);
}
else
{
    
    dot.sizeDelta = new Vector2(20f, 20f);
}
}
}
