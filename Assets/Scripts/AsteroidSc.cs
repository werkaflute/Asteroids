using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]

public class AsteroidSc : MonoBehaviour
{
    public float maxRotation = 4f;
    public float minScale = 0.2f, maxScale = 5f, maxSpawnRange = 500;

    private Vector3 rotation;
    private Vector3 velocity;

    private Rigidbody rb;
    private LevelManager levelManager;


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
        return levelManager.player.transform.position + Random.onUnitSphere * maxSpawnRange;
    }

    public void SetUp()
    {
        rb.AddTorque(RandRotation());
        float scale = Random.Range(minScale, maxScale);
        transform.localScale = new Vector3(scale, scale, scale);
        transform.position = ChoosePosition();
        rb.velocity = ChooseDirection(transform.position)*levelManager.GetRandomAsteroidSpeed();
    }

    private void GetShot(GameObject asteroid)
    {
        if(gameObject == asteroid)
        {
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Boundary"))
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
