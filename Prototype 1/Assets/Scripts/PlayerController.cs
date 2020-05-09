using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerController : MonoBehaviour
{
     
     public float speed;
     public float horsePower = 800;

    [SerializeField] GameObject centerOfMass;

    [SerializeField] TextMeshProUGUI speedMeterText;

    [SerializeField] TextMeshProUGUI gearText;
    [SerializeField] int gearInterval;
    [SerializeField] int gear;

    [SerializeField] List<WheelCollider> allWheels;
    [SerializeField] int wheelsOnGround;


    float turnSpeed = 58;
     float horizontalInput;
     float forwardInput;
     private Rigidbody playerRb;
    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        playerRb.centerOfMass = centerOfMass.transform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //Axis setup
        horizontalInput = Input.GetAxis("Horizontal");
        forwardInput = Input.GetAxis("Vertical");

        if (IsOnGround())
        {
            //Here we make the vehicle move forward
            //transform.Translate(Vector3.forward * Time.deltaTime * speed * forwardInput);
            playerRb.AddRelativeForce(Vector3.forward * horsePower);

            //move left and right
            //transform.Translate(Vector3.right * Time.deltaTime * turnSpeed * horizontalInput); solo se mueve de izq a derecha , no rota
            transform.Rotate(Vector3.up, turnSpeed * horizontalInput * Time.deltaTime);

            speed = playerRb.velocity.magnitude;
            int roundedSpeed = (int)speed; //int casting
            int mph = (int)(speed * 2.237f);
            speedMeterText.text = "Speed: " + mph + " mph";

            if (mph % gearInterval == 0)
            {
                gear = mph / gearInterval;
                gearText.SetText("Gear: " + gear);
            }

        }

    }

    bool IsOnGround()
    {

        wheelsOnGround = 0;
        foreach (WheelCollider wheel in allWheels)
        {
            if (wheel.isGrounded)
            {
                wheelsOnGround++;
            }
        }
        if(wheelsOnGround == 4)
        {
            return true;
        }
        else
        {
           return false;
        }
    }
}
