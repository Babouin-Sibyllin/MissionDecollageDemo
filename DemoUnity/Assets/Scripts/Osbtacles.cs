using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class LoopingObstacle : MonoBehaviour
{
    public Vector3 startPoint;   // Where the obstacle starts
    public Vector3 endPoint;     // Where the obstacle ends
    public float speed = 15f;     // Movement speed

    public GameObject Cube;

    private Vector3 direction;

    void Start()
    {
        
        Cube.transform.position = startPoint;

        direction = (endPoint - startPoint).normalized;
    }

    void Update()
    {
        
        Cube.transform.position += direction * speed * Time.deltaTime;

        
        if (Vector3.Distance(Cube.transform.position, startPoint) >= Vector3.Distance(startPoint, endPoint))
        {
            Cube.transform.position = startPoint;
        }
    }
}
