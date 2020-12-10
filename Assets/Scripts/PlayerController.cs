using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]

public class PlayerController : MonoBehaviour
{
    public float maxThrust;
    public float rotSpeed;

    public GameObject cannon;
    public GameObject cam;

    private Rigidbody rb;
    private Vector3 targetRotation, thrust;
    private CoolDownSc shootCooldown;

    private float screenHeightUnits, screenWidthUnits;
    private float pitchDelta = 0;
    private float yawDelta = 0;
    private float rollDelta = 0;
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
        // nwm czy to potrzebne 
        StayOnScreen();
        //targetRotation = transform.localRotation.eulerAngles + new Vector3(0f, Input.GetAxis("Horizontal") * rotSpeed, 0f);
        //thrust = transform.forward * Input.GetAxis("Vertical") * maxThrust;

        if(Input.GetKeyDown(KeyCode.Space))
        {
            if(shootCooldown.ResetTimer())
            {
                EventBroker.CallProjectileShot(cannon);
            }
        }

        // FORWARD
        if(Input.GetKey(KeyCode.W))
        {
            thrust = cam.transform.forward * maxThrust;
            //thrust = cam.transform.localRotation.eulerAngles * maxThrust;
        }
        else if(Input.GetKey(KeyCode.S))
        {
            thrust = -cam.transform.forward * maxThrust;
        }
        else
        {
            thrust = Vector3.zero;
        }

        //Rotation
        //zbieram deltę w kątach na poszczególnych osiach

        pitchDelta = 0;
        yawDelta = 0;
        rollDelta = 0;

        // SIDES
        if (Input.GetKey(KeyCode.A))
        {
            
            yawDelta = -rotSpeed;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            yawDelta = rotSpeed;
            
        }


        
        // FORWARD - UP/DOWN
        if (Input.GetKey(KeyCode.UpArrow))
        {
            pitchDelta = -rotSpeed;
           
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            pitchDelta = rotSpeed;
         
        }

        // SIDES - UP/DOWN
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            rollDelta = -rotSpeed;
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            rollDelta = rotSpeed;
            
        }
    }

    private void FixedUpdate()
    {
        //działania na eulerowskich kątach mają dużo ograniczeń - silnik nie wie czy rotacja jest o 361 czy o 1 stopień, też trzeba pamiętać które rotacje są lokalne a które globalne
        //dlatego łatwiej to załatwić kwaternionami - mnożenie obraca o dany kwaternion, więcej wiedzieć nie trzeba tak naprawdę
        rb.MoveRotation(transform.rotation*Quaternion.Euler(pitchDelta,yawDelta,rollDelta));

        rb.AddForce((thrust));
        //rb.AddRelativeForce(thrust);
    }
}
