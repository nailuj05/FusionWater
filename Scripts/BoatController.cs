using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("FusionWater/_Examples/BoatController")]
public class BoatController : MonoBehaviour
{
    public float moveSpeed = 10f;
    public float turnSpeed = 10f;

    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        float moveInput = Input.GetAxis("Vertical");
        float turnInput = Input.GetAxis("Horizontal");

        rb.AddForce(moveInput * moveSpeed * transform.right);
        rb.AddTorque(turnInput * turnSpeed * transform.forward);
    }
}
