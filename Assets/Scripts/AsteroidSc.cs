using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]

public class AsteroidSc : MonoBehaviour
{
    public float maxRotation = 4f;
    public float minScale = 0.2f, maxScale = 5f, maxSpawnRange = 500;
    public float vanishDistanceFromPlayer;

    private Vector3 rotation;
    private Vector3 velocity;

    private Rigidbody rb;
    private LevelManager levelManager;
    private float speedMultiply;


    private Vector3 RandRotation()
    {
        float x = Random.Range(-maxRotation, maxRotation);
        float y = Random.Range(-maxRotation, maxRotation);
        float z = Random.Range(-maxRotation, maxRotation);
        return new Vector3(x, y, z);
    }

    private Vector3 ChooseDirection(Vector3 from)
    {
        Vector3 randPoint = Random.onUnitSphere * levelManager.asteroidDecentralization;
        Vector3 target = levelManager.player.transform.position + randPoint + levelManager.player.transform.forward * 850;
        return (target - from).normalized;
    }

    private Vector3 ChoosePosition()
    {
        return levelManager.player.transform.position + Random.onUnitSphere * maxSpawnRange * 2 + levelManager.player.transform.forward * 600;
    }

    public void SetUp()
    {
        rb.AddTorque(RandRotation());
        float scale = Random.Range(minScale, maxScale) * 100;

        if (scale < maxScale * 100 / 3)
            speedMultiply = 1;
        else if (scale < maxScale * 100 / 1.5)
            speedMultiply = 0.5f;
        else
            speedMultiply = 0.2f;

        transform.localScale = new Vector3(scale, scale, scale);
        transform.position = ChoosePosition();
        rb.velocity = ChooseDirection(transform.position)*levelManager.GetRandomAsteroidSpeed()*speedMultiply;
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

    private void Update()
    {
        if (Vector3.Distance(this.transform.position, levelManager.player.transform.position + levelManager.player.transform.forward * 650) > vanishDistanceFromPlayer)    // this or gameobject?
        {
            gameObject.SetActive(false);
        }
    }
}
