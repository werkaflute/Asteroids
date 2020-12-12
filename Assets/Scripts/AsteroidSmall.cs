using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]

public class AsteroidSmall : AsteroidSc
{
    private Vector3 rotation;
    private Vector3 velocity;

    private Rigidbody rb;
    private LevelManager levelManager;
    private AsteroidSmallManager asteroidSmallManager;


    private Vector3 RandRotation()
    {
        float x = Random.Range(-maxRotation, maxRotation);
        float y = Random.Range(-maxRotation, maxRotation);
        float z = Random.Range(-maxRotation, maxRotation);
        return new Vector3(x, y, z);
    }

    private Vector3 ChooseDirection(Vector3 from)
    {
        Vector2 randPoint = Random.insideUnitCircle * levelManager.boundsSc.ySize * levelManager.asteroidDecentralization;
        Vector3 target = new Vector3(randPoint.x, 0f, randPoint.y);
        return (target - from).normalized;
    }

    private Vector3 ChoosePosition()
    {
        BoundsSc boundsSc = levelManager.boundsSc;
        int index = Random.Range(0, 3);
        Transform spawnSite = boundsSc.asteroidSpawns[index].transform;
        float offset;

        if (index % 2 == 0) // N or S
        {
            offset = Random.Range(-boundsSc.xSize / 2, boundsSc.xSize / 2);
            return new Vector3(offset, 0f, spawnSite.position.z);
        }
        else // W  or E
        {
            offset = Random.Range(-boundsSc.ySize / 2, boundsSc.ySize / 2);
            return new Vector3(spawnSite.position.x, 0f, offset);
        }
    }

    public new void SetUp()
    {
        //rb.AddTorque(RandRotation());
        float scale = Random.Range(minScale, maxScale);
        transform.localScale = new Vector3(scale, scale, scale);
        transform.position = new Vector3(0f,0f,0f);
        rb.velocity = ChooseDirection(transform.position) * levelManager.GetRandomAsteroidSpeed();
    }

    private void GetShot(GameObject asteroid)
    {
        if (gameObject == asteroid)
        {
            EventBroker.CallAsteroidSmallSpawn();
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Boundary"))
        {
            gameObject.SetActive(false);
        }
        if (other.CompareTag("Player"))
        {
            EventBroker.CallPlayerKilled();
        }
    }

    private void OnEnable()
    {
        SetUp();
        EventBroker.AsteroidShot += GetShot;
    }

    private void OnDisable()
    {
        EventBroker.AsteroidShot -= GetShot;
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        levelManager = FindObjectOfType<LevelManager>();
    }

    private void Start()
    {
        rb.angularDrag = 0f;
    }
}
