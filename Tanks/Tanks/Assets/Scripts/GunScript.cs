using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunScript : MonoBehaviour
{
    private GameObject spawnLocation;

    [SerializeField]
    private GameObject bullet, aim;

    void Start()
    {
        spawnLocation = gameObject.transform.GetChild(0).gameObject;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            GameObject copy = Instantiate(bullet, spawnLocation.transform.position, spawnLocation.transform.rotation);
            copy.tag = "Bullet";
            Destroy(copy, 3.0f);
        }
        //GameObject copy1 = Instantiate(aim, spawnLocation.transform.position, spawnLocation.transform.rotation);
        //Destroy(copy1, 0.02f);
    }
}
