using UnityEngine;

public class LoopingObstacle : MonoBehaviour
{
    public float speed = 15f;
    public GameObject Cube;

    private Vector3 startPoint;
    private Vector3 endPoint;
    private Vector3 direction;

    void Start()
    {
        // Pick random X positions between -1000 and 1000
        float startX = Random.Range(-1000f, 1000f);
        float endX = Random.Range(-1000f, 1000f);

        float startX2 = Random.Range(-1000f, 1000f);
        float endX2 = Random.Range(-1000f, 1000f);

        // Keep Y and Z the same as the Cube's original position
        Vector3 basePos = Cube.transform.position;

        startPoint = new Vector3(startX, basePos.y, basePos.z);
        endPoint   = new Vector3(endX, basePos.y, basePos.z);

        Cube.transform.position = startPoint;
        direction = (endPoint - startPoint).normalized;
    }

    void Update()
    {
        Cube.transform.position += direction * speed * Time.deltaTime;

        
        if (Vector3.Distance(Cube.transform.position, startPoint) >= Vector3.Distance(startPoint, endPoint))
        {
           
            float startX = Random.Range(-1000f, 1000f);
            float endX = Random.Range(-1000f, 1000f);

            startPoint = new Vector3(startX, Cube.transform.position.y, Cube.transform.position.z);
            endPoint   = new Vector3(endX, Cube.transform.position.y, Cube.transform.position.z);

            Cube.transform.position = startPoint;
            direction = (endPoint - startPoint).normalized;
        }
    }
}
