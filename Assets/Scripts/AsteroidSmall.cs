using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]

public class AsteroidSmall : AsteroidSc
{
    public override void SetUp()
    {
        rb.AddTorque(RandRotation());
        transform.localScale *= 0.5f;
        if(transform.localScale.x < 0.2f)
        {
            gameObject.SetActive(false);
        }
        rb.velocity = ChooseDirection(transform.position) * levelManager.GetRandomAsteroidSpeed();
    }

}
