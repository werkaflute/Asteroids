using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CoolDownSc))]
[RequireComponent(typeof(ObjectPool))]
public class AsteroidManagerSc : MonoBehaviour
{

    private CoolDownSc spawnCooldown;
    private ObjectPool asteroidPool;

    private void SpawnAsteroid()
    {
        asteroidPool.PlaceAndActivate(Vector3.zero);
    }

    private void OnEnable()
    {
        EventBroker.AsteroidSpawn += SpawnAsteroid;
    }

    private void OnDisable()
    {
        EventBroker.AsteroidSpawn -= SpawnAsteroid;
    }

    void Start()
    {
        spawnCooldown = GetComponent<CoolDownSc>();
        asteroidPool = GetComponent<ObjectPool>();
    }

    void Update()
    {
        if(spawnCooldown.ResetTimer())
        {
            EventBroker.CallAsteroidSpawn();
        }
    }
}
