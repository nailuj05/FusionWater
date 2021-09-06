using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("FusionWater/_Examples/DampedFollow")]
public class DampedFollow : MonoBehaviour
{
    public Transform followTransform;

    public float smoothTime = 0.3f;
    public float angleSmooth = 10f;
    private Vector3 velocity = Vector3.zero;
    private Vector3 angVel = Vector3.zero;

    void LateUpdate()
    {
        // Smoothly move the camera towards that target position
        transform.position = Vector3.SmoothDamp(transform.position, followTransform.position, ref velocity, smoothTime);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(followTransform.forward, Vector3.up), angleSmooth);
    }
}
