using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewObstacles : MonoBehaviour
{
    
    void Start()
    {
        UI ui = FindObjectOfType<UI>();
        if (ui != null)
            ui.RegisterObstacle(transform);
    }

    
    void Update()
    {
        
    }
}
