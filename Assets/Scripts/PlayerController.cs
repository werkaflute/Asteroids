using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]

public class PlayerController : MonoBehaviour
{
    public float maxThrust;
    public float rotSpeed;

    public GameObject cannon;

    private Rigidbody rb;
    private Vector3 targetRotation, thrust;
    private CoolDownSc shootCooldown;

    private float screenHeightUnits, screenWidthUnits;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        shootCooldown = GetComponent<CoolDownSc>();
        screenHeightUnits = 2*Camera.main.orthographicSize;
        screenWidthUnits = screenHeightUnits * Camera.main.aspect;
    }

    private void Die()
    {
        gameObject.SetActive(false);
        transform.position = Vector3.zero;
    }

    private void OnEnable()
    {
        EventBroker.PlayerKilled += Die;
    }

    private void OnDisable()
    {
        EventBroker.PlayerKilled -= Die;
    }

    private void StayOnScreen()
    {
        Vector3 posOnScreen = Camera.main.WorldToScreenPoint(transform.position);
        if (posOnScreen.y>Screen.height)
        {
            transform.position -= new Vector3(0f, 0f, screenHeightUnits);
        }
        else if (posOnScreen.y < 0f)
        {
            transform.position += new Vector3(0f, 0f, screenHeightUnits);
        }
        else if (posOnScreen.x > Screen.width)
        {
            transform.position -= new Vector3(screenWidthUnits, 0f, 0f);
        }
        else if (posOnScreen.x < 0f)
        {
            transform.position += new Vector3(screenWidthUnits, 0f, 0f);
        }
    }

    void Update()
    {
        StayOnScreen();
        targetRotation = transform.localRotation.eulerAngles + new Vector3(0f, Input.GetAxis("Horizontal") * rotSpeed, 0f);
        thrust = transform.forward * Input.GetAxis("Vertical") * maxThrust;
        if(Input.GetKeyDown(KeyCode.Space))
        {
            if(shootCooldown.ResetTimer())
            {
                EventBroker.CallProjectileShot(cannon);
            }
        }
    }

    private void FixedUpdate()
    {
        rb.MoveRotation(Quaternion.Euler(targetRotation));
        rb.AddForce(thrust);
    }
}
