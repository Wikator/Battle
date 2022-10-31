using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MovementScript : MonoBehaviour
{
    [SerializeField]
    [Range(0, 100)]
    private int moveSpeed = 30;

    [SerializeField]
    [Range(0, 100)]
    private int rotateSpeed = 50, health = 20;

    public TextMeshProUGUI text;
    public ParticleSystem explosion;


    void Update()
    {
        float axis = Input.GetAxis("Vertical");

        if ((Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.LeftArrow)) && axis != 0)
        {
            gameObject.transform.Rotate(0.0f, rotateSpeed * Mathf.Sign(axis) * Time.deltaTime, 0.0f);
        }

        if ((Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.RightArrow)) && axis != 0)
        {
            gameObject.transform.Rotate(0.0f, -rotateSpeed * Mathf.Sign(axis) * Time.deltaTime, 0.0f);
        }

        transform.position += axis * moveSpeed * Time.deltaTime * transform.forward;
        text.text = "Health: " + health;
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("EnemyBullet"))
        {
            Instantiate(explosion, transform.position, Quaternion.identity);
            Destroy(other.gameObject);
            health -= 1;
            if (health == 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
