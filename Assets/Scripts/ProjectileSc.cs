using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]

public class ProjectileSc : MonoBehaviour
{
    private Rigidbody rb;
    private LevelManager levelManager;

    private void Spawn(float speed)
    {
        rb.velocity = transform.forward * speed;
        gameObject.SetActive(true);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Boundary"))
        {
            gameObject.SetActive(false);
        }
        else if(other.CompareTag("Asteroid"))
        {
            gameObject.SetActive(false);
            EventBroker.CallAsteroidShot(other.gameObject);
        }
    }

    private void OnEnable()
    {
        Spawn(levelManager.projectileSpeed);
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        levelManager = FindObjectOfType<LevelManager>();
    }
}
