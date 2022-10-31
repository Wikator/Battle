using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    private readonly List<string> directions = new List<string>();
    private GameManager gameManager;
    private GameObject bulletEmpty;
    private string rotationDirection;
    private int speed, timeToRotate, rotateSpeed;
    private bool rotating = false, isAiming = false;

    [SerializeField]
    private GameObject bullet, spawnLocation;

    [SerializeField]
    private ParticleSystem explosion;

    void Awake()
    {
        bulletEmpty = GameObject.Find("Bullets");
        directions.Add("Left");
        directions.Add("Right");
        GetRandomNumbers();
        InvokeRepeating(nameof(Shoot), Random.Range(2.5f, 4.0f), Random.Range(1, 4));
        StartCoroutine(WaitForRotation());
    }

    void Update()
    {

        transform.position += speed * Time.deltaTime * transform.forward;

        if (rotating && !isAiming)
        {
            switch (rotationDirection)
            {
                case "Left":
                    gameObject.transform.Rotate(0.0f, rotateSpeed * -1 * Time.deltaTime, 0.0f);
                    break;
                case "Right":
                    gameObject.transform.Rotate(0.0f, rotateSpeed * Time.deltaTime, 0.0f);
                    break;
            }
        }
    }

    private void LateUpdate()
    {
        GameObject target = Vision();

        if (target)
        {
            if (target.CompareTag("Arena"))
            {
                isAiming = true;
                transform.Rotate(1000 * Time.deltaTime * Vector3.up, Space.Self);
            }

            if (!target.CompareTag(gameObject.tag) && (target.name == "YellowBot(Clone)" || target.name == "GreenBot(Clone)" || target.name == "BlueBot(Clone)" || target.name == "RedBot(Clone)"))
            {
                isAiming = true;
                transform.LookAt(target.transform.position);
            }
        }
    }

    private void GetRandomNumbers()
    {
        speed = Random.Range(8, 14);
        timeToRotate = Random.Range(2, 5);
        rotationDirection = directions[Random.Range(0, 2)];
        rotateSpeed = Random.Range(100, 350);
    }

    private void Shoot()
    {
        GameObject copy = Instantiate(bullet, spawnLocation.transform.position, spawnLocation.transform.rotation, bulletEmpty.transform);
        switch(gameObject.name)
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
    }

    private GameObject Vision()
    {

        /*
        Debug.DrawRay(spawnLocation.transform.position, transform.forward * 30.0f, Color.black);
        Debug.DrawRay(spawnLocation.transform.position, Quaternion.AngleAxis(20, transform.up) * transform.forward * 20.0f, Color.black);
        Debug.DrawRay(spawnLocation.transform.position, Quaternion.AngleAxis(-20, transform.up) * transform.forward * 20.0f, Color.black);
        */


        if (Physics.Raycast(spawnLocation.transform.position, transform.forward, out RaycastHit hit, 30.0f))
        {
            if (hit.distance < 0.1f && (!hit.collider.gameObject.CompareTag(gameObject.tag) && (hit.collider.gameObject.name == "YellowBot(Clone)" || hit.collider.gameObject.name == "GreenBot(Clone)" || hit.collider.gameObject.name == "BlueBot(Clone)" || hit.collider.gameObject.name == "RedBot(Clone)")))
            {
                hit.collider.gameObject.GetComponent<EnemyScript>().Destruction(Instantiate(bullet, transform));
            }

            return hit.collider.gameObject;
        }

        if (Physics.Raycast(spawnLocation.transform.position, Quaternion.AngleAxis(25, transform.up) * transform.forward, out hit, 20.0f))
        {
            return hit.collider.gameObject;
        }

        if (Physics.Raycast(spawnLocation.transform.position, Quaternion.AngleAxis(-25, transform.up) * transform.forward, out hit, 20.0f))
        {
            return hit.collider.gameObject;
        }

        return null;
    }

    private IEnumerator WaitForRotation()
    {
        yield return new WaitForSeconds(timeToRotate);
        rotating = true;
        speed = 10;
        yield return new WaitForSeconds(0.2f);
        rotating = false;
        GetRandomNumbers();
        StartCoroutine(WaitForRotation());
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag(gameObject.tag))
        {
            Destruction(other.gameObject);
        }
    }

    public void Destruction(GameObject other)
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

        Destroy(other);
        Instantiate(explosion, transform.position, Quaternion.identity, GameObject.Find("Explosions").transform);
        Destroy(gameObject);
    }
}
