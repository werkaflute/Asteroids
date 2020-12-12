using UnityEngine;
using System;

public class EventBroker
{
    public static event Action<GameObject> ProjectileShoot;
    public static event Action AsteroidSpawn;
    public static event Action AsteroidSmallSpawn;
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

    public static void CallAsteroidSmallSpawn()
    {
        AsteroidSmallSpawn?.Invoke();
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
