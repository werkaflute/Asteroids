using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CoolDownSc))]
[RequireComponent(typeof(NewObjectPool))]
public class AsteroidSmallManager : MonoBehaviour
{

    private CoolDownSc spawnCooldown;
    private NewObjectPool newObjectPool;

    private BoundsSc boundsSc;

    private void SpawnAsteroidSmall(Vector3 vector3, Vector3 scale)
    {
        newObjectPool.PlaceAndActivate(vector3, Vector3.zero, scale);
        newObjectPool.PlaceAndActivate(vector3, Vector3.zero, scale);
    }

    private void OnEnable()
    {
        EventBroker.AsteroidSmallSpawn += SpawnAsteroidSmall;
    }

    private void OnDisable()
    {
        EventBroker.AsteroidSmallSpawn -= SpawnAsteroidSmall;
    }

    void Start()
    {
        spawnCooldown = GetComponent<CoolDownSc>();
        newObjectPool = GetComponent<NewObjectPool>();
        boundsSc = FindObjectOfType<BoundsSc>();
    }

    void Update()
    {
       
    }
}
