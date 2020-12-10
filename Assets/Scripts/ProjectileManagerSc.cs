using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(GameObject))]
[RequireComponent(typeof(ObjectPool))]
public class ProjectileManagerSc : MonoBehaviour
{
    public float shotSpeed;
    private ObjectPool projectilePool;

    private void Shoot(GameObject cannon)
    {
        projectilePool.PlaceAndActivate(cannon.transform.position, cannon.transform.rotation.eulerAngles);
    }

    void Start()
    {
        projectilePool = GetComponent<ObjectPool>();
    }

    private void OnEnable()
    {
        EventBroker.ProjectileShoot += Shoot;
    }

    private void OnDisable()
    {
        EventBroker.ProjectileShoot -= Shoot;
    }

    void Update()
    {
        
    }
}
