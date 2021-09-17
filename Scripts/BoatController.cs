using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("FusionWater/_Examples/BoatController")]
public class BoatController : MonoBehaviour
{
    public float moveSpeed = 10f;
    public float turnSpeed = 10f;

    [Range(0.5f, 5)]
    public float accelerationTime = 3f;
    private float accTime = 0;

    public AnimationCurve accelerationCurve;

    public Transform boatMotor;
    private Vector3 startRotation;

    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        startRotation = boatMotor.localEulerAngles;
    }

    void FixedUpdate()
    {
        float moveInput = Input.GetAxis("Vertical");
        float turnInput = Input.GetAxis("Horizontal");

        if (moveInput == 0)
            accTime = 0;
        else
            accTime += Time.fixedDeltaTime;

        float accelerationFactor = 1 / accelerationTime;
        float acceleration = accelerationCurve.Evaluate(accTime * accelerationFactor);

        //Add Force at Boat Motors Position
        rb.AddForceAtPosition(moveInput * acceleration * moveSpeed * boatMotor.forward, boatMotor.position);

        //Rotate the Motor itself (subtract: Motor turning left = Boat turning right)
        boatMotor.localEulerAngles = startRotation;
        boatMotor.Rotate(Vector3.right, turnInput * turnSpeed, Space.Self);
    }
}
