using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoolDownSc : MonoBehaviour
{
    public float setTo;

    private float timeLeft;

    public bool ResetTimer()
    {
        if (timeLeft <= 0f)
        {
            timeLeft = setTo;
            return true;
        }
        else
        {
            return false;
        }
    }

    void Start()
    {
        timeLeft = setTo;
    }

    void Update()
    {
        timeLeft -= Time.deltaTime;
    }
}
