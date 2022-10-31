using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretScript : MonoBehaviour
{
    [SerializeField]
    [Range(0, 500)]
    private int mouseSensitivity = 100;

    private float mouseX;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
    }

    private void LateUpdate()
    {
        transform.Rotate(Vector3.up * mouseX);
    }
}
