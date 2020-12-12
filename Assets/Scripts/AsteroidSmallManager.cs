using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CoolDownSc))]
[RequireComponent(typeof(ObjectPool))]
public class AsteroidSmallManager : MonoBehaviour
{

    private CoolDownSc spawnCooldown;
    private NewObjectPool newObjectPool;

    private BoundsSc boundsSc;

    private void SpawnAsteroid()
    {
        newObjectPool.PlaceAndActivate(Vector3.zero);
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
        newObjectPool = GetComponent<NewObjectPool>();
        boundsSc = FindObjectOfType<BoundsSc>();
    }

    void Update()
    {
        if (spawnCooldown.ResetTimer())
        {
            EventBroker.CallAsteroidSmallSpawn();
        }
    }
}
