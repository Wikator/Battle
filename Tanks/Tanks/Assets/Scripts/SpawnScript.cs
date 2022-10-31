using UnityEngine;

public class SpawnScript : MonoBehaviour
{
    [SerializeField]
    private GameObject tank;

    [SerializeField]
    private int minX, maxX, minZ, maxZ, tanksAtStart;


    void Start()
    {
        for (int i = 0; i<tanksAtStart; i++)
        {
            Instantiate(tank, new Vector3(Random.Range(minX, maxX), -48.0f, Random.Range(minZ, maxZ)), transform.rotation, gameObject.transform);
        }
    }

    
    void FixedUpdate()
    {
        if (Random.Range(0, 5) == 0)
        {
            Instantiate(tank, new Vector3(Random.Range(minX, maxX), -48.0f, Random.Range(minZ, maxZ)), transform.rotation, gameObject.transform);
        }
    }
}
