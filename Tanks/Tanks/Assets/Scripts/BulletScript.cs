using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{

    [SerializeField]
    [Range(0, 150)]
    private int speed;

    private SphereCollider sphereCollider;

    public ParticleSystem artilleryExplosion;

    private void Awake()
    {
        sphereCollider = gameObject.GetComponent<SphereCollider>();
    }

    void Update()
    {
        transform.Translate(-speed * Time.deltaTime * transform.forward);

        if (Physics.Raycast(transform.position, transform.up, out RaycastHit hit, 3.0f))
        {
            if (hit.collider.gameObject.CompareTag("Arena"))
            {
                Destroy(gameObject);
            }
        }      
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Arena"))
        {
            StartCoroutine(Explode());
        }
    }

    private IEnumerator Explode()
    {
        yield return new WaitForSeconds(1);
        sphereCollider.radius = 200.0f;
        sphereCollider.isTrigger = true;
        Instantiate(artilleryExplosion, transform.position, Quaternion.identity, GameObject.Find("Explosions").transform);
        Destroy(gameObject, 0.1f);
    }
}
