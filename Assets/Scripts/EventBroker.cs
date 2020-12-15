using UnityEngine;
using System;

public class EventBroker
{
    public static event Action<GameObject> ProjectileShoot;
    public static event Action AsteroidSpawn;
    public static event Action<Vector3, Vector3> AsteroidSmallSpawn;
    public static event Action PlayerKilled;
    public static event Action<GameObject> AsteroidShot;

    public static void CallProjectileShot(GameObject cannon)
    {
        ProjectileShoot?.Invoke(cannon);
    }

    public static void CallAsteroidSpawn()
    {
        AsteroidSpawn?.Invoke();
    }

    public static void CallAsteroidSmallSpawn(Vector3 vector3, Vector3 scale)
    {
        AsteroidSmallSpawn?.Invoke(vector3, scale);
    }

    public static void CallAsteroidShot(GameObject asteroid)
    {
        AsteroidShot?.Invoke(asteroid);
    }

    public static void CallPlayerKilled()
    {
        PlayerKilled?.Invoke();
    }
}
