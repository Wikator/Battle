using System.Collections;
using UnityEngine;

public class EnemyArtilleryScript : MonoBehaviour
{
    //private GameManager gameManager;
    private GameObject bulletEmpty;
    private string rotating = "Right";

    [SerializeField]
    private GameObject bullet, spawnLocation;

    [SerializeField]
    private ParticleSystem explosion;

    void Awake()
    {
        StartCoroutine(Rotation());
        bulletEmpty = GameObject.Find("Bullets");
    }

    void Update()
    {
       switch (rotating)
       {
            case "Right":
                transform.Rotate(0.0f, -10 * Time.deltaTime, 0.0f);
                break;
            case "Left":
                transform.Rotate(0.0f, 10 * Time.deltaTime, 0.0f);
                break;
       }
    }

    private void FixedUpdate()
    {
        if (Random.Range(0, 1000) == 0)
        {
            Shoot();
        }
    }


    private void Shoot()
    {
        GameObject copy = Instantiate(bullet, spawnLocation.transform.position, spawnLocation.transform.rotation, bulletEmpty.transform);
        switch (gameObject.name)
        {
            case "GreenBot(Clone)":
                copy.tag = "GreenBullet";
                break;
            case "RedBot(Clone)":
                copy.tag = "RedBullet";
                break;
            case "BlueBot(Clone)":
                copy.tag = "BlueBullet";
                break;
            case "YellowBot(Clone)":
                copy.tag = "YellowBullet";
                break;
        }

        Rigidbody copyRigidBody = copy.GetComponent<Rigidbody>();
        copyRigidBody.GetComponent<BulletScript>().enabled = false;
        copy.GetComponent<CapsuleCollider>().enabled = false;
        copy.GetComponent<SphereCollider>().enabled = true;
        copy.GetComponent<MeshRenderer>().enabled = true;
        copy.GetComponentInChildren<TrailRenderer>().time = 0.5f;
        copyRigidBody.AddForce(Quaternion.AngleAxis(-50, transform.right) * transform.forward * Random.Range(30, 45), ForceMode.Impulse);
        copyRigidBody.useGravity = true;
    }

    private IEnumerator Rotation()
    {
        yield return new WaitForSeconds(5);
        if (rotating == "Right")
        {
            rotating = "Left";
        }
        else
        {
            rotating = "Right";
        }
        StartCoroutine(Rotation());
    }

  /*  private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag(gameObject.tag))
        {
            gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
            switch (other.tag)
            {
                case "GreenBullet":
                    gameManager.greenScore += 1;
                    break;
                case "BlueBullet":
                    gameManager.blueScore += 1;
                    break;
                case "RedBullet":
                    gameManager.redScore += 1;
                    break;
                case "YellowBullet":
                    gameManager.yellowScore += 1;
                    break;
            }

            Destroy(other.gameObject);
            Instantiate(explosion, transform.position, Quaternion.identity, GameObject.Find("Explosions").transform);
            Destroy(gameObject);
        }
    }*/
}

