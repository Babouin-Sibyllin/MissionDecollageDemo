using UnityEngine;

public class LoopingObstacle : MonoBehaviour
{
    public float speed = 15f;
    public GameObject obstaclePrefab;
    public float extraDistance = 2000f;

    public float extremeX = 2000f;   
    public float spawnRangeX = 500f; 

    private Vector3 startPoint;
    private Vector3 endPoint;
    private Vector3 direction;

    void Start()
    {
        
        Vector3 spawnPos = GetRandomSpawnPos(transform.position.y, transform.position.z);
    transform.position = spawnPos;

    GenerateNewPath(spawnPos);
    }

    void Update()
    {
        transform.position += direction * speed * Time.deltaTime;

        float traveled = Vector3.Distance(transform.position, startPoint);
        float pathLength = Vector3.Distance(startPoint, endPoint);

        if (traveled >= pathLength)
        {
            
            Vector3 newSpawnPos = GetRandomSpawnPos(startPoint.y, startPoint.z);
            Instantiate(obstaclePrefab, newSpawnPos, Quaternion.identity);

            
            GenerateNewPath(endPoint);
        }
    }

    void GenerateNewPath(Vector3 fromPosition)
    {
        startPoint = fromPosition;

        
        Vector3 nextExtremity = GetRandomExtremityPos(fromPosition.y, fromPosition.z);

        endPoint = nextExtremity;

        Vector3 extra = (endPoint - startPoint).normalized * extraDistance;
        endPoint += extra;

        direction = (endPoint - startPoint).normalized;
    }

    Vector3 GetRandomSpawnPos(float y, float z)
    {
        float x = Random.Range(-spawnRangeX, spawnRangeX);
        return new Vector3(x, y, z);
    }

    Vector3 GetRandomExtremityPos(float y, float z)
    {
        float x = Random.value < 0.5f ? -extremeX : extremeX;
        return new Vector3(x, y, z);
    }
}
