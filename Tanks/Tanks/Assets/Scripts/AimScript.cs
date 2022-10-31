using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimScript : MonoBehaviour
{

    [SerializeField]
    private int speed = 2000;

    void Update()
    {
        transform.Translate(-speed * Time.deltaTime * transform.forward);
    }


}
