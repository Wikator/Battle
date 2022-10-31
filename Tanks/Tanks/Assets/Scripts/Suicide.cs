using UnityEngine;

public class Suicide : MonoBehaviour
{
    [SerializeField]
    private float timeToDie;

    void Start()
    {
        Destroy(gameObject, timeToDie);
    }
}
