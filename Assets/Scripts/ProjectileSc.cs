using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]

public class ProjectileSc : MonoBehaviour
{
    private Rigidbody rb;
    private LevelManager levelManager;
    public float vanishDistanceFromPlayer;

    private void Spawn(float speed)
    {
        rb.velocity = transform.forward * (-speed);
        gameObject.SetActive(true);
    }

    // if too far from player, begone // behind the mist
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Asteroid"))
        {
            levelManager.AddScore(5);
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

    private void Update()
    {
        if(Vector3.Distance(this.transform.position, levelManager.player.transform.position) > vanishDistanceFromPlayer)    // this or gameobject?
        {
            gameObject.SetActive(false);
        }
    }
}
