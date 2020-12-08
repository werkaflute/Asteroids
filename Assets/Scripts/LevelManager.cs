using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public BoundsSc boundsSc;
    public GameObject player;
    public float projectileSpeed;
    public float minAsteroidSpeed, maxAsteroidSpeed;
    public float asteroidDecentralization;

    public float GetRandomAsteroidSpeed()
    {
        return Random.Range(minAsteroidSpeed, maxAsteroidSpeed);
    }

    void Start()
    {
        boundsSc = FindObjectOfType<BoundsSc>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }
}
